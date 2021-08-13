using SocketApp.Models;
using System;
using System.Net;
using System.Net.Sockets;


namespace SocketApp.Net
{
    [Serializable]
    public class TcpListenerManage : TcpListener
    {

        #region Contrustor
        public TcpListenerManage(IPEndPoint iPEndPoint) : base(iPEndPoint)
        {

        }


        #endregion

        #region Methods

        public TcpClientManage AcceptTcpClientManage()
        {           
            var tcpClient = AcceptTcpClient();
            
            return new TcpClientManage() { 
                ExclusiveAddressUse = tcpClient.ExclusiveAddressUse,
                LingerState = tcpClient.LingerState,
                NoDelay = tcpClient.NoDelay,
                ReceiveBufferSize = tcpClient.ReceiveBufferSize,
                ReceiveTimeout = tcpClient.ReceiveTimeout,
                SendBufferSize = tcpClient.SendBufferSize,
                SendTimeout = tcpClient.SendTimeout,
                Client = tcpClient.Client,
            };
        }


        public int SendData(byte[] data)
        {
            int total = 0;
            int size = data.Length;
            int dataleft = size;
            int sent;
            byte[] datasize = new byte[4];
            datasize = BitConverter.GetBytes(size);
            sent = Server.Send(datasize);
            while (total < size)
            {
                sent = Server.Send(data, total, dataleft, SocketFlags.None);
                total += sent;
                dataleft -= sent;
            }
            return total;
        }
        public byte[] ReceiveData()
        {
            int total = 0;
            int recv;
            byte[] datasize = new byte[4];
            try
            {
                recv = Server.Receive(datasize, 0, 4, 0);
            }
            catch (SocketException)
            {
                return null;
            }
            int size = BitConverter.ToInt32(datasize, 0);
            int dataleft = size;
            byte[] data = new byte[size];
            while (total < size)
            {
                try
                {
                    recv = Server.Receive(data, total, dataleft, 0);
                }
                catch (SocketException)
                {
                    return null;
                }
                if (recv == 0)
                {
                    return null;
                }
                total += recv;
                dataleft -= recv;
            }
            return data;
        }

        public int SendPacket(PacketSocketApp data)
        {
            byte[] byteData = null;
            try
            {
                byteData = ProcessData.SerializeData(data);
            }
            catch
            {
                return -1;
            }
            return SendData(byteData);
        }

        public PacketSocketApp ReceivePacket()
        {
            byte[] byteData = ReceiveData();
            if (byteData == null)
                return null;
            object data = ProcessData.DeserializeData(byteData);
            return data as PacketSocketApp;
        }
        #endregion
    }

    public class EventArgs_Packet : EventArgs
    {
        PacketSocketApp packet;

        public PacketSocketApp Packet
        {
            get
            {
                return this.packet;
            }


        }
        public EventArgs_Packet(PacketSocketApp packet)
        {
            this.packet = packet;
        }
        public EventArgs_Packet()
        {

        }

    }

    public class EventArgs_Message : EventArgs
    {
        Message message;

        public Message Message
        {
            get
            {
                return this.message;
            }


        }
        public EventArgs_Message(Message message)
        {
            this.message = message;
        }
        public EventArgs_Message()
        {

        }

    }

}
