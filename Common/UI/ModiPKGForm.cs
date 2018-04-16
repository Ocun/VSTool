// create By 08628 20180411

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Common.Implement.Entity;
using Common.Implement.Properties;
using Common.Implement.Tools;

namespace Common.Implement.UI {
    public partial class ModiPkgForm : Form {
        public ModiPkgForm() {
            InitializeComponent();
        }

        public ModiPkgForm(Toolpars toolpars) {
            InitializeComponent();
            Toolpars = toolpars;
        }

        public Toolpars Toolpars { get; set; }

        private void ModiPKGForm_Load(object sender, EventArgs e) {
            txtNewTypeKey.DataBindings.Add(new Binding("Text", Toolpars.FormEntity, "txtNewTypeKey", true,
                DataSourceUpdateMode.OnPropertyChanged));
            TypeKeyText.DataBindings.Add(new Binding("Text", Toolpars.FormEntity, "PkgTypekey", true,
                DataSourceUpdateMode.OnPropertyChanged));
            if (!TypeKeyText.Text.Equals(string.Empty))
                return;
            if (Toolpars.FormEntity.txtNewTypeKey.Length > 1)
                TypeKeyText.Text = Toolpars.FormEntity.txtNewTypeKey.Substring(1);
        }

        private void BtnOpenTo_Click(object sender, EventArgs e) {
            var txtPkGpath = Toolpars.FormEntity.txtPKGpath;
            var targetDir = (Toolpars.FormEntity.PkgTypekey==null|| Toolpars.FormEntity.PkgTypekey.Equals(string.Empty))?
                txtPkGpath: $@"{txtPkGpath}\Digiwin.ERP.{Toolpars.FormEntity.PkgTypekey}";
            if (FromServer.Checked) {
                if (CustomerText.Text.Trim().Equals(string.Empty))
                {
                    MessageBox.Show(Resource.CustomerNameNotNull);
                    return;
                }
                targetDir = GetServerDirPath();
                if (Directory.Exists(targetDir))
                {
                    Process.Start(targetDir);
                }
                else
                {
                    MessageBox.Show(string.Format(Resource.DirNotExisted, targetDir), Resource.WarningMsg, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else {
                OpenToDir(targetDir);
            }
        }

        private void BtnOK_Click(object sender, EventArgs e) {
            if ((Toolpars.FormEntity.PkgTypekey ?? string.Empty).Equals(string.Empty)) {
                MessageBox.Show(Resource.pkgNotNull);
                return;
            }
            if ( CustomerText.Text.Trim().Equals(string.Empty)) {

            }
            else {
                
            }
            DialogResult = DialogResult.OK;
        }

        void OpenToDir(string targetDir) {
            if (Directory.Exists(targetDir))
            {
                Process.Start(targetDir);
            }
            else
            {
                MessageBox.Show(string.Format(Resource.DirNotExisted, targetDir), Resource.WarningMsg, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void Button1_Click(object sender, EventArgs e) {
            var pkgPath = Toolpars.FormEntity.txtPKGpath;
          
            if (!Directory.Exists(pkgPath)) {
                MessageBox.Show(Resource.PkgDirNotExisted);
                return;
            }

            if (FromServer.Checked) {
                if (CustomerText.Text.Trim().Equals(string.Empty)) {
                    MessageBox.Show(Resource.CustomerNameNotNull);
                    return;
                }
                var customerName = CustomerText.Text.Trim();
                var typeKeyDir = GetServerDirPath();
                var pkgTypeKeyDir = $@"{pkgPath}\{customerName}\WD_PR_C\SRC";;
                var pkgTypeKeyPath = $@"{pkgTypeKeyDir}\Digiwin.ERP.{Toolpars.FormEntity.PkgTypekey}";
                if (!Directory.Exists(typeKeyDir)) {
                   
                        CopyPkg(pkgTypeKeyPath);
                }
                else {
                    var directoryInfo = new DirectoryInfo(typeKeyDir);
                    var targetDir = string.Empty;
                    var index = Toolpars.FormEntity.TxtToPath.LastIndexOf(@"WD_PR_C\SRC", StringComparison.Ordinal);
                    if (index > -1) {
                        targetDir = Toolpars.FormEntity.TxtToPath.Substring(0,index);
                    }

                    targetDir = $@"{targetDir}\{Toolpars.FormEntity.txtNewTypeKey}";
                    OldTools.CopyAllPkg(typeKeyDir, targetDir);

                    #region 从功能包copy对应的代码
                   
                    var clientFunctionName = directoryInfo.GetFiles("Client.FunctionPackage.dcxml", SearchOption.AllDirectories);
                    var serverFunctionName = directoryInfo.GetFiles("Server.FunctionPackage.dcxml", SearchOption.AllDirectories);
                    var listPath = new List<string>();
                    var xPath = @"AssemblyQualifiedName";
                    if (clientFunctionName.Length > 0)
                    {
                        var pkgList = XmlTools.GetPkgPath(clientFunctionName[0].FullName, xPath);
                        listPath.AddRange(pkgList);
                    }
                    if (serverFunctionName.Length > 0)
                    {
                        var pkgList = XmlTools.GetPkgPath(clientFunctionName[0].FullName, xPath);
                        listPath.AddRange(pkgList);
                    }
                    var pkgTypePathList = listPath.Distinct().ToList().Select(path => new
                    {
                        pkgTypePath = GetPkgTypePathFormServer(path)
                    }).Distinct().ToList();
                    if (pkgTypePathList.Any())
                    {
                        pkgTypePathList.ForEach(path =>
                        {
                            pkgTypeKeyPath = $@"{pkgTypeKeyDir}\{path.pkgTypePath}";
                            CopyPkg(pkgTypeKeyPath);
                        });
                    } 
                    #endregion

                }


            }
            else {
                var pkgTypeKeyPath = $@"{Toolpars.FormEntity.txtPKGpath}\Digiwin.ERP.{Toolpars.FormEntity.PkgTypekey}";

                CopyPkg(pkgTypeKeyPath);
            }
        }

        private string GetPkgTypePathFormServer(string path) {
            var pkgTypePath = string.Empty;
            var filterPath = path.Split('.');
            if (filterPath.Length <= 3) return pkgTypePath;
            var typeKey = filterPath[2];
            if (typeKey.StartsWith(@"X")) {
                pkgTypePath =  $@"{filterPath[0]}.{filterPath[1]}.{filterPath[2]}";
            }
            return pkgTypePath;
        }
        string GetServerDirPath() {
            var path = string.Empty;
            var pkgPath = Toolpars.FormEntity.txtPKGpath;
            var customerName = CustomerText.Text.Trim();
            var typeKeyDir = $@"{pkgPath}\{customerName}\WD_C";
            if (!Directory.Exists(typeKeyDir)) return path;
            var directoryInfo = new DirectoryInfo(typeKeyDir);
            //查找所有名为PkgTypekey的文件夹
            var filterDirs = directoryInfo.GetDirectories(Toolpars.FormEntity.PkgTypekey, SearchOption.AllDirectories);
            if (filterDirs.Length <= 0) return path;
            var typeDir = (from dir in filterDirs where dir.Parent?.Parent != null
                           let wdC = dir.Parent.Parent.FullName
                           where wdC.Equals(typeKeyDir)
                           select dir).FirstOrDefault();
            if (typeDir != null) {
                path = typeDir.FullName;
            }
            return path;

        }
        void CopyPkg(string pkgTypeKeyPath) {
            var success = MyTool.CopyAllPkG(Toolpars, pkgTypeKeyPath);
            if (!success) return;
            FromServer.Checked = false;
            DialogResult = DialogResult.Yes;
        }
        private void FromServer_CheckedChanged(object sender, EventArgs e) {
            Toolpars.FormEntity.txtPKGpath = FromServer.Checked ?
                $@"\\192.168.168.15\PKG_Source\{Toolpars.MVersion}" 
                : $@"{Toolpars.MdesignPath}\WD_PR\SRC";

            CustomerText.Enabled = FromServer.Checked;
        }

        private void CustomerText_TextChanged(object sender, EventArgs e) {
            var text = CustomerText.Text.Trim();
            var empStr = string.Empty;
            if (text.Equals(empStr)) {
                Toolpars.FormEntity.txtPKGpath = FromServer.Checked
                    ? $@"\\192.168.168.15\PKG_Source\{Toolpars.MVersion}"
                    : $@"{Toolpars.MdesignPath}\WD_PR\SRC";
            }
            else {
                Toolpars.FormEntity.txtPKGpath = FromServer.Checked
                    ? $@"\\192.168.168.15\E10_Shadow\{Toolpars.MVersion}"
                    : $@"{Toolpars.MdesignPath}\WD_PR\SRC";
            }
        }
    }
}