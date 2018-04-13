// create By 08628 20180411
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Common.Implement.Entity;

namespace Common.Implement.Tools
{
    public class sqlTools {
        private static string connectionString = "";
        private static SqlConnection connection ;
        private static StringBuilder builder = new StringBuilder();
        private static string address =string.Empty;

        public static SqlConnection Connection {
            get {
                connection = getConnectionStr();
                return connection;
            }
            set { connection = value; }
        }

        public static SqlConnection getConnectionStr()
        {
            address = CallUpdate.GetLocation();
            if (address.Substring(0, 2) == "TW") {
                connectionString =
                    "Data Source=10.20.86.68;Initial Catalog=WKE10;Persist Security Info=True;User ID=workday;Password=workday;";
            }
            else {
                connectionString =
                    "Data Source=172.16.1.28;Initial Catalog=NJE10;Persist Security Info=True;User ID=sa;Password=518518;";
            }
            return new SqlConnection(connectionString);
        }

        #region 防網路順斷，暫停三秒後繼續作業，並重試三次

        public static void insertToolInfo(string pDemandID, string pUseYear, string pTheMemo)
        {
            try
            {
                Connection.Open();
                builder.Length = 0;
                builder.AppendFormat(
                    "INSERT INTO WF_TOOLINFO (ToolName,UseDate, UseTime ,PCName,IsFailed,UsedCount,TheMemo,DemandID,UseYear) VALUES ('{0}','{1}',{2},'{3}','{4}',{5},'{6}','{7}',{8})",
                    new object[] {
                        "VSTool", DateTime.Now.ToString("yyyyMMddHHmmss"),
                        DateTime.Now.ToString("yyyyMMddHHmmss")
                            .Substring(8, DateTime.Now.ToString("yyyyMMddHHmmss").Length - 8),
                        Environment.MachineName,
                        "N", 1, pTheMemo, pDemandID, pUseYear
                    });
                new SqlCommand(builder.ToString(), Connection).ExecuteNonQuery();
                Connection.Close();
            }
            catch
            {
                Connection.Close();
            }
        }
        public static void insertToolInfo(string UseDate, string pDemandID, string pUseYear, string pTheMemo)
        {
            try
            {
                using (SqlConnection connection = Connection) {
                    connection.Open();
                    builder.Length = 0;
                    builder.AppendFormat(
                        "INSERT INTO WF_TOOLINFO (ToolName,UseDate, UseTime ,PCName,IsFailed,UsedCount,TheMemo,DemandID,UseYear) VALUES ('{0}','{1}',{2},'{3}','{4}',{5},'{6}','{7}',{8})",
                        new object[] {
                            "VSTool", UseDate,
                            DateTime.Now.ToString("yyyyMMddHHmmss")
                                .Substring(8, DateTime.Now.ToString("yyyyMMddHHmmss").Length - 8),
                            Environment.MachineName,
                            "N", 1, pTheMemo, pDemandID, pUseYear
                        });
                    new SqlCommand(builder.ToString(), connection).ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch(Exception ex)
            {
               // Connection.Close();
            }
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="typekey"></param>
        /// <param name="fileInfos"></param>
        public static void insertToolInfo(string typekey,IEnumerable<FileInfos> fileInfos)
        {
            try
            {
                using (SqlConnection connection = Connection) {
                    connection.Open();
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection)) {


                        bulkCopy.DestinationTableName = "WF_TOOLINFO";

                        DataTable dt = new DataTable();
                        dt.Columns.Add("ToolName", typeof(object));
                        dt.Columns.Add("UseDate", typeof(object));
                        dt.Columns.Add("UseTime", typeof(object));
                        dt.Columns.Add("PCName", typeof(object));
                        dt.Columns.Add("IsFailed", typeof(object));
                        dt.Columns.Add("UsedCount", typeof(object));
                        dt.Columns.Add("TheMemo", typeof(object));
                        dt.Columns.Add("DemandID", typeof(object));
                        dt.Columns.Add("UseYear", typeof(object));
                        int count = 1;
                        fileInfos.ToList().ForEach(fileInfo => {
                            
                            DateTime nowDate = DateTime.Now;
                            DataRow dr = dt.NewRow();
                            dr["ToolName"] = "VSTool";
                            dr["UseDate"] = nowDate.AddMilliseconds(count++).ToString("yyyyMMddHHmmssfff");
                            dr["UseTime"] = nowDate.ToString("yyyyMMddHHmmss")
                                .Substring(8, DateTime.Now.ToString("yyyyMMddHHmmss").Length - 8);
                            dr["PCName"] = Environment.MachineName;
                            dr["IsFailed"] = "N";
                            dr["UsedCount"] = 1;
                            string year = nowDate.ToString("yyyyMMdd");
                            string demandId = string.Format("S01231_{0}_01", year);
                            dr["TheMemo"] = typekey + "_" + fileInfo.FileName;
                            dr["DemandID"] = demandId;
                            dr["UseYear"] = year;
                            dt.Rows.Add(dr);
                        });

                        for (int i = 0; i < dt.Columns.Count; i++) {
                            bulkCopy.ColumnMappings.Add(dt.Columns[i].ColumnName, dt.Columns[i].ColumnName);
                        }
                        if (dt != null
                            && dt.Rows.Count != 0) {
                            bulkCopy.BatchSize = dt.Rows.Count;
                            bulkCopy.WriteToServer(dt);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Connection.Close();
            }
        }

        #endregion
    }
}