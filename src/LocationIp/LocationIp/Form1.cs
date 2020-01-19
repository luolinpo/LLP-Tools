using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocationIp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IPHostEntry fromHE = Dns.GetHostEntry(Dns.GetHostName());
            IPEndPoint ipEndPointFrom = new IPEndPoint(fromHE.AddressList[0], 80);
            EndPoint EndPointFrom = (ipEndPointFrom);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < fromHE.AddressList.Length; i++)
            {
                sb.AppendFormat("{0}--,ip地址{1}",i, fromHE.AddressList[i]);
                sb.AppendLine();
            }
            richTextBox1.Text = sb.ToString();
        }
    }
}
