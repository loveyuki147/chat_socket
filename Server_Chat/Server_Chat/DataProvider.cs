using SocketApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Chat
{
    class DataProvider
    {
        private static DataProvider instance;


        internal static DataProvider Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataProvider();
                return instance;
            }

            set
            {
                instance = value;
            }
        }

        public List<UserForServer> Users { get; set; }

        public DataProvider()
        {
            LoadUser();
        }

        void LoadUser()
        {
            Users = new List<UserForServer>();
            UserForServer user1 = new UserForServer("admin", "admin", "Adminn", Server_Chat.Properties.Resources.user);
            UserForServer user4 = new UserForServer("nam", "admin", "Nguyễn Hoàng Nam", Server_Chat.Properties.Resources.nam);
            UserForServer user5 = new UserForServer("dung", "admin", "Trần Thị Hoàng Dung", Server_Chat.Properties.Resources.dung);
            UserForServer user6 = new UserForServer("duyen", "admin", "Đỗ Thị Duyên", Server_Chat.Properties.Resources.duyen);

            Users.Add(user1);

            Users.Add(user4);
            Users.Add(user5);
            Users.Add(user6);
        }
    }
}
