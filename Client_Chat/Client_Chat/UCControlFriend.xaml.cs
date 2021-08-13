using SocketApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client_Chat
{
    /// <summary>
    /// Interaction logic for UCControlFriend.xaml
    /// </summary>
    public partial class UCControlFriend : UserControl
    {
        UserForClient user;

        public UserForClient User
        {
            get
            {
                return this.user;
            }

            private set
            {
                this.user = value;
            }
        }
        public UCControlFriend()
        {
            InitializeComponent();
            Online();
        }
        public UCControlFriend(UserForClient user)
        {
            InitializeComponent();
            this.User = user;
            Init();
        }
        void Init()
        {
            txbName.Text = User.Name;
            imageAvatar.Source = ProcessBitmap.BitmapToImageSource(user.Avatar);

            if (user.IsOnline)
            {
                Online();
            }
            else
                Offline();
        }

        public void Online()
        {
            borderOnl.Background = new SolidColorBrush(Color.FromRgb(69, 204, 88));
            txbOnl.Text = "Online";
        }

        public void Offline()
        {
            borderOnl.Background = new SolidColorBrush(Color.FromRgb(229, 45, 39));
            txbOnl.Text = "Offline";
        }
    }
}
