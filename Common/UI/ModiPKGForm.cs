// create By 08628 20180411

using System;
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
            if (txtPkGpath != null && txtPkGpath.Trim() != "")
                folderBrowserDialog1.SelectedPath = txtPkGpath.Trim();
            else
                folderBrowserDialog1.SelectedPath = Toolpars.GToIni;
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                return;
            var extension = Path.GetExtension(folderBrowserDialog1.SelectedPath);
            if (extension == null)
                return;
            var path = extension.Substring(1);

            TypeKeyText.Text = path;
        }

        private void BtnOK_Click(object sender, EventArgs e) {
            if ((Toolpars.FormEntity.PkgTypekey ?? string.Empty).Equals(string.Empty)) {
                MessageBox.Show(Resource.pkgNotNull);
                return;
            }

            DialogResult = DialogResult.OK;
        }
        
        private void Button1_Click(object sender, EventArgs e) {

            string pkgPath = Toolpars.FormEntity.PkgTypekey;
            if (FromServer.Checked) {
                pkgPath = @"\\192.168.168.15\E10_Shadow\";
            }
            if (!Directory.Exists(pkgPath)) {
                return;
            }
            var fromDir = new DirectoryInfo(pkgPath);
            var pkgMatch= $@"Digiwn.ERP.{Toolpars.FormEntity.PkgTypekey}";
            var typeKeyDir = fromDir.GetDirectories(Toolpars.FormEntity.PkgTypekey, SearchOption.AllDirectories);
            var pkgDir= fromDir.GetDirectories(pkgMatch, SearchOption.AllDirectories);

            //typeKey
            if (typeKeyDir.Length>1) {
                
            }
            //源码
            if (pkgDir.Length>0) {
                var pkgTypeKeyPath = $"{Toolpars.FormEntity.txtPKGpath}Digiwin.ERP.{Toolpars.FormEntity.PkgTypekey}";
                var success = MyTool.CopyAllPkG(Toolpars, pkgTypeKeyPath);
                if (success)
                    DialogResult = DialogResult.Yes;
            }
        }
    }
}