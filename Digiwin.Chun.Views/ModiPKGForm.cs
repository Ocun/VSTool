// create By 08628 20180411

using System;
using System.IO;
using System.Windows.Forms;
using Digiwin.Chun.Common.Tools;
using Digiwin.Chun.Views.Properties;
using Digiwin.Chun.Views.Tools;
using static Digiwin.Chun.Common.Tools.CommonTools;

namespace Digiwin.Chun.Views {
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
            PkgPathOld = Toolpars.FormEntity.PkgPath;
            //if (!TypeKeyText.Text.Equals(string.Empty))
            //    return;
            if (Toolpars.FormEntity.TxtNewTypeKey.Length > 1)
                TypeKeyText.Text = Toolpars.FormEntity.TxtNewTypeKey.Substring(1);
            InitPkgPath();
        }

        private string WdPr { get; set; }
        private string Spec { get; set; }

        private void BtnOpenTo_Click(object sender, EventArgs e) {
            var txtPath = Toolpars.FormEntity.PkgPath;
            var pathInfo = Toolpars.PathEntity;
            var pkgFullPath = pathInfo.PkgTypeKeySrcFullRootDir;
            var targetDir = (Toolpars.FormEntity.PkgTypekey==null|| Toolpars.FormEntity.PkgTypekey.Equals(string.Empty))?
                txtPath : FromServer.Checked ? $@"{txtPath}": pkgFullPath;

            if (FromServer.Checked) {
                //配置
                targetDir = PathTools.IsNullOrEmpty(Toolpars.FormEntity.PkgTypekey) ? targetDir:GetServerDirPath();
                //代码
                var codeDir =PathTools.IsNullOrEmpty(Toolpars.FormEntity.PkgTypekey) ? $@"{txtPath}\{WdPr}\SRC":
                    $@"{txtPath}\{WdPr}\SRC\Digiwin.ERP.{Toolpars.FormEntity.PkgTypekey}";
                //规格
                var specDir = $@"{txtPath}\{Spec}";
                OpenDir(codeDir);
                OpenDir(targetDir);
                OpenDir(specDir);
            }
            else {
                OpenDir(targetDir);
            }
        }

        private void BtnOK_Click(object sender, EventArgs e) {
            if ((Toolpars.FormEntity.PkgTypekey ?? string.Empty).Equals(string.Empty)) {
                MessageBox.Show(Resources.pkgNotNull);
                return;
            }
            var customerName = CustomerText.Text.Trim();
            var empStr = string.Empty;
            WdPr = IsPkg ? "WD_PR" : "WD_PR_C";
            Spec = IsPkg ? "SPEC" : "SPEC_C";
            if (FromServer.Checked) {

                Toolpars.FormEntity.PkgPath =
                    customerName.Equals(empStr)
                        ? PathTools.PathCombine(@"\\192.168.168.15\PKG_Source", Toolpars.MVersion)
                        : PathTools.PathCombine(@"\\192.168.168.15\E10_Shadow", Toolpars.MVersion, customerName);
                var pathInfo = Toolpars.PathEntity;
                var pkgDir = pathInfo.PkgTypeKeySrcFullRootDir;
                if (!Directory.Exists(pkgDir)) {
                    Toolpars.FormEntity.PkgPath = PkgPathOld;
                    MessageBox.Show(string.Format(Resources.DirNotExisted, pkgDir), Resources.WarningMsg,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else {
                var customerDir = string.Empty;
                if (!PkgPathOld.Equals(string.Empty))
                {
                    customerDir = Path.GetDirectoryName(Path.GetDirectoryName(PkgPathOld));
                }
                var customerFullPathDir = PathTools.PathCombine(customerDir, customerName);
                Toolpars.FormEntity.PkgPath =
                    IsPkg ? PkgPathOld : customerFullPathDir;
            }
            DialogResult = DialogResult.OK;
        }

        private void Button1_Click(object sender, EventArgs e) {
            if ((Toolpars.FormEntity.PkgTypekey ?? string.Empty).Equals(string.Empty))
            {
                MessageBox.Show(Resources.pkgNotNull);
                return;
            }
            CopyPkg();
        }


        private string GetServerDirPath() {
           return MyTools.GetServerDirPath(IsPkg);
        }

        private void CopyPkg() {
            var success = MyTools.CopyAllPkG(IsPkg);
            if (!success) return;
            FromServer.Checked = false;
            DialogResult = DialogResult.Yes;
        }

        private void FromServer_CheckedChanged(object sender, EventArgs e) {
          
            if (FromServer.Checked)
            {
                splitContainer1.Enabled = false;
                panel1.Enabled = false;
                RunAsync(() =>
                {
                    var status = ConnectionStatusTool.CheckServeStatus("192.168.168.15");
                    return status.Equals("200");
                }, (c) =>
                {
                    if (c)
                    {
                        splitContainer1.Enabled = true;
                        panel1.Enabled = true;
                        InitPkgPath();
                    }
                    else
                    {
                        MessageBox.Show(Resources.ServerNotFound);
                        FromServer.Checked = false;
                        splitContainer1.Enabled = true;
                        panel1.Enabled = true;
                    }
                });
            }
            else
            {
                InitPkgPath();
            }
        }

        private void CustomerText_TextChanged(object sender, EventArgs e) {
            InitPkgPath();
        }

        private bool IsPkg { get; set; } = true;
        private void InitPkgPath() {
            var customerName = CustomerText.Text.Trim();
            var empStr = string.Empty;
             IsPkg = customerName.Equals(empStr);
            WdPr = IsPkg ? "WD_PR" : "WD_PR_C";
            Spec = IsPkg ? "SPEC" : "SPEC_C";
            if (FromServer.Checked) {
               
                Toolpars.FormEntity.PkgPath = 
                    customerName.Equals(empStr) ?
                    PathTools.PathCombine(@"\\192.168.168.15\PKG_Source", Toolpars.MVersion)
                    : PathTools.PathCombine(@"\\192.168.168.15\E10_Shadow", Toolpars.MVersion, customerName);
            }
            else {
                var customerDir = string.Empty;
                if (!PkgPathOld.Equals(string.Empty)) {
                  customerDir =Path.GetDirectoryName(Path.GetDirectoryName(PkgPathOld));
                }
                Toolpars.FormEntity.PkgPath =
                    IsPkg ? PkgPathOld  : $@"{customerDir}\{customerName}";
            }

          
        }

        private void ModiPkgForm_FormClosed(object sender, FormClosedEventArgs e) {
            CustomerText.Text = string.Empty;
            FromServer.Checked = false;
            InitPkgPath();
        }
    }
}