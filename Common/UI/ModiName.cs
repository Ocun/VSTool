// create By 08628 20180411

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Common.Implement.Entity;
using Common.Implement.Properties;

namespace Common.Implement.UI {
    public sealed partial class ModiName : Form {
        private readonly Toolpars _toolpars;

        public ModiName() {
            InitializeComponent();
        }

        public ModiName(BuildeType itemBuildeType, Toolpars toolpars) {
            InitializeComponent();
            BuildeType = itemBuildeType;
            Text = BuildeType.Name;
            _toolpars = toolpars;
            FileInfos = new List<FileInfos>();
        }

        public BuildeType BuildeType { get; set; }
        public List<FileInfos> FileInfos { get; set; }

        private void btnOK_Click(object sender, EventArgs e) {
            if (string.Equals(txt01.Text, string.Empty, StringComparison.Ordinal) && txt01.Visible ||
                string.Equals(txt02.Text, string.Empty, StringComparison.Ordinal) && txt02.Visible) {
                MessageBox.Show(Resource.notNull);
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
                        var fromPath = _toolpars.MVSToolpath + @"\Template\" + path;
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
                            fileinfo.ToPath = _toolpars.GToIni + @"\" + newFilePath;
                        }
                        if (BuildeType.PartId != null
                            && !BuildeType.PartId.Equals(string.Empty)) {
                            fileinfo.PartId = BuildeType.PartId;
                            fileinfo.Id = BuildeType.Id;
                            fileinfo.IsMerge = BuildeType.IsMerge;
                        }
                        FileInfos.Add(fileinfo);
                    }
                    else {
                        fileInfo.Paths.ToList().ForEach(path => {
                            var classNameFiled = Path.GetFileName(path);
                            var fromPath = _toolpars.MVSToolpath + @"\Template\" + path;
                            var fileinfo = new FileInfos {
                                ActionName = "",
                                ClassName = classNameFiled,
                                FileName = classNameFiled,
                                FunctionName = "",
                                BasePath = path,
                                FromPath = fromPath
                            };
                            var oldFilePath = Path.GetFileNameWithoutExtension(path);
                            if (oldFilePath != null) {
                                var newFilePath = path.Replace(oldFilePath, fileinfo.FileName);

                                fileinfo.ToPath = _toolpars.GToIni + @"\" + newFilePath;
                            }

                            FileInfos.Add(fileinfo);
                        });
                    }
                DialogResult = DialogResult.OK;
            }
        }

        private void ModiName_Load(object sender, EventArgs e) {
            try {
                txt01.Text = string.Format(Resource.CreateFileName, BuildeType.Id);
                txt02.Text = string.Format(Resource.CreateFileName, BuildeType.Id);

                btnOK.Focus();
            }
            catch {
                // ignored
            }
        }


        private void txt01_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char) Keys.Enter)
                SendKeys.Send("{tab}");
        }
    }

    public class ActionPoint {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}