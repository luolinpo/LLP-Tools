using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tools
{
    public partial class IntefaceUrlCreate : Form
    {
        public IntefaceUrlCreate()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string oldString = textBox1.Text.Trim();
            string newString = string.Empty;
            foreach (var item in oldString)
            {
                var x = (int)item + 2;
                byte[] array = new byte[1];
                array[0] = (byte)(Convert.ToInt32(x)); //ASCII码强制转换二进制
                newString += Convert.ToString(System.Text.Encoding.ASCII.GetString(array));
            }
            textBox2.Text = newString;
        }
    }
}
