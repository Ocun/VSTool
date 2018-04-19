// create By 08628 20180411

using System;
using System.Collections.Generic;
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

        private string WdPr { get; set; }
        private string Wd { get; set; }
        private string Spec { get; set; }

        private void BtnOpenTo_Click(object sender, EventArgs e) {
            var txtPkGpath = Toolpars.FormEntity.txtPKGpath;
            var targetDir = (Toolpars.FormEntity.PkgTypekey==null|| Toolpars.FormEntity.PkgTypekey.Equals(string.Empty))?
                txtPkGpath: FromServer.Checked ? $@"{txtPkGpath}":$@"{txtPkGpath}\Digiwin.ERP.{Toolpars.FormEntity.PkgTypekey}";

            if (FromServer.Checked) {
                targetDir = GetServerDirPath();
                var codeDir = $@"{txtPkGpath}\{WdPr}\SRC\Digiwin.ERP.{Toolpars.FormEntity.PkgTypekey}";
                var specDir = $@"{txtPkGpath}\{Spec}";
                MyTool.OpenDir(codeDir);
                MyTool.OpenDir(targetDir);
                MyTool.OpenDir(specDir);
            }
            else {
                MyTool.OpenDir(targetDir);
            }
        }

        private void BtnOK_Click(object sender, EventArgs e) {
            if ((Toolpars.FormEntity.PkgTypekey ?? string.Empty).Equals(string.Empty)) {
                MessageBox.Show(Resource.pkgNotNull);
                return;
            }
            var customerName = CustomerText.Text.Trim();
            var empStr = string.Empty;
            var isPkg = customerName.Equals(empStr);
            WdPr = isPkg ? "WD_PR" : "WD_PR_C";
            Wd = isPkg ? "WD" : "WD_C";
            Spec = isPkg ? "SPEC" : "SPEC_C";
            if (FromServer.Checked)
            {

                Toolpars.FormEntity.txtPKGpath =
                    customerName.Equals(empStr) ?
                        $@"\\192.168.168.15\PKG_Source\{Toolpars.MVersion}\{WdPr}\SRC"
                        : $@"\\192.168.168.15\E10_Shadow\{Toolpars.MVersion}\{customerName}\{WdPr}\SRC";

            }
            else
            {
                Toolpars.FormEntity.txtPKGpath =
                    customerName.Equals(empStr) ?
                        $@"{Toolpars.MdesignPath}\{WdPr}\SRC"
                        : $@"{Toolpars.MdesignPath}\{customerName}\{WdPr}\SRC";
            }
            DialogResult = DialogResult.OK;
        }

        private void Button1_Click(object sender, EventArgs e) {
            var pkgPath = Toolpars.FormEntity.txtPKGpath;
          
            if (!Directory.Exists(pkgPath)) {
                MessageBox.Show(Resource.PkgDirNotExisted);
                return;
            }

            if (FromServer.Checked) {
                var typeKeyDir = GetServerDirPath();
                var pkgTypeKeyDir = $@"{pkgPath}\{WdPr}\SRC";
                var pkgTypeKeyPath = $@"{pkgTypeKeyDir}\Digiwin.ERP.{Toolpars.FormEntity.PkgTypekey}";
                //不存在TypeKey配置，直接copy代码
                if (!Directory.Exists(typeKeyDir)) {
                   
                        CopyPkg(pkgTypeKeyPath);
                }
                else {
                    var directoryInfo = new DirectoryInfo(typeKeyDir);
                    var targetDir = string.Empty;
                    var index = Toolpars.FormEntity.TxtToPath.LastIndexOf($@"{WdPr}\SRC", StringComparison.Ordinal);
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
            pkgTypePath =  $@"{filterPath[0]}.{filterPath[1]}.{filterPath[2]}";
            return pkgTypePath;
        }

        private string GetServerDirPath() {
            var path = string.Empty;
            var pkgPath = Toolpars.FormEntity.txtPKGpath;
            var typeKeyDir = $@"{pkgPath}\{Wd}";

            var batchObjectsDir = $@"{typeKeyDir}\BatchObjects";
            var businessObjectsDir = $@"{typeKeyDir}\BusinessObjects";
            var reportObjectsDir = $@"{typeKeyDir}\ReportObjects";
            var searchList = new List<string>{batchObjectsDir, businessObjectsDir, reportObjectsDir};
            foreach (var filterStr in searchList) {
                if (!Directory.Exists(filterStr)) {
                    continue;
                }
                var directoryInfo = new DirectoryInfo(filterStr);
                var filterDirs = directoryInfo.GetDirectories(Toolpars.FormEntity.PkgTypekey, SearchOption.TopDirectoryOnly);
                if (filterDirs.Length <= 0) continue;
                path = filterDirs[0].FullName;
                break;
            }
           return path;
        }

        private void CopyPkg(string pkgTypeKeyPath) {
            var success = MyTool.CopyAllPkG(Toolpars, pkgTypeKeyPath);
            if (!success) return;
            FromServer.Checked = false;
            DialogResult = DialogResult.Yes;
        }

        private void FromServer_CheckedChanged(object sender, EventArgs e) {
            SetPkgPath();
        }

        private void CustomerText_TextChanged(object sender, EventArgs e) {
            SetPkgPath();
        }
        
        private void SetPkgPath() {
            var customerName = CustomerText.Text.Trim();
            var empStr = string.Empty;
            var isPkg = customerName.Equals(empStr);
            WdPr = isPkg ? "WD_PR" : "WD_PR_C";
            Wd = isPkg ? "WD" : "WD_C";
            Spec = isPkg ? "SPEC" : "SPEC_C";
            if (FromServer.Checked) {
               
                Toolpars.FormEntity.txtPKGpath = 
                    customerName.Equals(empStr) ?
                    $@"\\192.168.168.15\PKG_Source\{Toolpars.MVersion}"
                    : $@"\\192.168.168.15\E10_Shadow\{Toolpars.MVersion}\{customerName}";
              
            }
            else
            {
                Toolpars.FormEntity.txtPKGpath =
                    customerName.Equals(empStr) ?
                        $@"{Toolpars.MdesignPath}\{WdPr}\SRC"
                        : $@"{Toolpars.MdesignPath}\{customerName}\{WdPr}\SRC";
            }

          
        }

      
    }
}