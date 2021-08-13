using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketApp.Models
{
    [Serializable]
    public class Message
    {
        #region Properties
        string token;
        DateTime time;
        string text;
        object obj;
        List<byte[]> files;
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
            }
        }
        public List<byte[]> Files
        {
            get
            {
                return this.files;
            }

            set
            {
                this.files = value;
            }
        }
        public DateTime Time
        {
            get
            {
                return this.time;
            }

        }


        public object Obj
        {
            get
            {
                return this.obj;
            }

        }



        public string TokenOfUser
        {
            get
            {
                return this.token;
            }

            set
            {
                this.token = value;
            }
        }



        #endregion

        public Message(string text, String user)
        {
            time = DateTime.Now;
            this.Text = text;
            this.TokenOfUser = user;
        }

        public Message(object obj, String user)
        {
            time = DateTime.Now;
            this.obj = obj;
            this.TokenOfUser = user;
            this.text = string.Empty;
        }

        public Message(String user)
        {
            time = DateTime.Now;
            this.text = string.Empty;
            this.TokenOfUser = user;
        }


  

        public Message(object obj)
        {
            time = DateTime.Now;
            this.obj = obj;
            this.text = string.Empty;
        }

        public Message()
        {
            time = DateTime.Now;
            this.text = string.Empty;
        }

    }
}
