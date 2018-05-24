using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Digiwin.Chun.Common.Controller;
using Digiwin.Chun.Common.Model;
using Digiwin.Chun.Common.Properties;

namespace Digiwin.Chun.Common.Views {
    /// <summary>
    /// 
    /// </summary>
    public partial class OpenDirForm : Form {
        /// <summary>
        /// </summary>
        public OpenDirForm() {
            InitializeComponent();
            Toolpars = MyTools.Toolpars;
            InitPars();
        }

        /// <summary>
        /// </summary>
        /// <param name="toolpars"></param>
        public OpenDirForm(Toolpars toolpars) {
            InitializeComponent();
            Toolpars = toolpars;
            InitPars();
        }

        private string WdPr { get; set; }
        private string Wd { get; set; }
        private string Spec { get; set; }

        private Toolpars Toolpars { get; }

        private void InitPars() {
            var pathInfo = Toolpars.PathEntity;
            CustomerTB.Text = Toolpars.CustomerName;
            TypeKeyTB.Text = Toolpars.FormEntity.TxtNewTypeKey;
            ClientTB.Text = pathInfo.DeployFullPath;
            ServerTB.Text = pathInfo.ServerFullPath;
            ShadowTB.Text = PathTools.PathCombine(@"\\192.168.168.15\E10_Shadow", Toolpars.MVersion,
                Toolpars.CustomerName);
           
            PublishTB.Text = PathTools.PathCombine(@"\\192.168.168.15\E10_Publish", Toolpars.MVersion,
                Toolpars.CustomerName);
            BaseTB.Text = Toolpars.Mplatform;
        }

        private void BtnOpenCustomer_Click(object sender, EventArgs e) {
            var btn = sender as Button;
            var name = btn?.Name;
            var dirPath = string.Empty;
            var txtToPath = Toolpars.FormEntity.TxtToPath;
            if (name == null) return;
            if (name.Equals(BtnOpenCustomer.Name)) {
                var customerName = CustomerTB.Text.Trim();
                if (!PathTools.IsNullOrEmpty(customerName)) {
                    var customerDir = string.Empty;
                    txtToPath = txtToPath.Replace(Toolpars.CustomerName, customerName);
                    if (!txtToPath.Equals(string.Empty)) {
                        customerDir = Path.GetDirectoryName(Path.GetDirectoryName(txtToPath));
                    }

                    dirPath = customerDir;
                }
            }
            else if (name.Equals(BtnOpenTypeKey.Name)) {
                var typeKey = TypeKeyTB.Text.Trim();
                var customerName = CustomerTB.Text.Trim();
                if (!PathTools.IsNullOrEmpty(customerName)) {
                    var customerDir = string.Empty;
                    txtToPath = txtToPath.Replace(Toolpars.CustomerName, customerName);
                    if (!txtToPath.Equals(string.Empty)) {
                        customerDir = PathTools.PathCombine(Path.GetDirectoryName(Path.GetDirectoryName(txtToPath)),
                            Wd);
                    }
                    dirPath = FindTypekeyDir(customerDir, typeKey);
                }
                else {
                    var shadowDir = ShadowTB.Text.Trim();
                    shadowDir = PathTools.PathCombine(shadowDir, Wd);
                    dirPath = PathTools.IsNullOrEmpty(typeKey) ? dirPath : FindTypekeyDir(shadowDir, typeKey);
                }
            }
            else if (name.Equals(BtnCode.Name)) {
                var typeKey = TypeKeyTB.Text.Trim();
                var customerName = CustomerTB.Text.Trim();
                if (!PathTools.IsNullOrEmpty(customerName)) {
                    txtToPath = txtToPath.Replace(Toolpars.CustomerName, customerName);
                    if (!txtToPath.Equals(string.Empty)) {
                         dirPath = PathTools.PathCombine(Path.GetDirectoryName(Path.GetDirectoryName(txtToPath)),
                            WdPr,"SRC", $"Digiwin.ERP.{typeKey}");
                    }  
                }
                else {
                    var shadowDir = ShadowTB.Text.Trim();
                    dirPath = PathTools.PathCombine(shadowDir, WdPr, "SRC", $"Digiwin.ERP.{typeKey}");
                }
            }
            else if (name.Equals(BtnOpenClient.Name)) {
                var typeKey = TypeKeyTB.Text.Trim();
                if (PathTools.IsNullOrEmpty(typeKey)) {
                    dirPath = ClientTB.Text.Trim();
                }
                else {
                    var clientDir = ClientTB.Text.Trim();
                    dirPath = FindTypekeyDir(clientDir, typeKey);
                    dirPath = string.IsNullOrEmpty(dirPath) ? clientDir : dirPath;
                }
            }
            else if (name.Equals(BtnOpenServer.Name)) {
                var typeKey = TypeKeyTB.Text.Trim();
                if (PathTools.IsNullOrEmpty(typeKey)) {
                    dirPath = ServerTB.Text.Trim();
                }
                else {
                    var serverDir = ServerTB.Text.Trim();
                    dirPath = FindTypekeyDir(serverDir, typeKey);
                    dirPath = string.IsNullOrEmpty(dirPath) ? serverDir: dirPath;
                }
            }
            else if (name.Equals(BtnOpenShadow.Name)) {
                var typeKey = TypeKeyTB.Text.Trim();
                var shadowDir = ShadowTB.Text.Trim();
                shadowDir = PathTools.PathCombine(shadowDir,Wd);
                dirPath = PathTools.IsNullOrEmpty(typeKey) ? shadowDir : FindTypekeyDir(shadowDir, typeKey);
            }
            else if (name.Equals(BtnOpenPublish.Name)) {
                    dirPath = PublishTB.Text.Trim();
            }
            else if (name.Equals(BtnOpenBase.Name)) {
                dirPath = BaseTB.Text.Trim();
            }
            if (PathTools.IsNullOrEmpty(dirPath)) {
                MessageBox.Show($@"不可为空或Typekey:{TypeKeyTB.Text.Trim()}不存在！");
                return;
            }

            if (!Directory.Exists(dirPath)) {
                MessageBox.Show(string.Format(Resources.DirNotExisted, dirPath));
                return;
            }
            MyTools.OpenDir(dirPath);
            MyTools.InsertInfo($"{BtnOpenCustomer.Name}");
        }

