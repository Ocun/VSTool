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
    public class SqlTools {
        private static string _connectionString = "";
        private static SqlConnection _connection ;
        private static StringBuilder _builder = new StringBuilder();
        private static string _address =string.Empty;

        public static SqlConnection Connection {
            get {
                _connection = GetConnectionStr();
                return _connection;
            }
            set => _connection = value;
        }

        // ReSharper disable once ConvertToAutoProperty
        public static StringBuilder Builder { get => _builder; set => _builder = value; }

        public static SqlConnection GetConnectionStr()
        {
            _address = CallUpdate.GetLocation();
            _connectionString = _address.Substring(0, 2) == "TW" ? 
                "Data Source=10.20.86.68;Initial Catalog=WKE10;Persist Security Info=True;User ID=workday;Password=workday;" 
                : "Data Source=172.16.1.28;Initial Catalog=NJE10;Persist Security Info=True;User ID=sa;Password=518518;";
            return new SqlConnection(_connectionString);
        }

        #region 防網路順斷，暫停三秒後繼續作業，並重試三次

        public static void InsertToolInfo(string pDemandId, string pUseYear, string pTheMemo)
        {
            try
            {
                Connection.Open();
                Builder.Length = 0;
                Builder.AppendFormat(
                    "INSERT INTO WF_TOOLINFO (ToolName,UseDate, UseTime ,PCName,IsFailed,UsedCount,TheMemo,DemandID,UseYear) VALUES ('{0}','{1}',{2},'{3}','{4}',{5},'{6}','{7}',{8})",
                    new object[] {
                        "VSTool", DateTime.Now.ToString("yyyyMMddHHmmss"),
                        DateTime.Now.ToString("yyyyMMddHHmmss")
                            .Substring(8, DateTime.Now.ToString("yyyyMMddHHmmss").Length - 8),
                        Environment.MachineName,
                        "N", 1, pTheMemo, pDemandId, pUseYear
                    });
                new SqlCommand(Builder.ToString(), Connection).ExecuteNonQuery();
                Connection.Close();
            }
            catch
            {
                Connection.Close();
            }
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="typekey"></param>
        /// <param name="fileInfos"></param>
        public static void InsertToolInfo(string typekey,IEnumerable<FileInfos> fileInfos)
        {
            try
            {
                using (var connection = Connection) {
                    connection.Open();
                    using (var bulkCopy = new SqlBulkCopy(connection)) {
                        bulkCopy.DestinationTableName = "WF_TOOLINFO";

                        var dt = new DataTable();
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
                            
                            var nowDate = DateTime.Now;
                            var dr = dt.NewRow();
                            dr["ToolName"] = "VSTool";
                            dr["UseDate"] = nowDate.AddMilliseconds(count++).ToString("yyyyMMddHHmmssfff");
                            dr["UseTime"] = nowDate.ToString("yyyyMMddHHmmss")
                                .Substring(8, DateTime.Now.ToString("yyyyMMddHHmmss").Length - 8);
                            dr["PCName"] = Environment.MachineName;
                            dr["IsFailed"] = "N";
                            dr["UsedCount"] = 1;
                            var year = nowDate.ToString("yyyyMMdd");
                            var demandId = $"S01231_{year}_01";
                            dr["TheMemo"] = typekey + "_" + fileInfo.FileName;
                            dr["DemandID"] = demandId;
                            dr["UseYear"] = year;
                            dt.Rows.Add(dr);
                        });

                        for (var i = 0; i < dt.Columns.Count; i++) {
                            bulkCopy.ColumnMappings.Add(dt.Columns[i].ColumnName, dt.Columns[i].ColumnName);
                        }
                        if (dt.Rows.Count == 0)
                            return;
                        bulkCopy.BatchSize = dt.Rows.Count;
                        bulkCopy.WriteToServer(dt);
                    }
                }
            }
            catch (Exception)
            {
                Connection.Close();
            }
        }

        #endregion
    }
}