using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Common.Implement.Tools {
    public class CallUpdate {
        //Kill執行緒 
        public static void KillTask(string exeFileName) {
            foreach (var p in Process.GetProcessesByName(exeFileName))
                p.Kill();
        }

        //取得電腦名稱
        public static string GetPcHostName() {
            return Environment.MachineName;
        }

        //取得電腦IP
        public static string GetIpAddress() {
            var svrIp = new IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);
            return svrIp.ToString();
        }

        //取得電腦所在地
        public static string GetLocation() {
            var ip = GetIpAddress();

            if (ip.IndexOf("10.20.87", StringComparison.Ordinal) != -1) //台北 VM
                return "TWVM";
            if (ip.IndexOf("10.20", StringComparison.Ordinal) != -1) //台北
                return "TW";
            if (ip.IndexOf("10.40", StringComparison.Ordinal) != -1) //台中
                return "TH";
            if (ip.IndexOf("192.168.168", StringComparison.Ordinal) != -1) //南京VM
                return "NJVM";
            if (ip.IndexOf("192.168", StringComparison.Ordinal) != -1) //南京
                return "NJ";
            return "NJ";
        }

        //取得執行檔執行資料夾路徑
        public static string GetExeFolder(string fullFilePath) {
            var exeFolder = fullFilePath.Substring(0, fullFilePath.LastIndexOf("\\", StringComparison.Ordinal));
            return exeFolder;
        }

        //取得執行檔名稱
        public static string GetExeName(string fullFilePath) {
            var exeFolder = fullFilePath.Substring(0, fullFilePath.LastIndexOf("\\", StringComparison.Ordinal));
            fullFilePath = fullFilePath.Substring(fullFilePath.LastIndexOf("\\", StringComparison.Ordinal) + 1,
                fullFilePath.Length - exeFolder.Length - 1);
            return fullFilePath;
        }

        //取得本地workday連線字串
        public static string GetWorkDayConnectionString(string getLocation) {
            if (getLocation == "TW"
                || getLocation == "TWVM"
                || getLocation == "TH")
                return
                    "Provider=SQLOLEDB.1;Password=workday;Persist Security Info=True;User ID=workday;Initial Catalog=WKWF;Data Source=10.20.86.68;Application Name=NEWDB";
            return
                "Provider=SQLOLEDB.1;Password=518518;Persist Security Info=True;User ID=sa;Initial Catalog=NJWF;Data Source=172.16.1.28;Application Name=NEWDB";
        }

        //取得Workday Insert指令
        public static string GetWorkDayInsertString(bool isWfTool, bool toolName, string useDate, string useTime,
            string pcName, string isFailed, int usedCount, string theMemo, string demandId, string useYear) {
            string workDayTableName;
            var insertString = new StringBuilder();
            workDayTableName = isWfTool ? "WF_TOOLINFO" : "YF_TOOLINFO";
            insertString.AppendFormat(
                "Insert into " + workDayTableName
                + " (ToolName,UseDate,UseTime,PCName,IsFailed,UsedCount,TheMemo,DemandID,UseYear ) Values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                isWfTool, toolName, useDate, useTime, pcName, isFailed, usedCount, theMemo, demandId, useYear);
            return insertString.ToString();
        }

        //取得主機上執行檔路徑
        public static string GetServerExePath(string toolName) {
            if (GetLocation().Trim().IndexOf("NJ", StringComparison.Ordinal) != -1)
                if (toolName.ToUpper().Trim() == "VSTool")
                    return @"\\192.168.168.15\E10_Tools\" + toolName + @"\" + toolName + ".exe";
                else
                    return @"\\192.168.168.15\E10_Tools\E10_Switch\" + toolName + @"\" + toolName + ".exe";
            if (toolName.ToUpper().Trim() == "E10_NEWDB")
                return @"\\10.40.99.158\E10_Tools\E10_Switch\" + toolName + @"\" + toolName + ".exe";
            return @"\\10.40.99.158\E10_Tools\E10_Switch\" + toolName + @"\" + toolName + ".exe";
        }

        public static bool CompareFileLastWritedate(string fullFilePath, string serverFilePath) {
            var fullFile = new FileInfo(fullFilePath);
            var serverFile = new FileInfo(serverFilePath);

            return fullFile.LastWriteTime != serverFile.LastWriteTime || File.Exists(fullFilePath) == false;
        }

        public static bool CallExe(int lpHandle, string lpProgram, string lpParameter) {
            var p = new Process {
                StartInfo = {
                    FileName = lpProgram,
                    Arguments = lpParameter
                }
            };

            p.Start();
            if (lpHandle == 1)
                p.WaitForExit();
            //System.Diagnostics.Process.Start(lpProgram, lpParameter);
            return true;
        }

        //取得機碼執行檔路徑

        public static void AutoUpgrade() {
            if (GetServerExePath("VSTool") != "") {
                if (File.Exists(GetServerExePath("VSTool")))
                    if (CompareFileLastWritedate(Application.ExecutablePath,
                        GetServerExePath("VSTool")))
                        if (File.Exists(GetServerExePath("WFLiveUpdate"))) {
                            if (File.Exists(Environment.CurrentDirectory + @"\WFLiveUpdate.exe"))
                                File.SetAttributes(Environment.CurrentDirectory + @"\WFLiveUpdate.exe",
                                    FileAttributes.Normal);
                            File.Copy(GetServerExePath("WFLiveUpdate"),
                                Environment.CurrentDirectory + @"\WFLiveUpdate.exe", true);
                            if (File.Exists(Environment.CurrentDirectory + @"\WFLiveUpdate.exe")) {
                                //string lpParameters = "";
                                ////取得From路徑
                                //lpParameters = lpParameters + VSTool.CallUpdate.GetServerExePath("VSTool").Replace(" ", "§") + " ";
                                ////取得TO路徑
                                //lpParameters = lpParameters + Application.ExecutablePath.Replace(" ", "§") + " ";
                                //VSTool.CallUpdate.CallExe(0, Environment.CurrentDirectory + @"\WFLiveUpdate.exe", lpParameters);
                                //Environment.Exit(Environment.ExitCode);
                                //Thread.Sleep(5000);
                            }
                        }
            }
            else {
                //
                MessageBox.Show("您的機器尚未執行過登錄檔，請先執行登錄檔才可使用工具!");
                Environment.Exit(Environment.ExitCode);
                //Exit;
            }
        }
    }
}