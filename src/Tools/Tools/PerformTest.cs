using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tools
{
    public partial class PerformTest : Form
    {
        public PerformTest()
        {
            InitializeComponent();
        }
        public const int Counts= 10000000;
        private void button1_Click(object sender, EventArgs e)
        {
            double A = 0;
            List<int> Old = new List<int>();
            for (int j= 0; j < Counts; j++)
            {
                Old.Add(j);
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //  
            int iterations =(int)Math.Ceiling((Old.Count*1.0 / 8));
            int startAt = Old.Count % 8;
            var i = 0;
            do
            {
                switch (startAt)
                {
                    case 0:
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        break;
                    case 7:
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        break;
                    case 6:
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        break;
                    case 5:
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        break;
                    case 4:
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        break;
                    case 3:
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        break;
                    case 2:
                        A += (Old[i++]);
                        A += (Old[i++]);
                        break;
                    case 1:
                        A += (Old[i++]);
                        break;
                }
                startAt = 0;
            } while (--iterations > 0);

            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            Console.WriteLine(A.ToString());
            Console.WriteLine("Stopwatch总共花费{0}ms.", ts2.TotalMilliseconds);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            double A2 = 0;
            List<int> Old2 = new List<int>();
            for (int j = 0; j < Counts; j++)
            {
                Old2.Add(j);
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < Old2.Count; i++)
            {
                A2 += Old2[i];
            }

            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            Console.WriteLine(A2.ToString());
            Console.WriteLine("Stopwatch总共花费{0}ms.", ts2.TotalMilliseconds);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double A3 = 0;
            List<int> Old3 = new List<int>();
            for (int j = 0; j < Counts; j++)
            {
                Old3.Add(j);
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int a = Old3.Count;
            for (int i = 0; i < a; i++)
            {
                A3 += Old3[i];
            }

            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            Console.WriteLine(A3.ToString());
            Console.WriteLine("Stopwatch总共花费{0}ms.", ts2.TotalMilliseconds);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double A4 = 0;
            List<int> Old4 = new List<int>();
            for (int j = 0; j < Counts; j++)
            {
                Old4.Add(j);
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = Old4.Count-1; i >0; i--)
            {
                A4 += Old4[i];
            }

            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            Console.WriteLine(A4.ToString());
            Console.WriteLine("Stopwatch总共花费{0}ms.", ts2.TotalMilliseconds);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            double A = 0;
            List<int> Old = new List<int>();
            for (int j = 0; j < Counts; j++)
            {
                Old.Add(j);
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //  
            int iterations = (int)Math.Ceiling((Old.Count * 1.0 / 10));
            int startAt = Old.Count % 10;
            var i = 0;
            do
            {
                switch (startAt)
                {
                    case 0:
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        break;
                    case 9:
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        break;
                    case 8:
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        break;
                    case 7:
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        break;
                    case 6:
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        break;
                    case 5:
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        break;
                    case 4:
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        break;
                    case 3:
                        A += (Old[i++]);
                        A += (Old[i++]);
                        A += (Old[i++]);
                        break;
                    case 2:
                        A += (Old[i++]);
                        A += (Old[i++]);
                        break;
                    case 1:
                        A += (Old[i++]);
                        break;
                }
                startAt = 0;
            } while (--iterations > 0);

            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            Console.WriteLine(A.ToString());
            Console.WriteLine("Stopwatch总共花费{0}ms.", ts2.TotalMilliseconds);
        }

        private void button6_Click(object sender, EventArgs e)
        {
          
      
        }
        public class Test : ICloneable
        {
            public object Clone()
            {
                throw new NotImplementedException();
            }
        }
    }
}
