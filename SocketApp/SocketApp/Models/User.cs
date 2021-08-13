using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketApp.Models
{
    [Serializable]
    public abstract class User
    {
        string iD;
        string username;
        string name;
        protected string token;
        bool isOnline;

        //List<Message> messages;
        public string ID
        {
            get
            {
                return this.iD;
            }

            set
            {
                this.iD = value;
            }
        }

        public string Username
        {
            get
            {
                return this.username;
            }

            set
            {
                this.username = value;
            }
        }


        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
            }
        }



        public string Token
        {
            get
            {
                return this.token;
            }

        }

        public bool IsOnline
        {
            get
            {
                return this.isOnline;
            }

            set
            {
                this.isOnline = value;
                if(OnChangeIsOnline != null)
                {
                    OnChangeIsOnline(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler OnChangeIsOnline;

        public User()
        {

        }

        public User(string username, string name, string token, bool isOnline)
        {
            this.token = token;
            this.Name = name;
            this.Username = username;
            this.IsOnline = isOnline;
        }

        public User(string username, string name)
        {
            this.Name = name;
            this.Username = username;

        }
    }
}
