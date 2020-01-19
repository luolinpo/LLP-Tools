using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationTest
{
    public class BoilWater
    {
        public int temperature = 0;
        public int temperature2 = 0;
        public string brand ="Haier";
        public string price = "1000元";
        public class BoilEventArgs : EventArgs
        {
            public readonly int temperature;
            public BoilEventArgs(int temperature)
            {
                this.temperature = temperature;
            }
        }
        public delegate void BoilEventHandler(object sender, BoilEventArgs e);
        public event BoilEventHandler Boil;

        public delegate string BoilStringEventHandler(int t);
        public event BoilStringEventHandler BoilString;

        public List<string> returnList = new List<string>();
        public List<string> BeginBoilWaterString()
        {
            for (int i = 0; i < 100; i++)
            {
                temperature2 = i;
                if (temperature2 > 95)
                {

 
                    //获取委托数组
                    Delegate[] deleArray = BoilString.GetInvocationList();
                    foreach (var item in deleArray)
                    {  
                        //向下的强转 获取当前的委托类型
                        BoilStringEventHandler item2 = (BoilStringEventHandler)item;
                        item2.BeginInvoke(temperature,null,null);
                        returnList.Add(item2(temperature2));//调用方法 并添加返回值
                    }
                }
               
            }
            return returnList;
        }

        protected virtual void OnBoiled(BoilEventArgs e)
        {
            if (Boil != null)
            { // 如果有对象注册
                //Boil(this, e); 
                Boil.BeginInvoke(this, e, null, null);
            }
        }

        public void BeginBoilWater()
        {
            for (int i = 0; i < 100; i++)
            {
                temperature = i;
  
                if (temperature>95)
                {
                    BoilEventArgs e = new BoilEventArgs(temperature);
                    Boil(this, e);
                    //OnBoiled(e);
                }
               
            }
        }
    }
}
