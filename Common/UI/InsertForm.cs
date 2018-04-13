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
    public partial class InsertForm : Form
    {
        public Toolpars Toolpars { get; set; }
        public InsertForm(Toolpars toolpars)
        {
            InitializeComponent();
            this.Toolpars = toolpars;
        }

        public InsertForm()
        {
            InitializeComponent();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                
            }
        }
    }
}
