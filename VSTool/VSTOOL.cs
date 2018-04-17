// create By 08628 20180411
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using Common.Implement.UI;
using Common.Implement.Entity;
using Common.Implement.EventHandler;
using System.Threading;
using Common.Implement.Tools;
using VSTool.Properties;
using static System.Windows.Forms.AnchorStyles;

namespace VSTool {
    // ReSharper disable once InconsistentNaming
    public partial class VSTOOL : Form {
        #region 屬性

        private Toolpars _toolpars = new Toolpars();

        public Toolpars Toolpars => _toolpars;

        #endregion
        

        public VSTOOL(string[] pToIni) {
            InitializeComponent();
            splitContainer2.Panel2.HorizontalScroll.Visible = false;
            //绑定类，当类或控件值改变时触发更新
            txtToPath.DataBindings.Add(new Binding("Text", Toolpars.FormEntity, "txtToPath", true,
                DataSourceUpdateMode.OnPropertyChanged));
            txtPKGpath.DataBindings.Add(new Binding("Text", Toolpars.FormEntity, "txtPKGpath", true,
                DataSourceUpdateMode.OnPropertyChanged));
            txtNewTypeKey.DataBindings.Add(new Binding("Text", Toolpars.FormEntity, "txtNewTypeKey", true,
                DataSourceUpdateMode.OnPropertyChanged));
            Industry.DataBindings.Add(new Binding("Checked", Toolpars.FormEntity, "Industry", true,
                DataSourceUpdateMode.OnPropertyChanged));

            #region 自動更新

            //CallUpdate.autoUpgrade();

            #endregion

            #region 複製最新的佈署dll

            try {
                //string mServerExePath = CallUpdate.GetExeFolder(CallUpdate.GetServerExePath("VSTool"));
                //OldTools.CopynewVSTool(mServerExePath, Toolpars.MVSToolpath);
            }
            catch {
                // ignored
            }

            #endregion

            if (pToIni == null) {
                Toolpars.FormEntity.TxtToPath = string.Empty;
                //btncopydll.Visible = false;
                //btncopyUIdll.Visible = false;
                //btnKill.Visible = false;
            }
            else {
                Toolpars.MALL = pToIni[0];
                var args = Toolpars.MALL.Split('&');
                Toolpars.Mpath = args[0]; //D:\DF_E10_2.0.2\C002152226(达峰机械)\WD_PR_C\SRC
                Toolpars.MInpath = args[1]; //D:\DF_E10_2.0.2\X30001(鼎捷紧固件)\WD_PR_I\SRC
                Toolpars.Mplatform = args[2]; //C:\DF_E10_2.0.2
                Toolpars.MdesignPath = args[3]; //E:\平台\E202
                Toolpars.MVersion = args[4]; //DF_E10_2.0.2
                Toolpars.MIndustry = Convert.ToBoolean(args[5]);
                Toolpars.CustomerName = args[6];

                Toolpars.FormEntity.TxtToPath = Toolpars.Mpath;
                if (Toolpars.MIndustry) {
                    Toolpars.FormEntity.TxtToPath = Toolpars.MInpath;
                }

                Toolpars.FormEntity.txtPKGpath = $@"{Toolpars.MdesignPath}\WD_PR\SRC";
                Toolpars.FormEntity.Industry = Toolpars.MIndustry;
                if (Toolpars.Mpath.Contains("PKG")
                    && !Toolpars.MIndustry) {
                    Toolpars.FormEntity.TxtToPath = $@"{Toolpars.MdesignPath}\WD_PR\SRC\";
                }
                btncopydll.Visible = true;
                btncopyUIdll.Visible = true;
               // btnKill.Visible = true;
            }

            Toolpars.OldTypekey = Toolpars.SettingPathEntity.TemplateTypeKey;
            MyTool.InitBuilderEntity(Toolpars);
            TreeViewTool.CreateRightView(myTreeView5, _toolpars);
        }
        
