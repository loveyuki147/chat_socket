using SocketApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Chat
{
    class UserDAL
    {
        private static UserDAL instance;


        internal static UserDAL Instance
        {
            get
            {
                if (instance == null)
                    instance = new UserDAL();
                return instance;
            }

            set
            {
                instance = value;
            }
        }

        public UserDAL()
        {

        }

        public UserForServer ValidDataLogin(Dictionary<string, string> data)
        {
            //var bte = ReceiveData();
            if (data == null)
                return null;
            if (data.ContainsKey("username") || data.ContainsKey("password"))
            {
                string username = data["username"];
                string passwd = data["password"];
                UserForServer user = DataProvider.Instance.Users.FirstOrDefault((u) => username == u.Username && passwd == u.Password);

                return user;
            }
            return null;

        }

        public UserForServer ValidDataLogin(string username, string passwd)
        {
            return DataProvider.Instance.Users.FirstOrDefault((u) => username == u.Username && passwd == u.Password);
        }


        public List<UserForClient> GetAllUserForClient()
        {
            var a = new List<UserForClient>();
            foreach (var item in DataProvider.Instance.Users)
            {
                a.Add(item.ConverToUserForClient());
            }
            return a;
        }
        public List<UserForClient> GetAllUserForClient(UserForClient userNotGet)
        {
            var a = new List<UserForClient>();
            foreach (var item in DataProvider.Instance.Users)
            {
                if (item.Token != userNotGet.Token)
                    a.Add(item.ConverToUserForClient());
            }
            return a;
        }
        //public List<UserForServer> GetAllUserNoPassword()
        //{
        //    return null;
        //}
    }
}
