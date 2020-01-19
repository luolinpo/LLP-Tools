using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tools.Gserver
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TcpClient client = new TcpClient();
            try
            {
                client.Connect("localhost", 8500);      // 与服务器连接
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

        }
    }
}
