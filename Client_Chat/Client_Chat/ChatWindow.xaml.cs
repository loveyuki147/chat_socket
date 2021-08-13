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
using System.Windows.Shapes;

namespace Client_Chat
{
    /// <summary>
    /// Interaction logic for WindowChat.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        UserForClient UserForClient { get; set; }
        ClientProgram clientProgram;
        internal ClientProgram ClientProgram
        {
            get
            {
                return this.clientProgram;
            }

            set
            {
                this.clientProgram = value;
                this.clientProgram.OnRecvMessage += this.ClientProgram_OnRecvMessage;
                this.clientProgram.OnSendMessageSuccess += this.ClientProgram_OnSendMessageSuccess;
                this.clientProgram.OnGetFriends += this.ClientProgram_OnGetFriends;
                clientProgram.ListeningRecv();
            }
        }

       

      
        public ChatWindow(UserForClient userForClient)
        {
    
                InitializeComponent();
                UserForClient = userForClient;

                txblockName.Text = UserForClient.Name;
                imageAvatar.Source = ProcessBitmap.BitmapToImageSource(UserForClient.Avatar);
     
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            string text = txbMess.Text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                ClientProgram.SendMessage(text);
                txbMess.Text = string.Empty;
            }
        }

        void SetMessage(Message mess, bool isRight)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                MessegaBlock messegaBlock = new MessegaBlock();

                UserForClient user = !isRight && ClientProgram.Users.ContainsKey(mess.TokenOfUser) ? ClientProgram.Users[mess.TokenOfUser].UserForClient : ClientProgram.UserForClient;


                messegaBlock.Mess = mess.Text;
                messegaBlock.Time = mess.Time.ToShortTimeString();
                messegaBlock.NameDisplay = user.Name;
                messegaBlock.Source = ProcessBitmap.BitmapToImageSource(user.Avatar);
                messegaBlock.Margin = new Thickness(0, 5, 0, 5);
                if (isRight)
                {
                    messegaBlock.IsRight = isRight;
                    messegaBlock.HorizontalAlignment = HorizontalAlignment.Right;
                }

                stackPanelMain.Children.Add(messegaBlock);
                scrollViewerMess.ScrollToBottom();
            });

        }

        private void ClientProgram_OnSendMessageSuccess(object sender, SocketApp.Net.EventArgs_Message e)
        {
            SetMessage(e.Message, true);
        }


        private void ClientProgram_OnRecvMessage(object sender, SocketApp.Net.EventArgs_Message e)
        {
            SetMessage(e.Message, false);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ClientProgram.Disconnect();
        }
        private void ClientProgram_OnGetFriends(object sender, SocketApp.Net.EventArgs_Packet e)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                foreach (var item in ClientProgram.Users)
                {
                    var value = item.Value.UCControlFriend;
                    value.Width = stackPnlFriends.Width;
                    stackPnlFriends.Children.Add(value);
                }
            });
        }

    }
}
