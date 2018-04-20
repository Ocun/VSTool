using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Common.Implement.Entity;
using Common.Implement.Properties;
using Common.Implement.Tools;
using Timer = System.Timers.Timer;

namespace Common.Implement.UI {
    public partial class ButtonManger : UserControl {
        private static Timer _aTimer = new Timer(2000);

        public ButtonManger() {
            InitializeComponent();
            InitCombox();
        }

        public Toolpars Toolpar { get; set; }

        private Hashtable CheckServerTable { get; set; }

        private void ButtonManger_Load(object sender, EventArgs e) {
        }

        private void ServerOn() {
            var isOn = CheckProcessOn("Digiwin.Mars.ServerStart");
            if (isOn) {
                MessageBox.Show(Resources.ServerRunning);
                return;
            }
            var tServerPath = Toolpar.Mplatform + "\\Server\\Control\\Digiwin.Mars.ServerStart.exe";
            ExecuteCmd("IISRESET", "");
            var args = string.Empty;
            if (!File.Exists(tServerPath)) {
                MessageBox.Show(string.Format(Resources.NotFindFile ,tServerPath));
                return;
            }
            if (comboBox1.SelectedValue.Equals(1)) {
                args += " /d";
            }
            else {
                args += " /r:false";
                args += " /l:false";
            }

            Process.Start(tServerPath, args);
        }

