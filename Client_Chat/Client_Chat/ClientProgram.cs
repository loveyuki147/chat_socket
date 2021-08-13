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
using System.Windows;

namespace Client_Chat
{
    class ClientProgram
    {
        #region Properties
        TcpClientManage tcpClient;
        Socket client;
        Thread MonitorClientsThread { get; set; }
        Thread AcceptClientThread { get; set; }
        Thread ReceiveThread { get; set; }
        Thread SendThread { get; set; }
        public UserForClient UserForClient { get; set; }
        public IPEndPoint Server_IPE { get; set; }
        public Dictionary<string, User> Users { get; set; }


        public TcpClientManage TcpClient
        {
            get
            {
                return this.tcpClient;
            }

            set
            {
                if (value == null)
                    return;
                this.tcpClient = value;
                client = this.TcpClient.Client;
            }
        }
        public Socket Client
        {
            get
            {
                return this.client;
            }
        }

        #endregion


        #region Contrustor
        public ClientProgram()
        {
            Server_IPE = new IPEndPoint(IPAddress.Loopback, 5000);
            Users = new Dictionary<string, User>();
        }

        #endregion

        #region Event
        event EventHandler<EventArgs_Message> onSendMessageSuccess;
        public event EventHandler<EventArgs_Message> OnSendMessageSuccess
        {
            add
            {
                onSendMessageSuccess += value;
            }
            remove
            {
                onSendMessageSuccess -= value;
            }
        }

        event EventHandler<EventArgs_Message> onRecvMessage;
        public event EventHandler<EventArgs_Message> OnRecvMessage
        {
            add
            {
                onRecvMessage += value;
            }
            remove
            {
                onRecvMessage -= value;
            }
        }

        event EventHandler<EventArgs_Packet> onUserDis;
        public event EventHandler<EventArgs_Packet> OnUserDis
        {
            add
            {
                onUserDis += value;
            }
            remove
            {
                onUserDis -= value;
            }
        }

        event EventHandler<EventArgs_Packet> onGetFriends;
        public event EventHandler<EventArgs_Packet> OnGetFriends
        {
            add
            {
                onGetFriends += value;
            }
            remove
            {
                onGetFriends -= value;
            }
        }
        #endregion

        public void InitTcpClient()
        {
            if (tcpClient != null)
            {
                TcpClient.Close();
                TcpClient.Dispose();
            }
            TcpClient = new TcpClientManage();
            TcpClient.ReceiveTimeout = 2000;

        }



        private void Connect()
        {

            bool flag = ConnectToServer();
            if (flag)
            {
                //MessageBox.Show("Kết nối thành công");

            }
            else
            {
                MessageBox.Show("Không có kết nối tới server");
            }

        }


        public bool ConnectToServer()
        {

            if (tcpClient.Connected)
                return true;
            try
            {
                tcpClient.Connect(Server_IPE);
            }
            catch (SocketException)
            {
                return false;
            }


            return tcpClient.Connected;

        }

        public UserForClient Login(string username, string password)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("username", username);
            data.Add("password", password);


            PacketSocketApp packetReq = new PacketSocketApp(data, "login");

            TcpClient.SendPacket(packetReq);

            PacketSocketApp packetRes = TcpClient.ReceivePacket();
            if (packetRes == null)
                return null;

            UserForClient userForClient = packetRes.Data as UserForClient;
            if (userForClient == null)
                return null;

            this.UserForClient = userForClient;


            return userForClient;

        }



        public void SendMessage(string text)
        {
            Message message = new Message(text, UserForClient.Token);
            PacketSocketApp packetSocketApp = new PacketSocketApp(message, "SEND_MESSAGE");

            ThreadStart threadStr = new ThreadStart(() =>
            {
                tcpClient.SendPacket(packetSocketApp);
                onSendMessageSuccess(this, new EventArgs_Message(message));             
            });
            Thread threadSend = new Thread(threadStr);
            threadSend.Start();
        }

        public void ListeningRecv()
        {
            ReceiveThread = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    PacketSocketApp packet = tcpClient.ReceivePacket();
                    if (packet != null)
                    {

                        String action = packet.Action;

                        switch (action)
                        {
                            case "RECV_MESSAGE":
                                {
                                    Message message = packet.Data as Message;
                                    if (onRecvMessage != null)
                                    {
                                        onRecvMessage(this, new EventArgs_Message(message));
                                    }
                                }
                                break;

                            case "USER_DISCONNECT":
                                {
                                    if (!Users.ContainsKey(packet.Data.ToString()))
                                    {
                                        break;
                                    }
                                    var t = Users[packet.Data.ToString()].UserForClient;
                                    t.IsOnline = false;
                                    if (onUserDis != null)
                                    {
                                        onUserDis(this, new EventArgs_Packet(packet));
                                    }
                                    break;
                                }
                            case "USER_ONL":
                                {
                                    string token = packet.Data.ToString();
                                    if (Users.ContainsKey(token))
                                    {
                                        Users[token].UserForClient.IsOnline = true;
                                    }
                                    break;
                                }
                            case "GET_FRIENDS":
                                {
                                    List<UserForClient> clients = packet.Data as List<UserForClient>;

                                    if (clients != null)
                                    {
                                        Users.Clear();
                                        foreach (var item in clients)
                                        {
                                            UCControlFriend uc = null;
                                            Application.Current.Dispatcher.Invoke((Action)delegate
                                            {
                                                uc = new UCControlFriend(item);
                                            });

                                            Users.Add(item.Token, new User(item, uc));
                                            item.OnChangeIsOnline += this.Item_OnChangeIsOnline;
                                        }

                                        if (onGetFriends != null)
                                            onGetFriends(this, new EventArgs_Packet(packet));
                                    }
                                    break;
                                }

                            default:
                                break;
                        }




                    }
                }


            }));

            ReceiveThread.IsBackground = true;
            ReceiveThread.Start();
        }

        private void Item_OnChangeIsOnline(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                UserForClient user = sender as UserForClient;
                User u = Users[user.Token];
                if (!u.UserForClient.IsOnline)
                    u.UCControlFriend.Offline();
                else
                    u.UCControlFriend.Online();
            });
        }

        public void Disconnect()
        {

            PacketSocketApp packet = new PacketSocketApp(null, "disconnect");
            ThreadStart threadStr = new ThreadStart(() =>
            {
                try {

                    tcpClient.SendPacket(packet);
                }
                catch(SocketException)
                {

                }
                if(ReceiveThread != null)             
                    ReceiveThread.Abort();
                TcpClient.Close();

            });
            Thread threadSend = new Thread(threadStr);
            threadSend.Start();

        }



    }

}
