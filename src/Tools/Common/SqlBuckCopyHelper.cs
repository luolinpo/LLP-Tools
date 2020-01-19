using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class SqlBuckCopyHelper
    {  
        /// <summary>
        /// 批量写入数据库
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="TableName">表名</param>
        /// <param name="dt">需要插入的Table</param>
        public static void SqlBulkCopyByDatatable(string connectionString, string TableName, DataTable dt)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.UseInternalTransaction))
                {
                    try
                    {
                        sqlbulkcopy.DestinationTableName = TableName;
                        int temp = dt.Columns.Count;
                        for (int i = 0; i < temp; i++)
                        {
                            sqlbulkcopy.ColumnMappings.Add(dt.Columns[i].ColumnName, dt.Columns[i].ColumnName);
                        }
                        sqlbulkcopy.WriteToServer(dt);
                    }
                    catch (System.Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
    }
}
