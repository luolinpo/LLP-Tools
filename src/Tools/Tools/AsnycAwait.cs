using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tools
{
    public partial class AsnycAwait : Form
    {
        public AsnycAwait()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            richTextBox1.Text += DateTime.Now.ToString()+"\r\n";
            Console.WriteLine(DateTime.Now.ToString());
            sw.Start();
            int a = 100;
            int b = 50;
            string ar = Add(a, b).ToString();
            string br = Subtraction(a, b).ToString();
            Wait();
            richTextBox1.Text += DateTime.Now.ToString() + "\r\n";
            Console.WriteLine(DateTime.Now.ToString());
            string temp = ar + "==" + br;
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            label1.Text += ts2.ToString();
            richTextBox1.Text += DateTime.Now.ToString() + "\r\n";
            Console.WriteLine(DateTime.Now.ToString());

        }
        public void Wait()
        {
            for (int i = 0; i < 100000000; i++)
            {
                int a = i * i + 1 - 200 * 5 - 6+ 115 + 8987 * 54984 - 8797815;
                int b = i - 1 + 15 + 4 + 9 * 9 - 987- 115 + 8987 * 54984 - 8797815;
                int c = i * (a - b) * i/98*115+8987*54984-8797815;
            }
        }
        public int Add(int a, int b)
        {
            Wait();
            return a + b;
        }
        public int Subtraction(int a, int b)
        {
            Wait();
            return a - b;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            richTextBox2.Text += DateTime.Now.ToString() + "\r\n";
            Console.WriteLine(DateTime.Now.ToString());
            sw.Start();
            int a = 100;
            int b = 50;
            int ar =await Add2(a, b);
            int br = await Subtraction2(a, b);
            Wait();
            richTextBox2.Text += DateTime.Now.ToString() + "\r\n";
            Console.WriteLine(DateTime.Now.ToString());
            string temp = ar + "==" + br;
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            label2.Text += ts2.ToString();
            richTextBox2.Text += DateTime.Now.ToString() + "\r\n";
            Console.WriteLine(DateTime.Now.ToString());

        }

        public async Task<int> Add2(int a, int b)
        {
            return await Task.Run(() =>
            {
                return Add(a, b);
            });
        }
        public async Task<int> Subtraction2(int a, int b)
        {
            return await Task.Run(() =>
            {
                return Subtraction(a, b);
            });

        }

    }
}