        private string FindTypekeyDir(string path, string typeKey) {
            var dirPath = string.Empty;
            try {
                var batchObjectsDir = $@"{path}\BatchObjects";
                var businessObjectsDir = $@"{path}\BusinessObjects";
                var reportObjectsDir = $@"{path}\ReportObjects";
                var searchList = new List<string> {batchObjectsDir, businessObjectsDir, reportObjectsDir};
              
                foreach (var filterStr in searchList) {
                    if (!Directory.Exists(filterStr))
                        continue;
                    var directoryInfo = new DirectoryInfo(filterStr);
                    var filterDirs = directoryInfo.GetDirectories(typeKey, SearchOption.TopDirectoryOnly);
                    if (filterDirs.Length <= 0) continue;
                    dirPath = filterDirs[0].FullName;
                    break;
                }
            }
            catch (Exception ex) {
                LogTools.LogError($"FindTypeKeyDir Error! Detail:{ex.Message}");
            }
        
            return dirPath;
        }

        private bool IsPkg { get; set; } = false;

        private void CustomerTB_Changed(object sender, EventArgs e)
        {
            var customerName = CustomerTB.Text.Trim();
            var IsPkg = PathTools.IsNullOrEmpty(customerName);
            WdPr = IsPkg ? "WD_PR" : "WD_PR_C";
            Wd = IsPkg ? "WD" : "WD_C";
            Spec = IsPkg ? "SPEC" : "SPEC_C";
            ShadowTB.Text = IsPkg ?
                PathTools.PathCombine(@"\\192.168.168.15\PKG_Source", Toolpars.MVersion)
                : PathTools.PathCombine(@"\\192.168.168.15\E10_Shadow", Toolpars.MVersion, customerName);
            
            PublishTB.Text = IsPkg ? string.Empty:PathTools.PathCombine(@"\\192.168.168.15\E10_Publish", Toolpars.MVersion,
                customerName);
        }
        
    }
}