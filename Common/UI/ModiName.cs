using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Implement.Entity;

namespace Common.Implement.UI
{
    public partial class ModiName : Form
    {
       
        public ModiName()
        {
            InitializeComponent();
          
        }

        public BuildeType BuildeType { get; set; }
        public ModiName(BuildeType itemBuildeType, toolpars toolpars) {
            InitializeComponent();
            this.BuildeType = itemBuildeType;
            this.Text = BuildeType.Name;
            this._toolpars = toolpars;
            FileInfos = new List<Entity.FileInfos>();
        }

        private toolpars _toolpars;
        public List<FileInfos> FileInfos { get; set; }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if ((txt01.Text == "" && txt01.Visible == true) || (txt02.Text == "" && txt02.Visible == true))
            {
                MessageBox.Show("不可为空");
            }
            else
            {
                var fileMapping = _toolpars.FileMappingEntity;
                var fileInfo = fileMapping.MappingItems.ToList().FirstOrDefault(filmap =>
                    filmap.Id.Equals(BuildeType.Id)
                );
                if (BuildeType.PartId != null
                    && !BuildeType.PartId.Equals(string.Empty)) {
                    fileInfo = fileMapping.MappingItems.ToList().FirstOrDefault(filmap =>
                        filmap.Id.Equals(BuildeType.PartId));
                }
               
                if (fileInfo?.Paths != null )
                {
                    if (fileInfo.Paths.Length == 1) {
                        FileInfos fileinfo = new FileInfos();
                        fileinfo.actionNameFiled = "";
                        fileinfo.ClassName = txt01.Text;
                        fileinfo.FileName = txt01.Text;
                        fileinfo.FunctionName = txt02.Text;
                        var path = fileInfo.Paths[0];
                        fileinfo.BasePath = fileInfo.Paths[0];
                        var fromPath = _toolpars.MVSToolpath + @"\Template\" + path;
                        fileinfo.FromPath = fromPath;
                        var oldFilePath = Path.GetFileNameWithoutExtension(path);
                        var newFilePath = path.Replace(oldFilePath, fileinfo.FileName);
                        fileinfo.ToPath = _toolpars.GToIni+ @"\" + newFilePath;
                        if (BuildeType.PartId != null
                            && !BuildeType.PartId.Equals(string.Empty))
                        {
                            fileinfo.PartId = BuildeType.PartId;
                            fileinfo.Id = BuildeType.Id;
                            fileinfo.IsMerge = BuildeType.IsMerge;

                        }
                        FileInfos.Add(fileinfo);
                    }
                    else {
                        fileInfo.Paths.ToList().ForEach(path => {
                            string ClassNameFiled = Path.GetFileName(path);
                            FileInfos fileinfo = new FileInfos();
                            fileinfo.actionNameFiled = "";
                            fileinfo.ClassName = ClassNameFiled;
                            fileinfo.FileName = ClassNameFiled;
                            fileinfo.FunctionName = "";
                            fileinfo.BasePath = path;
                            var fromPath = _toolpars.MVSToolpath + @"\Template\" + path;
                            fileinfo.FromPath = fromPath;
                            var oldFilePath = Path.GetFileNameWithoutExtension(path);
                            var newFilePath = path.Replace(oldFilePath, fileinfo.FileName);

                            fileinfo.ToPath = _toolpars.GToIni + @"\" + newFilePath;
                            
                            FileInfos.Add(fileinfo);
                      
                        });
                      
                    }
                }
                this.DialogResult = DialogResult.OK;
            }
        }

        private void ModiName_Load(object sender, EventArgs e)
        {
            //if (this.BuildeType.Id.Equals("Batch")) {

            //    List< BatchInfo > batchInfo = new List<BatchInfo>();
            //    batchInfo.AddRange(new [] {
            //       new BatchInfo(){Id = "FreeBatchService",Name = "普通批次"},
            //       new BatchInfo(){Id = "GuideInterceptor",Name = "向导式批次"}
            //    });
            //    BatchcomBox.DataSource = batchInfo;
            //    BatchcomBox.ValueMember = "Id";
            //    BatchcomBox.DisplayMember = "Name";
            //}
            try {
                this.txt01.Text = string.Format("Create{0}", this.BuildeType.Id);
                this.txt02.Text = string.Format("Create{0}", this.BuildeType.Id);

                this.btnOK.Focus();
            }
            catch { }

        }

        
        private void txt01_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }
    }
    public class ActionPoint
    {
        public string Id { get; set; }
        public string Name { get; set; }

    }
}
