using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VSTool
{
    public partial class ModiName : Form
    {
        VSTOOL Form1;
        public ModiName(VSTOOL f1)
        {
            InitializeComponent();
            Form1 = f1;
        }

     

        private void ModiName_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if ((txt01.Text == "" && txt01.Visible == true) || (txt02.Text == "" && txt02.Visible == true))
            //{
            //    MessageBox.Show("请确认是否");
            //}
          
           
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