        private void VSTOOL_Load(object sender, EventArgs e) {
            CreateTree("RootView");
            SpiltWidth = 200;
            MaxSplitCount = 3;
            CreateMainView();
        }

       
        /// <summary>
        /// 绘制左侧导航及主视图区
        /// </summary>
        /// <param name="id"></param>
        void CreateTree(string id) {
            var buildeEntity = _toolpars.BuilderEntity;

            var item = buildeEntity.BuildeTypies.Where(et => et.Id.Equals(id) || id.Equals("RootView")).ToList();

            if (item.Count <= 0)
                return;
            if (id.Equals("RootView")) {
                TreeViewTool.CreateTree(_toolpars,myTreeView1, item, false);
                return;
            }
            if (item[0].BuildeItems == null) return;
         
            var splitContainers = MySplitContainers;
            if(!splitContainers[0].Visible)return;
            var splitCount = SetSplitSize();
            var mainViews = MyTreeViews;
            mainViews.ForEach(tv =>tv.Nodes.Clear());
            var count = item[0].BuildeItems.Count();
            CreateTreeView(count, splitCount, item);
            var nodeCount = mainViews[0].Nodes.Count;
            var hight = nodeCount * mainViews[0].ItemHeight;
            if (hight > splitContainer2.Panel2.ClientSize.Height)
            {
                splitContainers[0].Size = new Size(splitContainer2.Panel2.ClientSize.Width, hight);
            }
            else
            {
                splitContainers[0].Size = new Size(splitContainer2.Panel2.ClientSize.Width,
                    splitContainer2.Panel2.ClientSize.Height);
            }
      
        }

        private void CreateTreeView(int count, int splitCount, List<BuildeType> item
            ) {
            var skipSeq = 0;
            var evgCount = count / splitCount;
            var surplusCount = count % splitCount;
            var splitContainers = MySplitContainers;
            var subSurplusCount = surplusCount % splitCount;
            foreach (var splitContainer in splitContainers) {
                var tv = splitContainer.Panel1.Controls[0] as MyTreeView;
                //项目小于分列数量，每项取一个
                if (evgCount == 0 && count < splitCount) {
                    if (skipSeq <= count - 1) {
                        var item2 = item[0].BuildeItems.Skip(skipSeq++).Take(1).ToList();
                        TreeViewTool.CreateTree(_toolpars, tv, item2, true);
                    }
                }
                //均分
                else if (surplusCount == 0) {
                    var item2 = item[0].BuildeItems.Skip(skipSeq).Take(evgCount).ToList();
                    skipSeq += evgCount;
                    TreeViewTool.CreateTree(_toolpars, tv, item2, true);
                }
                //有余项
                else {
                    var subCount = evgCount; ;
                    if (subSurplusCount != 0) {
                        subSurplusCount--;
                        subCount = evgCount + 1;
                    }
                 
                    var item2 = item[0].BuildeItems.Skip(skipSeq).Take(subCount).ToList();
                
                    skipSeq += subCount;
                    TreeViewTool.CreateTree(_toolpars, tv, item2, true);
                }
                if (splitContainer.Panel2Collapsed) {
                    break;
                }
            }

        }

