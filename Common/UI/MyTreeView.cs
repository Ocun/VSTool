using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Common.Implement.Entity;
using Common.Implement.Properties;

namespace Common.Implement.UI {
    public partial class MyTreeView : TreeView {
        #region 屬性

        //显示字体  
        private Font _NodeFont;

        public Font NodeFont {
            get { return _NodeFont; }
            set { _NodeFont = value; }
        }

        //选择TreeView TreeNode时的背景色  
        private Brush _BackgrountBrush;

        public Brush BackgroundBrush {
            get { return _BackgrountBrush; }
            set { _BackgrountBrush = value; }
        }

        //选择TreeView TreeNode时背景色的边框画笔  
        private Pen _BackgroundPen;

        public Pen BackgroundPen {
            get { return _BackgroundPen; }
            set { _BackgroundPen = value; }
        }

        //TreeView中TreeNode的节点显示图标的大小  
        private Size _NodeImageSize;

        public Size NodeImageSize {
            get { return _NodeImageSize; }
            set { _NodeImageSize = value; }
        }

        //节点显示图标离左边界的位置  
        private int _NodeOffset;

        public int NodeOffset {
            get { return _NodeOffset; }
            set { _NodeOffset = value; }
        }

        public int ImageWidth {
            get => _imageWidth;
            set => _imageWidth = value;
        }

        public int ImageHeight {
            get { return _imageHeight; }
            set { _imageHeight = value; }
        }

        #endregion

        private int _imageWidth = 25;
        private int _imageHeight = 25;
        private Image imgChecked;
        private Image imgUnchecked;
        private Image imgRBChecked;
        private Image imgRBUnchecked;
        private Image imgLeft;
        private Image imaDown;

        void initPars() {
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.AllPaintingInWmPaint, true);

            //不显示树形节点显示连接线  
            this.ShowLines = false;

            //设置绘制TreeNode的模式  
            this.DrawMode = TreeViewDrawMode.OwnerDrawAll;

            //不显示TreeNode前的“+”和“-”按钮  
            this.ShowPlusMinus = false;
            //this.HotTracking = true;
            //不支持CheckedBox  
            this.CheckBoxes = false;
            //设置默认BackgroundBrush  
            BackgroundBrush = new SolidBrush(Color.FromArgb(90, Color.FromArgb(205, 226, 252)));

            //设置默认BackgroundPen  
            BackgroundPen = new Pen(Color.FromArgb(130, 249, 252), 1);
            //设置默认节点显示图标便宜位置  
            NodeOffset = 0;
            //this.ItemHeight = 50;
            NodeImageSize = new Size(ImageWidth, ImageHeight);
            imgUnchecked = Resource.TreeNodeUnchecked;
            imgChecked = Resource.TreeNodeChecked;
            imgRBUnchecked = Resource.TreeNodeUnchecked;
            imgRBChecked = Resource.TreeNodeChecked;
            imgLeft = Resource.down_arrow;
            imaDown = Resource.right_arrow;
            // 设置TreeView为自己绘制文本和图标并绑定相关的事件  
            this.DrawMode = TreeViewDrawMode.OwnerDrawText;
            this.DrawNode += new DrawTreeNodeEventHandler(TreeView_DrawNode);
            this.MouseUp += new MouseEventHandler(TreeView_MouseUp);
        }

        public MyTreeView() {
            InitializeComponent();
            //this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |
            //              ControlStyles.AllPaintingInWmPaint, true);
            //this.UpdateStyles();
            initPars();
        }
        

        private void TreeView_MouseUp(object sender, MouseEventArgs e) {
          
            MyTreeNode node = GetNodeAt(e.X, e.Y) as MyTreeNode;
            if (node == null
                || !node.CheckBoxVisible
                || !node.IsVisible) {
                return;
            }

            //设置Image绘制Rectangle  
            Rectangle checkboxImgRect = new Rectangle(node.Bounds.X, node.Bounds.Y, ImageWidth, ImageHeight); //节点区域  


            // 如果点击的是checkbox图片  
            if (checkboxImgRect.Contains(e.X, e.Y)) {
                node.Checked = !node.Checked;

                // 如果是单选，则设置同级别的其它单选项为unchecked.  
                if (node.Parent != null
                    && node.CheckBoxStyle == MyTreeNode.CheckBoxStyleEnum.RadioButton) {
                    foreach (TreeNode siblingNode in node.Parent.Nodes) {
                        var siblingGNode = siblingNode as MyTreeNode;
                        if (siblingGNode == null) {
                            continue;
                        }
                        if (siblingGNode.Name != node.Name
                            && siblingGNode.CheckBoxStyle == MyTreeNode.CheckBoxStyleEnum.RadioButton
                            && siblingGNode.Checked) {
                            siblingGNode.Checked = false;
                        }
                    }
                }
            }
        }
   
