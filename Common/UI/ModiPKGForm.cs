using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Common.Implement.UI
{
    public partial class ModiPKGForm : Form
    {
        public toolpars Toolpars { get; set; }
        public ModiPKGForm()
        {
            InitializeComponent();
        }
        public ModiPKGForm(toolpars Toolpars)
        {
            InitializeComponent();
            this.Toolpars = Toolpars;
        }

        private void ModiPKGForm_Load(object sender, EventArgs e)
        {
            this.txtNewTypeKey.DataBindings.Add(new Binding("Text", Toolpars.formEntity, "txtNewTypeKey", true,
                DataSourceUpdateMode.OnPropertyChanged));
            this.TypeKeyText.DataBindings.Add(new Binding("Text", Toolpars.formEntity, "PkgTypekey", true,
                DataSourceUpdateMode.OnPropertyChanged));
            if (TypeKeyText.Text.Equals(string.Empty)) {
                if(Toolpars.formEntity.txtNewTypeKey.Length>1)
                TypeKeyText.Text = Toolpars.formEntity.txtNewTypeKey.Substring(1);
            }
          
        }

        private void btnOpenTo_Click(object sender, EventArgs e) {
            var txtPKGpath = Toolpars.formEntity.txtPKGpath;
            if (txtPKGpath != null && txtPKGpath.Trim()!= "")
            {
               folderBrowserDialog1.SelectedPath = txtPKGpath.Trim();
            }
            else
            {
                folderBrowserDialog1.SelectedPath = Toolpars.GToIni;
            }
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = Path.GetExtension(folderBrowserDialog1.SelectedPath);

                this.TypeKeyText.Text = path;



            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if ((Toolpars.formEntity.PkgTypekey??string.Empty).Equals(string.Empty)) {
                MessageBox.Show("借用TypeKey不可为空");
                return;
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}
