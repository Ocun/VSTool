// create By 08628 20180411

using System;
using System.IO;
using System.Windows.Forms;
using Common.Implement.Properties;

namespace Common.Implement.UI {
    /// <summary>
    /// 设置外部工具链接地址
    /// </summary>
    public partial class SetToolPath : Form {
        /// <summary>
        /// 
        /// </summary>
        public SetToolPath() {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public SetToolPath(string path) {
            InitializeComponent();
            textBox1.Text = (path??string.Empty).Trim();
            Path = (path ?? string.Empty).Trim();
            textBox1.DataBindings.Add(new Binding("Text", Path, "", true,
                DataSourceUpdateMode.OnPropertyChanged));
        }

        /// <summary>
        /// 
        /// </summary>
        public string Path { get; set; }

        private void BtnOpenTo_Click(object sender, EventArgs e) {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            if (File.Exists(openFileDialog1.FileName)) {
                textBox1.Text = openFileDialog1.FileName;
                Path = openFileDialog1.FileName;
            }
            else {
                MessageBox.Show(string.Format(Resources.NotFindFile,string.Empty));
            }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            Path = textBox1.Text;
        }
    }
}