        private void TreeView_DrawNode(object sender, DrawTreeNodeEventArgs e) {
            MyTreeNode node = e.Node as MyTreeNode;
            if (node == null) {
                return;
            }
            if (node.IsVisible) {
                if (node.buildeType.Description != null
                    && !(node.buildeType.Description.Equals(string.Empty))
                    ) {
                    this.ItemHeight = 50;
                }
                //else {
                //    this.ItemHeight = 25;
                //}
                //设置Image绘制Rectangle  
                Point pt = new Point(node.Bounds.X + NodeOffset, node.Bounds.Y);
                Rectangle nodeRect = new Rectangle(pt, NodeImageSize);
                if (node.Nodes.Count != 0) {
                    if (node.IsExpanded) {
                        e.Graphics.DrawImage(imgLeft, nodeRect);
                    }
                    else {
                        e.Graphics.DrawImage(imaDown, nodeRect);
                    }
                    nodeRect.X += 15;
                }


                // 如果需要显示CheckBox,则绘制  
                if (node.CheckBoxVisible) {
                    Point drawPt = new Point(nodeRect.X, nodeRect.Y + 2); //绘制图标的起始位置  
                    Rectangle imgRect = new Rectangle(drawPt, NodeImageSize);

                    
                    if (node.Checked || (node.buildeType.Checked != null
                                         && node.buildeType.Checked.Equals("True"))) {
                        if (node.CheckBoxStyle == MyTreeNode.CheckBoxStyleEnum.CheckBox) {
                            e.Graphics.DrawImage(imgChecked, imgRect);
                        }
                        else {
                            e.Graphics.DrawImage(imgRBChecked, imgRect);
                        }
                    }
                    else {
                        if (node.CheckBoxStyle == MyTreeNode.CheckBoxStyleEnum.CheckBox) {
                            e.Graphics.DrawImage(imgUnchecked, imgRect);
                        }
                        else {
                            e.Graphics.DrawImage(imgRBUnchecked, imgRect);
                        }
                    }
                }


                //-----------------------绘制文本 -------------------------------  
                Rectangle textRec;
                if (node.CheckBoxVisible) {
                    textRec = new Rectangle(nodeRect.X + NodeImageSize.Width + 15, nodeRect.Y, this.Width - 3,
                        node.Bounds.Height);
                }
                else {
                    textRec = new Rectangle(nodeRect.X + 15, nodeRect.Y, this.Width - 3, node.Bounds.Height);
                    ;
                }

                Font nodeFont = e.Node.NodeFont;
                if (nodeFont == null)
                    nodeFont = ((TreeView) sender).Font;
                Brush textBrush = SystemBrushes.WindowText;
                Graphics graphics = e.Graphics;
                //graphics.Clear(this.BackColor);
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                //Pen newPen = new Pen(Color.FromArgb(90, Color.FromArgb(205, 226, 252)));
                Pen newPen = new Pen(Color.FromArgb(90, Color.FromArgb(205, 226, 252)));
                if ((e.State & TreeNodeStates.Selected) != 0) {
                    graphics.FillRectangle(new SolidBrush(Color.FromArgb(90, Color.FromArgb(205, 226, 252))), 2,
                   // graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, Color.FromArgb(255, 255, 255))), 2,
                        node.Bounds.Y, this.Width, this.ItemHeight - 1);
                    ////绘制TreeNode选择后的边框线条  
                    //graphics.DrawRectangle(BackgroundPen, 0, node.Bounds.Y, this.Width - 2, this.ItemHeight - 1);
                    graphics.DrawRectangle(newPen, 0, node.Bounds.Y, this.Width - 2, this.ItemHeight - 1);
                }
                else if ((e.State & TreeNodeStates.Hot) > 0) {
                    graphics.FillRectangle(BackgroundBrush, 2, node.Bounds.Y, this.Width, this.ItemHeight - 1);
                    ////绘制TreeNode选择后的边框线条  
                    graphics.DrawRectangle(BackgroundPen, 1, node.Bounds.Y, this.Width - 3, this.ItemHeight - 1);
                }

                graphics.DrawString(node.Text, nodeFont, new SolidBrush(this.ForeColor),
                    Rectangle.Inflate(textRec, 2, -2));
                if (node.buildeType.Description != null
                    && !(node.buildeType.Description.Equals(string.Empty))) {
                    textRec.Y += 25;
                    var subNode = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular,
                        System.Drawing.GraphicsUnit.Point, ((byte) (134)));
                    ;
                    graphics.DrawString(node.buildeType.Description, subNode, new SolidBrush(this.ForeColor),
                        Rectangle.Inflate(textRec, 2, -2));
                }

                // 

                graphics.Dispose();
            }
        }


        /// <summary>  
        /// 节点被选中后，如果节点有事件处理程序，则调用   
        /// </summary>  
        /// <param name="e"></param>  
        protected override void OnAfterSelect(TreeViewEventArgs e) {
            MyTreeNode gNode = e.Node as MyTreeNode;
            //if (gNode != null && gNode.IsSelected)
            //{
            //    TreeViewEventArgs arg = new TreeViewEventArgs(e.Node);
            //   // gNode.SelectedNodeChanged(this, arg);
            //    return;
            //}
            base.OnAfterSelect(e);
        }

        public delegate void SetAutoScrollHandler(object sender,int upAndDown);

        public event SetAutoScrollHandler SetAutoScrollEvent;

        //自定义消息
        protected override void WndProc(ref Message m)
        {
            const int WM_mouse_Click = 0x0201;
            const int WM_mouse_double_click = 0x0203;
            const int WM_mouse_move = 0x0200;
            const int WM_mouse_move_out = 0x02A3;
            const int WM_mouse_wheel= 0x020A;
            const int WM_MOUSE_DOWN = 0x0210;
           #region 日后参考
		    //if (m.Msg == WM_mouse_Click)//单击  
            //{
            //    int wparam = m.LParam.ToInt32();
            //    Point point = new Point(
            //        LOWORD(wparam),
            //        HIWORD(wparam));
            //    //point = PointToClient(point);  
            //    TreeNode tn = this.GetNodeAt(point);
            //    if (tn == null)
            //    {
            //        base.WndProc(ref m);
            //        return;
            //    }
            //    if (tn.Level == 0)
            //    {
            //        if (tn.IsExpanded)
            //        {
            //            tn.Collapse();
            //        }
            //        else
            //        {
            //            tn.Expand();
            //        }
                  
            //        this.SelectedNode = tn;
            //        m.Result = IntPtr.Zero;
            //        base.WndProc(ref m);
            //        return;
            //    }
            //    else
            //    {
            //        base.WndProc(ref m);
            //        //tn.IsSelected = true;  
            //        //this.SelectedNode = tn;  
            //    }
            //}
            //else if (m.Msg == WM_mouse_double_click)//双击  
            //{
            //    int wparam = m.LParam.ToInt32();
            //    Point point = new Point(
            //        LOWORD(wparam),
            //        HIWORD(wparam));
            //    //point = PointToClient(point);  
            //    TreeNode tn = this.GetNodeAt(point);
            //    if (tn == null)
            //    {
            //        base.WndProc(ref m);
            //        return;
            //    }
            //    if (tn.Level == 0)
            //    {
            //        m.Result = IntPtr.Zero;
            //        return;
            //    }
            //    else
            //    {
            //        base.WndProc(ref m);
            //    }
            //}
            //else if (m.Msg == WM_mouse_move)//鼠标移动  
            //{
            //    try
            //    {
            //        int wparam = m.LParam.ToInt32();
            //        Point point = new Point(
            //            LOWORD(wparam),
            //            HIWORD(wparam));
            //        //point = PointToClient(point);  
            //        TreeNode tn = this.GetNodeAt(point);
            //        if (tn == null)
            //        {
            //            this.SelectedNode = null;
            //            base.WndProc(ref m);
            //            return;
            //        }
            //        this.SelectedNode = tn;

            //    }
            //    catch { }
            //}
            //else if (m.Msg == WM_mouse_move_out)//鼠标移出 WM_MOUSELEAVE = $02A3;  
            //{
            //    this.SelectedNode = null;
            //    base.WndProc(ref m);
            //    return;
            //} 
	#endregion
            if (m.Msg == WM_mouse_wheel) {
                // TrackedTopNode = this.
                int LParam = m.LParam.ToInt32();
                int HWnd = m.HWnd.ToInt32();
                // int wparam = m.WParam.ToInt32();
                var isup = ((m.WParam.ToInt64()) & 0xFFFF0000) / 0x10000;
                int up = -1;
                if (isup > 30000) {
                    up = 1;
                }

                SetAutoScrollEvent?.Invoke(this,up);

                base.WndProc(ref m);
            }
            else
            {
                base.WndProc(ref m);
            }
            //WM_LBUTTONDOWN = $0201  
            //WM_LBUTTONDBLCLK = $0203;  
          //  Application.DoEvents();
        }
        public static int LOWORD(int value)
        {
            return value & 0xFFFF;
        }
        public static int HIWORD(int value)
        {
            return value >> 16;
        }

        //返回TreeView中TreeNode的整行区域  
        private Rectangle NodeBounds(TreeNode node) {
            // Set the return value to the normal node bounds.  
            Rectangle bounds = node.Bounds;

            bounds.Width = this.Width;

            return bounds;
        }
    }

    public partial class MyTreeNode : TreeNode {
        public bool CheckBoxVisible { get; set; }
        public CheckBoxStyleEnum CheckBoxStyle { get; set; }
        public BuildeType buildeType { get; set; }

        public enum CheckBoxStyleEnum {
            RadioButton,
            CheckBox
        }

        public MyTreeNode(string text) : base(text) {
            buildeType = new BuildeType();
        }
    }
}