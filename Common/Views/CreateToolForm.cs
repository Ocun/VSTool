using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Digiwin.Chun.Common.Controller;
using Digiwin.Chun.Common.Model;
using IWshRuntimeLibrary;

namespace Digiwin.Chun.Common.Views {
    /// <summary>
    /// 创建工具项
    /// </summary>
    public partial class CreateToolForm : Form {
        /// <summary>
        /// </summary>
        public CreateToolForm() {
            InitializeComponent();
        }

        /// <summary>
        /// </summary>
        /// <param name="toolPath"></param>
        /// <param name="toolpar"></param>
        public CreateToolForm(string toolPath, Toolpars toolpar) {
            InitializeComponent();
            Toolpar = toolpar;
            ToolPath = toolPath;
            var extName = Path.GetExtension(toolPath);
            var tagertPath = string.Empty;
            if (extName != null && extName.Trim().Equals(".lnk")) {
                var shell = new WshShell();
                var shortCut = (IWshShortcut) shell.CreateShortcut(ToolPath);
                tagertPath = shortCut.TargetPath;
            }
            else if (extName != null && extName.Trim().Equals(".exe")) {
                tagertPath = ToolPath;
            }
            UrlTB.Text = tagertPath;

            var toolName = Path.GetFileNameWithoutExtension(tagertPath);
            IDTB.Text = $@"Create{toolName}ID";
            NameTB.Text = toolName;
        }

        /// <summary>
        /// </summary>
        public Toolpars Toolpar { get; set; }

        /// <summary>
        /// </summary>
        public string ToolPath { get; set; }

        private void BtnOK_Click(object sender, EventArgs e) {
            var bt = new BuildeType {
                Id = IDTB.Text.Trim(),
                Name = NameTB.Text.Trim(),
                IsTools = "True",
                ShowIcon = checkBox1.Checked ? "True" : "False",
                ShowCheckedBox = "False",
                Description = DesTb.Text.Trim(),
                Url = UrlTB.Text
            };
            var buildeItems = new List<BuildeType>() ;
            Toolpar.BuilderEntity.BuildeTypies.ToList().ForEach(item => {
                if (!item.Id.Equals("MYTools"))
                    return;
                buildeItems.AddRange(item.BuildeItems);
                buildeItems.Add(bt);
                item.BuildeItems = buildeItems.ToArray();
            });


            var xmlPath = PathTools.GetSettingPath("BuildeEntity", Toolpar.ModelType);
            ReadToEntityTools.SaveSerialize(Toolpar.BuilderEntity, Toolpar.ModelType, xmlPath);

            IconTools.SetExeIcon(bt.Url);
            DialogResult = DialogResult.OK;
        }

        private void CreateToolForm_Load(object sender, EventArgs e) {
            var tagertPath = UrlTB.Text;
            var existed = false;
            var buildeTypies = Toolpar.BuilderEntity.BuildeTypies.ToList();
            foreach (var item in buildeTypies) {

                if (!item.Id.Equals("MYTools"))
                    break;
                existed = item.BuildeItems.Any(builderItem => {
                    var equals = builderItem?.Url?.Equals(tagertPath);
                    return equals != null && (bool) equals;
                });
                if (existed)
                    break;
            }
            if (!existed)
                return;
            DialogResult = DialogResult.Cancel;
            Dispose();
        }
    }
}