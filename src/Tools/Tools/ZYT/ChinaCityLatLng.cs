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

namespace Tools.ZYT
{
    public partial class ChinaCityLatLng : Form
    {
        public class BaiduAddressEntity
        {
            /// <summary>
            /// 返回码状态表
            /// </summary>
            public string status { get; set; }
            /// <summary>
            /// 结果
            /// </summary>
            public AddresResult result { get; set; }
        }
        public class AddresResult
        {
            /// <summary>
            /// 拆分
            /// </summary>
            public AddressComponent addressComponent { get; set; }
        }
        public class AddressComponent
        {
            /// <summary>
            /// 省
            /// </summary>
            public string province { get; set; }

            /// <summary>
            /// 市、区
            /// </summary>
            public string city { get; set; }
        }
        public ChinaCityLatLng()
        {
            InitializeComponent();
        }
        private DataTable GetTableSchema()
        {
            DataTable dt = new DataTable();
            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("TopAreaID", typeof(string));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Level", typeof(int));
                dt.Columns.Add("CreateTime", typeof(DateTime));
                dt.Columns.Add("Lat1", typeof(string));
                dt.Columns.Add("Lng1", typeof(string));
                dt.Columns.Add("Lat2", typeof(string));
                dt.Columns.Add("Lng2", typeof(string));
                dt.Columns.Add("Lat3", typeof(string));
                dt.Columns.Add("Lng3", typeof(string));
            }
            return dt;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string outMsg = string.Empty;
            string filePath_XML = @"C:\Users\Administrator\Desktop\工具Html\Tools\Tools\App_Data\latitude.xml";
            ChinaEntity temp = SerializableXml.DeserializeByStream(typeof(ChinaEntity), filePath_XML, out outMsg) as ChinaEntity;

            #region 读取数据
            DataTable dt = GetTableSchema();
            var tempID = temp.Latitude.Count + 1;
            for (int i = 1; i <= temp.Latitude.Count; i++)
            {
                DataRow dataRow = dt.NewRow();
                dataRow["ID"] = i;
                dataRow["TopAreaID"] = 0;
                dataRow["Name"] = temp.Latitude[i - 1].Name;
                dataRow["Level"] = 1;
                dataRow["CreateTime"] = DateTime.Now;
                dataRow["Lat1"] = string.Empty;
                dataRow["Lng1"] = string.Empty;
                dataRow["Lat2"] = string.Empty;
                dataRow["Lng2"] = string.Empty;
                dataRow["Lat3"] = string.Empty;
                dataRow["Lng3"] = string.Empty;
                dt.Rows.Add(dataRow);
                foreach (var item in temp.Latitude[i - 1].City)
                {
                    DataRow dataRow2 = dt.NewRow();
                    dataRow2["ID"] = tempID; tempID++;
                    dataRow2["TopAreaID"] = i;
                    dataRow2["Name"] = item.Name;
                    dataRow2["Level"] = 2;
                    dataRow2["CreateTime"] = DateTime.Now;
                    if (item.CityChildren.Count >= 3)
                    {
                        dataRow2["Lat1"] = item.CityChildren[0].Lat;
                        dataRow2["Lng1"] = item.CityChildren[0].Lng;
                        dataRow2["Lat2"] = item.CityChildren[1].Lat;
                        dataRow2["Lng2"] = item.CityChildren[1].Lng;
                        dataRow2["Lat3"] = item.CityChildren[2].Lat;
                        dataRow2["Lng3"] = item.CityChildren[2].Lng;

                    }
                    if (item.CityChildren.Count == 2)
                    {
                        dataRow2["Lat1"] = item.CityChildren[0].Lat;
                        dataRow2["Lng1"] = item.CityChildren[0].Lng;
                        dataRow2["Lat2"] = item.CityChildren[1].Lat;
                        dataRow2["Lng2"] = item.CityChildren[1].Lng;

                    }
                    if (item.CityChildren.Count == 1)
                    {
                        dataRow2["Lat1"] = item.CityChildren[0].Lat;
                        dataRow2["Lng1"] = item.CityChildren[0].Lng;
                    }
                    dt.Rows.Add(dataRow2);
                }
            }
            #endregion
            #region name纠正
            StringBuilder sb = new StringBuilder();
            for (int i = 33; i < dt.Rows.Count; i++)
            {
                try
                {
                    DataRow tempRow = dt.Rows[i];
                    string name1 = GetBaiduAddress(tempRow["Lat1"].ToString(), tempRow["Lng1"].ToString()).result.addressComponent.city;
                    string name2 = GetBaiduAddress(tempRow["Lat2"].ToString(), tempRow["Lng2"].ToString()).result.addressComponent.city;
                    string name3 = GetBaiduAddress(tempRow["Lat3"].ToString(), tempRow["Lng3"].ToString()).result.addressComponent.city;
                    if (name1 == name2 || name2 == name3)
                    {
                        dt.Rows[i]["Name"] = name1;
                        sb.AppendFormat("【正常修改】原始为{0}，百度分别为{1},{2},{3}", dt.Rows[i]["Name"].ToString(), name1, name1, name1);
                        sb.AppendLine();
                    }
                    else
                    {
                        sb.AppendFormat("【正常修改失败】原始为{0}，百度分别为{1},{2},{3", dt.Rows[i]["Name"].ToString(), name1, name1, name1);
                        sb.AppendLine();
                    }
                }
                catch (Exception ex)
                {
                    sb.AppendFormat("【修改异常】{0}，百度分别为{1},{2},{3},{4},{5},{6}", 
                        dt.Rows[i]["Name"].ToString()
                        , dt.Rows[i]["Lat1"].ToString()
                        , dt.Rows[i]["Lng1"].ToString()
                        , dt.Rows[i]["Lat2"].ToString()
                        , dt.Rows[i]["Lng2"].ToString()
                        , dt.Rows[i]["Lat3"].ToString()
                        , dt.Rows[i]["Lng3"].ToString()
                        );
                    sb.AppendLine();
                }
                
            }
            richTextBox1.Text = sb.ToString();
            #endregion
            string conStr2 ="data source=gps200.coneton.com,4567;initial catalog=zyt_basegps_test;persist security info=true;user id=rdc_dev;password=rdc_dev6983";
            SqlBuckCopyHelper.SqlBulkCopyByDatatable(conStr2, "AreaInfo", dt);
            //进度
            MessageBox.Show("省市经纬度入库完成");
        }
        /// <summary>
        /// 根据经纬度获取百度地址
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        protected virtual BaiduAddressEntity GetBaiduAddress(string lat, string lng)
        {
            //纠偏

            string strUrl = string.Format("http://api.map.baidu.com/geocoder/v2/?ak={2}&location={0},{1}&output=json&pois=0&coordtype=wgs84ll", lat, lng, "Padq2FiGrjmUOQI30WfcIzfd");
            string responseString = HttpRequestHelper.HttpGet(strUrl);
            BaiduAddressEntity addressModel = JsonHelper.JSON.parse<BaiduAddressEntity>(responseString);
            return addressModel;
        }
    }
}
