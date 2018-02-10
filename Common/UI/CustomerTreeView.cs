using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Common.Implement.Properties;

namespace Common.Implement {
    public partial class CustomerTreeView : TreeView {
        public CustomerTreeView() {
            Init();
            // 设置TreeView为自己绘制文本和图标并绑定相关的事件  
            DrawMode = TreeViewDrawMode.OwnerDrawText;
            DrawNode += GTreeView_DrawNode;
            MouseUp += GTreeView_MouseUp;
        }

        #region 控件属性  

        #region 引用函数  

        #endregion

        #region 属性

        //选择TreeView TreeNode时的背景色  

        public Brush BackgroundBrush { get; set; }

        private Color _OverForeColor = Color.LightBlue;

        /// <summary>
        ///     鼠标悬停色
        /// </summary>
        [Description("鼠标悬停色")]
        [Category("XOProperty")]
        public Color OverForeColor {
            get => _OverForeColor;
            set {
                if (_OverForeColor != value) {
                    _OverForeColor = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        ///     节点高度
        /// </summary>
        public new int ItemHeight {
            get => base.ItemHeight;
            set {
                if (base.ItemHeight != value && value >= 20) {
                    base.ItemHeight = value;
                    Invalidate();
                }
            }
        }

        //TreeView中TreeNode的节点显示图标的大小  

        public Size NodeImageSize { get; set; }

        //节点显示图标离左边界的位置  

        public int NodeOffset { get; set; }


        public int CheckBoxImageWidth { get; } = 25;
        public int CheckBoxImageHeight { get; } = 25;
        public Image ImgChecked { get; private set; }
        public Image ImgUnchecked { get; private set; }
        public Image ImgRbChecked { get; private set; }
        public Image ImgRbUnchecked { get; private set; }
        public Image ImgDown { get; private set; }

        public Image ImgLeft { get; private set; }
        // private System.ComponentModel.IContainer components;

        #endregion

        #endregion

        #region Event Hanlder Methods  

        private void Init() {
            //采用双缓冲技术的控件必需的设置
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //this.SetStyle(ControlStyles.UserPaint, true);
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.AllPaintingInWmPaint, true);


            //设置绘制TreeNode的模式  
            DrawMode = TreeViewDrawMode.OwnerDrawAll;
            ItemHeight = 25;
            ForeColor = Color.LightGray;
            HotTracking = true;

            //不支持CheckedBox  
            CheckBoxes = false;
            // 初始化CheckBox相关的图片  
            ImgUnchecked = Resource.TreeNodeUnchecked;
            ImgChecked = Resource.TreeNodechecked;
            ImgRbUnchecked = Resource.TreeNodeRBUnchecked;
            ImgRbChecked = Resource.TreeNodeRBchecked;
            ImgDown = Resource.DownIcon;
            ImgLeft = Resource.LeftIcon;
            NodeOffset = 0;
            NodeImageSize = new Size(CheckBoxImageWidth, CheckBoxImageHeight); //图片大小  
            //设置默认BackgroundBrush  
            BackgroundBrush = new SolidBrush(ForeColor);
            //不显示TreeNode前的“+”和“-”按钮  
            ShowPlusMinus = false;
        }

        /// <summary>
        ///     判断如果点击的是Checkbox图片，则改变节点的IsCheck属性值
        ///     需要注意的是同一父节点下只允许一个RadioButton类型的节点为选中状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GTreeView_MouseUp(object sender, MouseEventArgs e) {
            var node = GetNodeAt(e.X, e.Y) as GTreeNode;
            if (node == null || !node.CheckBoxVisible)
                return;
            var checkboxImgRect = new Rectangle(node.Bounds.X, node.Bounds.Y, CheckBoxImageWidth,
                CheckBoxImageHeight); //节点区域  

            // 如果点击的是checkbox图片  
            if (checkboxImgRect.Contains(e.X, e.Y)) {
                node.Checked = !node.Checked;

                // 如果是单选，则设置同级别的其它单选项为unchecked.  
                if (node.Parent != null && node.CheckBoxStyle == GTreeNode.CheckBoxStyleEnum.RadioButton)
                    foreach (TreeNode siblingNode in node.Parent.Nodes) {
                        var siblingGNode = siblingNode as GTreeNode;
                        if (siblingGNode == null)
                            continue;
                        if (siblingGNode.Name != node.Name &&
                            siblingGNode.CheckBoxStyle == GTreeNode.CheckBoxStyleEnum.RadioButton &&
                            siblingGNode.Checked)
                            siblingGNode.Checked = false;
                    }
            }
        }

        /// <summary>
        ///     绘制节点的图标和文字，样式为Icon+CheckBox+NodeText
        ///     Checkbox支持节点级别的单选、复选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GTreeView_DrawNode(object sender, DrawTreeNodeEventArgs e) {
            var node = e.Node as GTreeNode;
            if (node == null)
                return;

            if (node.IsVisible) {
                var nodeRect = e.Node.Bounds;

                //绘制节点图片  
                var pt = new Point(nodeRect.X + NodeOffset, nodeRect.Y);
                var rt = new Rectangle(pt, NodeImageSize);
                if (node.Nodes.Count != 0) {
                    if (node.IsExpanded)
                        e.Graphics.DrawImage(ImgDown, rt);
                    else
                        e.Graphics.DrawImage(ImgLeft, rt);
                    rt.X += 15;
                }


                // 如果需要显示CheckBox,则绘制  
                if (node.CheckBoxVisible && node.Level > 0) {
                    //var drawPt = new Point(nodeRect.Location.X, nodeRect.Location.Y); //绘制图标的起始位置  
                    //var imgRect = new Rectangle(drawPt, NodeImageSize);
                    var graphics = e.Graphics;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                    if (e.Node.Checked) {
                        if (node.CheckBoxStyle == GTreeNode.CheckBoxStyleEnum.CheckBox)
                            graphics.DrawImage(ImgChecked, rt);
                        else
                            graphics.DrawImage(ImgRbChecked, rt);
                    }
                    else {
                        if (node.CheckBoxStyle == GTreeNode.CheckBoxStyleEnum.CheckBox)
                            graphics.DrawImage(ImgUnchecked, rt);
                        else
                            graphics.DrawImage(ImgRbUnchecked, rt);
                    }
                }


                //-----------------------绘制文本 -------------------------------  
                Rectangle textRec;
                if (node.CheckBoxVisible)

                    textRec = new Rectangle(rt.X + CheckBoxImageWidth + 2, nodeRect.Y + 3, nodeRect.Width,
                        nodeRect.Height);
                else
                    textRec = new Rectangle(rt.X + 15, nodeRect.Y + 3, nodeRect.Width,
                        nodeRect.Height);


                var nodeFont = e.Node.NodeFont;
                if (nodeFont == null)
                    nodeFont = ((TreeView) sender).Font;
                var textBrush = SystemBrushes.WindowText;
                var graphic = e.Graphics;
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                var BackgroundPen = new Pen(Color.FromArgb(130, 249, 252), 1);
                var BackBrush = new SolidBrush(Color.FromArgb(90, Color.FromArgb(205, 226, 252)));
                if ((e.State & TreeNodeStates.Focused) != 0) {
                    BackBrush = new SolidBrush(Color.FromArgb(90, Color.FromArgb(205, 226, 252)));
                    graphic.FillRectangle(BackBrush, 2, nodeRect.Y, Width, nodeRect.Height - 1);
                    e.Graphics.DrawRectangle(BackgroundPen, 0, nodeRect.Y, Width, nodeRect.Height - 1);
                    //graphic.FillRectangle(BackBrush, nodeRect.X + checkBoxImageWidth, nodeRect.Y, 2, nodeRect.Height);
                }
                else if ((e.State & TreeNodeStates.Hot) > 0) {
                    BackBrush = new SolidBrush(Color.FromArgb(90, Color.FromArgb(205, 226, 252)));
                    BackgroundPen = new Pen(Color.FromArgb(130, 249, 252), 1);
                    graphic.FillRectangle(BackBrush, 2, nodeRect.Y, Width, nodeRect.Height - 1);
                    e.Graphics.DrawRectangle(BackgroundPen, 1, nodeRect.Y, Width - 2, nodeRect.Height - 1);
                }
             
                BackgroundBrush = new SolidBrush(ForeColor);
                // public void DrawLines(Pen pen, Point[] points);
                // graphic.FillRectangle(BackBrush, nodeRect.X + checkBoxImageWidth, nodeRect.Y, 2, nodeRect.Height);
                graphic.DrawString(e.Node.Text, nodeFont, BackgroundBrush, Rectangle.Inflate(textRec, 2, -1));
                BackgroundPen.Dispose();
                BackBrush.Dispose();
            }
        }

