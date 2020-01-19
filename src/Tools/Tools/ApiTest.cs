using Common;
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
    public partial class ApiTest : Form
    {
        public ApiTest()
        {
            InitializeComponent();
        }
        //string url = "http://192.168.0.55:8077";
        string url = "http://localhost:4010";
        private void button1_Click(object sender, EventArgs e)
        {
            long timeStamp = DateTime.Now.Ticks;
            var paramDic= new SortedDictionary<string, string>();
            var headersDic = new SortedDictionary<string, string>();
            headersDic.Add("e", "chenghe");//username
            //param.Add("userpwd", "123");
            headersDic.Add("t", "202cb962ac59075b964b07152d234b70");//把secret加入进行加密 keysecret
            headersDic.Add("d", timeStamp.ToString());// datatime  _timestam
            var signDic = new SortedDictionary<string, string>();
            signDic = headersDic;
            foreach (var item in paramDic)
            {
                signDic.Add(item.Key, item.Value);
            }
            //请求参数拼接
            string sign = SignHelper.Sign(signDic, "123");
            headersDic.Add("n", sign);//sign
            string responseString = HttpRequestHelper.HttpPost(url+ "/GetDataByPost", headersDic, paramDic);

            MessageBox.Show(responseString);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            long timeStamp = DateTime.Now.Ticks;

            var param = new SortedDictionary<string, string>();
            param.Add("username", "chenghe");
            //param.Add("userpwd", "123");
            param.Add("appSecret", "202cb962ac59075b964b07152d234b70");//把secret加入进行加密
            param.Add("_timestamp", timeStamp.ToString());

            string sign = SignHelper.Sign(param, "123");
            param.Add("userpwd", sign);
            string responseString = HttpRequestHelper.HttpGet(url + "/GetDataByGet", param);

            MessageBox.Show(responseString);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            long timeStamp = DateTime.Now.Ticks;
            var paramDic = new SortedDictionary<string, string>();
            paramDic.Add("pa", "palpsadapapa");
            var headersDic = new SortedDictionary<string, string>();
            headersDic.Add("e", "chenghe");//username
            //param.Add("userpwd", "123");
            headersDic.Add("t", "202cb962ac59075b964b07152d234b70");//把secret加入进行加密 keysecret
            headersDic.Add("d", timeStamp.ToString());// datatime  _timestam
            var signDic = new SortedDictionary<string, string>();
            signDic = headersDic;
            foreach (var item in paramDic)
            {
                signDic.Add(item.Key, item.Value);
            }
            //请求参数拼接
            string sign = SignHelper.Sign(signDic, "123");
            headersDic.Add("n", sign);//sign
            var newtets = new newtets();
            newtets.name = "liahasd撒元";
            newtets.age = 181;
            string test= JsonHelper.JSON.stringify(newtets);
            string responseString = HttpRequestHelper.HttpPost(url + "/GetDataByPostPa", headersDic, test);

            MessageBox.Show(responseString);
        }
        public class newtets
        {
            public string name { get; set; }
            public int age { get; set; }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            long timeStamp = DateTime.Now.Ticks;
            var paramDic = new SortedDictionary<string, string>();
            paramDic.Add("pa", "palpsadapapa");
            var headersDic = new SortedDictionary<string, string>();
            headersDic.Add("e", "chenghe");//username
            //param.Add("userpwd", "123");
            headersDic.Add("t", "202cb962ac59075b964b07152d234b70");//把secret加入进行加密 keysecret
            headersDic.Add("d", timeStamp.ToString());// datatime  _timestam
            var signDic = new SortedDictionary<string, string>();
            signDic = headersDic;
            foreach (var item in paramDic)
            {
                signDic.Add(item.Key, item.Value);
            }
            //请求参数拼接
            string sign = SignHelper.Sign(signDic, "123");
            headersDic.Add("n", sign);//sign
            var newtets = new newtets();
            newtets.name = "liahasd撒元";
            newtets.age = 181;
            string test = JsonHelper.JSON.stringify(newtets);
            string responseString = HttpRequestHelper.HttpPost(url + "/GetDataByPostPa2", headersDic, test);

            MessageBox.Show(responseString);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            long timeStamp = DateTime.Now.Ticks;
            var paramDic = new SortedDictionary<string, string>();
            paramDic.Add("pa", "palpsadapapa");
            var headersDic = new SortedDictionary<string, string>();
            headersDic.Add("e", "chenghe");//username
            //param.Add("userpwd", "123");
            headersDic.Add("t", "202cb962ac59075b964b07152d234b70");//把secret加入进行加密 keysecret
            headersDic.Add("d", timeStamp.ToString());// datatime  _timestam
            var signDic = new SortedDictionary<string, string>();
            signDic = headersDic;
            foreach (var item in paramDic)
            {
                signDic.Add(item.Key, item.Value);
            }
            //请求参数拼接
            string sign = SignHelper.Sign(signDic, "123");
            headersDic.Add("n", sign);//sign
            var newtets = new newtets();
            newtets.name = "liahasd撒元";
            newtets.age = 181;
            string test = JsonHelper.JSON.stringify(newtets);
            string responseString = HttpRequestHelper.HttpPost(url + "/GetDataByPostPa3", headersDic, test);

            MessageBox.Show(responseString);
        }
    }
}
