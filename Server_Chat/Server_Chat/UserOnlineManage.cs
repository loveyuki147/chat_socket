using SocketApp.Models;
using SocketApp.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server_Chat
{
    class UserOnlineManage
    {
        #region Properties
        TcpClientManage tcpClientManage;
        UserForServer userForServer;



        public TcpClientManage TcpClientManage
        {
            get
            {
                return this.tcpClientManage;
            }

            set
            {
                this.tcpClientManage = value;
            }
        }

        public UserForServer UserForServer
        {
            get
            {
                return this.userForServer;
            }

            set
            {
                this.userForServer = value;
            }
        }

        #endregion

        #region Events
        event EventHandler<EventArgs_Packet> onRecvMessage;
        public event EventHandler<EventArgs_Packet> OnRecvMessage
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

        event EventHandler<EventArgs_Packet> onDisconnect;
        public event EventHandler<EventArgs_Packet> OnDisconnect
        {
            add
            {
                onDisconnect += value;
            }
            remove
            {
                onDisconnect -= value;
            }
        }
        #endregion

        #region Contrustor
        public UserOnlineManage(UserForServer userForServer, TcpClientManage tcpClientManage)
        {
            this.UserForServer = userForServer;
            this.TcpClientManage = tcpClientManage;
            ListenReq();
        }
        #endregion
        #region Methods
        public void ListenReq()
        {
            Thread thread = new Thread(() =>
            {
                while (tcpClientManage.Connected)
                {
                    string action = "ERROR";
                    PacketSocketApp packet = tcpClientManage.ReceivePacket();


                    if (packet != null)
                    {
                        action = packet.Action;
                    }


                    switch (action)
                    {
                        case "LOGIN":
                            break;
                        case "SEND_MESSAGE":
                            if (onRecvMessage != null)
                                onRecvMessage(this, new EventArgs_Packet(packet));
                            break;
                
                        case "DISCONNECT":
                            userForServer.IsOnline = false;
                            TcpClientManage.Close();
                            TcpClientManage.Dispose();

                            if (onDisconnect != null)
                                onDisconnect(this, new EventArgs_Packet(packet));
                            return;
                        case "ERROR":
                            //System.Windows.MessageBox.Show("Lỗi");
                            break;
                        default:
                            break;
                    }
                }
            });

            thread.IsBackground = true;
            thread.Start();

        }
        #endregion
    }
}


