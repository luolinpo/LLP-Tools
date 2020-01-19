using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tools
{
    public partial class Crawler : Form
    {
        public Crawler()
        {
            InitializeComponent();
        }

        private void btn_vehicleType_Click(object sender, EventArgs e)
        {
           
            string VLetter;//首字母
            string AType = "";//
            string BType = "";//
            string CType = "";//
            string Price = "暂无价格";//价格

            for (int i = 0; i < 26; i++)
            {
                DataTable dt = GetTable();
                VLetter= Encoding.ASCII.GetString(new byte[] { (byte)(i+65) });
                //VLetter = GetUpperLetter()[i];
                string pageHtml = HttpRequestUtil.GetPageHtmlGB2312("http://www.autohome.com.cn/grade/carhtml/" + VLetter + ".html");
                //string pageHtml = HttpRequestUtil.GetPageHtmlGB2312("http://price.pcauto.com.cn/cars/");
                //Regex regTypeVehicle = new Regex("<div class=\"h3-tit\">[\\s\\S]*?</dd>"); 主型号有误
                Regex regTypeVehicle = new Regex("<div><a href=\"http://car.autohome.com.cn/pic[\\s\\S]*?</dd>");

                MatchCollection mc = regTypeVehicle.Matches(pageHtml);
                foreach (Match match in mc)
                {
                    string vChildren = match.Value.ToString();
                    //==
                    //Regex regA = new Regex("(?<=(<div class=\"h3-tit\">)).*?(?=<)");主型号有误
                    Regex regA = new Regex("(?<=<div><a href=\"http://car.autohome.com.cn/pic/.*?>).*?(?=<)");
                    Match matchA = regA.Match(vChildren);
                    AType = matchA.Value;
                    //==
                    Regex regTypeVChildren = new Regex("<div class=\"h3-tit\">[\\s\\S]*?</ul>");
                    MatchCollection mcChildren = regTypeVChildren.Matches(vChildren);
                    foreach (Match citem in mcChildren)
                    {

                        string Type = citem.Value.ToString();

                        //==
                        Regex regB = new Regex("(?<=(<div class=\"h3-tit\">)).*?(?=<)");
                        Match matchB = regB.Match(Type);
                        BType = matchB.Value;
                        //==

                        Regex regType = new Regex("(<h4 class=\"toolong\"[\\s\\S]*?</li>)|(<h4><a href=[\\s\\S]*?</li>)");
                        // Regex regType = new Regex("<h4><a href=[\\s\\S]*?</li>");
                        MatchCollection mcType = regType.Matches(Type);
                        foreach (Match tItem in mcType)
                        {
                            string vData = tItem.Value.ToString();
                            Regex regVData = new Regex("<a[\\s\\S]*?</a>");
                            MatchCollection mcVData = regVData.Matches(vData);
                            //foreach (var item in mcVData)
                            //{
                            string vValue = tItem.Value.ToString();
                            Regex regValue = new Regex("\">[\\s\\S]*?</a>");
                            MatchCollection mcValue = regVData.Matches(vData);
                            if (mcValue.Count == 4 || mcValue.Count == 3 || mcValue.Count == 5)
                            {
                                Regex regC = new Regex("(?<=>).*?(?=<)");
                                Match matchC = regC.Match(mcValue[0].ToString());
                                CType = matchC.Value;
                                dt.Rows.Add(0, VLetter, AType, BType, CType, "暂无价格");

                            }
                            else if (mcValue.Count == 6)
                            {
                                Regex regC = new Regex("(?<=>).*?(?=<)");
                                Match matchC = regC.Match(mcValue[0].ToString());
                                CType = matchC.Value;

                                Match matchP = regC.Match(mcValue[1].ToString());
                                Price = matchP.Value;
                                dt.Rows.Add(0, VLetter, AType, BType, CType ?? "类型错误", Price ?? "价格错误");
                            }
                            //}
                        }
                    }
                }

                try
                {
                    //写库
                    //ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
                    //string conStr = connections["constrLocal"].ToString();
                    string conStr = "Data Source=127.0.0.1;Initial Catalog=EFDemo;User ID=sa;Password=123456";
                    SqlBuckCopyHelper.SqlBulkCopyByDatatable(conStr, "VehicleType", dt);
                    //进度
                    MessageBox.Show("车型入库完成");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
    }

        public DataTable GetTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));//
            dt.Columns.Add("VehicleInitial", typeof(char));//  
            dt.Columns.Add("VechileTypeParent", typeof(string));//  
            dt.Columns.Add("VechileTypeSecond", typeof(string));//  
            dt.Columns.Add("VechileTypeChildren", typeof(string));//  
            dt.Columns.Add("GuidingPrices", typeof(string));// 
            return dt;
        }

        private void btn_attack_Click(object sender, EventArgs e)
        {
            int page = 0;
            for (int j = 1; j <= 13; j++)
            {
                try
                {
                    string pageHtml = HttpRequestUtil.GetPageHtml("http://qq.yh31.com/dm/bl/List_" + j.ToString() + ".html");
                    Regex regA = new Regex("<a href=.*img.*</a>");
                    Regex regImg = new Regex("<img.*/>");
                    MatchCollection mc = regA.Matches(pageHtml);
                    Console.WriteLine($"网页共有{mc.Count}");
                    foreach (Match match in mc)
                    {

                        //lblPerson.Invoke(new Action(delegate () { lblPerson.Text = "已完成条数：" + personCount.ToString(); }));
                        MatchCollection mcImgPage = regImg.Matches(match.ToString());
                        foreach (Match matchImgPage in mcImgPage)
                        {
                            int start = matchImgPage.ToString().IndexOf("src=\"");
                            string imgUrl = matchImgPage.ToString().Substring(start + 5);
                            int end = imgUrl.IndexOf("\"");
                            imgUrl = imgUrl.Substring(0, end);
                            try
                            {

                                HttpRequestUtil.HttpDownloadFile("http://qq.yh31.com/" + imgUrl);
                                Console.WriteLine($"图片地址{imgUrl}");
                            }
                            catch { }
                            Thread.Sleep(1);
                        }


                    }
                }
                catch { }
                page++;
                if (page == 100)
                {
                    MessageBox.Show("完成！");
                }
            }
        }

        private void btn_maopoo_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string pageHtml = HttpRequestUtil.GetPageHtmlGB2312("https://cjb.alipay.com/gold/guide.htm");
                Regex regA = new Regex("(?:<span class=\"ft-56\">)[\\s\\S]*?</span>元／克");
                MatchCollection mc = regA.Matches(pageHtml);
                foreach (Match match in mc)
                {
                    string temp =match.ToString().Replace("<span class=\"ft-56\">", "").Replace("</span>元／克","");
                    double price = double.Parse(temp.Trim());

                }

                }
            catch { }
        }
    }
}
