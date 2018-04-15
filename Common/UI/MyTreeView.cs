//  create By 08628 20180411

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Common.Implement.Entity;
using Common.Implement.Properties;
using Common.Implement.Tools;

namespace Common.Implement.UI {
    public partial class MyTreeView : TreeView {
        public delegate void SetAutoScrollHandler(object sender, int upAndDown);

        public MyTreeView() {
            InitializeComponent();
            //this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |
            //              ControlStyles.AllPaintingInWmPaint, true);
            //this.UpdateStyles();
            InitPars();
        }


        private void InitPars() {
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.AllPaintingInWmPaint, true);

            //不显示树形节点显示连接线  
            ShowLines = false;

            //设置绘制TreeNode的模式  
            DrawMode = TreeViewDrawMode.OwnerDrawAll;

            //不显示TreeNode前的“+”和“-”按钮  
            ShowPlusMinus = false;
            //this.HotTracking = true;
            //不支持CheckedBox  
            CheckBoxes = false;
            //设置默认BackgroundBrush  
            BackgroundBrush = new SolidBrush(Color.FromArgb(90, Color.FromArgb(205, 226, 252)));

            //设置默认BackgroundPen  
            BackgroundPen = new Pen(Color.FromArgb(130, 249, 252), 1);

            //this.ItemHeight = 50;
            //NodeImageSize = new Size(ImageWidth, ImageHeight);
            _imgUnchecked = Resource.TreeNodeUnchecked;
            _imgChecked = Resource.TreeNodeChecked;
            _imgRbUnchecked = Resource.TreeNodeUnchecked;
            _imgRbChecked = Resource.TreeNodeChecked;
            _imgLeft = Resource.down_arrow;
            _imaDown = Resource.right_arrow;
            // 设置TreeView为自己绘制文本和图标并绑定相关的事件  
            DrawMode = TreeViewDrawMode.OwnerDrawText;
            DrawNode += TreeView_DrawNode;
            MouseUp += TreeView_MouseUp;
        }

        private void TreeView_MouseUp(object sender, MouseEventArgs e) {
            var node = GetNodeAt(e.X, e.Y) as MyTreeNode;
            if (node == null
                || !node.CheckBoxVisible
                || !node.IsVisible)
                return;

            //设置Image绘制Rectangle  
            var checkboxImgRect = new Rectangle(node.Bounds.X + PaddingSetting.X, node.Bounds.Y + PaddingSetting.Y + 3,
                NodeImageSize.Width, NodeImageSize.Height); //节点区域  


            // 如果点击的是checkbox图片  
            if (!checkboxImgRect.Contains(e.X, e.Y))
                return;
            node.Checked = !node.Checked;

            // 如果是单选，则设置同级别的其它单选项为unchecked.  
            if (node.Parent != null
                && node.CheckBoxStyle == MyTreeNode.CheckBoxStyleEnum.RadioButton)
                foreach (TreeNode siblingNode in node.Parent.Nodes) {
                    var siblingGNode = siblingNode as MyTreeNode;
                    if (siblingGNode == null)
                        continue;
                    if (siblingGNode.Name != node.Name
                        && siblingGNode.CheckBoxStyle == MyTreeNode.CheckBoxStyleEnum.RadioButton
                        && siblingGNode.Checked)
                        siblingGNode.Checked = false;
                }
        }

