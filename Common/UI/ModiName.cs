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

        
    }
}
