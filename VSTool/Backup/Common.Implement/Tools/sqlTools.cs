using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Common.Implement {
    public class sqlTools {
        private static string connectionString = "";
        private static SqlConnection connection ;
        private static StringBuilder builder = new StringBuilder();
        private static string address =string.Empty;

        public static SqlConnection Connection {
            get { 
                if (connection == null) {
                    connection = getConnectionStr();
                }
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

        #endregion
    }
}