        private void TreeView_DrawNode(object sender, DrawTreeNodeEventArgs e) {
            var node = e.Node as MyTreeNode;

            if (node == null)
                return;
            if (!node.IsVisible)
                return;
            var graphics = e.Graphics;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            var borderPen = new Pen(Color.FromArgb(90, Color.Gray));

            //设置Image绘制Rectangle  
            var pt = new Point(node.Bounds.X + PaddingSetting.X, node.Bounds.Y + PaddingSetting.Y);


            //Point pt = new Point(node.Bounds.X + PaddingSetting.X, node.Bounds.Y + PaddingSetting.Y);
            var nodeRect = new Rectangle(pt, NodeImageSize);
            if (node.Nodes.Count != 0) {
                if (node.IsExpanded)
                    graphics.DrawImage(_imgLeft, nodeRect);
                else
                    graphics.DrawImage(_imaDown, nodeRect);
                nodeRect.X += 15;
            }

            //-----------------------绘制文本 -------------------------------  
            Rectangle textRec;
            if (node.CheckBoxVisible)
                textRec = new Rectangle(nodeRect.X + NodeImageSize.Width + 15, nodeRect.Y, Width - 30,
                    node.Bounds.Height);
            else
                textRec = new Rectangle(nodeRect.X + 15, nodeRect.Y, Width - 30, node.Bounds.Height);

            var nodeFont = e.Node.NodeFont ?? ((TreeView) sender).Font;

            var newPen = SystemPens.Highlight;
            //  new Pen(Color.FromArgb(90, Color.FromArgb(205, 226, 252)));
            if ((e.State & TreeNodeStates.Selected) != 0) {
                graphics.FillRectangle(new SolidBrush(Color.FromArgb(90, Color.FromArgb(205, 226, 252))), 2,
                    node.Bounds.Y, Width + 2, ItemHeight - 2);

                //绘制TreeNode选择后的边框线条   
                graphics.DrawRectangle(newPen, 0, node.Bounds.Y, Width - 1, ItemHeight - 2);

                //graphics.FillRectangle(new SolidBrush(Color.FromArgb(90, Color.FromArgb(205, 226, 252))), 2,
                //pt.Y - 5, this.Width - 2, this.ItemHeight - PaddingSetting.Y + 3);
                //graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, Color.FromArgb(255, 255, 255))), PaddingSetting.X-1,
                //    pt.Y - 4, this.Width , this.ItemHeight - PaddingSetting.Y +3 );
            }
            else if ((e.State & TreeNodeStates.Hot) > 0) {
                graphics.FillRectangle(BackgroundBrush, 2, node.Bounds.Y, Width - 3, ItemHeight - 2);
                ////绘制TreeNode选择后的边框线条  
                graphics.DrawRectangle(newPen, 0, node.Bounds.Y, Width - 1, ItemHeight - 2);
                //graphics.DrawRectangle(newPen, 1, pt.Y-4 , this.Width -2, this.ItemHeight - PaddingSetting.Y);
                //graphics.FillRectangle(BackgroundBrush, 2, pt.Y - 5, this.Width - 3, this.ItemHeight - PaddingSetting.Y);
            }

            graphics.DrawString(node.Text, nodeFont, new SolidBrush(ForeColor),
                Rectangle.Inflate(textRec, 2, -2));

            if (node.BuildeType.Description != null
                && ShowDescription) {
                textRec.Y += 25;
                var subNode = new Font("宋体", 10F, FontStyle.Regular,
                    GraphicsUnit.Point, 134);

                graphics.DrawString(node.BuildeType.Description, subNode, new SolidBrush(DescriptionColor),
                    Rectangle.Inflate(textRec, 2, -2));
            }
            if (IsCard)
                graphics.DrawRectangle(borderPen, 1, pt.Y - 3, Width - 3, ItemHeight - PaddingSetting.Y);
            // 如果需要显示CheckBox,则绘制  
            if (node.CheckBoxVisible) {
                var drawPt = new Point(nodeRect.X, nodeRect.Y + 3); //绘制图标的起始位置  
                var imgRect = new Rectangle(drawPt, NodeImageSize);


                if (node.Checked || node.BuildeType.Checked != null
                    && node.BuildeType.Checked.Equals("True"))
                    if (node.CheckBoxStyle == MyTreeNode.CheckBoxStyleEnum.CheckBox)
                        graphics.DrawImage(_imgChecked, imgRect);
                    else
                        graphics.DrawImage(_imgRbChecked, imgRect);
                else
                    graphics.DrawImage(
                        node.CheckBoxStyle == MyTreeNode.CheckBoxStyleEnum.CheckBox ? _imgUnchecked : _imgRbUnchecked,
                        imgRect);
            }
            graphics.Dispose();
        }

        public event SetAutoScrollHandler SetAutoScrollEvent;