        /// <summary>
        /// 包括修改与创建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCreate_Click(object sender, EventArgs e) {
            try {
                if (!ModiCkb.Checked) {
                    var dicPath = MyTool.GetTreeViewFilePath(myTreeView5.Nodes, _toolpars);
                    List<FileInfos> fileInfos = new List<FileInfos>();
                    foreach (var kv in dicPath) {
                        fileInfos.AddRange(kv.Value);
                    }
                    new Thread(()=>  LogTool.WriteToServer(Toolpars, fileInfos)
                    ).Start();
                    
                    Toolpars.GToIni = Toolpars.FormEntity.TxtToPath;
                    if ((string.Equals(Toolpars.FormEntity.TxtToPath, string.Empty, StringComparison.Ordinal))
                        || (string.Equals(Toolpars.FormEntity.txtNewTypeKey, string.Empty, StringComparison.Ordinal))) {
                        MessageBox.Show(Resources.TypeKeyNotExisted, Resources.ErrorMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    var success = MyTool.CreateFile(myTreeView5, _toolpars);

                    if (success) {
                        if (myTreeView1.SelectedNode != null) {
                            var node = myTreeView1.SelectedNode as MyTreeNode;
                            ShowTreeView(node);
                        }
                        TreeViewTool.CreateRightView(myTreeView5,_toolpars);
                    }

                }
                else {
                    //OldTools.WriteLogByTreeView(Toolpars, listDATA);
                    var fileInfos = MyTool.GetTreeViewPath(treeView1.Nodes);
                    //new Thread( ()=> {
                    //        Invoke(new Action(()=>{
                    //            LogTool.WriteToServer(Toolpars, fileInfos);
                    //        }));
                    //    }
                    //).Start();
                    new Thread( ()=>  LogTool.WriteToServer(Toolpars, fileInfos)
                          
                    ).Start();

                    Toolpars.GToIni = Toolpars.FormEntity.TxtToPath;
                    if ((string.Equals(Toolpars.FormEntity.TxtToPath, string.Empty, StringComparison.Ordinal))
                        || (string.Equals(Toolpars.FormEntity.txtNewTypeKey, string.Empty, StringComparison.Ordinal))
                        ) {
                        MessageBox.Show(Resources.TypeKeyNotExisted, Resources.ErrorMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string pkgDir = $@"{Toolpars.FormEntity.txtPKGpath}\Digiwin.ERP.{Toolpars.FormEntity.PkgTypekey}";
                    if (Toolpars.FormEntity.PkgTypekey.StartsWith("Digiwin.ERP."))
                    {
                        pkgDir = $@"{Toolpars.FormEntity.txtPKGpath}\{Toolpars.FormEntity.PkgTypekey}";
                    }
                    if (!Directory.Exists(pkgDir))
                    {
                        MessageBox.Show(string.Format(Resources.DirNotExist, pkgDir), Resources.ErrorMsg, MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                    bool flag = true;
                    string targetDir = $@"{Toolpars.GToIni}\Digiwin.ERP.{Toolpars.FormEntity.txtNewTypeKey}";
                    if (Directory.Exists(targetDir) ){
                        DialogResult result =
                            MessageBox.Show(
                                Path.Combine(Toolpars.FormEntity.TxtToPath, Toolpars.FormEntity.txtNewTypeKey)
                                + Environment.NewLine+Resources.DirExisted,
                                Resources.WarnningMsg, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes) {
                            OldTools.DeleteAll(targetDir);
                        }
                        else {
                            flag = false;
                        }
                    }
                    if (flag) {
                        flag=MyTool.CopyModi(treeView1.Nodes, _toolpars);
                        if (flag) {
                            MessageBox.Show(Resources.GenerateSucess);
                            ModiCkb.Checked = false;
                        }
                    }
               
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //SqlTools.InsertToolInfo("S01231_20160503_01", "20160503", "Create" + Toolpars.FormEntity.txtNewTypeKey);
        }
    
        /// <summary>
        /// 打开文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpenTo_Click(object sender, EventArgs e) {
            var btn = sender as Button;
            var name = btn?.Name;
            if (name != null && name.Equals(btnOpenTo.Name)) {
                var txtToPathStr = Toolpars.FormEntity.TxtToPath;
                if (txtToPathStr != null && !string.Equals(txtToPathStr.Trim(), string.Empty, StringComparison.Ordinal))
                {
                    folderBrowserDialog1.SelectedPath = Toolpars.FormEntity.TxtToPath.Trim();
                }
                else
                {
                    folderBrowserDialog1.SelectedPath = Toolpars.GToIni;
                }
                if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                    return;
                Toolpars.FormEntity.TxtToPath = folderBrowserDialog1.SelectedPath;
                txtToPath.Text = folderBrowserDialog1.SelectedPath;
                Toolpars.GToIni = Toolpars.FormEntity.TxtToPath;
            }else if (name != null && name.Equals(PkgOpenTo.Name)) {
                var pkgPath = Toolpars.FormEntity.txtPKGpath;
                if (pkgPath != null && !string.Equals(pkgPath.Trim(), string.Empty, StringComparison.Ordinal))
                {
                    folderBrowserDialog1.SelectedPath = pkgPath.Trim();
                }
                else
                {
                    folderBrowserDialog1.SelectedPath = Toolpars.GToIni;
                }
                if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                    return;
                //Toolpars.FormEntity.txtPKGpath = folderBrowserDialog1.SelectedPath;
                txtPKGpath.Text = folderBrowserDialog1.SelectedPath;
                Toolpars.GToIni = Toolpars.FormEntity.txtPKGpath;
            }

          
        }
        
        private void BtnClear_Click(object sender, EventArgs e) {
            MyTool.InitBuilderEntity(_toolpars);
            if (myTreeView1.SelectedNode != null)
            {
                var node = myTreeView1.SelectedNode as MyTreeNode;
                ShowTreeView(node);
            }
            TreeViewTool.CreateRightView(myTreeView5, _toolpars);

        }

  
        
        private void BtnOpen_Click(object sender, EventArgs e) //打开文件夹
        {
            var targetDir = Toolpars.FormEntity.TxtToPath + @"\Digiwin.ERP."
                      + Toolpars.FormEntity.txtNewTypeKey;
            if (Directory.Exists(targetDir)) {
                Process.Start(targetDir);
            }
            else {
                MessageBox.Show(string.Format(Resources.DirNotExist,string.Empty), Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void TxtNewTypeKey_TextChanged(object sender, EventArgs e) {
            if (ModiCkb.Checked)
            {
                if (!string.Equals(Toolpars.FormEntity.TxtToPath, string.Empty, StringComparison.Ordinal)
                    && !string.Equals(Toolpars.FormEntity.txtNewTypeKey, string.Empty, StringComparison.Ordinal)
                    && !string.Equals(Toolpars.FormEntity.PkgTypekey, string.Empty, StringComparison.Ordinal)
                )
                {
                    var pkgDir =
                        $@"{Toolpars.FormEntity.txtPKGpath}\Digiwin.ERP.{Toolpars.FormEntity.PkgTypekey}";
                    if (Directory.Exists(pkgDir))
                    {
                        TreeViewTool.MyPaintTreeView(_toolpars, pkgDir);
                    }
                    else
                    {
                        treeView1.Nodes.Clear();
                    }
                }
                else
                {
                    treeView1.Nodes.Clear();
                }
            }
        }

        #region 复制到平台下

        private void Btncopydll_Click(object sender, EventArgs e) {
            try {
            

                MyTool.CopyDll(Toolpars);
                MessageBox.Show(Resources.CopySucess);
            }
            catch (Exception ex) {
                string[] processNames = {
                    "Digiwin.Mars.ClientStart", "Digiwin.Mars.ServerStart",
                    "Digiwin.Mars.AccountSetStart"
                };
                bool f = CheckCanCopyDll(processNames);
                if (f)
                {
                    Btncopydll_Click(null, null);
                }
                else
                {
                    MessageBox.Show(ex.Message, Resources.ErrorMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            SqlTools.InsertToolInfo("S01231_20160503_01", "20160503", "btncopydll_Click");
        }

        private void BtncopyUIdll_Click(object sender, EventArgs e) {
            try {
                var export = Toolpars.PathEntity.ExportPath;
                var toPath = $"{Toolpars.Mplatform}\\DeployServer\\Shared\\Customization\\Programs\\";
                var filterStr = $"*{Toolpars.FormEntity.txtNewTypeKey}.UI.*";
                if (Toolpars.MIndustry) {
                    toPath = $"{Toolpars.Mplatform}\\DeployServer\\Shared\\Industry\\Programs\\";
                }
                MyTool.FileCopyUIdll(export, toPath, filterStr);
            }
            catch (Exception ex) {
                string[] processNames = {
                    "Digiwin.Mars.ClientStart"
                };
                bool f = CheckCanCopyDll(processNames);
                if (f) {
                    BtncopyUIdll_Click(null,null);
                }
                else {
                    MessageBox.Show(ex.Message, Resources.ErrorMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            SqlTools.InsertToolInfo("S01231_20160503_01", "20160503", "btncopyUIdll_Click");
        }

        public bool CheckCanCopyDll(string[] processNames) {
            var flag = true;
            var infos = Process.GetProcesses();
            foreach (var info in infos)
            {
                if (processNames.Contains(info.ProcessName))
                {
                    flag = false;
                }
            }
            if (flag)
                return true;
            if (MessageBox.Show(Resources.DllUsedMsg, Resources.Warning, MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning) != DialogResult.OK)
                return false;
            OldTools.KillProcess(processNames);
            return true;
        }

        #endregion

  
        private void Industry_CheckedChanged(object sender, EventArgs e) {
            Toolpars.MIndustry = Industry.Checked;
            Toolpars.FormEntity.TxtToPath = Toolpars.Mpath;
            if (Toolpars.MIndustry) {
                Toolpars.FormEntity.TxtToPath = Toolpars.MInpath;
            }
        }


        private void BtnP_Click(object sender, EventArgs e) {
            if (Directory.Exists(@"\\192.168.168.15\E10_Shadow")) {
                Process.Start(@"\\192.168.168.15\E10_Shadow");
            }
            else {
                MessageBox.Show(string.Format(Resources.DirNotExist, string.Empty), Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnG_Click(object sender, EventArgs e) {
            if (Toolpars.FormEntity.PkgTypekey == "")
                return;
            var targetDir = $@"{Toolpars.FormEntity.txtPKGpath}\Digiwin.ERP.{Toolpars.FormEntity.PkgTypekey}";
            if (Directory.Exists(targetDir))
            {
                Process.Start(targetDir);
            }
            else
            {
                MessageBox.Show(string.Format(Resources.DirNotExist, string.Empty), Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Panel3_Paint(object sender, PaintEventArgs e) {
            var borderLineStyle = ButtonBorderStyle.Solid;
            var borderLineStyleNo = ButtonBorderStyle.None;
            var borderWidth = 1;
            var borderColor = Color.LightGray;
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                borderColor, borderWidth, borderLineStyle,
                borderColor, borderWidth, borderLineStyleNo,
                borderColor, borderWidth, borderLineStyleNo,
                borderColor, borderWidth, borderLineStyleNo);
        }
        
        private void MyTreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
            //预防非法跳转
            ModiCkb.Checked = false;

            MyTreeNode node = e.Node as MyTreeNode;
            myTreeView1.SelectedNode = node;
            ShowTreeView(node);
        }


        private void ShowTreeView(MyTreeNode node) {
                treeView1.Visible = false;
                scrollPanel.Visible = true;
                scrollPanel.Controls[0].Visible = true;
                CreateTree(node.BuildeType.Id);
        }

        int GetSplitCount() {
            var clientSize = splitContainer2.Panel2.ClientSize;
            var currentWidth = clientSize.Width;
            var count = currentWidth / SpiltWidth; //显示的个数
            count = count == 0 ? 1 : count;
            var splitCount = MySplitContainers.Count();
            count = count > splitCount ? splitCount : count;
            count = count > MaxSplitCount ? MaxSplitCount : count;
            return count;
        }
        /// <summary>
        /// 设置流式布局，最大3列。
        /// 因为没有找到较好的前端实现方式，此方法不妥，winform程序不太自由
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VSTOOL_ClientSizeChanged(object sender, EventArgs e) {
            if (myTreeView1.SelectedNode is MyTreeNode node)
                CreateTree(node.BuildeType.Id);
        }

        private int SetSplitSize() {
            var clientSize = splitContainer2.Panel2.ClientSize;
            var width = clientSize.Width;
            if (scrollPanel.Controls.Count == 0) {
                return 0;
            }
            var splitContainerList = MySplitContainers;
            var splitContainer3 = scrollPanel.Controls[0];
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
        /// 设置3个treeview外部panel  响应treeview滚动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="upAndDown"></param>
        private void MyTreeView2_SetAutoScrollEvent(object sender, int upAndDown) {
            var vscroll = scrollPanel.VerticalScroll.Visible;
            if (!vscroll)
                return;
            //var maxnum = scrollPanel.VerticalScroll.Maximum;
            var splitContainer = scrollPanel.Controls[0] as SplitContainer;
            var myTreeView2 =splitContainer?.Panel1.Controls[0] as MyTreeView;
            if (myTreeView2 == null)
                return;
            var growbase = myTreeView2.Nodes.Count / 20;
            var growNum = growbase == 0 ? 40 : growbase * 40;

            var minNum = scrollPanel.VerticalScroll.Minimum;
            var cnum = scrollPanel.VerticalScroll.Value;
            if (upAndDown == 1) {
                scrollPanel.VerticalScroll.Value += growNum;
            }
            else {
                if (cnum - growNum > minNum) {
                    scrollPanel.VerticalScroll.Value -= growNum;
                }
                else {
                    scrollPanel.VerticalScroll.Value = minNum;
                }
            }
        }

        /// <summary>
        /// 单击选择,选择后将建立对应的文件信息，
        /// 指示模版文件位置与将要创建的文件位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyTreeView2_AfterCheck(object sender, TreeViewEventArgs e) {
            Toolpars.MDistince = false;
            var trueStr = "True";
            var falseStr = "False";
            if (e.Node is MyTreeNode node)
            {
                var builderType = node.BuildeType;
                if (builderType.ReadOnly != null
                    && builderType.ReadOnly.Equals(trueStr))
                {
                    return;
                }

                var fileInfos = new List<FileInfos>();
                if (e.Node.Checked)
                {
                    builderType.Checked = trueStr;
                    if (!ModiCkb.Checked)
                    {
                        if ((builderType.ShowParWindow != null
                              && builderType.ShowParWindow.Equals(falseStr))
                        )
                        {
                            fileInfos = MyTool.CreateFileMappingInfo(_toolpars, builderType);

                        }
                        else
                        {
                            var myForm =
                                new ModiName(builderType, _toolpars)
                                { StartPosition = FormStartPosition.CenterParent };
                            if (myForm.ShowDialog() == DialogResult.OK)
                            {
                                fileInfos = myForm.FileInfos;
                            }
                            else
                            {
                                builderType.Checked = falseStr;
                                e.Node.Checked = false;
                            }
                        }
                    }
                }
                else
                {
                    builderType.Checked = falseStr;
                }
                if (myTreeView1.SelectedNode != null)
                {
                    var parNode = myTreeView1.SelectedNode as MyTreeNode;
                    var parItem = _toolpars.BuilderEntity.BuildeTypies.ToList()
                        .Where(et => parNode != null && et.Id.Equals(parNode.BuildeType.Id)).ToList();
                    if (parItem.Count > 0)
                    {
                        var citem = parItem[0].BuildeItems
                            .Where(et =>
                            {
                                var myTreeNode = e.Node as MyTreeNode;
                                return myTreeNode != null && et.Id.Equals(myTreeNode.BuildeType.Id);
                            }).ToList();
                        if (citem.Count > 0)
                        {
                            if (e.Node.Checked)
                            {
                                citem.ForEach(ee =>
                                {
                                    ee.Checked = trueStr;
                                    ee.FileInfos = fileInfos;
                                }

                                );
                            }
                            else
                            {
                                citem.ForEach(ee =>
                                {
                                    ee.Checked = falseStr;
                                    ee.FileInfos = fileInfos;
                                });
                            }
                        }
                    }
                }
            }
            TreeViewTool.CreateRightView(myTreeView5,_toolpars);
        }
        
        #region 修改

        /// <summary>
        /// 修改按钮，点击弹出窗口，指示将借用的TYPEKEY与新的TypeKey
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (ModiCkb.Checked)
            {
                var form1 = new ModiPkgForm(_toolpars);
                if (form1.ShowDialog() == DialogResult.OK) {
                    var empStr = string.Empty;
                    treeView1.Visible = true;
                    scrollPanel.Controls[0].Visible = false;
                    scrollPanel.Visible = false;
                    if (!string.Equals(Toolpars.FormEntity.TxtToPath, empStr, StringComparison.Ordinal)
                        && !string.Equals(Toolpars.FormEntity.txtNewTypeKey, empStr, StringComparison.Ordinal)
                        && !string.Equals(Toolpars.FormEntity.PkgTypekey, empStr, StringComparison.Ordinal))
                    {
                        var txtPkGpath = Toolpars.FormEntity.txtPKGpath;

                        var pkgDir = $@"{txtPkGpath}\Digiwin.ERP.{Toolpars.FormEntity.PkgTypekey}";
                        if (Toolpars.FormEntity.PkgTypekey.StartsWith("Digiwin.ERP."))
                        {
                            pkgDir = $@"{txtPkGpath}\{Toolpars.FormEntity.PkgTypekey}";
                        }

                        if (Directory.Exists(pkgDir))
                        {
                            myTreeView5.Nodes.Clear();
                            treeView1.Nodes.Clear();
                            treeView1.Nodes.Add(TreeViewTool.MyPaintTreeView(_toolpars, pkgDir)); //mfroma
                            treeView1.ExpandAll();
                        }
                        else
                        {
                            MessageBox.Show(string.Format(Resources.PKGNotExisted, pkgDir), Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ModiCkb.Checked = !ModiCkb.Checked;
                            myTreeView5.Nodes.Clear();

                        }
                    }
                    else
                    {
                        MessageBox.Show(Resources.TypeKeyNotExisted);
                        ModiCkb.Checked = !ModiCkb.Checked;
                    }
                }
                else
                {
                    ModiCkb.Checked = !ModiCkb.Checked;
                }
            }
            else
            {
                treeView1.Nodes.Clear();
                treeView1.Visible = false;
                scrollPanel.Visible = true;
                scrollPanel.Controls[0].Visible = true;
                if (myTreeView1.SelectedNode is MyTreeNode node)
                    CreateTree(node.BuildeType.Id);
            }
        }
        #endregion




        private void MyTreeView2_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            var node = e.Node as MyTreeNode;
            if (node == null) return;
            var bt = node.BuildeType;
            if (bt?.IsPlug != null  &&
                bt.IsPlug.Equals("True")
                ) {
                MyTool.CallModule(bt,Toolpars);
            }
            else if (bt?.IsTools == null
                     || !bt.IsTools.Equals("True")) {
                if (bt?.Checked != null
                    && bt.Checked.Equals("True")) {
                    node.Checked = false;
                }
                else {
                    node.Checked = true;
                }
            }
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
           new Thread(MyTool.OpenWord).Start();
        }

        private void CreateMainView() {
            var iActulaWidth = Screen.PrimaryScreen.Bounds.Width;
            var width = iActulaWidth*3/4;
            var count = width / SpiltWidth;
            count = count == 0 ? 1 : count;
            count = count > MaxSplitCount ? MaxSplitCount : count;
            var mySpilterContaniers = new List<SplitContainer>();
            for (var i = 0; i < count; i++) {
                var mySplitContainer = new SplitContainer {
                    IsSplitterFixed = true,
                    Location = new Point(0, 0),
                    Name = "MySplitContainer"+i,
                    SplitterWidth = 1

                };
                var myTreeView = CreateMyTreeView(i);
                mySplitContainer.Panel1.Controls.Add(myTreeView);
                if (i == 0) {
                    mySplitContainer.Anchor = AnchorStyles.Top | AnchorStyles.Left
                                              | AnchorStyles.Right;
                    mySplitContainer.Size = new Size(scrollPanel.ClientSize.Width, scrollPanel.ClientSize.Height);
                }
                else if(i<= count - 1){
                    mySplitContainer.Dock = DockStyle.Fill;
                    mySpilterContaniers[i-1].Panel2.Controls.Add(mySplitContainer);
                }
                if (i == count - 1) {
                    var lastmyTreeView = CreateMyTreeView(i+1);
                    mySplitContainer.Panel2.Controls.Add(lastmyTreeView);
                }

                mySpilterContaniers.Add(mySplitContainer);
            }
            scrollPanel.Controls.Clear();
            scrollPanel.Controls.Add(mySpilterContaniers[0]);
            
                
        }

        
        private MyTreeView CreateMyTreeView(int i) {
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
                NodeImageSize = new Size(20, 20),
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
            myTreeView.Leave -= EventHelper.myTreeView_Leave;
            myTreeView.Leave += EventHelper.myTreeView_Leave;
            return myTreeView;
        }

        /// <summary>
        /// 分割宽度，每间隔此宽度分割一列
        /// </summary>
        public int SpiltWidth { get; set; }

        public int MaxSplitCount { get; set; }
        private List<MyTreeView> _myTreeViews;
        public List<MyTreeView> MyTreeViews
        {
            get => _myTreeViews ?? (_myTreeViews = GetTreeViews(scrollPanel.Controls[0]));
            set => _myTreeViews = value;
        }
        private List<SplitContainer> _mySplitContainers;
        public List<SplitContainer> MySplitContainers
        {
            get => _mySplitContainers ?? (_mySplitContainers = GetSplitContainerList(scrollPanel.Controls[0]));
            set => _mySplitContainers = value;
        }
        /// <summary>
        /// 获取全部TreeView
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        List<MyTreeView> GetTreeViews(Control control) {
            var treeViewList = new List<MyTreeView>();
            var container = control as SplitContainer;
            if (container == null)
                return treeViewList;
            var treeViewControl = container.Panel1.Controls[0];
            if (treeViewControl is MyTreeView) {
                treeViewList.Add(treeViewControl as MyTreeView);
            }
            var splitContainerControl = container.Panel2.Controls[0];
            if (splitContainerControl is SplitContainer) {
                treeViewList.AddRange(GetTreeViews(splitContainerControl)); 
            }
            return treeViewList;
        }
        /// <summary>
        /// 获取全部SpiltContainer
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        List<SplitContainer> GetSplitContainerList(Control control) {
            var splitContainerList = new List<SplitContainer>();
            var container = control as SplitContainer;
            if (container == null)
                return splitContainerList;
            splitContainerList.Add(container);
            var panel2Control = container.Panel2.Controls[0];
            if (panel2Control is SplitContainer) {
                splitContainerList.AddRange(GetSplitContainerList(panel2Control));
            }
            return splitContainerList;
        }

        private void splitContainer2_Panel2_SizeChanged(object sender, EventArgs e)
        {

        }
    }
}