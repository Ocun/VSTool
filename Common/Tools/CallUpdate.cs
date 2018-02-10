using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Net;
using System.IO;

namespace Common.Implement {
    public class CallUpdate {
        //Kill執行緒 
        public static void KillTask(string ExeFileName) {
            foreach (Process p in Process.GetProcessesByName(ExeFileName)) {
                p.Kill();
            }
        }

        //取得電腦名稱
        public static string GetPCHostName() {
            return Environment.MachineName;
        }

        //取得電腦IP
        public static string GetIPAddress() {
            var SvrIP = new IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);
            return SvrIP.ToString();
        }

        //取得電腦所在地
        public static string GetLocation() {
            string IP = GetIPAddress();

            if (IP.IndexOf("10.20.87") != -1) //台北 VM
            {
                return "TWVM";
            }
            else if (IP.IndexOf("10.20") != -1) //台北
            {
                return "TW";
            }
            else if (IP.IndexOf("10.40") != -1) //台中
            {
                return "TH";
            }
            else if (IP.IndexOf("192.168.168") != -1) //南京VM
            {
                return "NJVM";
            }
            else if (IP.IndexOf("192.168") != -1) //南京
            {
                return "NJ";
            }
            else {
                return "NJ";
            }
        }

        //取得執行檔執行資料夾路徑
        public static string GetExeFolder(string FullFilePath) {
            string ExeFolder = FullFilePath.Substring(0, FullFilePath.LastIndexOf("\\"));
            return ExeFolder;
        }

        //取得執行檔名稱
        public static string GetExeName(string FullFilePath) {
            string ExeFolder = FullFilePath.Substring(0, FullFilePath.LastIndexOf("\\"));
            FullFilePath = FullFilePath.Substring(FullFilePath.LastIndexOf("\\") + 1,
                FullFilePath.Length - ExeFolder.Length - 1);
            return FullFilePath;
        }

        //取得本地workday連線字串
        public static string GetWorkDayConnectionString(string GetLocation) {
            if (GetLocation == "TW"
                || GetLocation == "TWVM"
                || GetLocation == "TH") {
                return
                    "Provider=SQLOLEDB.1;Password=workday;Persist Security Info=True;User ID=workday;Initial Catalog=WKWF;Data Source=10.20.86.68;Application Name=NEWDB";
            }
            else {
                return
                    "Provider=SQLOLEDB.1;Password=518518;Persist Security Info=True;User ID=sa;Initial Catalog=NJWF;Data Source=172.16.1.28;Application Name=NEWDB";
            }
        }

        //取得Workday Insert指令
        public static string GetWorkDayInsertString(bool IsWFTool, bool ToolName, string UseDate, string UseTime,
            string PCName, string IsFailed, int UsedCount, string TheMemo, string DemandId, string UseYear) {
            string WorkDayTableName;
            StringBuilder InsertString = new StringBuilder();
            if (IsWFTool == true) {
                WorkDayTableName = "WF_TOOLINFO";
            }
            else {
                WorkDayTableName = "YF_TOOLINFO";
            }
            InsertString.AppendFormat(
                "Insert into " + WorkDayTableName
                + " (ToolName,UseDate,UseTime,PCName,IsFailed,UsedCount,TheMemo,DemandID,UseYear ) Values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                IsWFTool, ToolName, UseDate, UseTime, PCName, IsFailed, UsedCount, TheMemo, DemandId, UseYear);
            return InsertString.ToString();
        }

        //取得主機上執行檔路徑
        public static string GetServerExePath(string ToolName) {
            if (GetLocation().Trim().IndexOf("NJ") != -1) {
                if (ToolName.ToUpper().Trim() == "VSTool") {
                    return @"\\192.168.168.15\E10_Tools\" + ToolName + @"\" + ToolName + ".exe";
                }
                else {
                    return @"\\192.168.168.15\E10_Tools\E10_Switch\" + ToolName + @"\" + ToolName + ".exe";
                }
            }
            else {
                if (ToolName.ToUpper().Trim() == "E10_NEWDB") {
                    //20131009 modi xethel 自動更新改在10.40.99.158
                    return @"\\10.40.99.158\E10_Tools\E10_Switch\" + ToolName + @"\" + ToolName + ".exe";
                }
                else {
                    return @"\\10.40.99.158\E10_Tools\E10_Switch\" + ToolName + @"\" + ToolName + ".exe";
                }
            }
        }

        public static bool CompareFileLastWritedate(string FullFilePath, string ServerFilePath) {
            FileInfo FullFile = new FileInfo(FullFilePath);
            FileInfo ServerFile = new FileInfo(ServerFilePath);

            return (FullFile.LastWriteTime != ServerFile.LastWriteTime || File.Exists(FullFilePath) == false);
        }

        public static bool CallExe(int lpHandle, string lpProgram, string lpParameter) {
            var p = new Process();
            p.StartInfo.FileName = lpProgram;
            p.StartInfo.Arguments = lpParameter;

            p.Start();
            if (lpHandle == 1) {
                p.WaitForExit();
            }
            //System.Diagnostics.Process.Start(lpProgram, lpParameter);
            return true;
        }

        //取得機碼執行檔路徑
    }
}