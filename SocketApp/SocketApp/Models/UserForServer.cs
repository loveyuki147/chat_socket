using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketApp.Models
{
    [Serializable]
    public class UserForServer : User
    {
        #region Properties
        string password;
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
        public string Password
        {
            get
            {
                return this.password;
            }

        }

        #endregion

        public UserForServer(string username, string password, string name, Bitmap avatar) : base(username, name)
        {
            this.password = password;
            this.avatar = avatar;
            SetToken();
        }

        public void SetToken()
        {
            token = CreateMD5(Username + "^" + Password);
        }

        public UserForClient ConverToUserForClient()
        {
            return new UserForClient(Username, Name, Token, Avatar, IsOnline);
        }

        private string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
