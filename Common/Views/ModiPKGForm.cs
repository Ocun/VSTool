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
    /// <summary>
    /// 借用标准
    /// </summary>
    public partial class ModiPkgForm : Form {
        /// <summary>
        /// 构造
        /// </summary>
        public ModiPkgForm() {
            InitializeComponent();
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="toolpars"></param>
        public ModiPkgForm(Toolpars toolpars) {
            InitializeComponent();
            Toolpars = toolpars;
        }

        /// <summary>
        /// 主窗体参数
        /// </summary>
        public Toolpars Toolpars { get; set; }

        /// <summary>
        /// 记录上次目录，用于还原
        /// </summary>
        public string PkgPathOld { get; set; }

        private void ModiPKGForm_Load(object sender, EventArgs e) {
            txtNewTypeKey.DataBindings.Add(new Binding("Text", Toolpars.FormEntity, "txtNewTypeKey", true,
                DataSourceUpdateMode.OnPropertyChanged));
            TypeKeyText.DataBindings.Add(new Binding("Text", Toolpars.FormEntity, "PkgTypekey", true,
                DataSourceUpdateMode.OnPropertyChanged));
            PkgPathOld = Toolpars.FormEntity.TxtPkGpath;
            if (!TypeKeyText.Text.Equals(string.Empty))
                return;
            if (Toolpars.FormEntity.TxtNewTypeKey.Length > 1)
                TypeKeyText.Text = Toolpars.FormEntity.TxtNewTypeKey.Substring(1);
         
        }

        private string WdPr { get; set; }
        private string Wd { get; set; }
        private string Spec { get; set; }

        private void BtnOpenTo_Click(object sender, EventArgs e) {
            var txtPkGpath = Toolpars.FormEntity.TxtPkGpath;
            var pathInfo = Toolpars.PathEntity;
            var pkgFullPath = pathInfo.PkgTypeKeyFullRootDir;
            var targetDir = (Toolpars.FormEntity.PkgTypekey==null|| Toolpars.FormEntity.PkgTypekey.Equals(string.Empty))?
                txtPkGpath: FromServer.Checked ? $@"{txtPkGpath}": pkgFullPath;

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
                MessageBox.Show(Resources.pkgNotNull);
                return;
            }
            var customerName = CustomerText.Text.Trim();
            var empStr = string.Empty;
            var isPkg = customerName.Equals(empStr);
            WdPr = isPkg ? "WD_PR" : "WD_PR_C";
            Wd = isPkg ? "WD" : "WD_C";
            Spec = isPkg ? "SPEC" : "SPEC_C";
            if (FromServer.Checked) {

                Toolpars.FormEntity.TxtPkGpath =
                    customerName.Equals(empStr)
                        ? PathTools.PathCombine(@"\\192.168.168.15\PKG_Source", Toolpars.MVersion, WdPr, "SRC")
                        : PathTools.PathCombine(@"\\192.168.168.15\E10_Shadow", Toolpars.MVersion, customerName, WdPr,
                            "SRC");
                var pathInfo = Toolpars.PathEntity;
                var pkgDir = pathInfo.PkgTypeKeyFullRootDir;
                if (!Directory.Exists(pkgDir)) {
                    Toolpars.FormEntity.TxtPkGpath = PkgPathOld;
                    MessageBox.Show(string.Format(Resources.DirNotExisted, pkgDir), Resources.WarningMsg,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else {
                var customerBaseDir = Path.GetDirectoryName(Path.GetDirectoryName(PkgPathOld));
                var customerFullPathDir = PathTools.PathCombine(customerBaseDir, customerName, WdPr, "SRC");
                Toolpars.FormEntity.TxtPkGpath =
                    isPkg ? PkgPathOld : customerFullPathDir;
                
            }
            DialogResult = DialogResult.OK;
        }

        private void Button1_Click(object sender, EventArgs e) {
            var pkgPath = Toolpars.FormEntity.TxtPkGpath;
            var pathInfo = Toolpars.PathEntity;
            var pkgFullPath = pathInfo.PkgTypeKeyFullRootDir;
            if (!Directory.Exists(pkgPath)) {
                MessageBox.Show(Resources.PkgDirNotExisted);
                return;
            }

            if (FromServer.Checked) {
                var typeKeyDir = GetServerDirPath();
                var pkgTypeKeyDir = PathTools.PathCombine(pkgPath, WdPr, "SRC");
                var pkgTypeKeyPath = pkgFullPath;
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
                    targetDir = PathTools.PathCombine(targetDir,Toolpars.FormEntity.TxtNewTypeKey);

                    OldTools.CopyAllPkg(typeKeyDir, targetDir);

                    #region 从功能包copy对应的代码
                   
                    var clientFunctionName = directoryInfo.GetFiles("Client.FunctionPackage.dcxml", SearchOption.AllDirectories);
                    var serverFunctionName = directoryInfo.GetFiles("Server.FunctionPackage.dcxml", SearchOption.AllDirectories);
                    var listPath = new List<string>();
                    var XPath = @"AssemblyQualifiedName";
                    if (clientFunctionName.Length > 0)
                    {
                        var pkgList = XmlTools.GetPathByXpath(clientFunctionName[0].FullName, XPath);
                        listPath.AddRange(pkgList);
                    }
                    if (serverFunctionName.Length > 0)
                    {
                        var pkgList = XmlTools.GetPathByXpath(clientFunctionName[0].FullName, XPath);
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
                            pkgTypeKeyPath = PathTools.PathCombine(pkgTypeKeyDir,path.pkgTypePath);
                            CopyPkg(pkgTypeKeyPath);
                        });
                    } 
                    #endregion

                }


            }
            else {
                var pkgTypeKeyPath = pkgFullPath;
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
            var pkgPath = Toolpars.FormEntity.TxtPkGpath;
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
            var success = MyTool.CopyAllPkG(pkgTypeKeyPath);
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
               
                Toolpars.FormEntity.TxtPkGpath = 
                    customerName.Equals(empStr) ?
                    PathTools.PathCombine(@"\\192.168.168.15\PKG_Source", Toolpars.MVersion)
                    : PathTools.PathCombine(@"\\192.168.168.15\E10_Shadow", Toolpars.MVersion, customerName);
            }
            else {
                var customerDir =Path.GetDirectoryName(Path.GetDirectoryName(PkgPathOld));
                Toolpars.FormEntity.TxtPkGpath =
                    isPkg ? PkgPathOld  : $@"{customerDir}\{customerName}\{WdPr}\SRC";
            }

          
        }

      
    }
}