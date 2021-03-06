﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Diagnostics;
using Common.Implement;

namespace VSTool {
    public partial class VSTOOL : Form
    {
        #region 屬性

        private toolpars _toolpars = new toolpars();

        public toolpars Toolpars {
            get { return _toolpars; }
        }

        #endregion

       
        public VSTOOL(string[] pToIni) {
           
            InitializeComponent();
         
            this.txtToPath.DataBindings.Add(new Binding("Text",Toolpars.formEntity, "txtToPath",true,DataSourceUpdateMode.OnPropertyChanged));
            this.txtPKGpath.DataBindings.Add(new Binding("Text", Toolpars.formEntity, "txtPKGpath",true,DataSourceUpdateMode.OnPropertyChanged));
            this.txtNewTypeKey.DataBindings.Add(new Binding("Text", Toolpars.formEntity, "txtNewTypeKey",true,DataSourceUpdateMode.OnPropertyChanged));
            this.Industry.DataBindings.Add(new Binding("Checked", Toolpars.formEntity, "Industry",true,DataSourceUpdateMode.OnPropertyChanged));

            #region 自動更新

            //CallUpdate.autoUpdate();

            #endregion

            #region 複製最新的佈署dll

            try {
                //string mServerExePath = CallUpdate.GetExeFolder(CallUpdate.GetServerExePath("VSTool"));
                //Tools.CopynewVSTool(mServerExePath, Toolpars.MVSToolpath);
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
                btnKill.Visible = true;
            }

            Toolpars.OldTypekey = "XTEST";
        }

        private List<string> Mnotes = new List<string>();


        private void VSTOOL_Load(object sender, EventArgs e) {
            #region 给treeview控件添加选项
            System.Xml.XmlDocument document = new System.Xml.XmlDataDocument();
            document.Load(Toolpars.MVSToolpath + @"Config\TYPE.xml");

            treeView1.Nodes.Clear();
        //    treeView1.ShowPlusMinus = false;
            treeView1.ShowLines = false;

            populateTreeControl(document.DocumentElement, this.treeView1.Nodes,null);
            //treeView1.ExpandAll();显示所有节点

            #endregion
        }

        private void populateTreeControl(XmlNode document, TreeNodeCollection nodes,string filterNode) {
            foreach (System.Xml.XmlNode node in document.ChildNodes) {
                string text = (node.Value ?? ((node.Attributes != null &&
                                               node.Attributes.Count > 0)
                    ? node.Attributes[0].Value
                    : node.Name));
                if (!findByName(node, filterNode)) {
                    continue;
                }
                  GTreeNode new_child = new GTreeNode(text);
                new_child.ForeColor = Color.Gray;
                new_child.CheckBoxVisible = true;

                if (node.ChildNodes.Count > 0)
                {
                    new_child.CheckBoxVisible = false;

                }


                nodes.Add(new_child);
                populateTreeControl(node, new_child.Nodes, filterNode);
            }
        }

       public bool findByName(XmlNode node, string filterNode) {
            var flag = (filterNode ?? string.Empty).Equals(string.Empty);
           if (node.HasChildNodes) {
               foreach (System.Xml.XmlNode newChildNode in node.ChildNodes) {
                   string text = (node.Value ?? ((node.Attributes != null &&
                                                  node.Attributes.Count > 0)
                                      ? node.Attributes[0].Value
                                      : node.Name));
                   if (!(filterNode ?? string.Empty).Equals(string.Empty)
                       && text.IndexOf(filterNode, StringComparison.CurrentCulture) > -1) {
                       flag = true;
                       break;
                   }
                   if (findByName(newChildNode, filterNode)) {
                       flag = true;
                       break;
                   }
               }
           }
           else {
               string text = (node.Value ?? ((node.Attributes != null &&
                                              node.Attributes.Count > 0)
                                  ? node.Attributes[0].Value
                                  : node.Name));

               if (!(filterNode ?? string.Empty).Equals(string.Empty)
                   && text.IndexOf(filterNode, StringComparison.CurrentCulture) > -1)
               {
                   flag = true;
               }
            
            }
           
            return flag;
        }

        private List<string> msave = new List<string>();

        #region insertToolInfo

      

        #endregion

        private void btnCreate_Click(object sender, EventArgs e) {
            try {
                Tools.WriteLog(Toolpars,listDATA);

                Toolpars.GToIni = Toolpars.formEntity.txtToPath;
                if ((Toolpars.formEntity.txtToPath == "")
                    || (Toolpars.formEntity.txtNewTypeKey == "")) {
                    MessageBox.Show("请输入创建地址及名称", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DirectoryInfo tCusSRC = new DirectoryInfo(Toolpars.GToIni + @"\");
                if (Directory.Exists(Path.Combine(Toolpars.GToIni + @"\", "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey))) {
                    DialogResult result =
                        MessageBox.Show(Path.Combine(Toolpars.formEntity.txtToPath, Toolpars.formEntity.txtNewTypeKey) + "\r\n目錄已存在，是否覆蓋??",
                            "Warnning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes) {
                        object tArgsPath = Path.Combine(Toolpars.GToIni + @"\", "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey);
                        Tools.DeleteAll(tArgsPath);
                    }
                    else {
                        return;
                    }
                }

                if (Directory.Exists(Toolpars.MVSToolpath + "Digiwin.ERP." + Toolpars.OldTypekey)) {
                    Tools.CopyAll(Toolpars.MVSToolpath + "Digiwin.ERP." + Toolpars.OldTypekey,
                        tCusSRC + "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey);
                }

                #region 修改文件名

                DirectoryInfo tDes =
                    new DirectoryInfo(Toolpars.GToIni + @"\" + "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey + @"\");
                List<string> tSearchPatternList = new List<string>();
                tSearchPatternList.Add("*xml");
                tSearchPatternList.Add("*.sln");
                tSearchPatternList.Add("*.repx");
                tSearchPatternList.Add("*proj");
                tSearchPatternList.Add("*.complete");
                tSearchPatternList.Add("*.cs");
                foreach (System.IO.DirectoryInfo d in tDes.GetDirectories("*", SearchOption.AllDirectories)) {
                    if (d.Name.IndexOf(Toolpars.formEntity.txtNewTypeKey) == -1) {
                        if (d.Name.IndexOf(Toolpars.OldTypekey) != -1) {
                            if (
                                File.Exists(d.Parent.FullName + "\\"
                                            + d.Name.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey))) {
                                File.SetAttributes(
                                    d.Parent.FullName + "\\" + d.Name.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey),
                                    FileAttributes.Normal);
                                File.Delete(d.Parent.FullName + "\\"
                                            + d.Name.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey));
                            }
                            if (
                                Directory.Exists(d.Parent.FullName + "\\" +
                                                 d.Name.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey)) == false) {
                                d.MoveTo(d.Parent.FullName + "\\"
                                         + d.Name.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey));
                            }
                            Application.DoEvents();
                        }
                    }
                }


                foreach (System.IO.FileInfo f in tDes.GetFiles("*", SearchOption.AllDirectories)) {
                    if (f.Name.IndexOf(Toolpars.formEntity.txtNewTypeKey) == -1) {
                        if (f.Name.IndexOf(Toolpars.OldTypekey) != -1) {
                            if (File.Exists(f.FullName)) {
                                if (
                                    File.Exists(f.Directory.FullName + "\\" +
                                                f.Name.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey)) == false) {
                                    f.MoveTo(f.Directory.FullName + "\\" +
                                             f.Name.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey));
                                }
                                Application.DoEvents();
                            }
                        }
                    }
                }


                for (int i = 0; i < tSearchPatternList.Count; i++) {
                    foreach (System.IO.FileInfo f in tDes.GetFiles(tSearchPatternList[i], SearchOption.AllDirectories)) {
                        if (File.Exists(f.FullName)) {
                            string text = File.ReadAllText(f.FullName);
                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                            File.Delete(f.FullName);
                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                            //^_^ 20160802 by 05485 for 编码方式修改
                        }
                    }
                }

                #endregion

                if (RBBatch.Checked
                    || RBReport.Checked) {
                    string mpatha = tCusSRC + "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey + @"\" + "Digiwin.ERP." +
                                    Toolpars.formEntity.txtNewTypeKey + ".UI.Implement\\FunctionWindowOpener";
                    if (Directory.Exists(mpatha)) {
                        DirectoryInfo di = new DirectoryInfo(mpatha);
                        di.Delete(true);
                    }
                    mpatha = tCusSRC + "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey + @"\" + "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey
                             +
                             ".UI.Implement\\MenuItem";
                    if (Directory.Exists(mpatha)) {
                        DirectoryInfo diS = new DirectoryInfo(mpatha);
                        diS.Delete(true);
                    }
                }
                if (listDATA.Items.Count != 0) {
                    CopyFile(tCusSRC + "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey);
                    if (rbModi.Checked) {
                        copyModi();
                    }
                }
                msave.Clear();
                listDATA.Items.Clear();
                foreach (TreeNode node in treeView1.Nodes) {
                    SetCheckedChildNodes(node);
                    node.Checked = false;
                }
                MessageBox.Show("生成成功 !!!");
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            sqlTools.insertToolInfo("S01231_20160503_01", "20160503", "Create" + Toolpars.formEntity.txtNewTypeKey);
        }

        public void CopyFileA(string mSTR) //统计相同切片不同时机点
        {
            for (int i = 0; i < listDATA.Items.Count; i++) {
                if (listDATA.Items[i].ToString().Contains(mSTR)) {
                    Toolpars.MDistince = true;
                    msave.Add(listDATA.Items[i].ToString());
                }
            }
        }

