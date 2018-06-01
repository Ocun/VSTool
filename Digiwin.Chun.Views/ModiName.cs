// create By 08628 20180411

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Digiwin.Chun.Models;
using Digiwin.Chun.Views.Properties;
using Digiwin.Chun.Views.Tools;

namespace Digiwin.Chun.Views {
    /// <summary>
    ///     新项目参数窗体
    /// </summary>
    public partial class ModiName : Form {
        private  Toolpars Toolpars { get; set; }

        /// <summary>
        /// </summary>
        public ModiName() {
            InitializeComponent();
        }

        /// <summary>
        /// </summary>
        /// <param name="itemBuildeType"></param>
        /// <param name="parBuliderType"></param>
        /// <param name="toolpars"></param>
        public ModiName(BuildeType itemBuildeType, BuildeType parBuliderType, Toolpars toolpars) {
            InitializeComponent();
            BuildeType = itemBuildeType;
            Text = BuildeType.Name;
            Toolpars = toolpars;
            ParBuliderType = parBuliderType;
            FileInfos = new List<FileInfos>();
        }

        /// <summary>
        /// 
        /// </summary>
        public BuildeType BuildeType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BuildeType ParBuliderType { get; set; }
        /// <summary>
        /// 模板文件信息
        /// </summary>
        public List<FileInfos> FileInfos { get; set; }

        private void BtnOK_Click(object sender, EventArgs e) {
            if (string.Equals(txt01.Text, string.Empty, StringComparison.Ordinal) && txt01.Visible ||
                string.Equals(txt02.Text, string.Empty, StringComparison.Ordinal) && txt02.Visible) {
                MessageBox.Show(Resources.notNull);
            }
            else {
                var fileInfos = MyTools.CreateFileMappingInfo(BuildeType, txt01.Text, txt02.Text);
                MyTools.SetFileInfo(ParBuliderType.Id, BuildeType, fileInfos);
                DialogResult = DialogResult.OK;
            }
        }
        

        private void ModiName_Load(object sender, EventArgs e) {
            try {
                txt01.Text = string.Format(Resources.CreateFileName, BuildeType.Id);
                txt02.Text = string.Format(Resources.CreateFileName, BuildeType.Id);
                if (BuildeType.Id.Equals("IService")) {
                    txt01.Text = @"ICreateService";
                    txt02.Text = @"ICreateService";
                }


                btnOK.Focus();
            }
            catch {
                // ignored
            }
        }


        private void Txt01_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char) Keys.Enter)
                SendKeys.Send("{tab}");
        }

        private void MergeBox_CheckedChanged(object sender, EventArgs e) {
            var checkedBox = (sender as CheckBox);
            if (checkedBox != null && checkedBox.Checked) {
                BuildeType.IsMerge = "True";
            }
            else {
                BuildeType.IsMerge = "False";
            }
        }
    }

    /// <summary>
    /// 时机点，暂时不用
    /// </summary>
    public class ActionPoint {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
    }
}