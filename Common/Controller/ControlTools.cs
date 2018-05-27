// create By 08628 20180411

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Digiwin.Chun.Common.Model;
using Digiwin.Chun.Common.Properties;
using Digiwin.Chun.Common.Views;

namespace Digiwin.Chun.Common.Controller {
    /// <summary>
    ///     事件帮助类
    /// </summary>
    public class ControlTools {
        #region 主界面控件属性
        private static SplitContainer _splitContainer1;
        private static SplitContainer _splitContainer2;
        private static MyPanel _scrollPanel;
        private static MyTreeView _myTreeView1;
        private static MyTreeView _myTreeView5;
        private static MyTreeView _treeView1;
        private static TextBox _txtPkGpath;
        private static CheckBox _modiCkb;
        private static MyPanel _headerPanel;
        private static readonly Toolpars Toolpars = MyTools.Toolpars;


        private static List<MyTreeView> _myTreeViews;

        private static List<SplitContainer> _mySplitContainers;

        /// <summary>
        /// </summary>
        public static Form VsForm { get; set; }

        /// <summary>
        ///     窗体控件SplitContainer1
        /// </summary>
        public static SplitContainer SplitContainer1 => _splitContainer1 ??
                                                        (_splitContainer1 =
                                                            GetControlByName<SplitContainer>("SplitContainer1"));

        /// <summary>
        ///     窗体控件SplitContainer2
        /// </summary>
        public static SplitContainer SplitContainer2 => _splitContainer2 ??
                                                        (_splitContainer2 =
                                                            GetControlByName<SplitContainer>("SplitContainer2"));

        /// <summary>
        ///     窗体控件ScrollPanel
        /// </summary>
        public static MyPanel ScrollPanel => _scrollPanel ?? (_scrollPanel = GetControlByName<MyPanel>("ScrollPanel"));

        /// <summary>
        ///     窗体控件MyTreeView1
        /// </summary>
        public static MyTreeView NavTreeView => _myTreeView1 ??
                                                (_myTreeView1 = GetControlByName<MyTreeView>("NavTreeView"));

        /// <summary>
        ///     窗体控件myTreeView5
        /// </summary>
        public static MyTreeView RighteTreeView => _myTreeView5 ??
                                                   (_myTreeView5 = GetControlByName<MyTreeView>("RighteTreeView"));

        /// <summary>
        ///     窗体控件treeView1
        /// </summary>
        public static MyTreeView TreeView1 => _treeView1 ?? (_treeView1 = GetControlByName<MyTreeView>("TreeView1"));

        /// <summary>
        ///     窗体控件TxtPKGpath
        /// </summary>
        public static TextBox TxtPkGpath => _txtPkGpath ?? (_txtPkGpath = GetControlByName<TextBox>("TxtPkGpath"));

        /// <summary>
        ///     窗体控件TxtPKGpath
        /// </summary>
        public static CheckBox ModiCkb => _modiCkb ?? (_modiCkb = GetControlByName<CheckBox>("ModiCkb"));

        /// <summary>
        /// </summary>
        public static MyPanel HeaderPanel => _headerPanel ?? (_headerPanel = GetControlByName<MyPanel>("HeaderPanel"));

        /// <summary>
        ///     当前创建TreeView
        /// </summary>
        public static List<MyTreeView> MyTreeViews => _myTreeViews ??
                                                      (_myTreeViews =
                                                          MyTreeViewTools.GetTreeViews(ScrollPanel.Controls[0]));

        /// <summary>
        ///     当前创建SplitContainers
        /// </summary>
        public static List<SplitContainer> MySplitContainers => _mySplitContainers ??
                                                                (_mySplitContainers =
                                                                    MyTreeViewTools.GetSplitContainerList(ScrollPanel
                                                                        .Controls[0]));

        #endregion
       
        /// <summary>
        ///     防止重获焦点时，选择项瞬间跳离的问题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void MyTreeView_Leave(object sender, EventArgs e) {
            var mytreeView = sender as MyTreeView;
            if (mytreeView != null)
                mytreeView.SelectedNode = null;
        }

