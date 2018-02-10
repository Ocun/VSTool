using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Common.Implement.Properties;


namespace Common.Implement.UI
{
    public  enum buttonStyle
    {
        /// <summary>
        /// 正常为选中按钮
        /// </summary>
        ButtonNormal,
        /// <summary>
        /// 获得焦点的按钮
        /// </summary>
        ButtonFocuse,
        /// <summary>
        /// 鼠标经过样式
        /// </summary>
        ButtonMouseOver,
        /// <summary>
        /// 获得焦点并鼠标经过
        /// </summary>
        ButtonFocuseAndMouseOver
    }
    public partial class CustmoerButton : Button
    {

        public Color OverColor { get; set; }
        private bool mouseover = false;//鼠标经过
        public CustmoerButton()
        {
            InitializeComponent();
            this.FlatStyle = FlatStyle.Flat;
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            OverColor = SystemColors.HighlightText;
        }

        protected override void OnPaint(PaintEventArgs e) {
            //在这里用自己的方法来绘制Button的外观(其实也就是几个框框)
            Graphics g = e.Graphics;
            g.Clear(this.BackColor);
            Rectangle rect = e.ClipRectangle;
            rect = new Rectangle(rect.X, rect.Y, rect.Width - 1, rect.Height - 2);
            if (mouseover)
            {
                if (Focused)
                {
                    DrawRoundButton(this.Text, g, rect, buttonStyle.ButtonFocuseAndMouseOver);
                    return;
                }
                DrawRoundButton(this.Text, g, rect, buttonStyle.ButtonMouseOver);
                return;
            }
            if (Focused)
            {
                DrawRoundButton(this.Text, g, rect, buttonStyle.ButtonFocuse);
                return;
            }
            DrawRoundButton(this.Text, g, rect, buttonStyle.ButtonNormal);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            mouseover = true;
            base.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            mouseover = false;
            base.OnMouseLeave(e);
        }

        /// <summary> 
        /// 绘制圆角按钮
        /// </summary> 
        /// <param name="Text">要绘制的文字</param>
        /// <param name="g">Graphics 对象</param> 
        /// <param name="rect">要填充的矩形</param> 
        /// <param name="btnStyle"></param>
        public  void DrawRoundButton(string Text, Graphics g, Rectangle rect, buttonStyle btnStyle)
        {
            //g.Clear(Color.White);
            g.SmoothingMode = SmoothingMode.AntiAlias;//消除锯齿
            Rectangle rectangle = rect;
            Brush b = new SolidBrush(Color.Black);
            OverColor = SystemColors.Highlight;
            if (btnStyle == buttonStyle.ButtonFocuse)
            {
                b = new SolidBrush(ColorTranslator.FromHtml("#338FCC"));
               
              
            }
            else if (btnStyle == buttonStyle.ButtonMouseOver)
            {
                b = new SolidBrush(ColorTranslator.FromHtml("#C6A300"));
            }
            else if (btnStyle == buttonStyle.ButtonFocuseAndMouseOver) {
                b = new SolidBrush(ColorTranslator.FromHtml("#C6A300"));
            }
            else {
                OverColor = Color.Black;
            }
          
            Pen p = new Pen(Color.Black, 0.5f);
            p.DashStyle = DashStyle.Dash;
            if (btnStyle == buttonStyle.ButtonFocuse || btnStyle == buttonStyle.ButtonFocuseAndMouseOver)
            {
                var BackBrush = new SolidBrush(Color.FromArgb(90, OverColor));
                g.FillRectangle(BackBrush, 0, rect.Y, Width, rect.Height - 1);
                //g.FillRectangles(p, rectangle);//虚线框
            }
      
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            g.DrawString(Text, this.Font, new SolidBrush(Color.FromArgb(90, OverColor)), rectangle, sf);
            p.Dispose();
            b.Dispose();
            g.SmoothingMode = SmoothingMode.Default;
        }



    }
}
