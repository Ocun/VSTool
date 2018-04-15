// create By 08628 20180411

using System;
using System.IO;
using System.Windows.Forms;

namespace Common.Implement.UI {
    public partial class SetToolPath : Form {
        public SetToolPath() {
            InitializeComponent();
        }

        public SetToolPath(string path) {
            InitializeComponent();
            textBox1.Text = (path??string.Empty).Trim();
            Path = path;
        }

        public string Path { get; set; }

        private void btnOpenTo_Click(object sender, EventArgs e) {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            if (File.Exists(openFileDialog1.FileName)) {
                textBox1.Text = openFileDialog1.FileName;
                Path = openFileDialog1.FileName;
            }
            else {
                MessageBox.Show("所选文件无效，请重新选择");
            }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
        }
    }
}