        /// <summary>
        ///     获取指定控件
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetControlByName<T>(string name) where T : class {
            try {
                var controls = VsForm.Controls.Find(name, true);
                T tagertControl = null;
                foreach (var c in controls) {
                    tagertControl = c as T;
                    if (tagertControl != null)
                        break;
                }
                return tagertControl;
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        ///     切换主视图与修改视图
        /// </summary>
        /// <param name="node"></param>
        public static void ShowTreeView(MyTreeNode node) {
            TreeView1.Visible = false;
            ScrollPanel.Visible = true;
            ScrollPanel.Controls[0].Visible = true;
            CreateTree(node.BuildeType.Id);
        }


        /// <summary>
        ///     绘制左侧导航及主视图区
        /// </summary>
        /// <param name="id"></param>
        public static void CreateTree(string id) {
            var buildeEntity = Toolpars.BuilderEntity;

            var item = buildeEntity.BuildeTypies.Where(et => et.Id.Equals(id) || id.Equals("RootView")).ToList();
            var editState = Toolpars.FormEntity.EditState;
            if (editState) {
                var bt = GetEditItem();
                item.Add(bt);
                if (!IconTools.ImageList.Contains(bt.Id))
                    IconTools.ImageList.Add(bt.Id, Resources.defautApp);
            }
            if (item.Count <= 0)
                return;
            if (id.Equals("RootView")) {
                MyTreeViewTools.CreateMainTreeNode(NavTreeView, item, false);
                return;
            }
            var mainViews = MyTreeViews;
            mainViews.ForEach(tv => tv.Nodes.Clear());
            if (item[0].BuildeItems == null)
                return;

            var splitContainers = MySplitContainers;
            if (!splitContainers[0].Visible)
                return;
            var splitCount = SetSplitSize();


            CreateTreeView(splitCount, item);
            var nodeCount = mainViews[0].Nodes.Count;
            var hight = nodeCount * mainViews[0].ItemHeight;
            if (hight > SplitContainer2.Panel2.ClientSize.Height)
                splitContainers[0].Size = new Size(SplitContainer2.Panel2.ClientSize.Width, hight);
            else
                splitContainers[0].Size = new Size(SplitContainer2.Panel2.ClientSize.Width,
                    SplitContainer2.Panel2.ClientSize.Height);
        }

        /// <summary>
        ///     获取每一列菜单数据
        /// </summary>
        /// <param name="items"></param>
        /// <param name="splitCount"></param>
        /// <param name="currentColmuns"></param>
        /// <returns></returns>
        public static List<BuildeType> GetColmunData(IReadOnlyList<BuildeType> items, int splitCount,
            int currentColmuns) {
            var resItem = new List<BuildeType>();
            var count = items.Count;
            if (currentColmuns >= count)
                return resItem;
            resItem.Add(items[currentColmuns]);
            if (currentColmuns + splitCount < count)
                resItem.AddRange(GetColmunData(items, splitCount, currentColmuns + splitCount));
            return resItem;
        }

        /// <summary>
        ///     增加编辑项
        /// </summary>
        /// <returns></returns>
        public static BuildeType GetEditItem() {
            return new BuildeType {
                Id = "MainAddNewItemID",
                Name = "添加你的选项",
                Description = "双击添加你的项目",
                EditState = "True",
                ShowCheckedBox = "False",
                ShowIcon = "True"
            };
        }

        /// <summary>
        ///     创建视图
        /// </summary>
        /// <param name="splitCount"></param>
        /// <param name="items"></param>
        public static void CreateTreeView(int splitCount, IReadOnlyList<BuildeType> items
        ) {
            var buildeItems = items[0].BuildeItems
                .Where(builderItem => !PathTools.IsFasle(builderItem.Visiable)).ToList();
            var splitContainers = MySplitContainers;
            var editState = Toolpars.FormEntity.EditState;
            if (editState) {
                var bt = GetEditItem();
                buildeItems.Add(bt);
                if (!IconTools.ImageList.Contains(bt.Id))
                    IconTools.ImageList.Add(bt.Id, Resources.defautApp);
            }
            for (var i = 0; i < splitCount; i++) {
                var splitContainer = splitContainers[i];
                var tv = splitContainer.Panel1.Controls[0] as MyTreeView;
                var item = GetColmunData(buildeItems, splitCount, i);

                MyTreeViewTools.CreateMainTreeNode(tv, item, true);
            }
        }

        /// <summary>
        ///     获得分割数量
        /// </summary>
        /// <returns></returns>
        public static int GetSplitCount() {
            var spiltWidth = Toolpars.FormEntity.SpiltWidth;
            var maxSplitCount = Toolpars.FormEntity.MaxSplitCount;
            var clientSize = SplitContainer2.Panel2.ClientSize;
            var currentWidth = clientSize.Width;
            var count = currentWidth / spiltWidth; //显示的个数
            count = count == 0 ? 1 : count;
            var splitCount = MySplitContainers.Count();
            count = count > splitCount ? splitCount : count;
            count = count > maxSplitCount ? maxSplitCount : count;
            return count;
        }

        #region 事件

        /// <summary>
        ///     主界面加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void VSTOOL_Load(object sender, EventArgs e) {
            MyTreeViewTools.CreateRightView(RighteTreeView);
            CreateTree("RootView");
            CreateMainView();
        }

        /// <summary>
        ///     主界面加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void VSTOOL_FormClosed(object sender, FormClosedEventArgs e) {
            //try
            //{
            //    var serverPath = @"\\192.168.168.15\E10_Tools\E10_Switch\VSTOOL\log";
            //    LogTools.UploadLog(serverPath);
            //}
            //catch
            //{
            //    // ignored
            //}
        }

