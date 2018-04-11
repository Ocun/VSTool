// create By 08628 20180411
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Common.Implement;
using Common.Implement.UI;
using Common.Implement.Entity;
using Common.Implement.EventHandler;
using System.Threading;
using Common.Implement.Tools;

namespace VSTool {
    public partial class VSTOOL : Form {
        #region 屬性

        private toolpars _toolpars = new toolpars();

        public toolpars Toolpars {
            get { return _toolpars; }
        }

        #endregion
        

        public VSTOOL(string[] pToIni) {
            InitializeComponent();
            splitContainer2.Panel2.HorizontalScroll.Visible = false;
            //绑定类，当类或控件值改变时触发更新
            this.txtToPath.DataBindings.Add(new Binding("Text", Toolpars.formEntity, "txtToPath", true,
                DataSourceUpdateMode.OnPropertyChanged));
            this.txtPKGpath.DataBindings.Add(new Binding("Text", Toolpars.formEntity, "txtPKGpath", true,
                DataSourceUpdateMode.OnPropertyChanged));
            this.txtNewTypeKey.DataBindings.Add(new Binding("Text", Toolpars.formEntity, "txtNewTypeKey", true,
                DataSourceUpdateMode.OnPropertyChanged));
            this.Industry.DataBindings.Add(new Binding("Checked", Toolpars.formEntity, "Industry", true,
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
            }

            #endregion

            if (pToIni == null) {
                Toolpars.formEntity.txtToPath = "";
                //btncopydll.Visible = false;
                //btncopyUIdll.Visible = false;
                //btnKill.Visible = false;
            }
            else {
                Toolpars.MALL = pToIni[0];
                string[] args = Toolpars.MALL.Split('&');
                Toolpars.Mpath = args[0]; //D:\DF_E10_2.0.2\C002152226(达峰机械)\WD_PR_C\SRC
                Toolpars.MInpath = args[1]; //D:\DF_E10_2.0.2\X30001(鼎捷紧固件)\WD_PR_I\SRC
                Toolpars.Mplatform = args[2]; //C:\DF_E10_2.0.2
                Toolpars.MdesignPath = args[3]; //E:\平台\E202
                Toolpars.MVersion = args[4]; //DF_E10_2.0.2
                Toolpars.MIndustry = Convert.ToBoolean(args[5]);
                Toolpars.CustomerName = args[6];

                Toolpars.formEntity.txtToPath = Toolpars.Mpath;
                if (Toolpars.MIndustry) {
                    Toolpars.formEntity.txtToPath = Toolpars.MInpath;
                }

                Toolpars.formEntity.txtPKGpath = Toolpars.MdesignPath + @"\WD_PR\SRC\";
                Toolpars.formEntity.Industry = Toolpars.MIndustry;
                if (Toolpars.Mpath.Contains("PKG")
                    && !Toolpars.MIndustry) {
                    Toolpars.formEntity.txtToPath = Toolpars.MdesignPath + @"\WD_PR\SRC\";
                }
                btncopydll.Visible = true;
                btncopyUIdll.Visible = true;
               // btnKill.Visible = true;
            }

            Toolpars.OldTypekey = "XTEST";
            MyTool.InitBuilderEntity(Toolpars);
            TreeViewTool.CreateRightView(myTreeView5, _toolpars);
        }

        private void addEventer() {
            this.myTreeView2.Leave += EventHelper.myTreeView_Leave;
            this.myTreeView3.Leave += EventHelper.myTreeView_Leave;
            this.myTreeView4.Leave += EventHelper.myTreeView_Leave;
        }

        private void VSTOOL_Load(object sender, EventArgs e) {
            createTree("RootView");
            addEventer();
        }

        /// <summary>
        /// 绘制左侧导航及主视图区
        /// </summary>
        /// <param name="id"></param>
        void createTree(string id) {
            BuildeEntity BuildeEntity = _toolpars.BuilderEntity;

            var item = BuildeEntity.BuildeTypies.Where(et => et.Id.Equals(id) || id.Equals("RootView")).ToList();

            if (item.Count > 0) {
                if (id.Equals("RootView")) {
                   TreeViewTool.createTree(_toolpars,myTreeView1, item, false);
                    return;
                }
                if (item[0].BuildeItems == null) return;
                //最大3列，平均显示
                myTreeView2.Nodes.Clear();
                myTreeView3.Nodes.Clear();
                myTreeView4.Nodes.Clear();
                // 右两排折叠
                int vnum = 0, ftake = 0, count = item[0].BuildeItems.Count();

                int elseNum = 0;
                if (splitContainer3.Panel2Collapsed) {
                    ftake = count;
                }
                //最右列折叠
                else if (splitContainer4.Panel2Collapsed) {
                    vnum = count / 2;
                    ftake = count - vnum;
                }
                else {
                    vnum = count / 3;
                    elseNum = count % 3;
                    ftake = elseNum == 2 ? count - 2 * vnum - 1 : count - 2 * vnum;
                }
                var item2 = item[0].BuildeItems.Take(ftake).ToList();
                var item3 = item[0].BuildeItems.Skip(ftake).Take(vnum).ToList();
                var item4 = item[0].BuildeItems.Skip(ftake + vnum).Take(vnum).ToList();
                TreeViewTool.createTree(_toolpars,myTreeView2, item2, true);
                TreeViewTool.createTree(_toolpars,myTreeView3, item3, true);
                TreeViewTool.createTree(_toolpars,myTreeView4, item4, true);
                int nodeCount = item2.Count;
                int hight = nodeCount * myTreeView2.ItemHeight;
                if (hight > splitContainer2.Panel2.ClientSize.Height) {
                    splitContainer3.Size = new Size(splitContainer2.Panel2.ClientSize.Width, hight);
                }
                else {
                    splitContainer3.Size = new Size(splitContainer2.Panel2.ClientSize.Width,
                        splitContainer2.Panel2.ClientSize.Height);
                }
            }
        }

        /// <summary>
        /// 包括修改与创建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, EventArgs e) {
            try {
                if (!ModiCkb.Checked) {
                    var dicPath = MyTool.GetTreeViewFilePath(myTreeView5.Nodes, _toolpars);
                    new Thread(delegate() {
                            this.Invoke(new Action(delegate() {
                                LogTool.writeToServer(Toolpars, dicPath);
                            }));
                        }
                    ).Start();
                    
                    Toolpars.GToIni = Toolpars.formEntity.txtToPath;
                    if ((Toolpars.formEntity.txtToPath == "")
                        || (Toolpars.formEntity.txtNewTypeKey == "")) {
                        MessageBox.Show("请输入创建地址及名称", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    bool success = MyTool.CreateFile(myTreeView5, _toolpars);

                    if (success) {
                        if (myTreeView1.SelectedNode != null) {
                            var node = myTreeView1.SelectedNode as MyTreeNode;
                            showTreeView(node);
                        }
                        TreeViewTool.CreateRightView(myTreeView5,_toolpars);
                    }

                }
                else {
                    //OldTools.WriteLog(Toolpars, listDATA);
                    Toolpars.GToIni = Toolpars.formEntity.txtToPath;
                    if ((Toolpars.formEntity.txtToPath == "")
                        || (Toolpars.formEntity.txtNewTypeKey == "")
                        ) {
                        MessageBox.Show("请输入创建地址及名称", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string strb1 = Toolpars.formEntity.txtPKGpath + "Digiwin.ERP."
                                   + Toolpars.formEntity.PkgTypekey;
                    if (Toolpars.formEntity.PkgTypekey.StartsWith("Digiwin.ERP."))
                    {
                        strb1 = Toolpars.formEntity.txtPKGpath + Toolpars.formEntity.PkgTypekey;
                    }
                    if (!Directory.Exists(strb1))
                    {
                        MessageBox.Show("文件夹" + strb1 + "不存在，请查看！！！", "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                    DirectoryInfo tCusSRC = new DirectoryInfo(Toolpars.GToIni + @"\");
                    bool flag = true;
                    if (Directory.Exists(Path.Combine(Toolpars.GToIni + @"\",
                        "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey))) {

                        DialogResult result =
                            MessageBox.Show(
                                Path.Combine(Toolpars.formEntity.txtToPath, Toolpars.formEntity.txtNewTypeKey)
                                + "\r\n目錄已存在，是否覆蓋??",
                                "Warnning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes) {
                            object tArgsPath = Path.Combine(Toolpars.GToIni + @"\",
                                "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey);
                            OldTools.DeleteAll(tArgsPath);
                        }
                        else {
                            flag = false;
                        }
                    }
                    if (flag) {
                        flag=MyTool.copyModi(treeView1.Nodes, _toolpars);
                        if (flag) {
                            MessageBox.Show("生成成功 !!!");
                            ModiCkb.Checked = false;
                        }
                    }
               
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            sqlTools.insertToolInfo("S01231_20160503_01", "20160503", "Create" + Toolpars.formEntity.txtNewTypeKey);
        }
    
        /// <summary>
        /// 打开文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenTo_Click(object sender, EventArgs e) {
            var txtToPathStr = Toolpars.formEntity.txtToPath;
            if (txtToPathStr != null && txtToPathStr.Trim() != "") { 
                folderBrowserDialog1.SelectedPath = Toolpars.formEntity.txtToPath.Trim();
            }
            else {
                folderBrowserDialog1.SelectedPath = Toolpars.GToIni;
            }
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                Toolpars.formEntity.txtToPath = folderBrowserDialog1.SelectedPath;
                txtToPath.Text = folderBrowserDialog1.SelectedPath;
                Toolpars.GToIni = Toolpars.formEntity.txtToPath;
            }
        }
        
        private void btnClear_Click(object sender, EventArgs e) {
            MyTool.InitBuilderEntity(_toolpars);
            if (myTreeView1.SelectedNode != null)
            {
                var node = myTreeView1.SelectedNode as MyTreeNode;
                showTreeView(node);
            }
            TreeViewTool.CreateRightView(myTreeView5, _toolpars);

        }

  
        
        private void btnOpen_Click(object sender, EventArgs e) //打开文件夹
        {
            if (Directory.Exists(Toolpars.formEntity.txtToPath + @"\Digiwin.ERP."
                                 + Toolpars.formEntity.txtNewTypeKey)) {
                Process.Start(Toolpars.formEntity.txtToPath + @"\Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey);
            }
            else {
                MessageBox.Show("文件夹不存在~", "注意!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void txtNewTypeKey_TextChanged(object sender, EventArgs e) {
            if (ModiCkb.Checked ) {
                if (Toolpars.formEntity.txtToPath != ""
                    && Toolpars.formEntity.txtNewTypeKey != "") {
                    string strb1 = Toolpars.formEntity.txtPKGpath + "Digiwin.ERP."
                                   + Toolpars.formEntity.txtNewTypeKey.Substring(1);
                    if (Directory.Exists(strb1)) {
                        TreeViewTool.myPaintTreeView(_toolpars, strb1);
                    }
                    else {
                        treeView1.Nodes.Clear();
                    }
                }
                else {
                    treeView1.Nodes.Clear();
                }
            }
        }

        #region 复制到平台下

        private void btncopydll_Click(object sender, EventArgs e) {
            try {
            

                MyTool.copyDll(Toolpars);
                MessageBox.Show("复制成功 !!!");
            }
            catch (Exception ex) {
                string[] processNames = {
                    "Digiwin.Mars.ClientStart", "Digiwin.Mars.ServerStart",
                    "Digiwin.Mars.AccountSetStart"
                };
                bool f = checkCanCopyDll(processNames);
                if (f)
                {
                    btncopydll_Click(null, null);
                }
                else
                {
                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            sqlTools.insertToolInfo("S01231_20160503_01", "20160503", "btncopydll_Click");
        }

        private void btncopyUIdll_Click(object sender, EventArgs e) {
            try {
                string Export = Toolpars.PathEntity.ExportPath;
                string toPath = Toolpars.Mplatform + "\\DeployServer\\Shared\\Customization\\Programs\\";
                string filterStr = "*" + Toolpars.formEntity.txtNewTypeKey + ".UI.*";
                if (Toolpars.MIndustry) {
                    toPath = Toolpars.Mplatform + "\\DeployServer\\Shared\\Industry\\Programs\\";
                }
                MyTool.FileCopyUIdll(Export, toPath, filterStr);
            }
            catch (Exception ex) {
                string[] processNames = {
                    "Digiwin.Mars.ClientStart"
                };
                bool f = checkCanCopyDll(processNames);
                if (f) {
                    btncopyUIdll_Click(null,null);
                }
                else {
                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            sqlTools.insertToolInfo("S01231_20160503_01", "20160503", "btncopyUIdll_Click");
        }

        bool checkCanCopyDll(string[] processNames) {
            //string processNames = "Digiwin.Mars.ClientStart";
            bool flag = true;
            var infos = Process.GetProcesses();
            if (infos != null)
            {
                foreach (var info in infos)
                {
                    if (processNames.Contains(info.ProcessName))
                    {
                        flag = false;
                    }
                }
                if (!flag)
                {
                    if (MessageBox.Show("当前DLL被占用，是否结束进程以复制？", "警告", MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Warning)
                        == DialogResult.OK) {
                        flag = true;
                        OldTools.killProcess(processNames);
                    }
                    else
                    {
                        flag = false ;
                    }
                }
            }

            return flag;
        }

        #endregion

        #region 复制pkg代码

        private void btncopypkg_Click(object sender, EventArgs e) {
            try
            {
                Toolpars.GToIni = Toolpars.formEntity.txtToPath;
                if (Toolpars.formEntity.txtToPath != ""
                    && Toolpars.formEntity.txtNewTypeKey != "")
                {
                    if (Directory.Exists(Toolpars.formEntity.txtToPath))
                    {
                        DirectoryInfo tCusSRC = new DirectoryInfo(Toolpars.GToIni + @"\");
                        string strb1 = Toolpars.formEntity.txtPKGpath + "Digiwin.ERP."
                                       + Toolpars.formEntity.txtNewTypeKey.Substring(1);
                        if (!Directory.Exists(strb1))
                        {
                            MessageBox.Show("文件夹" + strb1 + "不存在，请查看！！！", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            if (
                                Directory.Exists(Path.Combine(Toolpars.GToIni + @"\",
                                    "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey)))
                            {
                                DialogResult result =
                                    MessageBox.Show(
                                        Path.Combine(Toolpars.formEntity.txtToPath, Toolpars.formEntity.txtNewTypeKey)
                                        + "\r\n目錄已存在，是否覆蓋??",
                                        "Warnning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (result == DialogResult.Yes)
                                {
                                    object tArgsPath = Path.Combine(Toolpars.GToIni + @"\",
                                        "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey);
                                    OldTools.DeleteAll(tArgsPath);
                                }
                                else
                                {
                                    return;
                                }
                            }
                            OldTools.CopyAllPKG(strb1, tCusSRC + "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey);
                        }
                    }
                    else
                    {
                        MessageBox.Show("文件夹" + Toolpars.formEntity.txtToPath + "不存在，请查看！！！", "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("请输入创建地址及名称", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                #region 修改命名

                OldTools.ModiName(Toolpars);

                #endregion

                MessageBox.Show("生成成功 !!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            sqlTools.insertToolInfo("S01231_20160503_01", "20160503", "COPY PKG SOURCE");

            return;

            //Toolpars.GToIni = Toolpars.formEntity.txtToPath;
            //if (Toolpars.formEntity.txtToPath != ""
            //    && Toolpars.formEntity.txtNewTypeKey != "") {
            //    if (Directory.Exists(Toolpars.formEntity.txtToPath)) {
            //        DirectoryInfo tCusSRC = new DirectoryInfo(Toolpars.GToIni + @"\");
            //        string strb1 = Toolpars.formEntity.txtPKGpath + "Digiwin.ERP."
            //                       + Toolpars.formEntity.txtNewTypeKey.Substring(1);
            //        if (!Directory.Exists(strb1)) {
            //            MessageBox.Show("文件夹" + strb1 + "不存在，请查看！！！", "Error", MessageBoxButtons.OK,
            //                MessageBoxIcon.Error);
            //            return;
            //        }
            //        else {
            //            ModiPKG form1 = new ModiPKG(_toolpars);
            //            form1.ShowDialog();
            //        }
            //    }
            //}
            //else {
            //    MessageBox.Show("个案路径或标准路径不可为空");
            //}
        }

        #endregion

        private void button1_Click(object sender, EventArgs e) {
            string[] processNames = {
                "Digiwin.Mars.ClientStart", "Digiwin.Mars.ServerStart",
                "Digiwin.Mars.AccountSetStart"
            };
            OldTools.killProcess(processNames);
        }


        private void Industry_CheckedChanged(object sender, EventArgs e) {
            Toolpars.MIndustry = Industry.Checked;
            Toolpars.formEntity.txtToPath = Toolpars.Mpath;
            if (Toolpars.MIndustry) {
                Toolpars.formEntity.txtToPath = Toolpars.MInpath;
            }
        }


        private void btnP_Click(object sender, EventArgs e) {
            if (Directory.Exists(@"\\192.168.168.15\E10_Shadow")) {
                Process.Start(@"\\192.168.168.15\E10_Shadow");
            }
            else {
                MessageBox.Show("文件夹不存在~", "注意!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnG_Click(object sender, EventArgs e) {
            if (Directory.Exists(Toolpars.formEntity.txtPKGpath + "Digiwin.ERP."
                                 + Toolpars.formEntity.txtNewTypeKey.Substring(1))) {
                Process.Start(Toolpars.formEntity.txtPKGpath + "Digiwin.ERP."
                              + Toolpars.formEntity.txtNewTypeKey.Substring(1));
            }
            else {
                MessageBox.Show("文件夹不存在~", "注意!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e) {
            var BorderLineStyle = ButtonBorderStyle.Solid;
            var BorderLineStyleNo = ButtonBorderStyle.None;
            int BorderWidth = 1;
            var BorderColor = Color.LightGray;
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                BorderColor, BorderWidth, BorderLineStyle,
                BorderColor, BorderWidth, BorderLineStyleNo,
                BorderColor, BorderWidth, BorderLineStyleNo,
                BorderColor, BorderWidth, BorderLineStyleNo);
        }
        
        private void myTreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
            MyTreeNode node = e.Node as MyTreeNode;
            myTreeView1.SelectedNode = node;
            showTreeView(node);
        }


        private void showTreeView(MyTreeNode node) {
                treeView1.Visible = false;
                scrollPanel.Visible = true;
                splitContainer3.Visible = true;
                createTree(node.buildeType.Id);
        }

        /// <summary>
        /// 设置流式布局，最大3列。
        /// 因为没有找到较好的前端实现方式，此方法不妥，winform程序不太自由
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VSTOOL_ClientSizeChanged(object sender, EventArgs e) {
            var clientSize = splitContainer2.Panel2.ClientSize;
            int currentWidth = clientSize.Width;
            int width = currentWidth;

            if (currentWidth > 600
                && currentWidth <= 800) {
                splitContainer3.Panel2Collapsed = false;
                splitContainer4.Panel2Collapsed = true;
                splitContainer3.SplitterDistance = width / 2;
            }
            else if (currentWidth > 800) {
                splitContainer3.Panel2Collapsed = false;
                splitContainer4.Panel2Collapsed = false;

                width /= 3;
                splitContainer4.SplitterDistance = width;
                splitContainer3.SplitterDistance = width;
            }
            else {
                splitContainer3.Panel2Collapsed = true;
                splitContainer4.Panel2Collapsed = true;
            }
            if (splitContainer3.Visible) {
                if (myTreeView1.SelectedNode != null)
                {
                    MyTreeNode node = myTreeView1.SelectedNode as MyTreeNode;
                    createTree(node.buildeType.Id);
                }
            }
          
        }

        /// <summary>
        /// 设置3个treeview外部panel  响应treeview滚动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="upAndDown"></param>
        private void myTreeView2_SetAutoScrollEvent(object sender, int upAndDown) {
            bool vscroll = scrollPanel.VerticalScroll.Visible;
            if (vscroll) {
                var Maxnum = scrollPanel.VerticalScroll.Maximum;
                int growbase = myTreeView2.Nodes.Count / 20;
                int growNum = growbase == 0 ? 40 : growbase * 40;

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
        }

        /// <summary>
        /// 单击选择,选择后将建立对应的文件信息，
        /// 指示模版文件位置与将要创建的文件位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myTreeView2_AfterCheck(object sender, TreeViewEventArgs e) {
            Toolpars.MDistince = false;
            string StrA = "";
            var node = e.Node as MyTreeNode;
            BuildeType builderType = node.buildeType;
            if (builderType.ReadOnly != null
                && builderType.ReadOnly.Equals("True")) {
                return;
            }

            List<FileInfos> fileInfos = new List<FileInfos>();
            if (e.Node.Checked) {

                builderType.Checked = "True";
                if (!ModiCkb.Checked) {
                    if (builderType.ShowParWindow != null
                        && builderType.ShowParWindow.Equals("False")) {
                        fileInfos = MyTool.createFileMappingInfo(_toolpars, builderType);
                    }
                    else {
                        ModiName MYForm = new ModiName(builderType, _toolpars);
                        MYForm.StartPosition = FormStartPosition.CenterParent;
                        if (MYForm.ShowDialog() == DialogResult.OK)
                        {

                            StrA = MYForm.txt01.Text + ";" + MYForm.txt02.Text;
                            fileInfos = MYForm.FileInfos;
                            //fileInfo.ActionNameFiled = "";
                            //fileInfo.ClassNameFiled = MYForm.txt01.Text;
                            //fileInfo.FileNameFiled = MYForm.txt01.Text;
                            //fileInfo.FunctionNameFiled = MYForm.txt02.Text;
                        }
                        else
                        {
                            builderType.Checked = "False";
                            e.Node.Checked = false;
                        }
                    }
                }
            }
            else {
                builderType.Checked = "False";
            }
            if (myTreeView1.SelectedNode != null)
            {
                var par_node = myTreeView1.SelectedNode as MyTreeNode;
                var par_item = _toolpars.BuilderEntity.BuildeTypies.ToList()
                    .Where(et => et.Id.Equals(par_node.buildeType.Id)).ToList();
                if (par_item.Count > 0)
                {
                    var citem = par_item[0].BuildeItems
                        .Where(et => et.Id.Equals((e.Node as MyTreeNode).buildeType.Id)).ToList();
                    if (citem != null
                        && citem.Count > 0)
                    {
                        if (e.Node.Checked)
                        {
                            citem.ForEach(ee => {
                                    ee.Checked = "True";
                                    ee.FileInfos = fileInfos;
                                }

                            );
                        }
                        else
                        {
                            citem.ForEach(ee => {
                                ee.Checked = "False";
                                ee.FileInfos = fileInfos;
                            });
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
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (ModiCkb.Checked)
            {
                ModiPKGForm form1 = new ModiPKGForm(_toolpars);
                if (form1.ShowDialog() == DialogResult.OK)
                {
                    treeView1.Visible = true;
                    splitContainer3.Visible = false;
                    scrollPanel.Visible = false;
                    if (Toolpars.formEntity.txtToPath != ""
                        && Toolpars.formEntity.txtNewTypeKey != ""
                        && Toolpars.formEntity.PkgTypekey != "")
                    {
                        string txtPKGpath = Toolpars.formEntity.txtPKGpath;
                        if (!txtPKGpath.EndsWith(@"\"))
                        {
                            txtPKGpath += @"\";
                        }
                        string strb1 = txtPKGpath + "Digiwin.ERP."
                                       + Toolpars.formEntity.PkgTypekey;
                        if (Toolpars.formEntity.PkgTypekey.StartsWith("Digiwin.ERP."))
                        {
                            strb1 = txtPKGpath + Toolpars.formEntity.PkgTypekey;
                        }

                        if (Directory.Exists(strb1))
                        {
                            myTreeView5.Nodes.Clear();
                            treeView1.Nodes.Clear();
                            treeView1.Nodes.Add(TreeViewTool.myPaintTreeView(_toolpars, strb1)); //mfroma
                            treeView1.ExpandAll();
                        }
                        else
                        {
                            MessageBox.Show("标准代码" + strb1 + "不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ModiCkb.Checked = !ModiCkb.Checked;
                            myTreeView5.Nodes.Clear();

                        }
                    }
                    else
                    {
                        MessageBox.Show("创建位置或typeKey不可为空");
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
                splitContainer3.Visible = true;
                if (myTreeView1.SelectedNode != null)
                {
                    MyTreeNode node = myTreeView1.SelectedNode as MyTreeNode;
                    createTree(node.buildeType.Id);
                }
            }
        } 
        #endregion
    }
}