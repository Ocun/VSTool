using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Common.Implement.Entity;
using Common.Implement.Properties;
using Common.Implement.Tools;
using Timer = System.Timers.Timer;

namespace Common.Implement.UI {
    /// <summary>
    ///     ToolBar控件
    /// </summary>
    public partial class ToolBarManger : UserControl {
        private static Timer _aTimer = new Timer(2000);

        /// <summary>
        ///     构造
        /// </summary>
        public ToolBarManger() {
            InitializeComponent();
            InitCombox();
        }

        /// <summary>
        ///     主窗体参数
        /// </summary>
        public Toolpars Toolpar { get; set; }

        private Hashtable CheckServerTable { get; set; }

        private void ButtonManger_Load(object sender, EventArgs e) {
        }

        /// <summary>
        ///     打开服务器
        /// </summary>
        private void ServerOn() {
            var args = string.Empty;

            if (comboBox1.SelectedValue.Equals(1)) {
                args += " /d";
            }
            else {
                args += " /r:false";
                args += " /l:false";
            }
            MyTool.ServerOn(Toolpar, args);
        }

        /// <summary>
        ///     打开服务端/客户端
        /// </summary>
        private void AutoOnServerAndClient() {
            ServerOn();

            try {
                var isServerOn = MyTool.CheckProcessOn("Digiwin.Mars.ServerStart");
                if (!isServerOn)
                    return;
                var isOn = MyTool.CheckProcessOn("Digiwin.Mars.ClientStart");
                if (isOn) {
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
                var index = dirs.Length - 1;
                index = index > 0 ? index : 0;
                if (index < 0)
                    return;
                var tSerFolLogName = dirs[index];
                var tSerFolLogPath = tSerFolPath + @"\LOG\SWITCH";

                if (!Directory.Exists(tSerFolLogPath))
                    Directory.CreateDirectory(tSerFolLogPath);
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
            catch (Exception) {
                // ignored
            }
        }

        /// <summary>
        ///     开启服务端按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartBtn_Click(object sender, EventArgs e) {
            AutoOnServerAndClient();
        }

        /// <summary>
        ///     初始化调试选择项
        /// </summary>
        private void InitCombox() {
            var infoList = new List<ComboxInfo> {
                new ComboxInfo {Id = 1, Name = "调试"},
                new ComboxInfo {Id = 2, Name = "全启动"}
            };
            comboBox1.DataSource = infoList;
            comboBox1.ValueMember = "Id";
            comboBox1.DisplayMember = "Name";
        }

        /// <summary>
        ///     开始客户端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartClientBtn_Click(object sender, EventArgs e) {
            _aTimer.Enabled = false;
            var args = string.Empty;
            if (comboBox1.SelectedValue.Equals(1))
                args += " /d";
            MyTool.ClientOn(Toolpar, args);
        }

        //^_^20160511 add by nicknt9095 for 自動偵測SERVER啟動CLIENT<S00349_20160505_02>--begin
        /// <summary>
        ///     偵測SERVER是否啟動完畢
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnAutoOpenClientTimedEvent(object source, ElapsedEventArgs e) {
            var tIsServerOpen = MyTool.CheckProcessOn("Digiwin.Mars.ServerStart");

            if (!tIsServerOpen) {
                _aTimer.Enabled = false;
                return;
            }
            var tSerFolPath = CheckServerTable["tSerFolPath"].ToString();
            var tSerFolLogName = CheckServerTable["tSerFolLogName"].ToString();
            var tSerFolLogPath = CheckServerTable["tSerFolLogPath"].ToString();
            var tSerFolLogName1 = CheckServerTable["tSerFolLogName1"].ToString();
            if (File.Exists(tSerFolLogPath + @"\server.log"))
                File.Delete(tSerFolLogPath + @"\server.log");
            File.Copy(Path.Combine(tSerFolPath, tSerFolLogName), Path.Combine(tSerFolLogPath, tSerFolLogName1));
            using (var tLogFile = new StreamReader(tSerFolLogPath + @"\server.log")) {
                string line;
                while ((line = tLogFile.ReadLine()) != null) {
                    if (line.IndexOf("服務端啟動完畢，輸入q結束", StringComparison.Ordinal) == -1
                        && line.IndexOf("服务端启动完毕，输入q结束", StringComparison.Ordinal) == -1)
                        continue;
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

        private void Stop_Click(object sender, EventArgs e) {
            _aTimer.Enabled = false; //^_^20160511 add by nicknt9095 for 自動偵測SERVER啟動CLIENT<S00349_20160505_02>
            KillProcess();
        }

        /// <summary>
        ///     消灭进程
        /// </summary>
        private static void KillProcess() {
            var killProcessName = new[]
                {"Digiwin.Mars.ClientStart", "Digiwin.Mars.ServerStart", "Digiwin.Mars.AccountSetStart"};
            MyTool.KillProcess(killProcessName);
        }

        /// <summary>
        ///     重启客户端 服务端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshBtn_Click(object sender, EventArgs e) {
            _aTimer.Enabled = false;
            KillProcess();
            AutoOnServerAndClient();
        }

        /// <summary>
        ///     Copy客户端Dll
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyClientBtn_Click(object sender, EventArgs e) {
            MyTool.CopyUIdll(Toolpar);
        }

        /// <summary>
        ///     Copy客户端服务端Dll
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyBtn_Click(object sender, EventArgs e) {
            try {
                MyTool.CopyDll(Toolpar);
                MessageBox.Show(Resources.CopySucess);
            }
            catch (Exception ex) {
                string[] processNames = {
                    "Digiwin.Mars.ClientStart",
                    "Digiwin.Mars.ServerStart",
                    "Digiwin.Mars.AccountSetStart"
                };
                var f = MyTool.CheckCanCopyDll(processNames);
                if (f)
                    CopyBtn_Click(null, null);
                else
                    MessageBox.Show(ex.Message, Resources.ErrorMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            new Thread(() => SqlTools.InsertToolInfo("S01231_20160503_01", "20160503", "btncopydll_Click")).Start();
        }
    }


    /// <summary>
    ///     邦定调试选项
    /// </summary>
    public class ComboxInfo {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
    }
}