        //返回TreeView中TreeNode的整行区域  
        private Rectangle NodeBounds(TreeNode node) {
            // Set the return value to the normal node bounds.  
            var bounds = node.Bounds;

            //if (node.Tag != null)  
            //{  
            //    // Retrieve a Graphics object from the TreeView handle  
            //    // and use it to calculate the display width of the tag.  
            //    Graphics g = this.CreateGraphics();  
            //    int tagWidth = (int)g.MeasureString(node.Tag.ToString(), NodeFont).Width + 6;  

            //    // Adjust the node bounds using the calculated value.  
            //    bounds.Offset(tagWidth / 2, 0);  
            //    bounds = Rectangle.Inflate(bounds, tagWidth / 2, 0);  
            //    g.Dispose();  
            //}  

            bounds.Width = Width;

            return bounds;
        }

        /// <summary>
        ///     节点被选中后，如果节点有事件处理程序，则调用
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAfterSelect(TreeViewEventArgs e) {
            var gNode = e.Node as GTreeNode;
            //if (gNode != null && gNode.NodeSelected != null)
            //{
            //    TreeViewEventArgs arg = new TreeViewEventArgs(e.Node);
            //    gNode.NodeSelected(this, arg);
            //    return;
            //}
            base.OnAfterSelect(e);
        }

        #endregion
    }

    public class GTreeNode : TreeNode {
        public delegate void NodeSelected(object sender, TreeViewEventArgs e);

        public enum CheckBoxStyleEnum {
            CheckBox,
            RadioButton
        }

        public GTreeNode(string text) : base(text) {
        }

        public bool CheckBoxVisible { get; set; }
        public CheckBoxStyleEnum CheckBoxStyle { get; set; }
        public string Description { get; set; }
        public event NodeSelected NodeSelecteds;
    }
}