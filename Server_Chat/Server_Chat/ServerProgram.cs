using SocketApp.Models;
using SocketApp.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server_Chat
{
    delegate void SetText(string s);
    class ServerProgram
    {
        #region Properties
        public SetText SetLog = null;

        TcpListenerManage ServerTcp { get; set; }
        Thread ThreadListenReq { get; set; }

        Dictionary<string, UserOnlineManage> UserOnlineManages { get; set; }

        #endregion

        #region Constructor
        public ServerProgram()
        {
          
        }


        #endregion

        #region Methods
        public void StartServer()
        {
            ServerTcp = new TcpListenerManage(new IPEndPoint(IPAddress.Any, 5000));
            UserOnlineManages = new Dictionary<string, UserOnlineManage>();
            ServerTcp.Start();
            ThreadStart threadStart = new ThreadStart(() =>
            {
                while (true)
                {
                    TcpClientManage tcpClientManage = ServerTcp.AcceptTcpClientManage();

                    ListenLoginAuthentication(tcpClientManage);
                }
            });
            ThreadListenReq = new Thread(threadStart);

            ThreadListenReq.IsBackground = true;
            ThreadListenReq.Start();
        }



        void ListenLoginAuthentication(TcpClientManage tcpClientManage)
        {
            ThreadStart threadStartLogin = new ThreadStart(() =>
            {
                PacketSocketApp packet = tcpClientManage.ReceivePacket();

                if (packet == null)
                {
                    tcpClientManage.Close();
                    tcpClientManage.Dispose();
                    return;
                }
                Dictionary<string, string> keyValuePairs = packet.Data as Dictionary<string, string>;
                UserForServer userForServer = UserDAL.Instance.ValidDataLogin(keyValuePairs);

                if (userForServer != null && !UserOnlineManages.ContainsKey(userForServer.Token))
                {                 
                    userForServer.IsOnline = true;
                    UserForClient userForClient = userForServer.ConverToUserForClient();
                    PacketSocketApp packetSend = new PacketSocketApp(userForClient);
                    tcpClientManage.SendPacket(packetSend);

                    List<UserForClient> vs = UserDAL.Instance.GetAllUserForClient(userForClient);
                    PacketSocketApp packetUsers = new PacketSocketApp(vs, "GET_FRIENDS");
                    tcpClientManage.SendPacket(packetUsers);

                    foreach (var item in UserOnlineManages)
                    {
                        var value = item.Value;
                        PacketSocketApp packetUsers_Onl = new PacketSocketApp(userForClient.Token, "USER_ONL");
                        value.TcpClientManage.SendPacket(packetUsers_Onl);
                    }

                    UserOnlineManage userOnlineManage = new UserOnlineManage(userForServer, tcpClientManage);
                    userOnlineManage.OnRecvMessage += this.UserOnlineManage_OnRecvMessage;
                    userOnlineManage.OnDisconnect += this.UserOnlineManage_OnDisconnect;

                   

                    if (!UserOnlineManages.ContainsKey(userForServer.Token))
                    {
                        UserOnlineManages.Add(userForServer.Token, userOnlineManage);
                    }
                    else
                    {
                        UserOnlineManages[userForServer.Token] = userOnlineManage;
                    }

                    if (SetLog != null)
                    {
                        string log = userForServer.Username +  " is online";
                        SetLog(log);
                    }



                }
                else
                {
                    PacketSocketApp packetSend = new PacketSocketApp("LOGIN FAIL");
                    tcpClientManage.SendPacket(packetSend);
                   

                    if (SetLog != null)
                    {
                        string log = tcpClientManage.Client.RemoteEndPoint.ToString() + " login fail";
                        SetLog(log);
                    }

                    tcpClientManage.Close();
                    tcpClientManage.Dispose();
                }



            });
            Thread thread = new Thread(threadStartLogin);
            thread.Start();
        }

        private void UserOnlineManage_OnDisconnect(object sender, EventArgs_Packet e)
        {
            UserOnlineManage userOnlineManage = sender as UserOnlineManage;
            UserOnlineManages.Remove(userOnlineManage.UserForServer.Token);

            foreach (var item in UserOnlineManages)
            {
                var value = item.Value;

                PacketSocketApp packetSocketApp = new PacketSocketApp(userOnlineManage.UserForServer.Token, "USER_DISCONNECT");
                value.TcpClientManage.SendPacket(packetSocketApp);
            }

            if (SetLog != null)
            {
                string log = userOnlineManage.UserForServer.Username + " is offline";
                SetLog(log);
            }
        }

        private void UserOnlineManage_OnRecvMessage(object sender, EventArgs_Packet e)
        {
            UserOnlineManage user = sender as UserOnlineManage;
            PacketSocketApp packet = e.Packet;

            if (packet.Data == null)
                return;
            if ((packet.Data as Message) == null)
                return;
            packet.Action = "RECV_MESSAGE";
            Thread SendBC = new Thread(new ThreadStart(() =>
            {
                foreach (var item in UserOnlineManages)
                {
                    var value = item.Value;
                    if (value != user)
                    {
                        try
                        {

                            TcpClientManage tcpClient = value.TcpClientManage;
                            tcpClient.SendPacket(packet);
                        }
                        catch (SocketException)
                        {

                        }
                        catch(NullReferenceException)
                        {

                        }
                    }

                }          


            }));

            SendBC.IsBackground = true;
            SendBC.Start();
        }



    #endregion
}
}
