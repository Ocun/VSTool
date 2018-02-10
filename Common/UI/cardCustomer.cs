using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Common.Implement
{
    public partial class cardCustomer : UserControl
    {
        private Color _borderColor= Color.Gray;


        public int BorderWidth = 1;
        private ButtonBorderStyle _borderLineStyle = ButtonBorderStyle.Solid;

        public cardCustomer()
        {
            InitializeComponent();
        }

        public Color BorderColor {
            get { return _borderColor; }
            set { _borderColor = value; }
        }

        public ButtonBorderStyle BorderLineStyle {
            get { return _borderLineStyle; }
            set { _borderLineStyle = value; }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e) {

            BorderColor = SystemColors.MenuHighlight;

            BorderLineStyle = ButtonBorderStyle.Solid;
            this.panel1.Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                    BorderColor, BorderWidth, BorderLineStyle,
                    BorderColor, BorderWidth, BorderLineStyle,
                    BorderColor, BorderWidth, BorderLineStyle,
                    BorderColor, BorderWidth, BorderLineStyle);
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            BorderColor = Color.Gray;
            BorderLineStyle = ButtonBorderStyle.Solid;
            this.panel1.Refresh();
        }

        private void panel1_Click(object sender, EventArgs e) {
            panel1.BackColor = Color.FromArgb(230,230,230);
            System.Threading.Thread.Sleep(100);
            panel1.BackColor = Color.White;

        }
    }
}
