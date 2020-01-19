using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{   
    /// <summary>
    ///
    /// </summary>
    public static class ExeclNopiHelper
    {
        /// <summary>
        /// 将execl流转为DataTable
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static DataTable ReadExceleByStream(Stream stream)
        {
            DataTable dt = new DataTable();
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            using (stream)
            {
               
                HSSFWorkbook workbook = new HSSFWorkbook(stream);
                ISheet sheet = workbook.GetSheetAt(0);
                IRow irow = sheet.GetRow(0);
                //读表头
                int cellCount = irow.LastCellNum;
                for (int i = sheet.FirstRowNum; i < cellCount; i++)
                {
                    DataColumn dc = new DataColumn(irow.GetCell(i).StringCellValue);
                    dt.Columns.Add(dc);
                }
                int rowCount = sheet.LastRowNum;
                for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
                {
                    IRow row = sheet.GetRow(i);
                    DataRow dataRow = dt.NewRow();
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            dataRow[j] = row.GetCell(j).ToString();
                        }
                    }
                    dt.Rows.Add(dataRow);
                }
                workbook = null;//没有有Dispose 和close 
                sheet = null;
                return dt;
            }
    }
    }
}
