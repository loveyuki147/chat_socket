using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketApp.Models
{
    [Serializable]
    public class UserForClient : User
    {
        #region Properties
        Bitmap avatar;
     
        public Bitmap Avatar
        {
            get
            {
                return this.avatar;
            }

            set
            {
                this.avatar = value;
            }
        }

      
        #endregion

        public UserForClient(string username, string name, string token, Bitmap avatar, bool isOnline) : base(username, name, token, isOnline)
        {
            this.avatar = avatar;
        }
    }
}