        protected override void OnNodeMouseDoubleClick(TreeNodeMouseClickEventArgs e) {
            base.OnNodeMouseDoubleClick(e);
            var node = e.Node as MyTreeNode;
            var bt = node?.BuildeType;
            if (bt?.IsTools == null || !bt.IsTools.Equals("True"))
                return;
            if (bt.Url == null || bt.Url.Equals(string.Empty)) {
                MyTool.SetToolsPath(bt);
            }
            else {
                MyTool.OpenTools(bt);
            }
        }

        //自定义消息
        protected override void WndProc(ref Message m) {
            //const int wmMouseClick = 0x0201;
            //const int wmMouseDoubleClick = 0x0203;
            //const int wmMouseMove = 0x0200;
            //const int wmMouseMoveOut = 0x02A3;
            const int wmMouseWheel = 0x020A;
            //const int wmMouseDown = 0x0210;
            const int wmRbuttondown = 0x0204; //按下鼠标右键   

            #region 日后参考

            //if (m.Msg == WM_mouse_Click)//单击  
            //{
            //    int wparam = m.LParam.ToInt32();
            //    Point point = new Point(
            //        LOWORD(wparam),
            //        HIWORD(wparam));
            //    //point = PointToClient(point);  
            //    MyTreeNode tn = this.GetNodeAt(point) as MyTreeNode;
            //    if (tn == null) {
            //        base.WndProc(ref m);
            //        return;
            //    }
            //    else if (tn.buildeType.ReadOnly != null && tn.buildeType.ReadOnly.Equals("True")) {
            //        return;
            //    }
            //    else {
            //        base.WndProc(ref m);
            //        return;
            //    }
            //if (tn.Level == 0)
            //{
            //    if (tn.IsExpanded)
            //    {
            //        tn.Collapse();
            //    }
            //    else
            //    {
            //        tn.Expand();
            //    }
            //    this.SelectedNode = tn;
            //    m.Result = IntPtr.Zero;
            //    return;
            //}
            //else
            //{
            //    base.WndProc(ref m);
            //    //tn.IsSelected = true;  
            //    //this.SelectedNode = tn;  
            //}
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

            switch (m.Msg) {
                case wmRbuttondown:
                    break;
                case wmMouseWheel:
                    // TrackedTopNode = this.
                    // int wparam = m.WParam.ToInt32();
                    var isup = (m.WParam.ToInt64() & 0xFFFF0000) / 0x10000;
                    var up = -1;
                    if (isup > 30000)
                        up = 1;

                    SetAutoScrollEvent?.Invoke(this, up);

                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
            // Application.DoEvents();
        }

        public static int Loword(int value) {
            return value & 0xFFFF;
        }

        public static int Hiword(int value) {
            return value >> 16;
        }

        #region 屬性


        /// <summary>
        /// 显示字体
        /// </summary>
        public Font NodeFont { get; set; }

        /// <summary>
        /// 选择TreeView TreeNode时的背景色 
        /// </summary>
        public Brush BackgroundBrush { get; set; }

        /// <summary>
        /// 选择TreeView TreeNode时背景色的边框画笔  
        /// </summary>
        public Pen BackgroundPen { get; set; }

        /// <summary>
        /// TreeView中TreeNode的节点显示图标的大小 
        /// </summary>
        public Size NodeImageSize { get; set; }


        public Color DescriptionColor {
            get{
            if (_desColor == null)
            {
                _desColor = ForeColor;
            }
            return _desColor;
            }


            set => _desColor= value;
        }

        /// <summary>
        ///     是否以卡片形式绘制
        /// </summary>
        public bool IsCard { get; set; }

        /// <summary>
        ///     是否显示说明
        /// </summary>
        public bool ShowDescription { get; set; }

        public Point PaddingSetting { get; set; }

        private Image _imgChecked;
        private Image _imgUnchecked;
        private Image _imgRbChecked;
        private Image _imgRbUnchecked;
        private Image _imgLeft;
        private Image _imaDown;
        private Color _desColor;

        #endregion
    }

    public class MyTreeNode : TreeNode {
        public enum CheckBoxStyleEnum {
            RadioButton,
            CheckBox
        }

        public MyTreeNode(string text) : base(text) {
            BuildeType = new BuildeType();
        }

        public bool CheckBoxVisible { get; set; }
        public CheckBoxStyleEnum CheckBoxStyle { get; set; }
        public BuildeType BuildeType { get; set; }
    }
}