        private void AutoOnServerAndClient() {
            ServerOn();

            try
            {
                var isServerOn = CheckProcessOn("Digiwin.Mars.ServerStart");
                if (!isServerOn) {
                    return;}
                var isOn = CheckProcessOn("Digiwin.Mars.ClientStart");
                if (isOn)
                {
                    MessageBox.Show(Resources.ClientRunning);
                    return;
                }
                var tSerFolPath =
                    Toolpar.Mplatform
                    + @"\Server\Control"; //^_^20160511 add by nicknt9095 for 啟動前關閉所有SERVER、CLIENT<S00349_20160505_02>
                var myDate = DateTime.Now;
                var tToDayDate = myDate.ToString("yyyyMMdd");
                Thread.Sleep(3000);
                var dirs = Directory.GetFiles(tSerFolPath, tToDayDate + "*.log", SearchOption.AllDirectories);
                var tSerFolLogName = dirs[dirs.Count() - 1];
                var tSerFolLogPath = tSerFolPath + @"\LOG\SWITCH";

                if (!Directory.Exists(tSerFolLogPath)) Directory.CreateDirectory(tSerFolLogPath);
                CheckServerTable = new Hashtable {
                    {"tSerFolPath", tSerFolPath},
                    {"tSerFolLogName", tSerFolLogName},
                    {"tSerFolLogPath", tSerFolLogPath},
                    {"tSerFolLogName1", "server.log"}
                };


                _aTimer = new Timer(2000);
                _aTimer.Elapsed += OnAutoOpenClientTimedEvent;
                _aTimer.Enabled = true;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void StartBtn_Click(object sender, EventArgs e) {
            AutoOnServerAndClient();
        }

        private void InitCombox() {
            var infoList = new List<ComboxInfo> {
                new ComboxInfo {Id = 1, Name = "调试"},
                new ComboxInfo {Id = 2, Name = "全启动"}
            };
            comboBox1.DataSource = infoList;
            comboBox1.ValueMember = "Id";
            comboBox1.DisplayMember = "Name";
        }

        private void StartClientBtn_Click(object sender, EventArgs e) {
            if (!CheckProcessOn("Digiwin.Mars.ServerStart")) ServerOn();
            
            if (CheckProcessOn("Digiwin.Mars.ClientStart")) {
                MessageBox.Show(Resources.ClientRunning);
                return;
            }
            _aTimer.Enabled = false;
            var tClientPath = Toolpar.Mplatform + "\\DeployServer\\Shared\\Digiwin.Mars.ClientStart.exe";
            var args = string.Empty;
            if (!File.Exists(tClientPath)) {
                MessageBox.Show(string.Format(Resources.NotFindFile ,tClientPath));
                return;
            }
            if (comboBox1.SelectedValue.Equals(1))
                args += " /d";
            Process.Start(tClientPath, args);
        }

        private static bool CheckProcessOn(string processName) {
            var tIsOpen = false;
            foreach (var p in Process.GetProcesses())
                if (p.ProcessName.Contains(processName))
                    tIsOpen = true;
            return tIsOpen;
        }

        //^_^20160511 add by nicknt9095 for 自動偵測SERVER啟動CLIENT<S00349_20160505_02>--begin
        /// <summary>
        ///     偵測SERVER是否啟動完畢
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnAutoOpenClientTimedEvent(object source, ElapsedEventArgs e) {
            var tIsServerOpen = CheckProcessOn("Digiwin.Mars.ServerStart");

            if (!tIsServerOpen) {
                _aTimer.Enabled = false;
                return;
            }
            var tSerFolPath = CheckServerTable["tSerFolPath"].ToString();
            var tSerFolLogName = CheckServerTable["tSerFolLogName"].ToString();
            var tSerFolLogPath = CheckServerTable["tSerFolLogPath"].ToString();
            var tSerFolLogName1 = CheckServerTable["tSerFolLogName1"].ToString();
            string line;
            if (File.Exists(tSerFolLogPath + @"\server.log"))
                File.Delete(tSerFolLogPath + @"\server.log");
            File.Copy(Path.Combine(tSerFolPath, tSerFolLogName), Path.Combine(tSerFolLogPath, tSerFolLogName1));
            using (var tLogFile = new StreamReader(tSerFolLogPath + @"\server.log")) {
                while ((line = tLogFile.ReadLine()) != null) {
                    if (line.IndexOf("服務端啟動完畢，輸入q結束", StringComparison.Ordinal) == -1
                        && line.IndexOf("服务端启动完毕，输入q结束", StringComparison.Ordinal) == -1) continue;
                    var tClientPath = Toolpar.Mplatform + "\\DeployServer\\Shared\\Digiwin.Mars.ClientStart.exe";
                    var args = string.Empty;
                    if (!File.Exists(tClientPath)) {
                        _aTimer.Enabled = false;
                        MessageBox.Show(string.Format(Resources.NotFindFile, tClientPath));
                        return;
                    }
                    if (comboBox1.SelectedValue.Equals(1))
                        args += " /d";

                    Process.Start(tClientPath, args);
                    _aTimer.Enabled = false;
                }
            }
        }

        #region executeCmd

        private static void ExecuteCmd(string pFileName, string pArguments) {
            var process = new Process {
                StartInfo = {
                    FileName = pFileName,
                    Arguments = pArguments,
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                }
            };
            process.Start();
        }

        #endregion

        private void Stop_Click(object sender, EventArgs e) {
            _aTimer.Enabled = false; //^_^20160511 add by nicknt9095 for 自動偵測SERVER啟動CLIENT<S00349_20160505_02>
            KillProcess();

        }
        private void KillProcess() {
            _aTimer.Enabled = false; 
            foreach (var p in Process.GetProcesses()) {
                if (!p.ProcessName.Contains("Digiwin.Mars.")
                    || p.ProcessName.Contains(
                        "Digiwin.Mars.License")) continue;
                if (p.ProcessName.Contains("Digiwin.Mars.ClientStart")
                    || p.ProcessName.Contains("Digiwin.Mars.ServerStart")
                    || p.ProcessName.Contains("Digiwin.Mars.AccountSetStart")
                ) //^_^20151223 add by xethel for /r啟動是Digiwin.Mars.AccountSetStart
                    p.Kill();
            }

        }

        private void RefreshBtn_Click(object sender, EventArgs e) {
            _aTimer.Enabled = false;
            KillProcess();
            AutoOnServerAndClient();
        }

        private void CopyClientBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var export = Toolpar.PathEntity.ExportPath;
                var toPath = $"{Toolpar.Mplatform}\\DeployServer\\Shared\\Customization\\Programs\\";
                var filterStr = $"*{Toolpar.FormEntity.txtNewTypeKey}.UI.*";
                if (Toolpar.MIndustry)
                {
                    toPath = $"{Toolpar.Mplatform}\\DeployServer\\Shared\\Industry\\Programs\\";
                }
                MyTool.FileCopyUIdll(export, toPath, filterStr);
            }
            catch (Exception ex)
            {
                string[] processNames = {
                    "Digiwin.Mars.ClientStart"
                };
                var f = MyTool.CheckCanCopyDll(processNames);
                if (f)
                {
                    CopyClientBtn_Click(null, null);
                }
                else
                {
                    MessageBox.Show(ex.Message, Resources.ErrorMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            new Thread(() => SqlTools.InsertToolInfo("S01231_20160503_01", "20160503", "btncopyUIdll_Click")).Start();

        }

        private void CopyBtn_Click(object sender, EventArgs e)
        {
            try
            {
                MyTool.CopyDll(Toolpar);
                MessageBox.Show(Resources.CopySucess);
            }
            catch (Exception ex)
            {
                string[] processNames = {
                    "Digiwin.Mars.ClientStart",
                    "Digiwin.Mars.ServerStart",
                    "Digiwin.Mars.AccountSetStart"
                };
                var f = MyTool.CheckCanCopyDll(processNames);
                if (f)
                {
                    CopyBtn_Click(null, null);
                }
                else
                {
                    MessageBox.Show(ex.Message, Resources.ErrorMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            new Thread(() => SqlTools.InsertToolInfo("S01231_20160503_01", "20160503", "btncopydll_Click")).Start();

        }

    }


    public class ComboxInfo {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}