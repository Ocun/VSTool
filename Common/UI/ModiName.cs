using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        public ModiName(BuildeType itemBuildeType) {
            InitializeComponent();
            this.BuildeType = itemBuildeType;
            this.Text = BuildeType.Name;
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            if ((txt01.Text == "" && txt01.Visible == true) || (txt02.Text == "" && txt02.Visible == true))
            {
                MessageBox.Show("不可为空");
            }
            else
            {
                this.Close();
            }
        }

        private void ModiName_Load(object sender, EventArgs e)
        {
            if (this.BuildeType.Id.Equals("Batch")) {
                this.BatchcomBox.Visible = true;
                List< BatchInfo > batchInfo = new List<BatchInfo>();
                batchInfo.AddRange(new [] {
                   new BatchInfo(){Id = "FreeBatchService",Name = "普通批次"},
                   new BatchInfo(){Id = "GuideInterceptor",Name = "向导式批次"}
                });
                BatchcomBox.DataSource = batchInfo;
                BatchcomBox.ValueMember = "Id";
                BatchcomBox.DisplayMember = "Name";
            }
        }

        
        private void txt01_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }
    }
    public class BatchInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }

    }
}
