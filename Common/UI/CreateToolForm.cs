using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Implement.Entity;
using Common.Implement.Tools;
using IWshRuntimeLibrary;
using Microsoft.Office.Interop.Word;
using File = System.IO.File;

namespace Common.Implement.UI
{
    public partial class CreateToolForm : Form
    {
        public CreateToolForm()
        {
            InitializeComponent();
        }

        public CreateToolForm(string toolPath, Toolpars toolpar)
        {
            InitializeComponent();
            Toolpar = toolpar;
            ToolPath = toolPath;
            var extName = Path.GetExtension(toolPath);
            var tagertPath = string.Empty;
            if (extName != null && extName.Trim().Equals(".lnk"))
            {
                var shell = new WshShell();
                var shortCut = (IWshShortcut)shell.CreateShortcut(ToolPath);
                tagertPath = shortCut.TargetPath;
            }
            else if (extName != null && extName.Trim().Equals(".exe"))
            {
                tagertPath = ToolPath;
            }
            UrlTB.Text = tagertPath;
           
            var toolName = Path.GetFileNameWithoutExtension(tagertPath);
            IDTB.Text = $@"Create{toolName}ID";
            NameTB.Text = toolName;
        }

        public Toolpars Toolpar { get; set; }
        public string ToolPath { get; set; }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            var extName = Path.GetFileNameWithoutExtension(ToolPath);

            var bt = new BuildeType
            {
                Id = IDTB.Text.Trim(),
                Name = NameTB.Text.Trim(),
                IsTools = "True",
                ShowIcon = checkBox1.Checked ? "True" : "False",
                ShowCheckedBox = "False",
                Description = DesTb.Text.Trim(),
                Url = UrlTB.Text
            };
            var buildeItems = new List<BuildeType>(){ bt };
            Toolpar.BuilderEntity.BuildeTypies.ToList().ForEach(item => {
                if (!item.Id.Equals("MYTools"))
                    return;
                buildeItems.AddRange(item.BuildeItems);
                item.BuildeItems = buildeItems.ToArray();
            });
            var text= ReadToEntityTools.XmlSerialize<BuildeEntity>(Toolpar.BuilderEntity);
            var xmlPath = AppDomain.CurrentDomain.BaseDirectory + @"Config\BuildeEntity.xml";
            File.WriteAllText(xmlPath, text, Encoding.UTF8);
            //XmlTools.ModiXml(AppDomain.CurrentDomain.BaseDirectory + @"Config\BuildeEntity.xml",
            //    "MYTools", bt);
            IconTool.SetExeIcon(bt.Url);
            DialogResult = DialogResult.OK;


        }

        private void CreateToolForm_Load(object sender, EventArgs e)
        {
            var tagertPath =UrlTB.Text;
            var existed = false;
            Toolpar.BuilderEntity.BuildeTypies.ToList().ForEach(item => {
                if (!item.Id.Equals("MYTools"))
                    return;
                existed = item.BuildeItems.Any(builderItem => {
                    var @equals = builderItem?.Url?.Equals(tagertPath);
                    return @equals != null && (bool)@equals;
                });
                if (existed)
                    return;

            });
            if (existed)
            {
                DialogResult = DialogResult.Cancel;
                this.Dispose();
            }
        }
    }
}