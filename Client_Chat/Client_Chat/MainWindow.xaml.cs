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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClientProgram clientProgram;
        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            clientProgram = new ClientProgram();
            clientProgram.InitTcpClient();
            bool flag = clientProgram.ConnectToServer();
            if(flag)
            {
                var user = clientProgram.Login(txbUsername.Text, passwdBox.Password);
                if(user != null)
                {
                    ChatWindow chatWindow = new ChatWindow(user);
                    //clientProgram.UserForClient = user;
                    chatWindow.ClientProgram = clientProgram;
                    this.Hide();
                    chatWindow.ShowDialog();
                    this.Show();

                }
                else
                {
                    MessageBox.Show("Đăng nhập thất bại");
                }
            }
            else
            {
                clientProgram.Disconnect();
                MessageBox.Show("Không có kết nối tới Server");
               
            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
