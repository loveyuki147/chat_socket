using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server_Chat
{
    public partial class Form1 : Form
    {
        ServerProgram ServerProgram;
        public Form1()
        {
            InitializeComponent();
            ServerProgram = new ServerProgram();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            ServerProgram.StartServer();
            ServerProgram.SetLog = WriteLog;
        }

        void WriteLog(string log)
        {
            richTxbLog.Invoke((MethodInvoker)(() =>
            {
                richTxbLog.Text += log + "\r\n";
                richTxbLog.ScrollToCaret();
            }));
        }
    }
}