        /// <summary>
        /// 目标路径
        /// </summary>
        /// <param name="mpaths"></param>
        public void CopyFile(string mpaths) {
            int L = 0, H = 0, M = 0, N = 0, R = 0, T = 0, Q = 0, P = 0;
            int L1 = 0, H1 = 0, M1 = 0, N1 = 0, R1 = 0, T1 = 0, Q1 = 0, P1 = 0;
            string StrYY = "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey;
            for (int i = 0; i < listDATA.Items.Count; i++) {
                Random rad = new Random();
                int ra = rad.Next(0, 100);
                string mtypes = "";
                string str = listDATA.Items[i].ToString();
                string csname = str.Substring(0, str.LastIndexOf(";"));
                csname = csname.Substring(csname.LastIndexOf(";") + 1); //类名
                string hsname = str.Substring(str.LastIndexOf(";") + 1); //函数名
                string str2 = str.Substring(0, str.IndexOf(";"));
                if (str2.Contains("\\")) {
                    str = str2.Substring(0, str.IndexOf("\\"));
                }
                else {
                    str = str2;
                }
                //#region 建档

                #region 服务

                if (str == "服务") {
                    //接口
                    string mfrom = Toolpars.MVSToolpath +
                                   @"Digiwin.ERP.XTEST\Digiwin.ERP.XTEST.Business\InterFace\ITestsService.cs";
                    string mto = mpaths + @"\" + StrYY + ".Business\\InterFace\\ITestsService.cs";
                    if (!File.Exists(mto)) {
                        Tools.CopyFileCS(mpaths + @"\" + StrYY + ".Business", mto, mfrom, Toolpars);
                    }
                    else {
                        MessageBox.Show("已存在类" + mto.Substring(mto.LastIndexOf("\\") + 1) + "，请重新命名类名！", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                   //目标已存在
                    if (File.Exists(mto)) {
                        FileInfo f = new FileInfo(mto);
                        if (csname != "") {
                            string replaceText = "ITestsService";
                            string replacedText = f.Name.Replace(replaceText, csname);
                            string fullPath = string.Format("{0}\\{1}", f.Directory.FullName, replacedText);
                            if (File.Exists(fullPath) ) {
                                f.MoveTo(fullPath);
                                string text = File.ReadAllText(f.FullName);
                                text = text.Replace(replaceText, csname).Replace("Digiwin.ERP." + Toolpars.OldTypekey,
                                    "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey);
                                File.SetAttributes(f.FullName, FileAttributes.Normal);
                                File.Delete(f.FullName);
                                File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                //^_^ 20160802 by 05485 for 编码方式修改
                            }
                            Application.DoEvents();
                            Tools.ModiCS(mpaths + @"\" + StrYY + ".Business\\" + StrYY + ".Business.csproj",
                                "InterFace\\" + csname + ".cs");
                        }
                        else {
                            if (
                                File.Exists(f.Directory.FullName + "\\" +
                                            f.Name.Replace("ITestsService", "ITests" + ra + "Service")) == false) {
                                f.MoveTo(f.Directory.FullName + "\\" +
                                         f.Name.Replace("ITestsService", "ITests" + ra + "Service"));
                                string text = File.ReadAllText(f.FullName);
                                text = text.Replace("ITestsService", "ITests" + ra + "Service");
                                text = text.Replace("Digiwin.ERP." + Toolpars.OldTypekey,
                                    "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey);
                                File.SetAttributes(f.FullName, FileAttributes.Normal);
                                File.Delete(f.FullName);
                                File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                //^_^ 20160802 by 05485 for 编码方式修改
                            }
                            Application.DoEvents();
                            Tools.ModiCS(mpaths + @"\" + StrYY + ".Business\\" + StrYY + ".Business.csproj",
                                "InterFace\\ITests" + ra + "Service.cs");
                        }
                    }

                    //实现
                    mfrom = Toolpars.MVSToolpath +
                            @"Digiwin.ERP.XTEST\Digiwin.ERP.XTEST.Business.Implement\Implement\TestsService.cs";
                    mto = mpaths + @"\" + StrYY + ".Business.Implement\\Implement\\TestsService.cs";
                    if (!File.Exists(mto)) {
                        Tools.CopyFileCS(mpaths + @"\" + StrYY + ".Business.Implement", mto, mfrom, Toolpars);
                    }
                    else {
                        MessageBox.Show("已存在类" + mto.Substring(mto.LastIndexOf("\\") + 1) + "，请重新命名类名！", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    //System.IO.File.Copy(mfrom, mto, true);
                    if (File.Exists(mto)) {
                        FileInfo f = new FileInfo(mto);
                        if (csname != "") {
                            if (
                                File.Exists(f.Directory.FullName + "\\" +
                                            f.Name.Replace("TestsService", csname.Substring(1))) == false) {
                                f.MoveTo(f.Directory.FullName + "\\" +
                                         f.Name.Replace("TestsService", csname.Substring(1)));
                                string text = File.ReadAllText(f.FullName);
                                text = text.Replace("TestsService", csname.Substring(1));
                                text = text.Replace("Digiwin.ERP." + Toolpars.OldTypekey,
                                    "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey);
                                File.SetAttributes(f.FullName, FileAttributes.Normal);
                                File.Delete(f.FullName);
                                File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                //^_^ 20160802 by 05485 for 编码方式修改
                            }
                            Application.DoEvents();
                            Tools.ModiCS(
                                mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY + ".Business.Implement.csproj",
                                "Implement\\" + csname.Substring(1) + ".cs");
                        }
                        else {
                            if (
                                File.Exists(f.Directory.FullName + "\\" +
                                            f.Name.Replace("TestsService", "Tests" + ra + "Service")) == false) {
                                f.MoveTo(f.Directory.FullName + "\\" +
                                         f.Name.Replace("TestsService", "Tests" + ra + "Service"));
                                string text = File.ReadAllText(f.FullName);
                                text = text.Replace("TestsService", "Tests" + ra + "Service");
                                text = text.Replace("Digiwin.ERP." + Toolpars.OldTypekey,
                                    "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey);
                                File.SetAttributes(f.FullName, FileAttributes.Normal);
                                File.Delete(f.FullName);
                                File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                //^_^ 20160802 by 05485 for 编码方式修改
                            }
                            Application.DoEvents();
                            Tools.ModiCS(
                                mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY + ".Business.Implement.csproj",
                                "Implement\\Tests" + ra + "Service.cs");
                        }
                    }
                }

                #endregion

                #region 建档

                if (RBBusiness.Checked) {
                    #region 数据跟踪

                    if (str == "数据跟踪") {
                        string mfrom = Toolpars.MVSToolpath +
                                       @"Digiwin.ERP.XTEST\Digiwin.ERP.XTEST.UI.Implement\Interceptor\ChangeInterceptor.cs";
                        string mto = mpaths + @"\" + StrYY + ".UI.Implement\\Interceptor\\ChangeInterceptor.cs";
                        if (!File.Exists(mto)) {
                            Tools.CopyFileCS(mpaths + @"\" + StrYY + ".UI.Implement", mto, mfrom, Toolpars);
                        }
                        else {
                            MessageBox.Show("已存在类" + mto.Substring(mto.LastIndexOf("\\") + 1) + "，请重新命名类名！", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        //System.IO.File.Copy(mfrom, mto, true);
                        if (File.Exists(mto)) {
                            FileInfo f = new FileInfo(mto);
                            if (csname != "") {
                                if (hsname != "") {
                                    if (
                                        File.Exists(f.Directory.FullName + "\\" +
                                                    f.Name.Replace("ChangeInterceptor", csname)) == false) {
                                        f.MoveTo(f.Directory.FullName + "\\" +
                                                 f.Name.Replace("ChangeInterceptor", csname));
                                        string text = File.ReadAllText(f.FullName);
                                        text = text.Replace("ChangeInterceptor", csname);
                                        text = text.Replace("DataChanging", hsname);
                                        text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                        File.SetAttributes(f.FullName, FileAttributes.Normal);
                                        File.Delete(f.FullName);
                                        File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                        //^_^ 20160802 by 05485 for 编码方式修改
                                    }
                                }
                                else {
                                    if (
                                        File.Exists(f.Directory.FullName + "\\" +
                                                    f.Name.Replace("ChangeInterceptor", csname)) == false) {
                                        f.MoveTo(f.Directory.FullName + "\\" +
                                                 f.Name.Replace("ChangeInterceptor", csname));
                                        string text = File.ReadAllText(f.FullName);
                                        text = text.Replace("ChangeInterceptor", csname);
                                        text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                        File.SetAttributes(f.FullName, FileAttributes.Normal);
                                        File.Delete(f.FullName);
                                        File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                        //^_^ 20160802 by 05485 for 编码方式修改
                                    }
                                }
                                Application.DoEvents();
                                Tools.ModiCS(mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                    "Interceptor\\" + csname + ".cs");
                            }
                            else if (hsname != "") {
                                if (File.Exists(f.Directory.FullName + "\\" + f.Name)) {
                                    string text = File.ReadAllText(f.FullName);
                                    text = text.Replace("DataChanging", hsname);
                                    text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                    File.SetAttributes(f.FullName, FileAttributes.Normal);
                                    File.Delete(f.FullName);
                                    File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                    //^_^ 20160802 by 05485 for 编码方式修改
                                }
                                Application.DoEvents();
                                Tools.ModiCS(mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                    "Interceptor\\ChangeInterceptor.cs");
                            }
                            else {
                                if (File.Exists(f.Directory.FullName + "\\" + f.Name)) {
                                    string text = File.ReadAllText(f.FullName);
                                    text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                    File.SetAttributes(f.FullName, FileAttributes.Normal);
                                    File.Delete(f.FullName);
                                    File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                    //^_^ 20160802 by 05485 for 编码方式修改
                                }
                                Application.DoEvents();
                                Tools.ModiCS(mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                    "Interceptor\\ChangeInterceptor.cs");
                            }
                        }
                    }
                        #endregion
                        #region 菜单

                    else if (str == "菜单") {
                        if (str2.Substring(str2.LastIndexOf("\\") + 1) == "表决器") {
                            string mfrom = Toolpars.MVSToolpath +
                                           @"Digiwin.ERP.XTEST\Digiwin.ERP.XTEST.UI.Implement\MenuItem\testCommand.cs";
                            string mto = mpaths + @"\" + StrYY + ".UI.Implement\\MenuItem\\testCommand.cs";
                            if (!File.Exists(mto)) {
                                Tools.CopyFileCS(mpaths + @"\" + StrYY + ".UI.Implement", mto, mfrom, Toolpars);
                            }
                            else {
                                MessageBox.Show("已存在类" + mto.Substring(mto.LastIndexOf("\\") + 1) + "，请重新命名类名！", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            if (File.Exists(mto)) {
                                FileInfo f = new FileInfo(mto);
                                if (csname != "") {
                                    if (File.Exists(f.Directory.FullName + "\\" + f.Name.Replace("test", csname)) ==
                                        false) {
                                        if (hsname != "") {
                                            f.MoveTo(f.Directory.FullName + "\\" + f.Name.Replace("test", csname));
                                            string text = File.ReadAllText(f.FullName);
                                            text = text.Replace("test", csname);
                                            text = text.Replace("EnableDecider", hsname);
                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        else {
                                            f.MoveTo(f.Directory.FullName + "\\" + f.Name.Replace("test", csname));
                                            string text = File.ReadAllText(f.FullName);
                                            text = text.Replace("test", csname);
                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                    }
                                    Application.DoEvents();
                                    Tools.ModiCS(
                                        mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                        "MenuItem\\" + csname + "Command.cs");
                                }
                                else if (hsname != "") {
                                    if (File.Exists(f.Directory.FullName + "\\" + f.Name)) {
                                        string text = File.ReadAllText(f.FullName);
                                        text = text.Replace("EnableDecider", hsname);
                                        text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                        File.SetAttributes(f.FullName, FileAttributes.Normal);
                                        File.Delete(f.FullName);
                                        File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                        //^_^ 20160802 by 05485 for 编码方式修改
                                    }
                                    Application.DoEvents();
                                    Tools.ModiCS(
                                        mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                        "MenuItem\\testCommand.cs");
                                }
                                else {
                                    if (File.Exists(f.Directory.FullName + "\\" + f.Name)) {
                                        string text = File.ReadAllText(f.FullName);
                                        text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                        File.SetAttributes(f.FullName, FileAttributes.Normal);
                                        File.Delete(f.FullName);
                                        File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                        //^_^ 20160802 by 05485 for 编码方式修改
                                    }
                                    Application.DoEvents();
                                    Tools.ModiCS(
                                        mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                        "MenuItem\\testCommand.cs");
                                }
                            }
                            //Tools.ModiCS(mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj", "MenuItem\\" + csname + "Command.cs");
                        }
                        else {
                            string mfrom = Toolpars.MVSToolpath +
                                           @"Digiwin.ERP.XTEST\Digiwin.ERP.XTEST.UI.Implement\MenuItem\test2Command.cs";
                            string mto = mpaths + @"\" + StrYY + ".UI.Implement\\MenuItem\\test2Command.cs";
                            if (!File.Exists(mto)) {
                                Tools.CopyFileCS(mpaths + @"\" + StrYY + ".UI.Implement", mto, mfrom, Toolpars);
                            }
                            else {
                                MessageBox.Show("已存在类" + mto.Substring(mto.LastIndexOf("\\") + 1) + "，请重新命名类名！", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            if (File.Exists(mto)) {
                                FileInfo f = new FileInfo(mto);
                                if (csname != "") {
                                    if (File.Exists(f.Directory.FullName + "\\" + f.Name.Replace("test2", csname)) ==
                                        false) {
                                        f.MoveTo(f.Directory.FullName + "\\" + f.Name.Replace("test2", csname));
                                        string text = File.ReadAllText(f.FullName);
                                        text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                        text = text.Replace("test2", csname);
                                        File.SetAttributes(f.FullName, FileAttributes.Normal);
                                        File.Delete(f.FullName);
                                        File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                        //^_^ 20160802 by 05485 for 编码方式修改
                                    }
                                    Application.DoEvents();
                                    Tools.ModiCS(
                                        mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                        "MenuItem\\" + csname + "Command.cs");
                                }
                                else {
                                    if (File.Exists(f.Directory.FullName + "\\" + f.Name)) {
                                        string text = File.ReadAllText(f.FullName);
                                        text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                        File.SetAttributes(f.FullName, FileAttributes.Normal);
                                        File.Delete(f.FullName);
                                        File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                        //^_^ 20160802 by 05485 for 编码方式修改
                                    }
                                    Application.DoEvents();
                                    Tools.ModiCS(
                                        mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                        "MenuItem\\test2Command.cs");
                                }
                            }
                        }
                    }
                        #endregion
                        #region 功能开窗

                    else if (str == "功能开窗") {
                        string mfrom = Toolpars.MVSToolpath +
                                       @"Digiwin.ERP.XTEST\Digiwin.ERP.XTEST.UI.Implement\FunctionWindowOpener\FWOpener.cs";
                        string mto = mpaths + @"\" + StrYY + ".UI.Implement\\FunctionWindowOpener\\FWOpener.cs";
                        if (!File.Exists(mto)) {
                            Tools.CopyFileCS(mpaths + @"\" + StrYY + ".UI.Implement", mto, mfrom, Toolpars);
                        }
                        else {
                            MessageBox.Show("已存在类" + mto.Substring(mto.LastIndexOf("\\") + 1) + "，请重新命名类名！", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (File.Exists(mto)) {
                            FileInfo f = new FileInfo(mto);
                            if (csname != "") {
                                if (File.Exists(f.Directory.FullName + "\\" + f.Name.Replace("FWOpener", csname)) ==
                                    false) {
                                    f.MoveTo(f.Directory.FullName + "\\" + f.Name.Replace("FWOpener", csname));
                                    string text = File.ReadAllText(f.FullName);
                                    text = text.Replace("FWOpener", csname);
                                    text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                    File.SetAttributes(f.FullName, FileAttributes.Normal);
                                    File.Delete(f.FullName);
                                    File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                    //^_^ 20160802 by 05485 for 编码方式修改
                                }
                                Application.DoEvents();
                                Tools.ModiCS(mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                    "FunctionWindowOpener\\" + csname + ".cs");
                            }
                            else {
                                if (File.Exists(f.Directory.FullName + "\\" + f.Name)) {
                                    string text = File.ReadAllText(f.FullName);
                                    text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                    File.SetAttributes(f.FullName, FileAttributes.Normal);
                                    File.Delete(f.FullName);
                                    File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                    //^_^ 20160802 by 05485 for 编码方式修改
                                }
                                Application.DoEvents();
                                Tools.ModiCS(mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                    "FunctionWindowOpener\\FWOpener.cs");
                            }
                        }
                    }
                        #endregion
                        #region 校验

                    else if (str == "校验") {
                        string mfrom = Toolpars.MVSToolpath +
                                       @"Digiwin.ERP.XTEST\Digiwin.ERP.XTEST.Business.Implement\Interceptor\testValidate.cs";
                        string mto = mpaths + @"\" + StrYY + ".Business.Implement\\Interceptor\\testValidate.cs";
                        if (!File.Exists(mto)) {
                            Tools.CopyFileCS(mpaths + @"\" + StrYY + ".Business.Implement", mto, mfrom, Toolpars);
                        }
                        else {
                            MessageBox.Show("已存在类" + mto.Substring(mto.LastIndexOf("\\") + 1) + "，请重新命名类名！", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        //System.IO.File.Copy(mfrom, mto, true);
                        if (File.Exists(mto)) {
                            FileInfo f = new FileInfo(mto);
                            if (csname != "") {
                                if (File.Exists(f.Directory.FullName + "\\" + f.Name.Replace("testValidate", csname)) ==
                                    false) {
                                    if (hsname != "") {
                                        f.MoveTo(f.Directory.FullName + "\\" + f.Name.Replace("testValidate", csname));
                                        string text = File.ReadAllText(f.FullName);
                                        text = text.Replace("testValidate", csname);
                                        text = text.Replace("ValidateS", hsname);
                                        text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                        File.SetAttributes(f.FullName, FileAttributes.Normal);
                                        File.Delete(f.FullName);
                                        File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                        //^_^ 20160802 by 05485 for 编码方式修改
                                    }
                                    else {
                                        f.MoveTo(f.Directory.FullName + "\\" + f.Name.Replace("testValidate", csname));
                                        string text = File.ReadAllText(f.FullName);
                                        text = text.Replace("testValidate", csname);
                                        text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                        File.SetAttributes(f.FullName, FileAttributes.Normal);
                                        File.Delete(f.FullName);
                                        File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                        //^_^ 20160802 by 05485 for 编码方式修改
                                    }
                                }
                                Application.DoEvents();
                                Tools.ModiCS(
                                    mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY +
                                    ".Business.Implement.csproj", "Interceptor\\" + csname + ".cs");
                            }
                            else if (hsname != "") {
                                if (File.Exists(f.Directory.FullName + "\\" + f.Name)) {
                                    string text = File.ReadAllText(f.FullName);
                                    text = text.Replace("ValidateS", hsname);
                                    text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                    File.SetAttributes(f.FullName, FileAttributes.Normal);
                                    File.Delete(f.FullName);
                                    File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                    //^_^ 20160802 by 05485 for 编码方式修改
                                }
                                Application.DoEvents();
                                Tools.ModiCS(
                                    mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY +
                                    ".Business.Implement.csproj", "Interceptor\\testValidate.cs");
                            }
                            else {
                                if (File.Exists(f.Directory.FullName + "\\" + f.Name)) {
                                    string text = File.ReadAllText(f.FullName);
                                    text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                    File.SetAttributes(f.FullName, FileAttributes.Normal);
                                    File.Delete(f.FullName);
                                    File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                    //^_^ 20160802 by 05485 for 编码方式修改
                                }
                                Application.DoEvents();
                                Tools.ModiCS(
                                    mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY +
                                    ".Business.Implement.csproj", "Interceptor\\testValidate.cs");
                            }
                        }
                        //Tools.ModiCS(mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY + ".Business.Implement.csproj", "Interceptor\\" + csname + ".cs");
                    }
                        #endregion
                        #region 保存

                    else if (str == "保存") {
                        string saveA = str2.Substring(0, str2.LastIndexOf("\\"));
                        saveA = saveA.Substring(saveA.LastIndexOf("\\") + 1);

                        if (saveA == "ISaveServiceEvents") {
                            if (L == 0) {
                                L = 1;
                                CopyFileA(str2.Substring(0, str2.LastIndexOf("\\")));
                                string mfrom = Toolpars.MVSToolpath +
                                               @"Digiwin.ERP.XTEST\Digiwin.ERP.XTEST.Business.Implement\Interceptor\SaveSInterceptor.cs";
                                string mto = mpaths + @"\" + StrYY +
                                             ".Business.Implement\\Interceptor\\SaveSInterceptor.cs";
                                if (!File.Exists(mto)) {
                                    Tools.CopyFileCS(mpaths + @"\" + StrYY + ".Business.Implement", mto, mfrom, Toolpars);
                                }
                                else {
                                    MessageBox.Show("已存在类" + mto.Substring(mto.LastIndexOf("\\") + 1) + "，请重新命名类名！",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                                if (File.Exists(mto)) {
                                    FileInfo f = new FileInfo(mto);
                                    if (csname != "") {
                                        if (
                                            File.Exists(f.Directory.FullName + "\\" +
                                                        f.Name.Replace("SaveSInterceptor", csname)) == false) {
                                            f.MoveTo(f.Directory.FullName + "\\" +
                                                     f.Name.Replace("SaveSInterceptor", csname));
                                            string text = File.ReadAllText(f.FullName);
                                            text = text.Replace("SaveSInterceptor", csname);

                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "Save" + ss;
                                                }

                                                string STRAS = "[EventInterceptor(typeof(ISaveServiceEvents),\"" + point
                                                               +
                                                               "\")]\r\n"
                                                               + "        public void " + mFUNC +
                                                               "(object sender, SaveEventArgs e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }

                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY +
                                            ".Business.Implement.csproj", "Interceptor\\" + csname + ".cs");
                                    }
                                    else {
                                        if (File.Exists(f.Directory.FullName + "\\" + f.Name)) {
                                            string text = File.ReadAllText(f.FullName);
                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "Saves" + ss;
                                                }
                                                string STRAS = "[EventInterceptor(typeof(ISaveServiceEvents),\"" + point
                                                               +
                                                               "\")]\r\n"
                                                               + "        public void " + mFUNC +
                                                               "(object sender, SaveEventArgs e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }

                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY +
                                            ".Business.Implement.csproj", "Interceptor\\SaveSInterceptor.cs");
                                    }
                                }
                                //Tools.ModiCS(mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY + ".Business.Implement.csproj", "Interceptor\\" + csname + ".cs");
                            }
                        }
                        else {
                            if (H == 0) {
                                H = 1;
                                CopyFileA(str2.Substring(0, str2.LastIndexOf("\\"))); //统计相同切片不同时机点
                                string mfrom = Toolpars.MVSToolpath +
                                               @"Digiwin.ERP.XTEST\Digiwin.ERP.XTEST.UI.Implement\Interceptor\SaveInterceptor.cs";
                                string mto = mpaths + @"\" + StrYY + ".UI.Implement\\Interceptor\\SaveInterceptor.cs";

                                if (!File.Exists(mto)) {
                                    Tools.CopyFileCS(mpaths + @"\" + StrYY + ".UI.Implement", mto, mfrom, Toolpars);
                                }
                                else {
                                    MessageBox.Show("已存在类" + mto.Substring(mto.LastIndexOf("\\") + 1) + "，请重新命名类名！",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                if (File.Exists(mto)) {
                                    FileInfo f = new FileInfo(mto);
                                    if (csname != "") {
                                        if (
                                            File.Exists(f.Directory.FullName + "\\" +
                                                        f.Name.Replace("SaveInterceptor", csname)) == false) {
                                            f.MoveTo(f.Directory.FullName + "\\" +
                                                     f.Name.Replace("SaveInterceptor", csname));
                                            string text = File.ReadAllText(f.FullName);
                                            text = text.Replace("SaveInterceptor", csname);

                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "mSave" + ss;
                                                }
                                                string STRAS =
                                                    "[EventInterceptor(typeof(IDocumentSaveServiceEvents),\"" + point +
                                                    "\")]\r\n"
                                                    + "        public void " + mFUNC +
                                                    "(object sender, DocumentSaveEventArgs e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }

                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                            "Interceptor\\" + csname + ".cs");
                                    }
                                    else {
                                        if (File.Exists(f.Directory.FullName + "\\" + f.Name)) {
                                            string text = File.ReadAllText(f.FullName);
                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "mSave" + ss;
                                                }
                                                string STRAS =
                                                    "[EventInterceptor(typeof(IDocumentSaveServiceEvents),\"" + point +
                                                    "\")]\r\n"
                                                    + "        public void " + mFUNC +
                                                    "(object sender, DocumentSaveEventArgs e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }
                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                            "Interceptor\\SaveInterceptor.cs");
                                    }
                                }
                            }
                        }
                    }
                        #endregion
                        #region 审核

                    else if (str == "审核") {
                        string ConfirmA = str2.Substring(0, str2.LastIndexOf("\\"));
                        ConfirmA = ConfirmA.Substring(ConfirmA.LastIndexOf("\\") + 1);

                        if ((ConfirmA == "IConfirmServiceEvents")
                            || (ConfirmA == "IDisconfirmServiceEvents")) {
                            if (M == 0) {
                                M = 1;
                                CopyFileA("审核\\IConfirmServiceEvents"); //统计相同切片不同时机点
                                CopyFileA("审核\\IDisconfirmServiceEvents");
                                string mfrom = Toolpars.MVSToolpath +
                                               @"Digiwin.ERP.XTEST\Digiwin.ERP.XTEST.Business.Implement\Interceptor\ApproveSInterceptor.cs";
                                string mto = mpaths + @"\" + StrYY +
                                             ".Business.Implement\\Interceptor\\ApproveSInterceptor.cs";
                                if (!File.Exists(mto)) {
                                    Tools.CopyFileCS(mpaths + @"\" + StrYY + ".Business.Implement", mto, mfrom, Toolpars);
                                }
                                else {
                                    MessageBox.Show("已存在类" + mto.Substring(mto.LastIndexOf("\\") + 1) + "，请重新命名类名！",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                if (File.Exists(mto)) {
                                    FileInfo f = new FileInfo(mto);
                                    if (csname != "") {
                                        if (
                                            File.Exists(f.Directory.FullName + "\\" +
                                                        f.Name.Replace("ApproveSInterceptor", csname)) == false) {
                                            f.MoveTo(f.Directory.FullName + "\\" +
                                                     f.Name.Replace("ApproveSInterceptor", csname));
                                            string text = File.ReadAllText(f.FullName);
                                            text = text.Replace("ApproveSInterceptor", csname);
                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mSERVER = msave[ss].Substring(0, msave[ss].LastIndexOf("\\"));
                                                mSERVER = mSERVER.Substring(mSERVER.LastIndexOf("\\") + 1);
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "Execute" + ss;
                                                }
                                                string STRAS = "[EventInterceptor(typeof(" + mSERVER + "),\"" + point +
                                                               "\")]\r\n"
                                                               + "        public void " + mFUNC +
                                                               "(object sender, ConfirmDetailEventArgs e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }

                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY +
                                            ".Business.Implement.csproj", "Interceptor\\" + csname + ".cs");
                                    }
                                    else {
                                        if (File.Exists(f.Directory.FullName + "\\" + f.Name)) {
                                            string text = File.ReadAllText(f.FullName);
                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mSERVER = msave[ss].Substring(0, msave[ss].LastIndexOf("\\"));
                                                mSERVER = mSERVER.Substring(mSERVER.LastIndexOf("\\") + 1);
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "Execute" + ss;
                                                }
                                                string STRAS = "[EventInterceptor(typeof(" + mSERVER + "),\"" + point +
                                                               "\")]\r\n"
                                                               + "        public void " + mFUNC +
                                                               "(object sender, ConfirmDetailEventArgs e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }

                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY +
                                            ".Business.Implement.csproj", "Interceptor\\ApproveSInterceptor.cs");
                                    }
                                }
                                //Tools.ModiCS(mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY + ".Business.Implement.csproj", "Interceptor\\" + csname + ".cs");
                            }
                        }
                        else {
                            if (N == 0) {
                                N = 1;
                                CopyFileA(str2.Substring(0, str2.LastIndexOf("\\"))); //统计相同切片不同时机点
                                string mfrom = Toolpars.MVSToolpath +
                                               @"Digiwin.ERP.XTEST\Digiwin.ERP.XTEST.UI.Implement\Interceptor\DocumentConfirmInterceptor.cs";
                                string mto = mpaths + @"\" + StrYY +
                                             ".UI.Implement\\Interceptor\\DocumentConfirmInterceptor.cs";
                                if (!File.Exists(mto)) {
                                    Tools.CopyFileCS(mpaths + @"\" + StrYY + ".UI.Implement", mto, mfrom, Toolpars);
                                }
                                else {
                                    MessageBox.Show("已存在类" + mto.Substring(mto.LastIndexOf("\\") + 1) + "，请重新命名类名！",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                if (File.Exists(mto)) {
                                    FileInfo f = new FileInfo(mto);
                                    if (csname != "") {
                                        if (
                                            File.Exists(f.Directory.FullName + "\\" +
                                                        f.Name.Replace("DocumentConfirmInterceptor", csname)) == false) {
                                            f.MoveTo(f.Directory.FullName + "\\" +
                                                     f.Name.Replace("DocumentConfirmInterceptor", csname));
                                            string text = File.ReadAllText(f.FullName);
                                            text = text.Replace("DocumentConfirmInterceptor", csname);
                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mSERVER = msave[ss].Substring(0, msave[ss].LastIndexOf("\\"));
                                                mSERVER = mSERVER.Substring(mSERVER.LastIndexOf("\\") + 1);
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "NewCodition" + ss;
                                                }
                                                string STRAS = "[EventInterceptor(typeof(" + mSERVER + "),\"" + point +
                                                               "\")]\r\n"
                                                               + "        public void " + mFUNC +
                                                               "(object sender, DocumentConfirmConditionPanelLoadingEventArgs e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }

                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                            "Interceptor\\" + csname + ".cs");
                                    }
                                    else {
                                        if (File.Exists(f.Directory.FullName + "\\" + f.Name)) {
                                            string text = File.ReadAllText(f.FullName);
                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mSERVER = msave[ss].Substring(0, msave[ss].LastIndexOf("\\"));
                                                mSERVER = mSERVER.Substring(mSERVER.LastIndexOf("\\") + 1);
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "NewCodition" + ss;
                                                }
                                                if (point == "ConditionPanelLoading") {
                                                    mtypes = "DocumentConfirmConditionPanelLoadingEventArgs";
                                                }
                                                else {
                                                    mtypes = "DocumentConfirmEventArgs ";
                                                }
                                                string STRAS = "[EventInterceptor(typeof(" + mSERVER + "),\"" + point +
                                                               "\")]\r\n"
                                                               + "        public void " + mFUNC + "(object sender, " +
                                                               mtypes + " e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }
                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                            "Interceptor\\DocumentConfirmInterceptor.cs");
                                    }
                                }
                            }
                        }
                    }
                        #endregion
                        #region 普通切片

                    else if (str == "普通切片") {
                        string ConfirmA = str2.Substring(0, str2.LastIndexOf("\\"));
                        ConfirmA = ConfirmA.Substring(ConfirmA.LastIndexOf("\\") + 1);
                        if ((ConfirmA == "IEditorView")
                            || (ConfirmA == "IBrowseView")
                            ||
                            (ConfirmA == "IDataEntityTraceService")) {
                            if (R == 0) {
                                R = 1;
                                CopyFileA("普通切片\\IEditorView"); //统计相同切片不同时机点
                                CopyFileA("普通切片\\IBrowseView");
                                CopyFileA("普通切片\\IDataEntityTraceService");
                                string mfrom = Toolpars.MVSToolpath +
                                               @"Digiwin.ERP.XTEST\Digiwin.ERP.XTEST.UI.Implement\Interceptor\DetailInterceptor.cs";
                                string mto = mpaths + @"\" + StrYY + ".UI.Implement\\Interceptor\\DetailInterceptor.cs";
                                if (!File.Exists(mto)) {
                                    Tools.CopyFileCS(mpaths + @"\" + StrYY + ".UI.Implement", mto, mfrom, Toolpars);
                                }
                                else {
                                    MessageBox.Show("已存在类" + mto.Substring(mto.LastIndexOf("\\") + 1) + "，请重新命名类名！",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                if (File.Exists(mto)) {
                                    FileInfo f = new FileInfo(mto);
                                    if (csname != "") {
                                        if (
                                            File.Exists(f.Directory.FullName + "\\" +
                                                        f.Name.Replace("DetailInterceptor", csname)) == false) {
                                            f.MoveTo(f.Directory.FullName + "\\" +
                                                     f.Name.Replace("DetailInterceptor", csname));
                                            string text = File.ReadAllText(f.FullName);
                                            text = text.Replace("DetailInterceptor", csname);
                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mSERVER = msave[ss].Substring(0, msave[ss].LastIndexOf("\\"));
                                                mSERVER = mSERVER.Substring(mSERVER.LastIndexOf("\\") + 1);
                                                //mSERVER = mSERVER.Substring(1);
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "Design" + ss;
                                                }
                                                string STRAS = "[EventInterceptor(typeof(" + mSERVER + "),\"" + point +
                                                               "\")]\r\n"
                                                               + "        public void " + mFUNC +
                                                               "(object sender, EventArgs e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }

                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                            "Interceptor\\" + csname + ".cs");
                                    }
                                    else {
                                        if (
                                            File.Exists(f.Directory.FullName + "\\" +
                                                        f.Name.Replace("DetailInterceptor", "DetailInterceptor" + ra))
                                            ==
                                            false) {
                                            f.MoveTo(f.Directory.FullName + "\\" +
                                                     f.Name.Replace("DetailInterceptor", "DetailInterceptor" + ra));
                                            string text = File.ReadAllText(f.FullName);
                                            text = text.Replace("DetailInterceptor", "DetailInterceptor" + ra);
                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mSERVER = msave[ss].Substring(0, msave[ss].LastIndexOf("\\"));
                                                mSERVER = mSERVER.Substring(mSERVER.LastIndexOf("\\") + 1);
                                                //mSERVER = mSERVER.Substring(1);
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "Design" + ss;
                                                }
                                                string STRAS = "[EventInterceptor(typeof(" + mSERVER + "),\"" + point +
                                                               "\")]\r\n"
                                                               + "        public void " + mFUNC +
                                                               "(object sender, EventArgs e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }

                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                            "Interceptor\\DetailInterceptor" + ra + ".cs");
                                    }
                                }
                            }
                        }
                        else if (ConfirmA == "IDocumentDataConvertSubServiceEvents") {
                            if (T == 0) {
                                T = 1;
                                CopyFileA("普通切片\\IDocumentDataConvertSubServiceEvents");
                                //统计相同切片不同时机点                          
                                string mfrom = Toolpars.MVSToolpath +
                                               @"Digiwin.ERP.XTEST\Digiwin.ERP.XTEST.UI.Implement\Interceptor\DetailInterceptor.cs";
                                string mto = mpaths + @"\" + StrYY + ".UI.Implement\\Interceptor\\DetailInterceptor.cs";
                                if (!File.Exists(mto)) {
                                    Tools.CopyFileCS(mpaths + @"\" + StrYY + ".UI.Implement", mto, mfrom, Toolpars);
                                }
                                else {
                                    MessageBox.Show("已存在类" + mto.Substring(mto.LastIndexOf("\\") + 1) + "，请重新命名类名！",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                if (File.Exists(mto)) {
                                    FileInfo f = new FileInfo(mto);
                                    if (csname != "") {
                                        if (
                                            File.Exists(f.Directory.FullName + "\\" +
                                                        f.Name.Replace("DetailInterceptor", csname)) == false) {
                                            f.MoveTo(f.Directory.FullName + "\\" +
                                                     f.Name.Replace("DetailInterceptor", csname));
                                            string text = File.ReadAllText(f.FullName);
                                            text = text.Replace("DetailInterceptor", csname);

                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mSERVER = msave[ss].Substring(0, msave[ss].LastIndexOf("\\"));
                                                mSERVER = mSERVER.Substring(mSERVER.LastIndexOf("\\") + 1);
                                                //mSERVER = mSERVER.Substring(1);
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "Convert" + ss;
                                                }
                                                string STRAS = "[EventInterceptor(typeof(" + mSERVER + "),\"" + point +
                                                               "\")]\r\n"
                                                               + "        public void " + mFUNC +
                                                               "(object sender, DocumentDataConvertSubServiceEventArgs e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }

                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                            "Interceptor\\" + csname + ".cs");
                                    }
                                    else {
                                        if (
                                            File.Exists(f.Directory.FullName + "\\" +
                                                        f.Name.Replace("DetailInterceptor", "DetailInterceptor" + ra))
                                            ==
                                            false) {
                                            f.MoveTo(f.Directory.FullName + "\\" +
                                                     f.Name.Replace("DetailInterceptor", "DetailInterceptor" + ra));
                                            string text = File.ReadAllText(f.FullName);
                                            text = text.Replace("DetailInterceptor", "DetailInterceptor" + ra);
                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mSERVER = msave[ss].Substring(0, msave[ss].LastIndexOf("\\"));
                                                mSERVER = mSERVER.Substring(mSERVER.LastIndexOf("\\") + 1);
                                                //mSERVER = mSERVER.Substring(1);
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "Convert" + ss;
                                                }
                                                string STRAS = "[EventInterceptor(typeof(" + mSERVER + "),\"" + point +
                                                               "\")]\r\n"
                                                               + "        public void " + mFUNC +
                                                               "(object sender, DocumentDataConvertSubServiceEventArgs e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }

                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                            "Interceptor\\DetailInterceptor" + ra + ".cs");
                                    }
                                }
                            }
                        }
                        else if ((ConfirmA == "IDeleteServiceEvents")
                                 || (ConfirmA == "IApproveStatusServiceEvents")
                                 ||
                                 (ConfirmA == "IDataConvertServiceEvents")) {
                            if (Q == 0) {
                                Q = 1;
                                CopyFileA("普通切片\\IDeleteServiceEvents"); //统计相同切片不同时机点
                                CopyFileA("普通切片\\IApproveStatusServiceEvents");
                                CopyFileA("普通切片\\IDataConvertServiceEvents");
                                string mfrom = Toolpars.MVSToolpath +
                                               @"Digiwin.ERP.XTEST\Digiwin.ERP.XTEST.Business.Implement\Interceptor\DetailInterceptorS.cs";
                                string mto = mpaths + @"\" + StrYY +
                                             ".Business.Implement\\Interceptor\\DetailInterceptorS.cs";
                                if (!File.Exists(mto)) {
                                    Tools.CopyFileCS(mpaths + @"\" + StrYY + ".Business.Implement", mto, mfrom, Toolpars);
                                }
                                else {
                                    MessageBox.Show(
                                        "已存在类" + mto.Substring(mto.LastIndexOf("\\") + 1) + "，请重新命名类名！", "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                if (File.Exists(mto)) {
                                    FileInfo f = new FileInfo(mto);
                                    if (csname != "") {
                                        if (
                                            File.Exists(f.Directory.FullName + "\\" +
                                                        f.Name.Replace("DetailInterceptorS", csname)) == false) {
                                            f.MoveTo(f.Directory.FullName + "\\" +
                                                     f.Name.Replace("DetailInterceptorS", csname));
                                            string text = File.ReadAllText(f.FullName);
                                            text = text.Replace("DetailInterceptorS", csname);

                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mSERVER = msave[ss].Substring(0, msave[ss].LastIndexOf("\\"));
                                                mSERVER = mSERVER.Substring(mSERVER.LastIndexOf("\\") + 1);
                                                //mSERVER = mSERVER.Substring(1);
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "mDesign" + ss;
                                                }
                                                string MTYPE = "";
                                                if (mSERVER == "IDeleteServiceEvents") {
                                                    MTYPE = "DeleteEventArgs";
                                                }
                                                else if (mSERVER == "IApproveStatusServiceEvents") {
                                                    MTYPE = "ApproveStatusEventArgs";
                                                }
                                                else {
                                                    MTYPE = "DataConvertEventArgs";
                                                }
                                                string STRAS = "[EventInterceptor(typeof(" + mSERVER + "),\"" +
                                                               point + "\")]\r\n"
                                                               + "        public void " + mFUNC + "(object sender, " +
                                                               MTYPE +
                                                               " e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }

                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY +
                                            ".Business.Implement.csproj", "Interceptor\\" + csname + ".cs");
                                    }
                                    else {
                                        if (
                                            File.Exists(f.Directory.FullName + "\\" +
                                                        f.Name.Replace("DetailInterceptorS",
                                                            "DetailInterceptorS" + ra)) == false) {
                                            f.MoveTo(f.Directory.FullName + "\\" +
                                                     f.Name.Replace("DetailInterceptorS", "DetailInterceptorS" + ra));
                                            string text = File.ReadAllText(f.FullName);
                                            text = text.Replace("DetailInterceptorS", "DetailInterceptorS" + ra);
                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mSERVER = msave[ss].Substring(0, msave[ss].LastIndexOf("\\"));
                                                mSERVER = mSERVER.Substring(mSERVER.LastIndexOf("\\") + 1);
                                                //mSERVER = mSERVER.Substring(1);
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "mDesign" + ss;
                                                }
                                                string MTYPE = "";
                                                if (mSERVER == "IDeleteServiceEvents") {
                                                    MTYPE = "DeleteEventArgs";
                                                }
                                                else if (mSERVER == "IApproveStatusServiceEvents") {
                                                    MTYPE = "ApproveStatusEventArgs";
                                                }
                                                else {
                                                    MTYPE = "DataConvertEventArgs";
                                                }
                                                string STRAS = "[EventInterceptor(typeof(" + mSERVER + "),\"" +
                                                               point + "\")]\r\n"
                                                               + "        public void " + mFUNC + "(object sender, " +
                                                               MTYPE +
                                                               " e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }

                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY +
                                            ".Business.Implement.csproj",
                                            "Interceptor\\DetailInterceptorS" + ra + ".cs");
                                    }
                                }
                            }
                        }
                    }

                    #endregion
                }

                #endregion

                #region 批次

                if (RBBatch.Checked) {
                    string mService = str2.Substring(str2.IndexOf("\\") + 1);
                    if (mService.Contains("\\")) {
                        mService = mService.Substring(0, mService.IndexOf("\\"));
                    }

                    #region 向导式批次

                    if (str == "向导式批次") {
                        #region GuideInterceptor

                        if (mService == "GuideInterceptor") {
                            if (P == 0) {
                                P = 1;
                                CopyFileA(str2.Substring(0, str2.LastIndexOf("\\")));
                                string mfrom = Toolpars.MVSToolpath +
                                               @"Digiwin.ERP.XTEST\Digiwin.ERP.XTEST.UI.Implement\Interceptor\GuideInterceptor.cs";
                                string mto = mpaths + @"\" + StrYY + ".UI.Implement\\Interceptor\\GuideInterceptor.cs";
                                if (!File.Exists(mto)) {
                                    Tools.CopyFileCS(mpaths + @"\" + StrYY + ".UI.Implement", mto, mfrom, Toolpars);
                                }
                                else {
                                    MessageBox.Show("已存在类" + mto.Substring(mto.LastIndexOf("\\") + 1) + "，请重新命名类名！",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                if (File.Exists(mto)) {
                                    FileInfo f = new FileInfo(mto);
                                    if (csname != "") {
                                        if (
                                            File.Exists(f.Directory.FullName + "\\" +
                                                        f.Name.Replace("GuideInterceptor", csname)) == false) {
                                            f.MoveTo(f.Directory.FullName + "\\" +
                                                     f.Name.Replace("GuideInterceptor", csname));
                                            string text = File.ReadAllText(f.FullName);
                                            text = text.Replace("GuideInterceptor", csname);

                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "StepActionService" + ss;
                                                }
                                                string STRAS =
                                                    "[EventInterceptor(typeof(IStepActionControlServiceEvents),\"" +
                                                    point + "\")]\r\n"
                                                    + "        public void " + mFUNC +
                                                    "(object sender, StepActionEventArgs e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }

                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                            "Interceptor\\" + csname + ".cs");
                                    }
                                    else {
                                        if (File.Exists(f.Directory.FullName + "\\" + f.Name)) {
                                            string text = File.ReadAllText(f.FullName);
                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "StepActionService" + ss;
                                                }
                                                string STRAS =
                                                    "[EventInterceptor(typeof(IStepActionControlServiceEvents),\"" +
                                                    point + "\")]\r\n"
                                                    + "        public void " + mFUNC +
                                                    "(object sender, StepActionEventArgs e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }
                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                            "Interceptor\\GuideInterceptor.cs");
                                    }
                                }
                            }
                        }
                            #endregion
                            #region ConditionAction

                        else if (mService == "ConditionAction") {
                            string mfrom = Toolpars.MVSToolpath +
                                           @"Digiwin.ERP.XTEST\Digiwin.ERP.XTEST.UI.Implement\Interceptor\GConditionAction.cs";
                            string mto = mpaths + @"\" + StrYY + ".UI.Implement\\Interceptor\\GConditionAction.cs";
                            if (!File.Exists(mto)) {
                                Tools.CopyFileCS(mpaths + @"\" + StrYY + ".UI.Implement", mto, mfrom, Toolpars);
                            }
                            else {
                                MessageBox.Show("已存在类" + mto.Substring(mto.LastIndexOf("\\") + 1) + "，请重新命名类名！", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            if (File.Exists(mto)) {
                                FileInfo f = new FileInfo(mto);
                                if (csname != "") {
                                    if (
                                        File.Exists(f.Directory.FullName + "\\" +
                                                    f.Name.Replace("GConditionAction", csname)) == false) {
                                        f.MoveTo(f.Directory.FullName + "\\" +
                                                 f.Name.Replace("GConditionAction", csname));
                                        string text = File.ReadAllText(f.FullName);
                                        text = text.Replace("GConditionAction", csname);
                                        text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                        File.SetAttributes(f.FullName, FileAttributes.Normal);
                                        File.Delete(f.FullName);
                                        File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                        //^_^ 20160802 by 05485 for 编码方式修改
                                    }
                                    Application.DoEvents();
                                    Tools.ModiCS(
                                        mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                        "Interceptor\\" + csname + ".cs");
                                }
                                else {
                                    if (File.Exists(f.Directory.FullName + "\\" + f.Name)) {
                                        string text = File.ReadAllText(f.FullName);
                                        text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                        File.SetAttributes(f.FullName, FileAttributes.Normal);
                                        File.Delete(f.FullName);
                                        File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                        //^_^ 20160802 by 05485 for 编码方式修改
                                    }
                                    Application.DoEvents();
                                    Tools.ModiCS(
                                        mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                        "Interceptor\\GConditionAction.cs");
                                }
                            }
                        }
                            #endregion
                            #region QueryResultAction

                        else if (mService == "QueryResultAction") {
                            string mfrom = Toolpars.MVSToolpath +
                                           @"Digiwin.ERP.XTEST\Digiwin.ERP.XTEST.UI.Implement\Interceptor\QueryResultAction.cs";
                            string mto = mpaths + @"\" + StrYY + ".UI.Implement\\Interceptor\\QueryResultAction.cs";
                            if (!File.Exists(mto)) {
                                Tools.CopyFileCS(mpaths + @"\" + StrYY + ".UI.Implement", mto, mfrom, Toolpars);
                            }
                            else {
                                MessageBox.Show("已存在类" + mto.Substring(mto.LastIndexOf("\\") + 1) + "，请重新命名类名！", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            if (File.Exists(mto)) {
                                FileInfo f = new FileInfo(mto);
                                if (csname != "") {
                                    if (
                                        File.Exists(f.Directory.FullName + "\\" +
                                                    f.Name.Replace("QueryResultAction", csname)) == false) {
                                        f.MoveTo(f.Directory.FullName + "\\" +
                                                 f.Name.Replace("QueryResultAction", csname));
                                        string text = File.ReadAllText(f.FullName);
                                        text = text.Replace("QueryResultAction", csname);
                                        text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                        File.SetAttributes(f.FullName, FileAttributes.Normal);
                                        File.Delete(f.FullName);
                                        File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                        //^_^ 20160802 by 05485 for 编码方式修改
                                    }
                                    Application.DoEvents();
                                    Tools.ModiCS(
                                        mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                        "Interceptor\\" + csname + ".cs");
                                }
                                else {
                                    if (File.Exists(f.Directory.FullName + "\\" + f.Name)) {
                                        string text = File.ReadAllText(f.FullName);
                                        text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                        File.SetAttributes(f.FullName, FileAttributes.Normal);
                                        File.Delete(f.FullName);
                                        File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                        //^_^ 20160802 by 05485 for 编码方式修改
                                    }
                                    Application.DoEvents();
                                    Tools.ModiCS(
                                        mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                        "Interceptor\\QueryResultAction.cs");
                                }
                            }
                        }
                            #endregion
                            #region QueryDeleteAction

                        else if (mService == "QueryDeleteAction") {
                            string mfrom = Toolpars.MVSToolpath +
                                           @"Digiwin.ERP.XTEST\Digiwin.ERP.XTEST.UI.Implement\Interceptor\QueryDeleteAction.cs";
                            string mto = mpaths + @"\" + StrYY + ".UI.Implement\\Interceptor\\QueryDeleteAction.cs";
                            if (!File.Exists(mto)) {
                                Tools.CopyFileCS(mpaths + @"\" + StrYY + ".UI.Implement", mto, mfrom, Toolpars);
                            }
                            else {
                                MessageBox.Show("已存在类" + mto.Substring(mto.LastIndexOf("\\") + 1) + "，请重新命名类名！", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            if (File.Exists(mto)) {
                                FileInfo f = new FileInfo(mto);
                                if (csname != "") {
                                    if (
                                        File.Exists(f.Directory.FullName + "\\" +
                                                    f.Name.Replace("QueryDeleteAction", csname)) == false) {
                                        f.MoveTo(f.Directory.FullName + "\\" +
                                                 f.Name.Replace("QueryDeleteAction", csname));
                                        string text = File.ReadAllText(f.FullName);
                                        text = text.Replace("QueryDeleteAction", csname);
                                        text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                        File.SetAttributes(f.FullName, FileAttributes.Normal);
                                        File.Delete(f.FullName);
                                        File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                        //^_^ 20160802 by 05485 for 编码方式修改
                                    }
                                    Application.DoEvents();
                                    Tools.ModiCS(
                                        mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                        "Interceptor\\" + csname + ".cs");
                                }
                                else {
                                    if (File.Exists(f.Directory.FullName + "\\" + f.Name)) {
                                        string text = File.ReadAllText(f.FullName);
                                        text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                        File.SetAttributes(f.FullName, FileAttributes.Normal);
                                        File.Delete(f.FullName);
                                        File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                        //^_^ 20160802 by 05485 for 编码方式修改
                                    }
                                    Application.DoEvents();
                                    Tools.ModiCS(
                                        mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                        "Interceptor\\QueryDeleteAction.cs");
                                }
                            }
                        }
                            #endregion
                            #region QueryAddAction

                        else if (mService == "QueryAddAction") {
                            string mfrom = Toolpars.MVSToolpath +
                                           @"Digiwin.ERP.XTEST\Digiwin.ERP.XTEST.UI.Implement\Interceptor\QueryAddAction.cs";
                            string mto = mpaths + @"\" + StrYY + ".UI.Implement\\Interceptor\\QueryAddAction.cs";
                            if (!File.Exists(mto)) {
                                Tools.CopyFileCS(mpaths + @"\" + StrYY + ".UI.Implement", mto, mfrom, Toolpars);
                            }
                            else {
                                MessageBox.Show("已存在类" + mto.Substring(mto.LastIndexOf("\\") + 1) + "，请重新命名类名！", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            if (File.Exists(mto)) {
                                FileInfo f = new FileInfo(mto);
                                if (csname != "") {
                                    if (
                                        File.Exists(f.Directory.FullName + "\\" +
                                                    f.Name.Replace("QueryAddAction", csname)) == false) {
                                        f.MoveTo(f.Directory.FullName + "\\" + f.Name.Replace("QueryAddAction", csname));
                                        string text = File.ReadAllText(f.FullName);
                                        text = text.Replace("QueryAddAction", csname);
                                        text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                        File.SetAttributes(f.FullName, FileAttributes.Normal);
                                        File.Delete(f.FullName);
                                        File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                        //^_^ 20160802 by 05485 for 编码方式修改
                                    }
                                    Application.DoEvents();
                                    Tools.ModiCS(
                                        mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                        "Interceptor\\" + csname + ".cs");
                                }
                                else {
                                    if (File.Exists(f.Directory.FullName + "\\" + f.Name)) {
                                        string text = File.ReadAllText(f.FullName);
                                        text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                        File.SetAttributes(f.FullName, FileAttributes.Normal);
                                        File.Delete(f.FullName);
                                        File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                        //^_^ 20160802 by 05485 for 编码方式修改
                                    }
                                    Application.DoEvents();
                                    Tools.ModiCS(
                                        mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                        "Interceptor\\QueryAddAction.cs");
                                }
                            }
                        }

                        #endregion
                    }
                        #endregion

                    else {
                        #region FreeBatchService

                        if (mService == "FreeBatchService") {
                            string mfrom = Toolpars.MVSToolpath +
                                           @"Digiwin.ERP.XTEST\Digiwin.ERP.XTEST.Business.Implement\Interceptor\XBatchService.cs";
                            string mto = mpaths + @"\" + StrYY + ".Business.Implement\\Interceptor\\XBatchService.cs";
                            if (!File.Exists(mto)) {
                                Tools.CopyFileCS(mpaths + @"\" + StrYY + ".Business.Implement", mto, mfrom, Toolpars);
                            }
                            else {
                                MessageBox.Show("已存在类" + mto.Substring(mto.LastIndexOf("\\") + 1) + "，请重新命名类名！", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            if (File.Exists(mto)) {
                                FileInfo f = new FileInfo(mto);
                                if (csname != "") {
                                    if (
                                        File.Exists(f.Directory.FullName + "\\" +
                                                    f.Name.Replace("XBatchService", csname)) == false) {
                                        f.MoveTo(f.Directory.FullName + "\\" + f.Name.Replace("XBatchService", csname));
                                        string text = File.ReadAllText(f.FullName);
                                        text = text.Replace("XBatchService", csname);
                                        text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                        File.SetAttributes(f.FullName, FileAttributes.Normal);
                                        File.Delete(f.FullName);
                                        File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                        //^_^ 20160802 by 05485 for 编码方式修改
                                    }
                                    Application.DoEvents();
                                    Tools.ModiCS(
                                        mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY +
                                        ".Business.Implement.csproj", "Interceptor\\" + csname + ".cs");
                                }
                                else {
                                    if (File.Exists(f.Directory.FullName + "\\" + f.Name)) {
                                        string text = File.ReadAllText(f.FullName);
                                        text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                        File.SetAttributes(f.FullName, FileAttributes.Normal);
                                        File.Delete(f.FullName);
                                        File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                        //^_^ 20160802 by 05485 for 编码方式修改
                                    }
                                    Application.DoEvents();
                                    Tools.ModiCS(
                                        mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY +
                                        ".Business.Implement.csproj", "Interceptor\\XBatchService.cs");
                                }
                            }
                        }
                            #endregion
                            #region DataEntityChangedInterceptorAttribute

                        else if (mService == "DataEntityChangedInterceptorAttribute") {
                            string mfrom = Toolpars.MVSToolpath +
                                           @"Digiwin.ERP.XTEST\Digiwin.ERP.XTEST.UI.Implement\Interceptor\DefaultInterceptor.cs";
                            string mto = mpaths + @"\" + StrYY + ".UI.Implement\\Interceptor\\DefaultInterceptor.cs";
                            if (!File.Exists(mto)) {
                                Tools.CopyFileCS(mpaths + @"\" + StrYY + ".UI.Implement", mto, mfrom, Toolpars);
                            }
                            else {
                                MessageBox.Show("已存在类" + mto.Substring(mto.LastIndexOf("\\") + 1) + "，请重新命名类名！", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            if (File.Exists(mto)) {
                                FileInfo f = new FileInfo(mto);
                                if (csname != "") {
                                    if (hsname != "") {
                                        if (
                                            File.Exists(f.Directory.FullName + "\\" +
                                                        f.Name.Replace("DefaultInterceptor", csname)) == false) {
                                            f.MoveTo(f.Directory.FullName + "\\" +
                                                     f.Name.Replace("DefaultInterceptor", csname));
                                            string text = File.ReadAllText(f.FullName);
                                            text = text.Replace("DefaultInterceptor", csname);
                                            text = text.Replace("Default001", hsname);
                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                    }
                                    else {
                                        if (
                                            File.Exists(f.Directory.FullName + "\\" +
                                                        f.Name.Replace("DefaultInterceptor", csname)) == false) {
                                            f.MoveTo(f.Directory.FullName + "\\" +
                                                     f.Name.Replace("DefaultInterceptor", csname));
                                            string text = File.ReadAllText(f.FullName);
                                            text = text.Replace("DefaultInterceptor", csname);
                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                    }
                                    Application.DoEvents();
                                    Tools.ModiCS(
                                        mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                        "Interceptor\\" + csname + ".cs");
                                }
                                else if (hsname != "") {
                                    if (File.Exists(f.Directory.FullName + "\\" + f.Name)) {
                                        string text = File.ReadAllText(f.FullName);
                                        text = text.Replace("Default001", hsname);
                                        text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                        File.SetAttributes(f.FullName, FileAttributes.Normal);
                                        File.Delete(f.FullName);
                                        File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                        //^_^ 20160802 by 05485 for 编码方式修改
                                    }
                                    Application.DoEvents();
                                    Tools.ModiCS(
                                        mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                        "Interceptor\\DefaultInterceptor.cs");
                                }
                                else {
                                    if (File.Exists(f.Directory.FullName + "\\" + f.Name)) {
                                        string text = File.ReadAllText(f.FullName);
                                        text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                        File.SetAttributes(f.FullName, FileAttributes.Normal);
                                        File.Delete(f.FullName);
                                        File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                        //^_^ 20160802 by 05485 for 编码方式修改
                                    }
                                    Application.DoEvents();
                                    Tools.ModiCS(
                                        mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                        "Interceptor\\DefaultInterceptor.cs");
                                }
                            }
                        }
                            #endregion
                            #region IDocumentBatchServiceEvents

                        else if (mService == "IDocumentBatchServiceEvents") {
                            if (L1 == 0) {
                                L1 = 1;
                                CopyFileA(str2.Substring(0, str2.LastIndexOf("\\"))); //统计相同切片不同时机点
                                string mfrom = Toolpars.MVSToolpath +
                                               @"Digiwin.ERP.XTEST\Digiwin.ERP.XTEST.UI.Implement\Interceptor\DetailInterceptor.cs";
                                string mto = mpaths + @"\" + StrYY + ".UI.Implement\\Interceptor\\DetailInterceptor.cs";
                                if (!File.Exists(mto)) {
                                    Tools.CopyFileCS(mpaths + @"\" + StrYY + ".UI.Implement", mto, mfrom, Toolpars);
                                }
                                else {
                                    MessageBox.Show("已存在类" + mto.Substring(mto.LastIndexOf("\\") + 1) + "，请重新命名类名！",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                if (File.Exists(mto)) {
                                    FileInfo f = new FileInfo(mto);
                                    if (csname != "") {
                                        if (
                                            File.Exists(f.Directory.FullName + "\\" +
                                                        f.Name.Replace("DetailInterceptor", csname)) == false) {
                                            f.MoveTo(f.Directory.FullName + "\\" +
                                                     f.Name.Replace("DetailInterceptor", csname));
                                            string text = File.ReadAllText(f.FullName);
                                            text = text.Replace("DetailInterceptor", csname);

                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "DataValidator" + ss;
                                                }

                                                if (point == "Confirm") {
                                                    mtypes = "DocumentBatchConfirmEventArgs";
                                                }
                                                else {
                                                    mtypes = "DocumentBatchServiceEventArgs";
                                                }
                                                string STRAS =
                                                    "[EventInterceptor(typeof(IDocumentBatchServiceEvents),\"" + point +
                                                    "\")]\r\n"
                                                    + "        public void " + mFUNC + "(object sender, " + mtypes +
                                                    " e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }

                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                            "Interceptor\\" + csname + ".cs");
                                    }
                                    else {
                                        if (File.Exists(f.Directory.FullName + "\\" + f.Name)) {
                                            f.MoveTo(f.Directory.FullName + "\\" +
                                                     f.Name.Replace("DetailInterceptor", "DetailInterceptor" + ra));
                                            string text = File.ReadAllText(f.FullName);
                                            text = text.Replace("DetailInterceptor", "DetailInterceptor" + ra);
                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "DataValidator" + ss;
                                                }
                                                if (point == "Confirm") {
                                                    mtypes = "DocumentBatchConfirmEventArgs";
                                                }
                                                else {
                                                    mtypes = "DocumentBatchServiceEventArgs";
                                                }
                                                string STRAS =
                                                    "[EventInterceptor(typeof(IDocumentBatchServiceEvents),\"" + point +
                                                    "\")]\r\n"
                                                    + "        public void " + mFUNC + "(object sender, " + mtypes +
                                                    " e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }
                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                            "Interceptor\\DetailInterceptor" + ra + ".cs");
                                    }
                                }
                            }
                        }
                            #endregion
                            #region IEditorView/IDataEntityTraceService

                        else if (mService == "IEditorView"
                                 || mService == "IDataEntityTraceService") {
                            if (M1 == 0) {
                                M1 = 1;
                                CopyFileA("普通批次\\IEditorView"); //统计相同切片不同时机点                                
                                CopyFileA("普通批次\\IDataEntityTraceService");
                                string mfrom = Toolpars.MVSToolpath +
                                               @"Digiwin.ERP.XTEST\Digiwin.ERP.XTEST.UI.Implement\Interceptor\DetailInterceptor.cs";
                                string mto = mpaths + @"\" + StrYY + ".UI.Implement\\Interceptor\\DetailInterceptor.cs";
                                if (!File.Exists(mto)) {
                                    Tools.CopyFileCS(mpaths + @"\" + StrYY + ".UI.Implement", mto, mfrom, Toolpars);
                                }
                                else {
                                    MessageBox.Show("已存在类" + mto.Substring(mto.LastIndexOf("\\") + 1) + "，请重新命名类名！",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                if (File.Exists(mto)) {
                                    FileInfo f = new FileInfo(mto);
                                    if (csname != "") {
                                        if (
                                            File.Exists(f.Directory.FullName + "\\" +
                                                        f.Name.Replace("DetailInterceptor", csname)) == false) {
                                            f.MoveTo(f.Directory.FullName + "\\" +
                                                     f.Name.Replace("DetailInterceptor", csname));
                                            string text = File.ReadAllText(f.FullName);
                                            text = text.Replace("DetailInterceptor", csname);
                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mSERVER = msave[ss].Substring(0, msave[ss].LastIndexOf("\\"));
                                                mSERVER = mSERVER.Substring(mSERVER.LastIndexOf("\\") + 1);
                                                //mSERVER = mSERVER.Substring(1);
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "DesignC" + ss;
                                                }
                                                string STRAS = "[EventInterceptor(typeof(" + mSERVER + "),\"" + point +
                                                               "\")]\r\n"
                                                               + "        public void " + mFUNC +
                                                               "(object sender, EventArgs e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }

                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                            "Interceptor\\" + csname + ".cs");
                                    }
                                    else {
                                        if (File.Exists(f.Directory.FullName + "\\" + f.Name)) {
                                            f.MoveTo(f.Directory.FullName + "\\" +
                                                     f.Name.Replace("DetailInterceptor", "DetailInterceptor" + ra));
                                            string text = File.ReadAllText(f.FullName);
                                            text = text.Replace("DetailInterceptor", "DetailInterceptor" + ra);

                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mSERVER = msave[ss].Substring(0, msave[ss].LastIndexOf("\\"));
                                                mSERVER = mSERVER.Substring(mSERVER.LastIndexOf("\\") + 1);
                                                //mSERVER = mSERVER.Substring(1);
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "DesignC" + ss;
                                                }
                                                string STRAS = "[EventInterceptor(typeof(" + mSERVER + "),\"" + point +
                                                               "\")]\r\n"
                                                               + "        public void " + mFUNC +
                                                               "(object sender, EventArgs e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }

                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                            "Interceptor\\DetailInterceptor" + ra + ".cs");
                                    }
                                }
                            }
                        }
                            #endregion
                            #region IDocumentResponseServiceEvents

                        else if (mService == "IDocumentResponseServiceEvents") {
                            if (H1 == 0) {
                                H1 = 1;
                                CopyFileA(str2.Substring(0, str2.LastIndexOf("\\"))); //统计相同切片不同时机点
                                string mfrom = Toolpars.MVSToolpath +
                                               @"Digiwin.ERP.XTEST\Digiwin.ERP.XTEST.UI.Implement\Interceptor\DetailInterceptor.cs";
                                string mto = mpaths + @"\" + StrYY + ".UI.Implement\\Interceptor\\DetailInterceptor.cs";
                                if (!File.Exists(mto)) {
                                    Tools.CopyFileCS(mpaths + @"\" + StrYY + ".UI.Implement", mto, mfrom, Toolpars);
                                }
                                else {
                                    MessageBox.Show("已存在类" + mto.Substring(mto.LastIndexOf("\\") + 1) + "，请重新命名类名！",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                if (File.Exists(mto)) {
                                    FileInfo f = new FileInfo(mto);
                                    if (csname != "") {
                                        if (
                                            File.Exists(f.Directory.FullName + "\\" +
                                                        f.Name.Replace("DetailInterceptor", csname)) == false) {
                                            f.MoveTo(f.Directory.FullName + "\\" +
                                                     f.Name.Replace("DetailInterceptor", csname));
                                            string text = File.ReadAllText(f.FullName);
                                            text = text.Replace("DetailInterceptor", csname);

                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "ReciveData" + ss;
                                                }

                                                if (point == "ReqeustParamProcessing") {
                                                    mtypes = "ReqeustParamProcessingEventArgs";
                                                }
                                                else {
                                                    mtypes = "RespondOutputPreparingEventArgs";
                                                }
                                                string STRAS =
                                                    "[EventInterceptor(typeof(IDocumentResponseServiceEvents),\"" +
                                                    point + "\")]\r\n"
                                                    + "        public void " + mFUNC + "(object sender, " + mtypes +
                                                    " e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }

                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                            "Interceptor\\" + csname + ".cs");
                                    }
                                    else {
                                        if (File.Exists(f.Directory.FullName + "\\" + f.Name)) {
                                            f.MoveTo(f.Directory.FullName + "\\" +
                                                     f.Name.Replace("DetailInterceptor", "DetailInterceptor" + ra));
                                            string text = File.ReadAllText(f.FullName);
                                            text = text.Replace("DetailInterceptor", "DetailInterceptor" + ra);
                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "ReciveData" + ss;
                                                }
                                                if (point == "ReqeustParamProcessing") {
                                                    mtypes = "ReqeustParamProcessingEventArgs";
                                                }
                                                else {
                                                    mtypes = "RespondOutputPreparingEventArgs";
                                                }
                                                string STRAS =
                                                    "[EventInterceptor(typeof(IDocumentResponseServiceEvents),\"" +
                                                    point + "\")]\r\n"
                                                    + "        public void " + mFUNC + "(object sender, " + mtypes +
                                                    " e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }
                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".UI.Implement\\" + StrYY + ".UI.Implement.csproj",
                                            "Interceptor\\DetailInterceptor" + ra + ".cs");
                                    }
                                }
                            }
                        }

                        #endregion
                    }
                }

                #endregion

                #region 报表

                if (RBReport.Checked) {
                    string mService = str2.Substring(str2.IndexOf("\\") + 1);
                    if (mService.Contains("\\")) {
                        mService = mService.Substring(0, mService.IndexOf("\\"));
                    }

                    if (str == "报表") {
                        #region IReportQueryServiceEvents

                        if (mService == "IReportQueryServiceEvents") {
                            if (N1 == 0) {
                                N1 = 1;
                                CopyFileA(str2.Substring(0, str2.LastIndexOf("\\"))); //统计相同切片不同时机点
                                string mfrom = Toolpars.MVSToolpath +
                                               @"Digiwin.ERP.XTEST\Digiwin.ERP.XTEST.Business.Implement\Interceptor\DetailInterceptorS.cs";
                                string mto = mpaths + @"\" + StrYY +
                                             ".Business.Implement\\Interceptor\\DetailInterceptorS.cs";

                                if (!File.Exists(mto)) {
                                    Tools.CopyFileCS(mpaths + @"\" + StrYY + ".Business.Implement", mto, mfrom, Toolpars);
                                }
                                else {
                                    MessageBox.Show("已存在类" + mto.Substring(mto.LastIndexOf("\\") + 1) + "，请重新命名类名！",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                if (File.Exists(mto)) {
                                    FileInfo f = new FileInfo(mto);
                                    if (csname != "") {
                                        if (
                                            File.Exists(f.Directory.FullName + "\\" +
                                                        f.Name.Replace("DetailInterceptorS", csname)) == false) {
                                            f.MoveTo(f.Directory.FullName + "\\" +
                                                     f.Name.Replace("DetailInterceptorS", csname));
                                            string text = File.ReadAllText(f.FullName);
                                            text = text.Replace("DetailInterceptorS", csname);

                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "OnCustomDataSource" + ss;
                                                }


                                                string STRAS =
                                                    "[EventInterceptor(typeof(IReportQueryServiceEvents),\"" + point +
                                                    "\")]\r\n"
                                                    + "        public void " + mFUNC +
                                                    "(object sender, ReportQueryServiceEventArgs e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }

                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY +
                                            ".Business.Implement.csproj", "Interceptor\\" + csname + ".cs");
                                    }
                                    else {
                                        if (File.Exists(f.Directory.FullName + "\\" + f.Name)) {
                                            f.MoveTo(f.Directory.FullName + "\\" +
                                                     f.Name.Replace("DetailInterceptorS", "DetailInterceptorS" + ra));
                                            string text = File.ReadAllText(f.FullName);
                                            text = text.Replace("DetailInterceptorS", "DetailInterceptorS" + ra);
                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "OnCustomDataSource" + ss;
                                                }

                                                string STRAS =
                                                    "[EventInterceptor(typeof(IReportQueryServiceEvents),\"" + point +
                                                    "\")]\r\n"
                                                    + "        public void " + mFUNC +
                                                    "(object sender, ReportQueryServiceEventArgs e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }
                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY +
                                            ".Business.Implement.csproj", "Interceptor\\DetailInterceptorS" + ra + ".cs");
                                    }
                                }
                            }
                        }
                            #endregion
                            #region IReportServiceEvents

                        else if (mService == "IReportServiceEvents") {
                            if (R1 == 0) {
                                R1 = 1;
                                CopyFileA(str2.Substring(0, str2.LastIndexOf("\\"))); //统计相同切片不同时机点
                                string mfrom = Toolpars.MVSToolpath +
                                               @"Digiwin.ERP.XTEST\Digiwin.ERP.XTEST.Business.Implement\Interceptor\DetailInterceptorS.cs";
                                string mto = mpaths + @"\" + StrYY +
                                             ".Business.Implement\\Interceptor\\DetailInterceptorS.cs";
                                if (!File.Exists(mto)) {
                                    Tools.CopyFileCS(mpaths + @"\" + StrYY + ".Business.Implement", mto, mfrom, Toolpars);
                                }
                                else {
                                    MessageBox.Show("已存在类" + mto.Substring(mto.LastIndexOf("\\") + 1) + "，请重新命名类名！",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                if (File.Exists(mto)) {
                                    FileInfo f = new FileInfo(mto);
                                    if (csname != "") {
                                        if (
                                            File.Exists(f.Directory.FullName + "\\" +
                                                        f.Name.Replace("DetailInterceptorS", csname)) == false) {
                                            f.MoveTo(f.Directory.FullName + "\\" +
                                                     f.Name.Replace("DetailInterceptorS", csname));
                                            string text = File.ReadAllText(f.FullName);
                                            text = text.Replace("DetailInterceptorS", csname);

                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "OnReportSourceed" + ss;
                                                }


                                                string STRAS = "[EventInterceptor(typeof(IReportServiceEvents),\"" +
                                                               point + "\")]\r\n"
                                                               + "        public void " + mFUNC +
                                                               "(object sender, ReportServiceEventArgs e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }

                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY +
                                            ".Business.Implement.csproj", "Interceptor\\" + csname + ".cs");
                                    }
                                    else {
                                        if (File.Exists(f.Directory.FullName + "\\" + f.Name)) {
                                            f.MoveTo(f.Directory.FullName + "\\" +
                                                     f.Name.Replace("DetailInterceptorS", "DetailInterceptorS" + ra));
                                            string text = File.ReadAllText(f.FullName);
                                            text = text.Replace("DetailInterceptorS", "DetailInterceptorS" + ra);
                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "OnReportSourceed" + ss;
                                                }

                                                string STRAS = "[EventInterceptor(typeof(IReportServiceEvents),\"" +
                                                               point + "\")]\r\n"
                                                               + "        public void " + mFUNC +
                                                               "(object sender, ReportServiceEventArgs e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }
                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY +
                                            ".Business.Implement.csproj", "Interceptor\\DetailInterceptorS" + ra + ".cs");
                                    }
                                }
                            }
                        }
                            #endregion
                            #region IQueryJetServiceServiceEvents

                        else if (mService == "IQueryJetServiceServiceEvents") {
                            if (T1 == 0) {
                                T1 = 1;
                                CopyFileA(str2.Substring(0, str2.LastIndexOf("\\"))); //统计相同切片不同时机点
                                string mfrom = Toolpars.MVSToolpath +
                                               @"Digiwin.ERP.XTEST\Digiwin.ERP.XTEST.Business.Implement\Interceptor\DetailInterceptorS.cs";
                                string mto = mpaths + @"\" + StrYY +
                                             ".Business.Implement\\Interceptor\\DetailInterceptorS.cs";
                                if (!File.Exists(mto)) {
                                    Tools.CopyFileCS(mpaths + @"\" + StrYY + ".Business.Implement", mto, mfrom, Toolpars);
                                }
                                else {
                                    MessageBox.Show("已存在类" + mto.Substring(mto.LastIndexOf("\\") + 1) + "，请重新命名类名！",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                if (File.Exists(mto)) {
                                    FileInfo f = new FileInfo(mto);
                                    if (csname != "") {
                                        if (
                                            File.Exists(f.Directory.FullName + "\\" +
                                                        f.Name.Replace("DetailInterceptorS", csname)) == false) {
                                            f.MoveTo(f.Directory.FullName + "\\" +
                                                     f.Name.Replace("DetailInterceptorS", csname));
                                            string text = File.ReadAllText(f.FullName);
                                            text = text.Replace("DetailInterceptorS", csname);

                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "BeforePrint" + ss;
                                                }


                                                string STRAS =
                                                    "[EventInterceptor(typeof(IQueryJetServiceServiceEvents),\"" + point
                                                    +
                                                    "\")]\r\n"
                                                    + "        public void " + mFUNC +
                                                    "(object sender, QueryJetServiceEventArgs e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }

                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY +
                                            ".Business.Implement.csproj", "Interceptor\\" + csname + ".cs");
                                    }
                                    else {
                                        if (File.Exists(f.Directory.FullName + "\\" + f.Name)) {
                                            f.MoveTo(f.Directory.FullName + "\\" +
                                                     f.Name.Replace("DetailInterceptorS", "DetailInterceptorS" + ra));
                                            string text = File.ReadAllText(f.FullName);
                                            text = text.Replace("DetailInterceptorS", "DetailInterceptorS" + ra);

                                            for (int ss = 0; ss < msave.Count; ss++) {
                                                string point = msave[ss].Substring(msave[ss].LastIndexOf("\\") + 1);
                                                point = point.Substring(0, point.IndexOf(";"));
                                                string mFUNC = msave[ss].Substring(msave[ss].LastIndexOf(";") + 1);
                                                if (mFUNC == "") {
                                                    mFUNC = "BeforePrint" + ss;
                                                }

                                                string STRAS =
                                                    "[EventInterceptor(typeof(IQueryJetServiceServiceEvents),\"" + point
                                                    +
                                                    "\")]\r\n"
                                                    + "        public void " + mFUNC +
                                                    "(object sender, QueryJetServiceEventArgs e)\r\n        {\r\n        }\r\n        //ADD";
                                                text = text.Replace("//ADD", STRAS);
                                            }
                                            text = text.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey);
                                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                                            File.Delete(f.FullName);
                                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                                            //^_^ 20160802 by 05485 for 编码方式修改
                                        }
                                        Application.DoEvents();
                                        Tools.ModiCS(
                                            mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY +
                                            ".Business.Implement.csproj", "Interceptor\\DetailInterceptorS" + ra + ".cs");
                                    }
                                }
                            }
                        }

                        #endregion
                    }
                }

                #endregion

                msave.Clear();
            }
        }


        private void btnOpenTo_Click(object sender, EventArgs e) {
         
            if (Toolpars.formEntity.txtToPath.Trim() != "") {
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

      

        #region 勾选事件，生成切片种类          

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e) {
            Toolpars.MDistince = false;

            string StrA = "";
            if (RBBatch.Checked) {
                if (treeView1.Nodes.Count == 3) {
                    if (e.Node.Text == "普通批次"
                        && e.Node.Checked) {
                        treeView1.Nodes.Remove(treeView1.Nodes[1]);
                    }

                    if (e.Node.Text == "向导式批次"
                        && e.Node.Checked) {
                        treeView1.Nodes.Remove(treeView1.Nodes[2]);
                    }
                    for (; treeView1.TopNode.FirstNode != null;) {
                        treeView1.TopNode.FirstNode.Remove();
                    }
                    treeView1.Refresh();
                }
            }


            if (e.Node.Checked) {
                if (e.Node.Parent != null) {
                    e.Node.Parent.Checked = true;
                }
                if (e.Node.Nodes.Count == 0) {
                    VSTool.ModiName MYForm = new VSTool.ModiName(this);
                    string mFullPath = e.Node.FullPath.ToString();

                    #region 画面调整

                    if (mFullPath == "服务"
                        || mFullPath.Contains("无表决器")) {
                        MYForm.txt02.Visible = false;
                        MYForm.label2.Visible = false;
                    }
                    else {
                        MYForm.txt02.Visible = true;
                        MYForm.label2.Visible = true;
                    }
                    if (mFullPath.Contains("\\")) {
                        string mfirstname = mFullPath.Substring(0, mFullPath.IndexOf("\\"));
                        string msecondname = mFullPath.Substring(0, mFullPath.LastIndexOf("\\"));
                        msecondname = msecondname.Substring(msecondname.LastIndexOf("\\") + 1);
                        if (mfirstname == "保存"
                            || mfirstname == "审核"
                            || mfirstname == "普通切片"
                            || mfirstname == "普通批次"
                            || msecondname == "IReportQueryServiceEvents"
                            || msecondname == "IReportServiceEvents"
                            || msecondname == "IQueryJetServiceServiceEvents"
                            || msecondname == "GuideInterceptor") {
                            #region 普通切片                            

                            if (mfirstname == "普通切片") {
                                if (msecondname == "IEditorView"
                                    || msecondname == "IDataEntityTraceService"
                                    ||
                                    msecondname == "IBrowseView") {
                                    for (int i = 0; i < listDATA.Items.Count; i++) {
                                        if (listDATA.Items[i].ToString().Contains("IEditorView")
                                            ||
                                            listDATA.Items[i].ToString().Contains("IBrowseView")
                                            ||
                                            listDATA.Items[i].ToString().Contains("IDataEntityTraceService")) {
                                            Toolpars.MDistince = true;
                                            break;
                                        }
                                    }
                                }
                                if (msecondname == "IDataConvertServiceEvents"
                                    || msecondname == "IDeleteServiceEvents"
                                    ||
                                    msecondname == "IApproveStatusServiceEvents") {
                                    for (int i = 0; i < listDATA.Items.Count; i++) {
                                        if (listDATA.Items[i].ToString().Contains("IDataConvertServiceEvents")
                                            ||
                                            listDATA.Items[i].ToString().Contains("IDeleteServiceEvents")
                                            ||
                                            listDATA.Items[i].ToString().Contains("IApproveStatusServiceEvents")) {
                                            Toolpars.MDistince = true;
                                            break;
                                        }
                                    }
                                }
                                if (msecondname == "IDocumentDataConvertSubServiceEvents") {
                                    for (int i = 0; i < listDATA.Items.Count; i++) {
                                        if (listDATA.Items[i].ToString()
                                            .Contains("IDocumentDataConvertSubServiceEvents")) {
                                            Toolpars.MDistince = true;
                                            break;
                                        }
                                    }
                                }
                            }
                                #endregion
                                #region 保存

                            else if (mfirstname == "保存") {
                                if (msecondname == "ISaveServiceEvents") {
                                    for (int i = 0; i < listDATA.Items.Count; i++) {
                                        if (listDATA.Items[i].ToString().Contains("ISaveServiceEvents")) {
                                            Toolpars.MDistince = true;
                                            break;
                                        }
                                    }
                                }
                                if (msecondname == "IDocumentSaveServiceEvents") {
                                    for (int i = 0; i < listDATA.Items.Count; i++) {
                                        if (listDATA.Items[i].ToString().Contains("IDocumentSaveServiceEvents")) {
                                            Toolpars.MDistince = true;
                                            break;
                                        }
                                    }
                                }
                            }
                                #endregion
                                #region 审核

                            else if (mfirstname == "审核") {
                                if (msecondname == "IConfirmServiceEvents"
                                    || msecondname == "IDisconfirmServiceEvents") {
                                    for (int i = 0; i < listDATA.Items.Count; i++) {
                                        if (listDATA.Items[i].ToString().Contains("IConfirmServiceEvents")
                                            ||
                                            listDATA.Items[i].ToString().Contains("IDisconfirmServiceEvents")) {
                                            Toolpars.MDistince = true;
                                            break;
                                        }
                                    }
                                }
                                if (msecondname == "IDocumentConfirmServiceEvents") {
                                    for (int i = 0; i < listDATA.Items.Count; i++) {
                                        if (listDATA.Items[i].ToString().Contains("IDocumentConfirmServiceEvents")) {
                                            Toolpars.MDistince = true;
                                            break;
                                        }
                                    }
                                }
                            }
                                #endregion
                                #region 普通批次                            

                            else if (mfirstname == "普通批次") {
                                if (msecondname == "IEditorView"
                                    || msecondname == "IDataEntityTraceService") {
                                    for (int i = 0; i < listDATA.Items.Count; i++) {
                                        if (listDATA.Items[i].ToString().Contains("IEditorView")
                                            ||
                                            listDATA.Items[i].ToString().Contains("IDataEntityTraceService")) {
                                            Toolpars.MDistince = true;
                                            break;
                                        }
                                    }
                                }
                                else {
                                    for (int i = 0; i < listDATA.Items.Count; i++) {
                                        if (listDATA.Items[i].ToString().Contains(msecondname)) {
                                            Toolpars.MDistince = true;
                                            break;
                                        }
                                    }
                                }
                            }
                                #endregion

                            else {
                                for (int i = 0; i < listDATA.Items.Count; i++) {
                                    if (listDATA.Items[i].ToString().Contains(mfirstname)) {
                                        Toolpars.MDistince = true;
                                        break;
                                    }
                                }
                            }
                            if (Toolpars.MDistince) {
                                MYForm.lblattention.Text = "默认合并生成一个cs文件，类名根据第一次选择的时机点设置";

                                MYForm.txt01.Visible = false;
                                MYForm.label1.Visible = false;
                            }
                            else {
                                MYForm.txt01.Visible = true;
                                MYForm.label1.Visible = true;
                            }
                        }
                        if (mfirstname == "菜单") {
                            MYForm.lblattention.Text = "类名已自动添加command 例:mocommand 输入mo即可";
                        }
                    }

                    #endregion

                    if (!rbModi.Checked) {
                        int gLeft = MYForm.Width/2 - MYForm.lblattention.Width/2;
                        MYForm.lblattention.Location = new Point(gLeft, MYForm.lblattention.Top);
                        MYForm.StartPosition = FormStartPosition.CenterParent;
                        MYForm.ShowDialog();

                        if (MYForm.txt01.Text != String.Empty
                            || MYForm.txt02.Text != String.Empty) {
                            StrA = mFullPath + ";" + MYForm.txt01.Text + ";" + MYForm.txt02.Text;
                         //   listDATA.Items.Add(StrA);
                            addListData(StrA);
                        }
                        else {
                            e.Node.Checked = false;
                        }
                    }
                    else {
                        if (mFullPath.Contains(".cs")
                            || mFullPath.Contains(".resx")) {
                            StrA = mFullPath + ";;";
                          //  listDATA.Items.Add(StrA);

                            addListData(StrA);
                        }
                    }
                }
            }
            else {
                if (e.Node.Parent != null) {
                    e.Node.Parent.Checked = false;
                    //SetParentChecked(e.Node);
                }
                for (int i = 0; i < listDATA.Items.Count; i++) {
                    string s = listDATA.Items[i].ToString();
                    if (s.Substring(0, s.IndexOf(";")) == e.Node.FullPath) {
                        listDATA.Items.Remove(s);
                    }
                }
            }
        }

        #endregion
        private void addListData(string str)
        {
            listDATA.Items.Add(str);
            //listDATA.ItemHeight = 50;
            //listDATA.ColumnWidth = 50;
        }


        public void SetParentChecked(TreeNode childNode) {
            TreeNode parentNode = childNode.Parent;
            if (!parentNode.Checked
                && childNode.Checked) {
                int ichecks = 0;
                for (int i = 0; i < parentNode.GetNodeCount(false); i++) {
                    TreeNode node = parentNode.Nodes[i];
                    if (node.Checked) {
                        ichecks++;
                    }
                }
                if (ichecks == parentNode.GetNodeCount(false)) {
                    parentNode.Checked = true;
                    if (parentNode.Parent != null) {
                        SetParentChecked(parentNode);
                    }
                }
            }
            else if (parentNode.Checked
                     && !childNode.Checked) {
                parentNode.Checked = false;
            }
        }

       
        private void btnCreateCS_Click(object sender, EventArgs e) //生成类
        {
            Toolpars.GToIni = Toolpars.formEntity.txtToPath;
            Tools.WriteLog(Toolpars,listDATA);
            if ((Toolpars.formEntity.txtToPath == "")
                || (Toolpars.formEntity.txtNewTypeKey == "")) {
                MessageBox.Show("请输入创建地址及名称", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Directory.Exists(Toolpars.formEntity.txtToPath + "\\Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey)) {
                if (listDATA.Items.Count != 0) {
                    if (rbModi.Checked) {
                        copyModi();
                    }
                    else {
                        CopyFile(Toolpars.formEntity.txtToPath + "\\Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey);
                    }
                }
                msave.Clear();
                listDATA.Items.Clear();
                foreach (TreeNode node in treeView1.Nodes) {
                    SetCheckedChildNodes(node);
                    node.Checked = false;
                }
                MessageBox.Show("生成成功 !!!");
            }
            else {
                MessageBox.Show("文件夹" + Toolpars.formEntity.txtToPath + "\\Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey + "不存在，请查看！！！", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            sqlTools.insertToolInfo("S01231_20160503_01", "20160503", "ADDCS");
        }

        private void btnClear_Click(object sender, EventArgs e) {
            listDATA.Items.Clear();
            foreach (TreeNode node in treeView1.Nodes) {
                SetCheckedChildNodes(node);
                node.Checked = false;
            }
        }

        private void SetCheckedChildNodes(TreeNode node) {
            for (int i = 0; i < node.Nodes.Count; i++) {
                node.Nodes[i].Checked = false;
                SetCheckedChildNodes(node.Nodes[i]);
            }
        }

      

        private void RBBusiness_CheckedChanged(object sender, EventArgs e) {
            sertTree(RBBusiness.Checked, Toolpars.MVSToolpath + @"Config\TYPE.xml");
        }

        private void RBBatch_CheckedChanged(object sender, EventArgs e) {
          
            sertTree(RBBatch.Checked, Toolpars.MVSToolpath + @"Config\TYPEBatch.xml");
        }

        public void sertTree(bool chk,string name) {
            if (chk)
            {
                System.Xml.XmlDocument document = new System.Xml.XmlDataDocument();
                //string strins = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;                
                document.Load(name);
                treeView1.Nodes.Clear();
                populateTreeControl(document.DocumentElement, this.treeView1.Nodes, null);
                listDATA.Items.Clear();
            }
        }
        private void RBReport_CheckedChanged(object sender, EventArgs e) {
            sertTree(RBReport.Checked, Toolpars.MVSToolpath + @"Config\TYPEReport.xml");
        }

        private void btnOpen_Click(object sender, EventArgs e) //打开文件夹
        {
            if (Directory.Exists(Toolpars.formEntity.txtToPath + @"\Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey)) {
                Process.Start(Toolpars.formEntity.txtToPath + @"\Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey);
            }
            else {
                MessageBox.Show("文件夹不存在~", "注意!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #region 将标准的代码复制到个案

        private void rbModi_CheckedChanged(object sender, EventArgs e) {
            if (rbModi.Checked) {
                if (Toolpars.formEntity.txtToPath != ""
                    && Toolpars.formEntity.txtNewTypeKey != "") {
                    string strb1 = Toolpars.formEntity.txtPKGpath + "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey.Substring(1);
                    if (Directory.Exists(strb1)) {
                        paintTreeView(treeView1, strb1); //mfroma
                        listDATA.Items.Clear();
                    }
                    else {
                        MessageBox.Show("标准代码" + strb1 + "不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        treeView1.Nodes.Clear();
                        listDATA.Items.Clear();
                    }
                }
                else {
                    treeView1.Nodes.Clear();
                }
            }
        }

        #region treeview 重组节点

        private void paintTreeView(TreeView TreeView, string fullPath) {
            try {
                TreeView.Nodes.Clear();
                DirectoryInfo dirs = new DirectoryInfo(fullPath);
                DirectoryInfo[] dir = dirs.GetDirectories();
                FileInfo[] file = dirs.GetFiles();
                int dircount = dir.Count();
                int filecount = file.Count();
                for (int i = 0; i < dircount; i++) {
                    TreeView.Nodes.Add(dir[i].Name);
                    string pathNode = fullPath + @"\" + dir[i].Name;
                    GetMultNode(TreeView.Nodes[i], pathNode);
                }
                for (int j = 0; j < filecount; j++) {
                    if (file[j].Name.Substring(file[j].Name.LastIndexOf(".") + 1) == "cs") {
                        TreeView.Nodes.Add(file[j].Name);
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message + "\r\n paintTreeview");
            }
        }

        private bool GetMultNode(TreeNode treeNode, string path) {
            if (Directory.Exists(path) == false) {
                return false;
            }
            DirectoryInfo dirs = new DirectoryInfo(path);
            DirectoryInfo[] dir = dirs.GetDirectories();
            FileInfo[] file = dirs.GetFiles();
            int dircount = dir.Count();
            int filecount = file.Count();
            int sumcount = dircount + filecount;
            if (sumcount == 0) {
                return false;
            }
            for (int j = 0; j < dircount; j++) {
                treeNode.Nodes.Add(dir[j].Name);

                string pathNodeB = path + @"\" + dir[j].Name;
                GetMultNode(treeNode.Nodes[j], pathNodeB);
            }
            for (int i = 0; i < filecount; i++) {
                if (file[i].Name.Substring(file[i].Name.LastIndexOf(".") + 1) == "cs"
                    ||
                    file[i].Name.Substring(file[i].Name.LastIndexOf(".") + 1) == "resx") {
                    treeNode.Nodes.Add(file[i].Name);
                }
            }
            return true;
        }

        #endregion

        private void copyModi() {
            string typeKN = Toolpars.formEntity.txtNewTypeKey;
            string stra = Toolpars.formEntity.txtToPath.Substring(0, Toolpars.formEntity.txtToPath.IndexOf("\\"));
            string strb = Toolpars.formEntity.txtToPath.Substring(Toolpars.formEntity.txtToPath.IndexOf("\\") + 1);
            strb = strb.Substring(0, strb.IndexOf("\\"));
            for (int i = 0; i < listDATA.Items.Count; i++) {
                string ITEM = listDATA.Items[i].ToString();
                ITEM = ITEM.Substring(0, ITEM.Length - 2);
                string mcspro = "";
                string MPKG = "";
                if (ITEM.Contains("\\")) {
                    mcspro = ITEM.Substring(0, ITEM.IndexOf("\\"));
                    MPKG = ITEM.Substring(ITEM.IndexOf("\\"));
                }
                string mstrins = "";
                if (mcspro.Contains(".")) {
                    if (mcspro.Substring(mcspro.LastIndexOf(".") + 1) == "Business") {
                        mstrins = ".Business";
                    }
                    if (mcspro.Contains("Business.Implement")) {
                        mstrins = ".Business.Implement";
                    }
                    if (mcspro.Contains("UI.Implement")) {
                        mstrins = ".UI.Implement";
                    }
                }
                else if (mcspro.Substring(mcspro.LastIndexOf(".") + 1) == "Business") {
                    mstrins = ".Business";
                }
                string mfroma = Toolpars.formEntity.txtPKGpath + "Digiwin.ERP." + typeKN.Substring(1) + "\\" + ITEM;
                string mtoa = Toolpars.formEntity.txtToPath + @"\Digiwin.ERP." + typeKN + "\\" + "Digiwin.ERP." + typeKN + mstrins +
                              MPKG;
                string mpatha = mtoa.Substring(0, mtoa.LastIndexOf("\\"));
                if (File.Exists(mfroma)) {
                    if (Directory.Exists(mpatha.Substring(0, mpatha.LastIndexOf("\\")))) {
                        if (!Directory.Exists(mpatha)) {
                            Directory.CreateDirectory(mpatha);
                        }
                        System.IO.File.Copy(mfroma, mtoa, true);
                        if (File.Exists(mtoa)) {
                            FileInfo f = new FileInfo(mtoa);
                            string text = File.ReadAllText(f.FullName);
                            text = text.Replace("Digiwin.ERP." + typeKN.Substring(1), "Digiwin.ERP." + typeKN);
                            File.SetAttributes(f.FullName, FileAttributes.Normal);
                            File.Delete(f.FullName);
                            File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                            //^_^ 20160802 by 05485 for 编码方式修改
                        }
                        Application.DoEvents();
                        string mpan = mtoa.Substring(0, mtoa.LastIndexOf("\\"));
                        string mfileN = mpan.Substring(mpan.LastIndexOf("\\") + 1);
                        mpan = mpan.Substring(0, mpan.LastIndexOf("\\"));
                        string mpans = mpan.Substring(mpan.LastIndexOf("\\") + 1);
                        Tools.ModiCS(mpan + "\\" + mpans + ".csproj", mfileN + mtoa.Substring(mtoa.LastIndexOf("\\")));
                        string[] strpath = mtoa.Split('\\');

                        //Tools.ModiCS(mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY + ".Business.Implement.csproj", "Implement\\" + csname.Substring(1) + ".cs");
                    }
                }
            }
        }

        #endregion

        private void txtNewTypeKey_TextChanged(object sender, EventArgs e) {
            if (rbModi.Checked) {
                if (Toolpars.formEntity.txtToPath != ""
                    && Toolpars.formEntity.txtNewTypeKey != "") {
                    string strb1 = Toolpars.formEntity.txtPKGpath + "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey.Substring(1);
                    if (Directory.Exists(strb1)) {
                        paintTreeView(treeView1, strb1);
                        listDATA.Items.Clear();
                    }
                    else {
                        treeView1.Nodes.Clear();
                        listDATA.Items.Clear();
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
                Tools.copyDll(Toolpars);
                MessageBox.Show("复制成功 !!!");
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            sqlTools.insertToolInfo("S01231_20160503_01", "20160503", "btncopydll_Click");
        }

        private void btncopyUIdll_Click(object sender, EventArgs e) {
            try {
               // Tools.getPathEntity(Toolpars);
                string Export = Toolpars.PathEntity.ExportPath;
                string toPath = Toolpars.Mplatform + "\\DeployServer\\Shared\\Customization\\Programs\\";
                string filterStr = "*" + Toolpars.formEntity.txtNewTypeKey + ".UI.*";
                if (Toolpars.MIndustry) {
                    toPath = Toolpars.Mplatform + "\\DeployServer\\Shared\\Industry\\Programs\\";
                }
                Tools.FileCopyUIdll(Export, toPath, filterStr);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            sqlTools.insertToolInfo("S01231_20160503_01", "20160503", "btncopyUIdll_Click");
        }

        #endregion

        #region 复制pkg代码

        private void btncopypkg_Click(object sender, EventArgs e) {
            try {
                Toolpars.GToIni = Toolpars.formEntity.txtToPath;
                if (Toolpars.formEntity.txtToPath != ""
                    && Toolpars.formEntity.txtNewTypeKey != "") {
                    if (Directory.Exists(Toolpars.formEntity.txtToPath)) {
                        DirectoryInfo tCusSRC = new DirectoryInfo(Toolpars.GToIni + @"\");
                        string strb1 = Toolpars.formEntity.txtPKGpath + "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey.Substring(1);
                        if (!Directory.Exists(strb1)) {
                            MessageBox.Show("文件夹" + strb1 + "不存在，请查看！！！", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            return;
                        }
                        else {
                            if (
                                Directory.Exists(Path.Combine(Toolpars.GToIni + @"\",
                                    "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey))) {
                                DialogResult result =
                                    MessageBox.Show(
                                        Path.Combine(Toolpars.formEntity.txtToPath, Toolpars.formEntity.txtNewTypeKey) + "\r\n目錄已存在，是否覆蓋??",
                                        "Warnning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (result == DialogResult.Yes) {
                                    object tArgsPath = Path.Combine(Toolpars.GToIni + @"\",
                                        "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey);
                                    Tools.DeleteAll(tArgsPath);
                                }
                                else {
                                    return;
                                }
                            }
                            Tools.CopyAllPKG(strb1, tCusSRC + "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey);
                        }
                    }
                    else {
                        MessageBox.Show("文件夹" + Toolpars.formEntity.txtToPath + "不存在，请查看！！！", "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                }
                else {
                    MessageBox.Show("请输入创建地址及名称", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                #region 修改命名

                Tools.ModiName(Toolpars);

                #endregion

                MessageBox.Show("生成成功 !!!");
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            sqlTools.insertToolInfo("S01231_20160503_01", "20160503", "COPY PKG SOURCE");
        }

        #endregion

        private void button1_Click(object sender, EventArgs e) {
            string[] processNames = {
                "Digiwin.Mars.ClientStart", "Digiwin.Mars.ServerStart",
                "Digiwin.Mars.AccountSetStart"
            };
            Tools.killProcess(processNames);
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
            if (Directory.Exists(Toolpars.formEntity.txtPKGpath + "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey.Substring(1))) {
                Process.Start(Toolpars.formEntity.txtPKGpath + "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey.Substring(1));
            }
            else {
                MessageBox.Show("文件夹不存在~", "注意!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    
        private void panel3_Paint(object sender, PaintEventArgs e)
        {
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

        private void listDATA_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(e.BackColor), e.Bounds);
            if (e.Index >= 0)
            {
                StringFormat sStringFormat = new StringFormat();
                sStringFormat.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds, sStringFormat);
            }
            e.DrawFocusRectangle();  
        }

        private void listDATA_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = e.ItemHeight + 15;  
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter) {
                System.Xml.XmlDocument document = new System.Xml.XmlDataDocument();
                document.Load(Toolpars.MVSToolpath + @"Config\TYPE.xml");
                treeView1.Nodes.Clear();
                populateTreeControl(document.DocumentElement, this.treeView1.Nodes,textBox1.Text.Trim());
                listDATA.Items.Clear();
            }
        }
    }
}