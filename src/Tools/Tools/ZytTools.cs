using Common;
using Model;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Tools
{
    public partial class ZytTools : Form
    {
        public ZytTools()
        {
            InitializeComponent();
        }
        public class execlEntity
        {
            public int ID { get; set; }
            public string plate { get; set; }

            public string plate2 { get; set; }

            public string time1 { get; set; }

            public DateTime time2 { get; set; }


            public string cha { get; set; }
        }

        //public bool GetRealData(string plate)
        //{
        //    List<AppVehicleEntity> returnAllAlarmSettingData = new List<AppVehicleEntity>();
        //    try
        //    {
        //        if (VehicleIDString == null || VehicleIDString == string.Empty)
        //        {
        //            return returnAllAlarmSettingData;
        //        }
        //        using (zytapp_basegpsEntities db = new zytapp_basegpsEntities())
        //        {
        //            db.Database.Connection.ConnectionString = conStr;
        //            db.Configuration.ValidateOnSaveEnabled = false;
        //            string sqlStr = string.Format(@"SELECT VehicleID,RecvTime,Lng,Lat,Lng2,Lat2,Validate AS GpsAv,LBS AS LbsAv,ISNULL(Alarm,'')  as State FROM dbo.PositionLast 
        //                                             WHERE  VehicleID IN ({1})
        //                                             AND  Validate=1 
        //                                             AND  RecvTime>'{0}'", BeforeTime.ToString(), VehicleIDString);
        //            returnAllAlarmSettingData = db.Database.SqlQuery<AppVehicleEntity>(sqlStr).ToList<AppVehicleEntity>();
        //            LogHelper.WriteLog(sqlStr);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.WriteErrorLog("查询监控车辆的最后位置数据DAL", ex);
        //    }
        //    return returnAllAlarmSettingData;
        //}

        private void btn_openflp_Click(object sender, EventArgs e)
        {
            //List<string> plateString = new List<string>();
            //DialogResult dr = ofd_red.ShowDialog();
            //if (dr.Equals(DialogResult.OK))
            //{

            //    DataTable dt = new DataTable();
            //    using (Stream s = File.OpenRead(ofd_red.FileName))
            //    {
            //        dt = ExeclNopiHelper.ReadExceleByStream(s);

            //    }
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        plateString.Add((dt.Rows[i]).ItemArray[0].ToString().Trim().Replace('-', ' '));
            //    }
            //    StringBuilder sb = new StringBuilder();
            //    foreach (var item in plateString)
            //    {
            //        if (!string.IsNullOrWhiteSpace(item))
            //        {
            //            ""

            //            sb.AppendFormat("", item);
            //        }

            //    }
            //    SqlConnection sqlConn = new SqlConnection("data source=125.64.16.47,8163;initial catalog=zyt_basegps;persist security info=True;user id=sa;password=772379yl");
            //    SqlTransaction sqlTrans = null;
            //    try
            //    {

            //        sqlConn.Open();
            //        sqlTrans = sqlConn.BeginTransaction();//事务开始 
            //        SqlCommand cmd = new SqlCommand();
            //        cmd.Connection = sqlConn;
            //        cmd.Transaction = sqlTrans;
            //        cmd.CommandText = sb.ToString();//Convert.ToString(sql);
            //        cmd.ExecuteNonQuery();
            //        sqlTrans.Commit();

            //    }
            //    catch (Exception)
            //    {

            //    }
            //}

        }

        public class EData
        {
            public string VPlate { get; set; }
            public string VTime { get; set; }
            public string ID { get; set; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            List<int> needSetVid = new List<int>();
            List<int> existVid = new List<int>();
            List<int> insertVid = new List<int>();
            DialogResult dr = ofd_red.ShowDialog();
            if (dr.Equals(DialogResult.OK))
            {
                //StreamReader sr = new StreamReader(ofd_red.FileName, Encoding.Default);
                DataTable dt = new DataTable();
                using (Stream s = File.OpenRead(ofd_red.FileName))
                {
                    dt = ExeclNopiHelper.ReadExceleByStream(s);

                }
                List<EData> plateOList = new List<EData>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    EData ed = new EData();
                    ed.ID = (dt.Rows[i]).ItemArray[0].ToString().Trim();
                    ed.VPlate = (dt.Rows[i]).ItemArray[1].ToString().Trim();
                    ed.VTime = (dt.Rows[i]).ItemArray[0].ToString().Trim();
                    plateOList.Add(ed);
                }
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < plateOList.Count; i++)
                {
                    if (!string.IsNullOrWhiteSpace(plateOList[i].VPlate))
                    {
                        sb.AppendFormat(" SELECT ID FROM dbo.Vehicle WHERE Plate LIKE '%{0}%' union", plateOList[i].VPlate);
                        sb.AppendLine();
                        if (i == plateOList.Count - 1)
                        {
                            sb.AppendFormat(" SELECT ID FROM dbo.Vehicle WHERE Plate LIKE '%{0}%' ", plateOList[i]);
                            sb.AppendLine();

                        }
                    }
                }

                using (zyt_basegpsEntities db = new zyt_basegpsEntities())
                {
                    needSetVid = db.Database.SqlQuery<int>(sb.ToString()).ToList();
                }
            }
        }


        public class ExcelHelper
        {
            /// <summary>
            /// 类版本
            /// </summary>
            public string version
            {
                get { return "0.1"; }
            }
            readonly int EXCEL03_MaxRow = 65535;

            /// <summary>
            /// 将DataTable转换为excel2003格式。
            /// </summary>
            /// <param name="dt"></param>
            /// <returns></returns>
            public byte[] DataTable2Excel(DataTable dt, string sheetName)
            {

                IWorkbook book = new HSSFWorkbook();
                if (dt.Rows.Count < EXCEL03_MaxRow)
                    DataWrite2Sheet(dt, 0, dt.Rows.Count - 1, book, sheetName);
                else
                {
                    int page = dt.Rows.Count / EXCEL03_MaxRow;
                    for (int i = 0; i < page; i++)
                    {
                        int start = i * EXCEL03_MaxRow;
                        int end = (i * EXCEL03_MaxRow) + EXCEL03_MaxRow - 1;
                        DataWrite2Sheet(dt, start, end, book, sheetName + i.ToString());
                    }
                    int lastPageItemCount = dt.Rows.Count % EXCEL03_MaxRow;
                    DataWrite2Sheet(dt, dt.Rows.Count - lastPageItemCount, lastPageItemCount, book, sheetName + page.ToString());
                }
                MemoryStream ms = new MemoryStream();
                book.Write(ms);
                return ms.ToArray();
            }
            private void DataWrite2Sheet(DataTable dt, int startRow, int endRow, IWorkbook book, string sheetName)
            {
                ISheet sheet = book.CreateSheet(sheetName);
                IRow header = sheet.CreateRow(0);

                //设置表头样式
                ICellStyle style = book.CreateCellStyle();
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                IFont font = book.CreateFont();
                font.Boldweight = (short)FontBoldWeight.Bold;
                style.SetFont(font);

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ICell cell = header.CreateCell(i);
                    string val = dt.Columns[i].Caption ?? dt.Columns[i].ColumnName;
                    cell.SetCellValue(val);
                    cell.CellStyle = style;

                    sheet.SetColumnWidth(i, 20 * 256);
                }
                int rowIndex = 1;
                for (int i = startRow; i <= endRow; i++)
                {
                    DataRow dtRow = dt.Rows[i];
                    IRow excelRow = sheet.CreateRow(rowIndex++);
                    for (int j = 0; j < dtRow.ItemArray.Length; j++)
                    {
                        excelRow.CreateCell(j).SetCellValue(dtRow[j].ToString());
                        //sheet.SetColumnWidth(j, System.Text.Encoding.Default.GetBytes(dtRow[j].ToString()).Length * 2 * 256);
                    }
                }

            }


            /// <summary>
            /// 导出Excel
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="sheetName"></param>
            /// <param name="excelHead"></param>
            /// <param name="NeedToElement"></param>
            /// <param name="InDataList"></param>
            /// <param name="excelName"></param>
            /// <returns></returns>
            public static string DataToExcel<T>(string sheetName, List<string> excelHead, string[] NeedToElement, List<T> InDataList, string excelName)
            {
                if (InDataList.Count == 0)
                {
                    return "没有可导数据";
                }
                sheetName = sheetName ?? "导出数据";
                excelName = excelName ?? DateTime.Now.ToString();
                //新建Execl画布
                HSSFWorkbook workbook = new HSSFWorkbook();
                //新建sheet选项卡
                ISheet sheet = workbook.CreateSheet(sheetName);
                //新建行(表头)
                IRow rowHeader = sheet.CreateRow(0);
                Type t = InDataList[0].GetType();
                PropertyInfo[] piArray = t.GetProperties();
                //获取并设置表头
                if (excelHead == null)
                {
                    for (int i = 0; i < piArray.Length; i++)
                    {
                        //报表的头部
                        rowHeader.CreateCell(i).SetCellValue(piArray[i].Name);
                    }
                }
                else
                {
                    for (int i = 0; i < excelHead.Count; i++)
                    {
                        //报表的头部
                        rowHeader.CreateCell(i).SetCellValue(excelHead[i]);
                    }
                }
                int rowCursor = 1;
                foreach (var item in InDataList)
                {
                    Type tChildren = item.GetType();
                    PropertyInfo[] piChildren = tChildren.GetProperties();
                    var row = sheet.CreateRow(rowCursor);

                    for (int i = 0; i < piChildren.Length; i++)
                    {
                        #region bool对应转换 
                        //if (piChildren[i].PropertyType == typeof(bool))
                        //{
                        //    if ((bool)piChildren[i].GetValue(item))
                        //    {
                        //        row.CreateCell(i).SetCellValue("");
                        //    }
                        //    else
                        //    {
                        //        row.CreateCell(i).SetCellValue("");
                        //    }
                        //}
                        //else
                        //{
                        //    
                        //} 
                        #endregion
                        if (NeedToElement.Contains(piChildren[i].Name))
                        {
                            int cellNum = NeedToElement.ToList().IndexOf(piChildren[i].Name);
                            row.CreateCell(cellNum).SetCellValue(piChildren[i].GetValue(item) == null ? "" : piChildren[i].GetValue(item).ToString());
                        }
                    }
                    sheet.AutoSizeColumn(rowCursor);
                    rowCursor++;
                }
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "temp") == false)
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "temp");
                }
                var fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "/temp/" + excelName + ".xls", FileMode.Create);
                workbook.Write(fs);
                fs.Dispose();
                fs.Close();
                string filePath = "/temp/" + excelName + ".xls";
                return filePath;
            }
        }
    }
}
