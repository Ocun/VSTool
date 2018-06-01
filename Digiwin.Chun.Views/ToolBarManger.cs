using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Digiwin.Chun.Common.Tools;
using Digiwin.Chun.Models;
using Digiwin.Chun.Views.Properties;
using Digiwin.Chun.Views.Tools;
using Timer = System.Timers.Timer;

namespace Digiwin.Chun.Views {
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
        /// 主窗体参数
        /// </summary>
        private Toolpars Toolpars { get; set; }
        private Hashtable CheckServerTable { get; set; }

        private void ButtonManger_Load(object sender, EventArgs e) {
            Toolpars = MyTools.Toolpars;
        }

        /// <summary>
        ///     打开服务器
        /// </summary>
        private void ServerOn() {
            var args = string.Empty;

            if (comboBox1.SelectedValue.Equals(1)) {
                args += " /d";
                args += " /l:true";
            }
            else if(comboBox1.SelectedValue.Equals(2))
            {
                args += " /r:false";
                args += " /l:true";
            }
            else {
                args += " /r:true";
                args += " /l:true";
            }
            MyTools.ServerOn(args);
        }


        /// <summary>
        /// 当前正在执行自启动客户端
        /// </summary>
        public bool isAutoRunning { get; set; }
        /// <summary>
        ///     打开服务端/客户端
        /// </summary>
        private void AutoOnServerAndClient() {
            ServerOn();
         
            try {
               
                var isServerOn = MyTools.CheckProcessOn("Digiwin.Mars.ServerStart");
                if (!isServerOn)
                    return;
                if (isAutoRunning) return;
              
                var isOn = MyTools.CheckProcessOn("Digiwin.Mars.ClientStart");
                if (isOn) {
                    MessageBox.Show(Resources.ClientRunning);
                    return;
                }
                isAutoRunning = true;
                var tSerFolPath =
                    Toolpars.Mplatform
                    + @"\Server\Control";
                var myDate = DateTime.Now;
                var tToDayDate = myDate.ToString("yyyyMMdd");
                Thread.Sleep(3000);
                if (!Directory.Exists(tSerFolPath))
                    return;
                var dirs = Directory.GetFiles(tSerFolPath, tToDayDate + "*.log", SearchOption.TopDirectoryOnly);
                var index = dirs.Length - 1;
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
                new ComboxInfo {Id = 2, Name = "数据库"},
                new ComboxInfo {Id = 3, Name = "全启动"}
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
            else {
                args += " /d:false";
            }
            MyTools.ClientOn(args);
        }

        //^_^20160511 add by nicknt9095 for 自動偵測SERVER啟動CLIENT<S00349_20160505_02>--begin
        /// <summary>
        ///     偵測SERVER是否啟動完畢
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnAutoOpenClientTimedEvent(object source, ElapsedEventArgs e) {
            var tIsServerOpen = MyTools.CheckProcessOn("Digiwin.Mars.ServerStart");

            if (!tIsServerOpen) {
                _aTimer.Enabled = false;
                return;
            }
            try {
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
                        var tClientPath = Toolpars.Mplatform + "\\DeployServer\\Shared\\Digiwin.Mars.ClientStart.exe";
                        var args = string.Empty;
                        if (!File.Exists(tClientPath)) {
                            _aTimer.Enabled = false;
                            MessageBox.Show(string.Format(Resources.NotFindFile, tClientPath));
                            return;
                        }
                        if (SelectedValue.Equals(1))
                            args += " /d";

                        Process.Start(tClientPath, args);
                        _aTimer.Enabled = false;
                        isAutoRunning = false;
                    }
                }
            }
            catch (Exception ex) {
                isAutoRunning = false;
                LogTools.LogError($@"AutoClient Error! Detail:{ex.Message}");
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
            MyTools.KillProcess(killProcessName);
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
            try {
                var success = MyTools.CopyDll(CopyModelType.Client);
                if (!success) return;
                var isServerOn = MyTools.CheckProcessOn("Digiwin.Mars.ServerStart");
                var isOn = MyTools.CheckProcessOn("Digiwin.Mars.ClientStart");
                if (isServerOn && !isOn)
                {
                    if (MessageBox.Show($@"{Resources.CopySucess} 是否启动客户端?", Resources.Information,
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        StartClientBtn_Click(null, null);
                    }
                }
                else
                {
                    MessageBox.Show(Resources.CopySucess, Resources.Information,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex){
                LogTools.LogError($"CopyUIDll error! Detail:{ex.Message}");
                string[] processNames = {
                    "Digiwin.Mars.ClientStart"
                };
                var f = MyTools.CheckProcessRunning(processNames);
                if (!f)
                {
                    MessageBox.Show(ex.Message, Resources.ErrorMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (MessageBox.Show(Resources.DllUsedMsg, Resources.WarningMsg, MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        MyTools.KillProcess(processNames);
                        CopyClientBtn_Click(null,null);

                    }

                }
            }
        }

        /// <summary>
        ///     Copy客户端服务端Dll
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyBtn_Click(object sender, EventArgs e) {
            try {
                var success = MyTools.CopyDll(CopyModelType.ALL);
                if (!success) return;
                var isServerOn = MyTools.CheckProcessOn("Digiwin.Mars.ServerStart");
                if (!isServerOn) {
                    if (MessageBox.Show($@"{Resources.CopySucess} 是否启动服务端?", Resources.Information,
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Information) == DialogResult.OK) {
                        StartBtn_Click(null, null);
                    }
                }
                else {
                    MessageBox.Show(Resources.CopySucess, Resources.Information,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
             
            }
            catch (Exception ex)
            {
                string[] processNames = {
                    "Digiwin.Mars.ClientStart",
                    "Digiwin.Mars.ServerStart",
                    "Digiwin.Mars.AccountSetStart"
                };
                var f = MyTools.CheckProcessRunning(processNames);
                if (!f)
                    MessageBox.Show(ex.Message, Resources.ErrorMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    if (MessageBox.Show(Resources.DllUsedMsg, Resources.WarningMsg, MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        MyTools.KillProcess(processNames);
                        CopyBtn_Click(null,null);
                    }
                }


            }
        }

        public object SelectedValue { get; set; } = 1;
    

        private void ComboBox1_SelectedValueChanged(object sender, EventArgs e) {
                SelectedValue = comboBox1.SelectedValue ;
        }
    }


    /// <summary>
    ///     邦定调试选项
    /// </summary>
    public class ComboxInfo {
        /// <summary>
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// </summary>
        public string Name { get; set; }
    }
}