        /// <summary>
        ///     增加快捷键编辑菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void VSTOOL_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode != Keys.E
                || !e.Alt
                || !e.Control)
                return;
            Toolpars.FormEntity.EditState = !Toolpars.FormEntity.EditState;
            HeaderPanel.Visible = !Toolpars.FormEntity.EditState;
            SplitContainer1.Panel2Collapsed = Toolpars.FormEntity.EditState;
            InitMainView();
        }

        /// <summary>
        /// 重新加载导航栏与主菜单画面
        /// </summary>
        public static void InitMainView() {
            var selectNode = NavTreeView.SelectedNode;
            CreateTree("RootView");
            var myTreeNode = selectNode as MyTreeNode;
            if (myTreeNode == null)
                return;
            foreach (MyTreeNode node in NavTreeView.Nodes)
            {
                var bt = node.BuildeType;
                if (!bt.Id.Equals(myTreeNode.BuildeType.Id))
                    continue;
                NavTreeView.SelectedNode = node;
                break;
            }
            CreateTree(myTreeNode.BuildeType.Id);
           
        }


        /// <summary>
        ///     设置流式布局，最大可设定
        ///     因为没有找到较好的前端实现方式，此方法不妥，winform程序不太自由
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void VSTOOL_ClientSizeChanged(object sender, EventArgs e) {
            if (NavTreeView.SelectedNode is MyTreeNode node)
                CreateTree(node.BuildeType.Id);
        }

        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void BtnCreate_Click(object sender, EventArgs e) {
            try {
                var pathInfo = Toolpars.PathEntity;
                if (!ModiCkb.Checked) {
                    var dicPath = MyTools.GetTreeViewFilePath(RighteTreeView.Nodes);
                    var fileInfos = new List<FileInfos>();
                    foreach (var kv in dicPath)
                        fileInfos.AddRange(kv.Value);
                    Task.Factory.StartNew(() => {
                        Thread.CurrentThread.IsBackground = false;
                        LogTools.WriteToServer(fileInfos);
                    });

                    if (PathTools.IsNullOrEmpty(Toolpars.FormEntity.TxtToPath)
                        || PathTools.IsNullOrEmpty(Toolpars.FormEntity.TxtNewTypeKey)) {
                        MessageBox.Show(Resources.TypekeyNotExisted, Resources.ErrorMsg, MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                    var success = MyTools.CreateFile(RighteTreeView);

                    if (!success)
                        return;
                    if (NavTreeView.SelectedNode != null) {
                        var node = NavTreeView.SelectedNode as MyTreeNode;
                        ShowTreeView(node);
                    }
                    MyTreeViewTools.CreateRightView(RighteTreeView);
                }
                else {
                    var fileInfos = MyTools.GetTreeViewPath(TreeView1.Nodes);
                    Task.Factory.StartNew(() => {
                        Thread.CurrentThread.IsBackground = false;
                        LogTools.WriteToServer(fileInfos);
                    });

                    if (PathTools.IsNullOrEmpty(Toolpars.FormEntity.TxtToPath)
                        || PathTools.IsNullOrEmpty(Toolpars.FormEntity.TxtNewTypeKey)
                    ) {
                        MessageBox.Show(Resources.TypekeyNotExisted, Resources.ErrorMsg, MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                    var pkgDir = pathInfo.PkgTypeKeyFullRootDir;

                    if (!Directory.Exists(pkgDir)) {
                        MessageBox.Show(string.Format(Resources.DirNotExisted, pkgDir), Resources.ErrorMsg,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                    
                    var flag = MyTools.CopyModi(TreeView1.Nodes);
                    if (!flag)
                        return;
                    MessageBox.Show(Resources.GenerateSucess);
                    ModiCkb.Checked = false;
                }
            }
            catch (Exception ex) {
                LogTools.LogError($"GenerClass Error! Detail {ex.Message}");
                MessageBox.Show(ex.Message, Resources.ErrorMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///     清空按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void BtnClear_Click(object sender, EventArgs e) {
            try {
                MyTools.InitBuilderEntity();
                if (NavTreeView.SelectedNode != null) {
                    var node = NavTreeView.SelectedNode as MyTreeNode;
                    ShowTreeView(node);
                }
                MyTreeViewTools.CreateRightView(RighteTreeView);
            }
            catch (Exception ex) {
                LogTools.LogError($"ClearSelect Error! Detail {ex.Message}");
            }
        }


        /// <summary>
        ///     打开文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void BtnOpenTo_Click(object sender, EventArgs e) {
            var btn = sender as Button;
            var name = btn?.Name;
            try {
                if (name != null
                    && name.Equals("BtnOpenTo")) {
                    var targetDir = Toolpars.FormEntity.TxtToPath;
                    MyTools.OpenDir(targetDir);
                }
                else if (name != null
                         && name.Equals("PkgOpenTo")) {
                    var targetDir = Toolpars.FormEntity.TxtPkGpath;
                    MyTools.OpenDir(targetDir);
                }
            }
            catch (Exception ex) {
                LogTools.LogError($"OpenDir {name} Error! Detail {ex.Message}");
            }
        }

        /// <summary>
        ///     打开个案目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void BtnOpen_Click(object sender, EventArgs e) //打开文件夹
        {
            try {
                var openDirForm = new OpenDirForm();
                openDirForm.ShowDialog();
            }
            catch (Exception ex) {
                LogTools.LogError($"openDirForm Error! Detail {ex.Message}");
            }
        }  
        /// <summary>
        ///     打开个案目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void OpenCode_Click(object sender, EventArgs e) //打开文件夹
        {
            var typekey = Toolpars.FormEntity.TxtNewTypeKey;
            try {
                var targetDir = Toolpars.PathEntity.TypeKeyFullRootDir;
                if (typekey.Equals(string.Empty)) targetDir = Path.GetDirectoryName(targetDir);

                MyTools.OpenDir(targetDir);
            }
            catch (Exception ex) {
                LogTools.LogError($"OpenDir {typekey} Error! Detail {ex.Message}");
            }
        }

        /// <summary>
        ///     动态改变借用typekey
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void TxtNewTypeKey_TextChanged(object sender, EventArgs e) {
            if (!ModiCkb.Checked)
                return;
            var typeKeyTbBox = sender as TextBox;
            if (typeKeyTbBox == null)
                // ReSharper disable once RedundantJumpStatement
                return;
        }

        /// <summary>
        ///     行业包选中时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Industry_CheckedChanged(object sender, EventArgs e) {
            var control = sender as CheckBox;
            if (control == null)
                return;
            Toolpars.MIndustry = control.Checked;
            Toolpars.FormEntity.TxtToPath = Toolpars.Mpath;
        }


        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void BtnP_Click(object sender, EventArgs e) {
            if (Directory.Exists(@"\\192.168.168.15\E10_Shadow"))
                Process.Start(@"\\192.168.168.15\E10_Shadow");
            else
                MessageBox.Show(string.Format(Resources.DirNotExisted, string.Empty), Resources.WarningMsg,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void BtnG_Click(object sender, EventArgs e) {
            if (Toolpars.FormEntity.PkgTypekey == "")
                return;
            var pathInfo = Toolpars.PathEntity;
            var targetDir = pathInfo.PkgTypeKeyFullRootDir;
            if (Directory.Exists(targetDir))
                Process.Start(targetDir);
            else
                MessageBox.Show(string.Format(Resources.DirNotExisted, string.Empty), Resources.WarningMsg,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #region 借用按钮

        /// <summary>
        ///     借用界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void TreeView1_AfterSelect(object sender, TreeViewEventArgs e) {
            var node = e.Node as MyTreeNode;
            if (node == null) return;
            node.BuildeType.Checked = node.Checked ? "True" : "False";
        }

        /// <summary>
        ///     修改按钮，点击弹出窗口，指示将借用的TYPEKEY与新的TypeKey
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void CheckBox1_CheckedChanged(object sender, EventArgs e) {
            if (ModiCkb.Checked) {
                var form1 = new ModiPkgForm(Toolpars);
                if (form1.ShowDialog() == DialogResult.OK) {
                    TreeView1.Visible = true;
                    ScrollPanel.Controls[0].Visible = false;
                    ScrollPanel.Visible = false;
                    if (!PathTools.IsNullOrEmpty(Toolpars.FormEntity.TxtToPath)
                        && !PathTools.IsNullOrEmpty(Toolpars.FormEntity.TxtNewTypeKey)
                        && !PathTools.IsNullOrEmpty(Toolpars.FormEntity.PkgTypekey)) {
                        var pathInfo = Toolpars.PathEntity;
                        var pkgDir = pathInfo.PkgTypeKeyFullRootDir;

                        if (Directory.Exists(pkgDir)) {
                            RighteTreeView.Nodes.Clear();
                            TreeView1.Nodes.Clear();
                            TreeView1.Nodes.Add(MyTreeViewTools.MyPaintTreeView(pkgDir)); //mfroma
                            TreeView1.ExpandAll();
                        }
                        else {
                            MessageBox.Show(string.Format(Resources.DirNotExisted, pkgDir), Resources.WarningMsg,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ModiCkb.Checked = !ModiCkb.Checked;
                            RighteTreeView.Nodes.Clear();
                        }
                    }
                    else {
                        MessageBox.Show(Resources.TypekeyNotExisted);
                        ModiCkb.Checked = !ModiCkb.Checked;
                    }
                }
                else {
                    ModiCkb.Checked = !ModiCkb.Checked;
                }
            }
            else {
                TreeView1.Nodes.Clear();
                TreeView1.Visible = false;
                ScrollPanel.Visible = true;
                ScrollPanel.Controls[0].Visible = true;
                MyTools.InitBuilderEntity();
                if (NavTreeView.SelectedNode is MyTreeNode node)
                    CreateTree(node.BuildeType.Id);
            }
            TxtPkGpath.Text = Toolpars.FormEntity.TxtPkGpath;
        }

        #endregion


        /// <summary>
        ///     帮助按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Task.Factory.StartNew(() => 
                MyTools.OpenWord("Help.docx")
            );
        }

        /// <summary>
        ///     左侧导航TreeView单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void MyTreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
            //预防非法跳转
            ModiCkb.Checked = false;
            var node = e.Node as MyTreeNode;
            NavTreeView.SelectedNode = node;
            ShowTreeView(node);
        }


        #region 拖拽事件

        /// <summary>
        ///     拖拽事件进入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void ScrollPanel_DragEnter(object sender, DragEventArgs e) {
            if (NavTreeView.SelectedNode == null)
                return;
            var node = NavTreeView.SelectedNode as MyTreeNode;
            var b = node?.BuildeType.Id.Equals("MYTools");
            if (b == null
                || !(bool) b)
                return;
            var fileList = (Array) e.Data.GetData(DataFormats.FileDrop);
            var f = true;
            foreach (var filePath in fileList) {
                string[] extName = {".lnk", ".exe"};
                var exeExtension = Path.GetExtension(filePath.ToString());
                if (!extName.Contains(exeExtension))
                    f = false;
            }
            if (!f)
                return;

            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Move : DragDropEffects.None;
        }

        /// <summary>
        ///     拖拽事件结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void ScrollPanel_DragDrop(object sender, DragEventArgs e) {
            var fileList = (Array) e.Data.GetData(DataFormats.FileDrop);
            foreach (var file in fileList) {
                var filePath = file.ToString();
                if (!File.Exists(filePath))
                    return;
                var form = new CreateToolForm(filePath, Toolpars);
                if (form.ShowDialog() != DialogResult.OK)
                    continue;
                if (NavTreeView.SelectedNode == null)
                    return;
                var node = NavTreeView.SelectedNode as MyTreeNode;
                if (node != null)
                    CreateTree(node.BuildeType.Id);
            }
        }

        #endregion

        #endregion

        #region 创建主视图

        /// <summary>
        ///     创建主视图SplitContainer
        /// </summary>
        public static void CreateMainView() {
            var iActulaWidth = Screen.PrimaryScreen.Bounds.Width;
            var width = iActulaWidth * 3 / 4;
            var spiltWidth = Toolpars.FormEntity.SpiltWidth;
            var maxSplitCount = Toolpars.FormEntity.MaxSplitCount;
            var count = width / spiltWidth;
            count = count == 0 ? 1 : count;
            count = count > maxSplitCount ? maxSplitCount : count;
            var mySpilterContaniers = new List<SplitContainer>();
            for (var i = 0; i < count; i++) {
                var mySplitContainer = new SplitContainer {
                    IsSplitterFixed = true,
                    Location = new Point(0, 0),
                    Name = "MySplitContainer" + i + 1,
                    SplitterWidth = 1
                };
                var myTreeView = CreateMyTreeView(i);
                mySplitContainer.Panel1.Controls.Add(myTreeView);
                if (i == 0) {
                    mySplitContainer.Anchor = AnchorStyles.Top | AnchorStyles.Left
                                              | AnchorStyles.Right;
                    mySplitContainer.Size = new Size(ScrollPanel.ClientSize.Width, ScrollPanel.ClientSize.Height);
                }
                else if (i <= count - 1) {
                    mySplitContainer.Dock = DockStyle.Fill;
                    mySpilterContaniers[i - 1].Panel2.Controls.Add(mySplitContainer);
                }
                if (i == count - 1) {
                    var lastmyTreeView = CreateMyTreeView(i + 1);
                    mySplitContainer.Panel2.Controls.Add(lastmyTreeView);
                }

                mySpilterContaniers.Add(mySplitContainer);
            }

            ScrollPanel.Controls.Clear();
            ScrollPanel.Controls.Add(mySpilterContaniers[0]);
        }


        /// <summary>
        ///     创建主视图TreeView
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static MyTreeView CreateMyTreeView(int i) {
            var myTreeView = new MyTreeView {
                BackColor = SystemColors.Window,
                BorderStyle = BorderStyle.None,
                DescriptionColor = Color.DimGray,
                Dock = DockStyle.Fill,
                DrawMode = TreeViewDrawMode.OwnerDrawText,
                Font = new Font("新宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134),
                ForeColor = Color.Black,
                FullRowSelect = true,
                HotTracking = true,
                ImeMode = ImeMode.On,
                IsCard = false,
                ItemHeight = 85,
                Location = new Point(0, 0),
                Margin = new Padding(4, 4, 4, 4),
                Name = "myTreeView" + i,
                NodeFont = null,
                NodeCheckBoxSize = new Size(20, 20),
                PaddingSetting = new Point(5, 15),
                Scrollable = false,
                ShowDescription = true,
                ShowLines = false,
                ShowPlusMinus = false,
                Size = new Size(333, 584),
                TabIndex = 35
            };
            myTreeView.SetAutoScrollEvent -= MyTreeView2_SetAutoScrollEvent;
            myTreeView.SetAutoScrollEvent += MyTreeView2_SetAutoScrollEvent;
            myTreeView.AfterCheck -= MyTreeView2_AfterCheck;
            myTreeView.AfterCheck += MyTreeView2_AfterCheck;
            myTreeView.NodeMouseDoubleClick -= MyTreeView2_NodeMouseDoubleClick;
            myTreeView.NodeMouseDoubleClick += MyTreeView2_NodeMouseDoubleClick;
            myTreeView.Leave -= MyTreeView_Leave;
            myTreeView.Leave += MyTreeView_Leave;
            return myTreeView;
        }


        /// <summary>
        ///     设置分割尺寸
        /// </summary>
        /// <returns></returns>
        public static int SetSplitSize() {
            var clientSize = SplitContainer2.Panel2.ClientSize;
            var width = clientSize.Width;
            if (ScrollPanel.Controls.Count == 0)
                return 0;
            var splitContainerList = MySplitContainers;
            var splitContainer3 = ScrollPanel.Controls[0];
            splitContainer3.Size = new Size(clientSize.Width, clientSize.Height);
            var count = GetSplitCount(); //显示的个数
            width /= count;
            for (var i = 0; i < count; i++) {
                var splitContainer = splitContainerList[i];
                splitContainer.SplitterDistance = width;
                splitContainer.Panel2Collapsed = i == count - 1;
            }
            return count;
        }

        /// <summary>
        ///     设置3个treeview外部panel  响应treeview滚动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="upAndDown"></param>
        public static void MyTreeView2_SetAutoScrollEvent(object sender, int upAndDown) {
            var vscroll = ScrollPanel.VerticalScroll.Visible;
            if (!vscroll)
                return;
            var splitContainer = ScrollPanel.Controls[0] as SplitContainer;
            var myTreeView2 = splitContainer?.Panel1.Controls[0] as MyTreeView;
            if (myTreeView2 == null)
                return;
            var growbase = myTreeView2.Nodes.Count / 20;
            var growNum = growbase == 0 ? 40 : growbase * 40;

            var minNum = ScrollPanel.VerticalScroll.Minimum;
            var cnum = ScrollPanel.VerticalScroll.Value;
            if (upAndDown == 1) {
                ScrollPanel.VerticalScroll.Value += growNum;
            }
            else {
                if (cnum - growNum > minNum)
                    ScrollPanel.VerticalScroll.Value -= growNum;
                else
                    ScrollPanel.VerticalScroll.Value = minNum;
            }
        }

        /// <summary>
        ///     单击选择,选择后将建立对应的文件信息，
        ///     指示模版文件位置与将要创建的文件位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void MyTreeView2_AfterCheck(object sender, TreeViewEventArgs e) {
            Toolpars.MDistince = false;
            var trueStr = "True";
            var falseStr = "False";
            if (e.Node is MyTreeNode node) {
                var builderType = node.BuildeType;
                if (Toolpars.FormEntity.EditState)
                    return;
                if (PathTools.IsTrue(builderType.ReadOnly))
                    return;

                var fileInfos = new List<FileInfos>();
                if (e.Node.Checked) {
                    builderType.Checked = trueStr;
                    if (!ModiCkb.Checked)
                        if (PathTools.IsFasle(builderType.ShowParWindow)
                        ) {
                            fileInfos = MyTools.CreateFileMappingInfo(builderType);
                        }
                        else {
                            var myForm =
                                new ModiName(builderType, Toolpars)
                                    {StartPosition = FormStartPosition.CenterParent};
                            if (myForm.ShowDialog() == DialogResult.OK) {
                                fileInfos = myForm.FileInfos;
                            }
                            else {
                                builderType.Checked = falseStr;
                                e.Node.Checked = false;
                            }
                        }
                }
                else {
                    builderType.Checked = falseStr;
                }
                if (NavTreeView.SelectedNode != null) {
                    var parNode = NavTreeView.SelectedNode as MyTreeNode;
                    var parItem = Toolpars.BuilderEntity.BuildeTypies.ToList()
                        .Where(et => parNode != null && et.Id.Equals(parNode.BuildeType.Id)).ToList();
                    if (parItem.Count > 0) {
                        var citem = parItem[0].BuildeItems
                            .Where(et => {
                                var myTreeNode = e.Node as MyTreeNode;
                                return myTreeNode != null && et.Id.Equals(myTreeNode.BuildeType.Id);
                            }).ToList();
                        if (citem.Count > 0)
                            if (e.Node.Checked)
                                citem.ForEach(ee => {
                                        ee.Checked = trueStr;
                                        ee.FileInfos = fileInfos;
                                    }
                                );
                            else
                                citem.ForEach(ee => {
                                    ee.Checked = falseStr;
                                    ee.FileInfos = fileInfos;
                                });
                    }
                }
            }
            if (!Toolpars.FormEntity.EditState)
                MyTreeViewTools.CreateRightView(RighteTreeView);
        }

        /// <summary>
        ///     主界面双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void MyTreeView2_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            var node = e.Node as MyTreeNode;
            if (node == null)
                return;
            var bt = node.BuildeType;
            if (Toolpars.FormEntity.EditState)
                return;
            if (bt?.IsPlug != null
                && PathTools.IsTrue(bt.IsPlug)
            )
                MyTools.CallModule(bt);
            else if (!PathTools.IsTrue(bt?.IsTools))
                node.Checked = !PathTools.IsTrue(bt?.Checked);
        }

        #endregion
    }
}