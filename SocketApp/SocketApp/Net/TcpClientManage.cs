using SocketApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketApp.Net
{
    [Serializable]
    public class TcpClientManage : TcpClient
    {
        #region Contrustor
        public TcpClientManage(IPEndPoint iPEndPoint) : base(iPEndPoint)
        {

        }

        public TcpClientManage(string host, int port) : base(host, port)
        {
            
        }

        public TcpClientManage()
        {

        }

        #endregion

        #region Methods
        public int SendData(byte[] data)
        {
            int total = 0;
            int size = data.Length;
            int dataleft = size;
            int sent;
            byte[] datasize = new byte[4];
            datasize = BitConverter.GetBytes(size);
            sent = Client.Send(datasize);
            while (total < size)
            {
                sent = Client.Send(data, total, dataleft, SocketFlags.None);
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
                recv = Client.Receive(datasize, 0, 4, 0);
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
                    recv = Client.Receive(data, total, dataleft, 0);
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
}
