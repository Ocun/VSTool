// create By 08628 20180411

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Digiwin.Chun.Common.Model;
using Digiwin.Chun.Common.Properties;

namespace Digiwin.Chun.Common.Views {
    /// <summary>
    ///     新项目参数窗体
    /// </summary>
    public partial class ModiName : Form {
        private readonly Toolpars _toolpars;

        /// <summary>
        /// </summary>
        public ModiName() {
            InitializeComponent();
        }

        /// <summary>
        /// </summary>
        /// <param name="itemBuildeType"></param>
        /// <param name="toolpars"></param>
        public ModiName(BuildeType itemBuildeType, Toolpars toolpars) {
            InitializeComponent();
            BuildeType = itemBuildeType;
            Text = BuildeType.Name;
            _toolpars = toolpars;
            FileInfos = new List<FileInfos>();
        }

        /// <summary>
        /// 
        /// </summary>
        public BuildeType BuildeType { get; set; }

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
                var fileMapping = _toolpars.FileMappingEntity;
                var fileInfo = fileMapping.MappingItems.ToList().FirstOrDefault(filmap =>
                    filmap.Id.Equals(BuildeType.Id)
                );
                if (BuildeType.PartId != null
                    && !BuildeType.PartId.Equals(string.Empty))
                    fileInfo = fileMapping.MappingItems.ToList().FirstOrDefault(filmap =>
                        filmap.Id.Equals(BuildeType.PartId));

                if (fileInfo?.Paths != null)
                    if (fileInfo.Paths.Length == 1) {
                        var path = fileInfo.Paths[0];
                        var fromPath = $@"{_toolpars.MvsToolpath}\Template\{path}";
                        var fileinfo = new FileInfos {
                            ActionName = "",
                            ClassName = txt01.Text,
                            FileName = txt01.Text,
                            FunctionName = txt02.Text,
                            BasePath = fileInfo.Paths[0],
                            FromPath = fromPath
                        };
                        var oldFilePath = Path.GetFileNameWithoutExtension(path);
                        if (oldFilePath != null) {
                            var newFilePath = path.Replace(oldFilePath, fileinfo.FileName);
                            fileinfo.ToPath = $@"{_toolpars.FormEntity.TxtToPath}\{newFilePath}";
                        }
                        if (BuildeType.PartId != null
                            && !BuildeType.PartId.Equals(string.Empty)) {
                            fileinfo.PartId = BuildeType.PartId;
                            fileinfo.Id = BuildeType.Id;
                            fileinfo.IsMerge = BuildeType.IsMerge;
                        }
                        if (MergeBox.Checked)
                            fileinfo.IsMerge = "True";
                        FileInfos.Add(fileinfo);
                    }
                    else {
                        fileInfo.Paths.ToList().ForEach(path => {
                            var classNameFiled = Path.GetFileName(path);
                            var fromPath = $@"{_toolpars.MvsToolpath}\Template\{path}";

                            var fileinfo = new FileInfos {
                                ActionName = "",
                                ClassName = classNameFiled,
                                FileName = classNameFiled,
                                FunctionName = "",
                                BasePath = path,
                                FromPath = fromPath
                            };
                            var oldFilePath = Path.GetFileNameWithoutExtension(path);
                            //针对服务，这种解决方法，我认为非常蠢
                            if (BuildeType.Id.Equals("Service"))
                                if (classNameFiled != null && classNameFiled.StartsWith(@"I")) {
                                    fileinfo.ClassName = txt01.Text;
                                    fileinfo.FileName = txt01.Text;
                                }
                                else {
                                    fileinfo.ClassName = txt01.Text.Substring(1);
                                    fileinfo.FileName = txt01.Text.Substring(1);
                                }


                            if (oldFilePath != null) {
                                var newFilePath = path.Replace(oldFilePath, fileinfo.FileName);

                                fileinfo.ToPath = _toolpars.FormEntity.TxtToPath + @"\" + newFilePath;
                            }


                            if (MergeBox.Checked)
                                fileinfo.IsMerge = "True";
                            FileInfos.Add(fileinfo);
                        });
                    }
                DialogResult = DialogResult.OK;
            }
        }

        private void ModiName_Load(object sender, EventArgs e) {
            try {
                txt01.Text = string.Format(Resources.CreateFileName, BuildeType.Id);
                txt02.Text = string.Format(Resources.CreateFileName, BuildeType.Id);
                if (BuildeType.Id.Equals("Service")) {
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