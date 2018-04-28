using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Digiwin.Chun.Common.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Digiwin.Chun.Common.Controller {
    /// <summary>
    /// 自动升级辅助类
    /// </summary>
    public class CallUpdate {
        
        /// <summary>
        /// Kill執行緒
        /// </summary>
        /// <param name="exeFileName"></param>
        public static void KillTask(string exeFileName) {
            foreach (var p in Process.GetProcessesByName(exeFileName))
                p.Kill();
        }

        /// <summary>
        /// 检查自动更新
        /// </summary>
        /// <param name="oldVer"></param>
        public static bool CheckAndUpdate(string oldVer) {
            var existedUpdate = false;
            try {
                var updatePath = @"\\192.168.168.15\E10_Tools\E10_Switch\VSTOOL\version.json";
                if (File.Exists(updatePath)) {
                    var jsonText = File.ReadAllText(updatePath);
                    //var jo = JObject.Parse(jsonText);
                    var jo = (JArray)JsonConvert.DeserializeObject(jsonText);
                    var version = jo[0]["Version"].ToString();
                    if (string.Compare(oldVer, version, StringComparison.Ordinal) >= 1) {
                        existedUpdate = true;
                    }
                  
                }
              
            }
            catch (Exception ex) {
                LogTools.LogError($@"CheckAndUpdate Error！ Detail:{ex.Message}");
            }
            return existedUpdate;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void MyCallUpdate() {
            var path = $@"{MyTools.Toolpars.MvsToolpath}\AutoUpdateVsTool.exe";
            if (File.Exists(path))
            {
                MyTools.OpenExe(path);
            }
            else
            {
                MessageBox.Show($@"Can't Find AutoUpdate.exe!");
            }
        }
        /// <summary>E
        /// 取得電腦名稱
        /// </summary>
        /// <returns></returns>
        public static string GetPcHostName() {
            return Environment.MachineName;
        }
        
        /// <summary>
        /// 取得電腦IP
        /// </summary>
        /// <returns></returns>
        public static string GetIpAddress() {
            var svrIp = new IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);

            return svrIp.ToString();
        }
        
        /// <summary>
        /// 取得電腦所在地
        /// </summary>
        /// <returns></returns>
        public static string GetLocation() {
            var ip = GetIpAddress();

            if (ip.IndexOf("10.20.87", StringComparison.Ordinal) != -1) //台北 VM
                return "TWVM";
            if (ip.IndexOf("10.20", StringComparison.Ordinal) != -1) //台北
                return "TW";
            if (ip.IndexOf("10.40", StringComparison.Ordinal) != -1) //台中
                return "TH";
            return ip.IndexOf("192.168.168", StringComparison.Ordinal) != -1 ? "NJVM" : "NJ";
        }

        
        /// <summary>
        /// 取得執行檔執行資料夾路徑
        /// </summary>
        /// <param name="fullFilePath"></param>
        /// <returns></returns>
        public static string GetExeFolder(string fullFilePath) {
            var exeFolder = fullFilePath.Substring(0, fullFilePath.LastIndexOf("\\", StringComparison.Ordinal));
            return exeFolder;
        }

        
        /// <summary>
        /// 取得執行檔名稱
        /// </summary>
        /// <param name="fullFilePath"></param>
        /// <returns></returns>
        public static string GetExeName(string fullFilePath) {
            var exeFolder = fullFilePath.Substring(0, fullFilePath.LastIndexOf("\\", StringComparison.Ordinal));
            fullFilePath = fullFilePath.Substring(fullFilePath.LastIndexOf("\\", StringComparison.Ordinal) + 1,
                fullFilePath.Length - exeFolder.Length - 1);
            return fullFilePath;
        }

        
        /// <summary>
        /// 取得本地workday連線字串
        /// </summary>
        /// <param name="getLocation"></param>
        /// <returns></returns>
        public static string GetWorkDayConnectionString(string getLocation) {
            if (getLocation == "TW"
                || getLocation == "TWVM"
                || getLocation == "TH")
                return
                    "Provider=SQLOLEDB.1;Password=workday;Persist Security Info=True;User ID=workday;Initial Catalog=WKWF;Data Source=10.20.86.68;Application Name=NEWDB";
            return
                "Provider=SQLOLEDB.1;Password=518518;Persist Security Info=True;User ID=sa;Initial Catalog=NJWF;Data Source=172.16.1.28;Application Name=NEWDB";
        }

        
        /// <summary>
        /// 取得Workday Insert指令
        /// </summary>
        /// <param name="isWfTool"></param>
        /// <param name="toolName"></param>
        /// <param name="useDate"></param>
        /// <param name="useTime"></param>
        /// <param name="pcName"></param>
        /// <param name="isFailed"></param>
        /// <param name="usedCount"></param>
        /// <param name="theMemo"></param>
        /// <param name="demandId"></param>
        /// <param name="useYear"></param>
        /// <returns></returns>
        public static string GetWorkDayInsertString(bool isWfTool, bool toolName, string useDate, string useTime,
            string pcName, string isFailed, int usedCount, string theMemo, string demandId, string useYear) {
            var insertString = new StringBuilder();
            var workDayTableName = isWfTool ? "WF_TOOLINFO" : "YF_TOOLINFO";
            insertString.AppendFormat(
                "Insert into " + workDayTableName
                + " (ToolName,UseDate,UseTime,PCName,IsFailed,UsedCount,TheMemo,DemandID,UseYear ) Values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                isWfTool, toolName, useDate, useTime, pcName, isFailed, usedCount, theMemo, demandId, useYear);
            return insertString.ToString();
        }

        
        /// <summary>
        /// 取得主機上執行檔路徑
        /// </summary>
        /// <param name="toolName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullFilePath"></param>
        /// <param name="serverFilePath"></param>
        /// <returns></returns>
        public static bool CompareFileLastWritedate(string fullFilePath, string serverFilePath) {
            var fullFile = new FileInfo(fullFilePath);
            var serverFile = new FileInfo(serverFilePath);

            return fullFile.LastWriteTime != serverFile.LastWriteTime || File.Exists(fullFilePath) == false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lpHandle"></param>
        /// <param name="lpProgram"></param>
        /// <param name="lpParameter"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// 取得機碼執行檔路徑
        /// </summary>
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