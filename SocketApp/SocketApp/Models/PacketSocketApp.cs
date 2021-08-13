using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketApp.Models
{
    [Serializable]
    public class PacketSocketApp
    {
        string action;
        //Int32 totalLength;
        object data;


        //public Int32 TotalLength
        //{
        //    get
        //    {
        //        return this.totalLength;
        //    }

        //    set
        //    {
        //        this.totalLength = value;
        //    }
        //}

        public object Data
        {
            get
            {
                return this.data;
            }

            set
            {
                this.data = value;
            }
        }

        public string Action
        {
            get
            {
                return this.action;
            }

            set
            {
                this.action = value;
            }
        }

        public PacketSocketApp(object data, string aciton)
        {
            this.Action = aciton.ToUpper().Trim();
            Data = data;
            
            //totalLength = 0;

        }

        public PacketSocketApp(object data)
        {
            this.Action = "response".ToUpper();
            Data = data;
            //totalLength = 0;

        }
    }
}
