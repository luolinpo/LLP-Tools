using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplicationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("开始");
            BoilWater bw = new BoilWater();


            bw.Boil += Bw_Boil1;
            bw.Boil += delegate (object sender, BoilWater.BoilEventArgs e)
              {
                  Console.WriteLine("delegate温度" + e.temperature);
              };
            bw.Boil += (x, y) =>  Console.WriteLine("(x, y)" + y.temperature);
            Console.WriteLine("中间");
            bw.Boil += Bw_Boil;
            //bw.BoilString += Bw_BoilString1;

            //bw.BoilString += Bw_BoilString;
            //List<string> sss = bw.BeginBoilWaterString();
            //foreach (var item in sss)
            //{
            //    Console.WriteLine("sss"+ item);
            //}
            bw.BeginBoilWater();
            Console.WriteLine("结束");
            Console.ReadKey();
        }

        private static void Bw_Boil1(object sender, BoilWater.BoilEventArgs e)
        {
            BoilWater bw = (BoilWater)sender;
            Thread.Sleep(5000);
            Console.WriteLine("延迟温度" + e.temperature);
        }

        private static string Bw_BoilString1(int t)
        {
            Thread.Sleep(5000);
            return "测试是555" + t.ToString();
        }

        private static string Bw_BoilString(int t)
        {
            //Console.WriteLine("温度==============" + t);

            return "测试是"+t.ToString();
        }

        private static void Bw_Boil(object sender, BoilWater.BoilEventArgs e)
        {
            BoilWater bw = (BoilWater)sender;
            //Console.WriteLine("价格"+bw.price);
           // Console.WriteLine("温度 obj" + bw.temperature);
            Console.WriteLine("温度" + e.temperature);
        }
    }
}
