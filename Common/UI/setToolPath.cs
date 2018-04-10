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
    public partial class setToolPath : Form
    {
        public string path { get; set; }
        public setToolPath()
        {
            InitializeComponent();
        }
        public setToolPath(string path)
        {
            InitializeComponent();
            this.textBox1.Text = path;
            this.path = path;
        }

        private void btnOpenTo_Click(object sender, EventArgs e) {

            if (openFileDialog1.ShowDialog() == DialogResult.OK) {

                if (File.Exists(openFileDialog1.FileName)) {
                    this.textBox1.Text = openFileDialog1.FileName;
                    path = openFileDialog1.FileName;
                }
                else {
                    MessageBox.Show("所选文件无效，请重新选择");
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.OK;
        }
    }
}
