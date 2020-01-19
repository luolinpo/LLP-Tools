using Common;
using Model;
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
    public partial class AMapSubwayCrawler : Form
    {
        public AMapSubwayCrawler()
        {
            InitializeComponent();
        }
        public DataTable GetTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));//
            dt.Columns.Add("CityName", typeof(string));//  
            dt.Columns.Add("LineID", typeof(string));//  
            dt.Columns.Add("LineName", typeof(string));//  
            dt.Columns.Add("StationSequence", typeof(int));//  
            dt.Columns.Add("StationName", typeof(string));// 
            dt.Columns.Add("StationLng", typeof(string));//  
            dt.Columns.Add("StationLat", typeof(string));// 
            return dt;
        }
        private void btn_Start_Click(object sender, EventArgs e)
        {
            DataTable dt = GetTable();
            string CityName = "成都";//
            int LineID = 0;//
            string LineName = "";//
            for (int i = 1; i < 9; i++)
            {
                try
                {
                    LineID = i;
                    LineName = string.Format("{0}地铁{1}号线", CityName, LineID);
                    string tempLineName = System.Web.HttpUtility.UrlEncode(LineName, Encoding.UTF8);
                    string pageHtml = HttpRequestUtil.GetPageHtml("http://ditu.amap.com/service/poiInfo?query_type=TQUERY&pagesize=20&pagenum=1&qii=true&cluster_state=5&need_utd=true&utd_sceneid=1000&div=PC1000&addr_poi_merge=true&is_classify=true&city=510100&geoobj=104.05071%7C30.584681%7C104.06101%7C30.591479&keywords=" + tempLineName);
                    Regex regTypeVehicle = new Regex("{\"type\"[\\s\\S]*?(?=,{\"type\":\"polyline\")");

                    MatchCollection mc = regTypeVehicle.Matches(pageHtml);
                    Match match = mc[0];
                    string vChildren = match.Value.ToString();
                    SubWayReturnEntity listSubWay = JsonHelper.JSON.parse<SubWayReturnEntity>(vChildren);

                    foreach (var item in listSubWay.busData)
                    {

                        dt.Rows.Add(0, CityName, LineID, LineName, item.sequence, item.name, item.location.lng, item.location.lat);
                    }

                    //写库
                    //ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
                    //string conStr = connections["constrLocal"].ToString();

                    string conStr = "Data Source=127.0.0.1;Initial Catalog=EFDemo;User ID=sa;Password=123456";
                    SqlBuckCopyHelper.SqlBulkCopyByDatatable(conStr, "SubWayLocationT", dt);
                    dt.Clear();
                    //进度
                    this.rbox_info.Text += string.Format("{0} 数据获取成功\r\n", LineName);

                    //
                    Thread.Sleep(20000);
                }
                catch (Exception ex)
                {
                    this.rbox_errInfo.Text += ex.ToString();
                    this.rbox_info.Text += string.Format("{0} 数据获取失败\r\n", LineName);
                }
            }
        }
    }
}
