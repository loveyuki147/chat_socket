using SocketApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Chat
{
    class User
    {
        UserForClient userForClient;

        public UCControlFriend UCControlFriend { get; set; }
        public UserForClient UserForClient
        {
            get
            {
                return this.userForClient;
            }

            set
            {
                this.userForClient = value;
            }
        }

        public User(UserForClient user, UCControlFriend uc )
        {
            UserForClient = user;
            UCControlFriend = uc;
        }
    }
}
