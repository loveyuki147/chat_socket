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
    /// Interaction logic for MessegeBlock.xaml
    /// </summary>
    public partial class MessegaBlock : UserControl
    {

        string mess;
        string time;
        string nameDisplay;
        ImageSource source;
        bool isRight;
        internal string Mess
        {
            get
            {
                return this.mess;
            }

            set
            {
                this.mess = value;
                txbMessMain.Text = value;
            }
        }

        internal string NameDisplay
        {
            get
            {
                return this.nameDisplay;
            }

            set
            {
                this.nameDisplay = value;
                lbnameDisplay.Content = value;
            }
        }

        internal string Time
        {
            get
            {
                return this.time;
            }

            set
            {
                this.time = value;
                lbTime.Content = value;
            }
        }



        internal ImageSource Source
        {
            get
            {
                return this.source;
            }

            set
            {
                this.source = value;
                imageAvatar.Source = value;
            }
        }

        public bool IsRight
        {
            get
            {
                return this.isRight;
            }

            set
            {
                this.isRight = value;
                if(isRight)
                {
                    var a = gridAvatar;
                    stackMain.Children.Remove(a);
                    stackMain.Children.Add(a);
                }
                else
                {
                    var a = GridMessage;
                    stackMain.Children.Remove(a);
                    stackMain.Children.Add(a);
                }
            }
        }

        public MessegaBlock()
        {
            InitializeComponent();
   
        }

        void AddBinding()
        {
            //var myBinding = new Binding("Text")
            //{
            //    Source = Mess,
            //};
            //txbMessMain.SetBinding(TextBlock.TextProperty, myBinding);

            //var myBinding2 = new Binding("Mess")
            //{
            //    Source = Time,
            //};
            //lbTime.SetBinding(Label.ContentProperty, myBinding2);

            //var myBinding3 = new Binding("Text")
            //{
            //    Source = Time,
            //};
            //lbnameDisplay.SetBinding(Label.ContentProperty, myBinding3);
        }
        
    }
}
