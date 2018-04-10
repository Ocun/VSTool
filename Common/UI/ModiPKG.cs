using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Common.Implement.UI {
    public partial class ModiPKG : Form {
        public toolpars Toolpars { get; set; }

        public ModiPKG() {
            InitializeComponent();
        }

        public ModiPKG(toolpars Toolpars) {
            InitializeComponent();
            this.Toolpars = Toolpars;
        }


        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e) {
            Toolpars.MDistince = false;
            string StrA = "";
            if (e.Node.Checked) {
                if (e.Node.Parent != null) {
                    e.Node.Parent.Checked = true;
                }
                if (e.Node.Nodes.Count == 0) {
                    ModiName MYForm = new ModiName();
                    string mFullPath = e.Node.FullPath.ToString();


                    if (mFullPath.Contains(".cs")
                        || mFullPath.Contains(".resx")) {
                        StrA = mFullPath + ";;";
                          listDATA.Items.Add(StrA);
                        
                    }
                }
            }
            else {
                if (e.Node.Parent != null) {
                    e.Node.Parent.Checked = false;
                }
                for (int i = 0; i < listDATA.Items.Count; i++) {
                    string s = listDATA.Items[i].ToString();
                    if (s.Substring(0, s.IndexOf(";")) == e.Node.FullPath) {
                        listDATA.Items.Remove(s);
                    }
                }
            }
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked) {
                btncopypkg_Click(null,null);
                return;
            }
            try { 
            Tools.WriteLog(Toolpars, listDATA);

            Toolpars.GToIni = Toolpars.formEntity.txtToPath;
            if ((Toolpars.formEntity.txtToPath == "")
                || (Toolpars.formEntity.txtNewTypeKey == ""))
            {
                MessageBox.Show("请输入创建地址及名称", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DirectoryInfo tCusSRC = new DirectoryInfo(Toolpars.GToIni + @"\");
            if (Directory.Exists(Path.Combine(Toolpars.GToIni + @"\",
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
                    Tools.DeleteAll(tArgsPath);
                }
                else
                {
                    return;
                }
            }

            if (Directory.Exists(Toolpars.MVSToolpath + "Digiwin.ERP." + Toolpars.OldTypekey))
            {
                Tools.CopyAll(Toolpars.MVSToolpath + "Digiwin.ERP." + Toolpars.OldTypekey,
                    tCusSRC + "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey);
            }

            #region 修改文件名

            DirectoryInfo tDes =
                new DirectoryInfo(
                    Toolpars.GToIni + @"\" + "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey + @"\");
            List<string> tSearchPatternList = new List<string>();
            tSearchPatternList.Add("*xml");
            tSearchPatternList.Add("*.sln");
            tSearchPatternList.Add("*.repx");
            tSearchPatternList.Add("*proj");
            tSearchPatternList.Add("*.complete");
            tSearchPatternList.Add("*.cs");
            foreach (System.IO.DirectoryInfo d in tDes.GetDirectories("*", SearchOption.AllDirectories))
            {
                if (d.Name.IndexOf(Toolpars.formEntity.txtNewTypeKey) == -1)
                {
                    if (d.Name.IndexOf(Toolpars.OldTypekey) != -1)
                    {
                        if (
                            File.Exists(d.Parent.FullName + "\\"
                                        + d.Name.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey)))
                        {
                            File.SetAttributes(
                                d.Parent.FullName + "\\"
                                + d.Name.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey),
                                FileAttributes.Normal);
                            File.Delete(d.Parent.FullName + "\\"
                                        + d.Name.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey));
                        }
                        if (
                            Directory.Exists(d.Parent.FullName + "\\" +
                                             d.Name.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey))
                            == false)
                        {
                            d.MoveTo(d.Parent.FullName + "\\"
                                     + d.Name.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey));
                        }
                        Application.DoEvents();
                    }
                }
            }


            foreach (System.IO.FileInfo f in tDes.GetFiles("*", SearchOption.AllDirectories))
            {
                if (f.Name.IndexOf(Toolpars.formEntity.txtNewTypeKey) == -1)
                {
                    if (f.Name.IndexOf(Toolpars.OldTypekey) != -1)
                    {
                        if (File.Exists(f.FullName))
                        {
                            if (
                                File.Exists(f.Directory.FullName + "\\" +
                                            f.Name.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey))
                                == false)
                            {
                                f.MoveTo(f.Directory.FullName + "\\" +
                                         f.Name.Replace(Toolpars.OldTypekey, Toolpars.formEntity.txtNewTypeKey));
                            }
                            Application.DoEvents();
                        }
                    }
                }
            }


            for (int i = 0; i < tSearchPatternList.Count; i++)
            {
                foreach (System.IO.FileInfo f in tDes.GetFiles(tSearchPatternList[i], SearchOption.AllDirectories)
                )
                {
                    if (File.Exists(f.FullName))
                    {
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

            if (listDATA.Items.Count != 0)
            {
               copyModi();
            }
            listDATA.Items.Clear();
            foreach (TreeNode node in treeView1.Nodes)
            {
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

        private void SetCheckedChildNodes(TreeNode node)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i].Checked = false;
                SetCheckedChildNodes(node.Nodes[i]);
            }
        }

        /// <summary>
        /// 修改方案
        /// </summary>
        private void copyModi()
        {
            string typeKN = Toolpars.formEntity.txtNewTypeKey;
            string stra = Toolpars.formEntity.txtToPath.Substring(0, Toolpars.formEntity.txtToPath.IndexOf("\\"));
            string strb = Toolpars.formEntity.txtToPath.Substring(Toolpars.formEntity.txtToPath.IndexOf("\\") + 1);
            strb = strb.Substring(0, strb.IndexOf("\\"));
            for (int i = 0; i < listDATA.Items.Count; i++)
            {
                string ITEM = listDATA.Items[i].ToString();
                ITEM = ITEM.Substring(0, ITEM.Length - 2);
                string mcspro = "";
                string MPKG = "";
                if (ITEM.Contains("\\"))
                {
                    mcspro = ITEM.Substring(0, ITEM.IndexOf("\\"));
                    MPKG = ITEM.Substring(ITEM.IndexOf("\\"));
                }
                string mstrins = "";
                if (mcspro.Contains("."))
                {
                    if (mcspro.Substring(mcspro.LastIndexOf(".") + 1) == "Business")
                    {
                        mstrins = ".Business";
                    }
                    if (mcspro.Contains("Business.Implement"))
                    {
                        mstrins = ".Business.Implement";
                    }
                    if (mcspro.Contains("UI.Implement"))
                    {
                        mstrins = ".UI.Implement";
                    }
                }
                else if (mcspro.Substring(mcspro.LastIndexOf(".") + 1) == "Business")
                {
                    mstrins = ".Business";
                }
                string mfroma = Toolpars.formEntity.txtPKGpath + "Digiwin.ERP." + typeKN.Substring(1) + "\\" + ITEM;
                string mtoa = Toolpars.formEntity.txtToPath + @"\Digiwin.ERP." + typeKN + "\\" + "Digiwin.ERP." + typeKN
                              + mstrins +
                              MPKG;
                string mpatha = mtoa.Substring(0, mtoa.LastIndexOf("\\"));
                if (File.Exists(mfroma))
                {
                    if (Directory.Exists(mpatha.Substring(0, mpatha.LastIndexOf("\\"))))
                    {
                        if (!Directory.Exists(mpatha))
                        {
                            Directory.CreateDirectory(mpatha);
                        }
                        System.IO.File.Copy(mfroma, mtoa, true);
                        if (File.Exists(mtoa))
                        {
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


        private void btncopypkg_Click(object sender, EventArgs e)
        {
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
                                    Tools.DeleteAll(tArgsPath);
                                }
                                else
                                {
                                    return;
                                }
                            }
                            Tools.CopyAllPKG(strb1, tCusSRC + "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey);
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

                Tools.ModiName(Toolpars);

                #endregion

                MessageBox.Show("生成成功 !!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            sqlTools.insertToolInfo("S01231_20160503_01", "20160503", "COPY PKG SOURCE");
        }

        private void ModiPKG_Load(object sender, EventArgs e)
        {
            //绑定类，当类或控件值改变时触发更新
            this.txtToPath.DataBindings.Add(new Binding("Text", Toolpars.formEntity, "txtToPath", true,
                DataSourceUpdateMode.OnPropertyChanged));
            this.txtPKGpath.DataBindings.Add(new Binding("Text", Toolpars.formEntity, "txtPKGpath", true,
                DataSourceUpdateMode.OnPropertyChanged));
            this.txtNewTypeKey.DataBindings.Add(new Binding("Text", Toolpars.formEntity, "txtNewTypeKey", true,
                DataSourceUpdateMode.OnPropertyChanged));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Toolpars.formEntity.txtToPath != ""
                && Toolpars.formEntity.txtNewTypeKey != "")
            {
                string strb1 = Toolpars.formEntity.txtPKGpath + "Digiwin.ERP."
                               + Toolpars.formEntity.txtNewTypeKey.Substring(1);
                if (Directory.Exists(strb1)) {
                    Tools.paintTreeView(treeView1, strb1);
                    listDATA.Items.Clear();
                }
                else
                {
                    MessageBox.Show("标准代码" + strb1 + "不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    treeView1.Nodes.Clear();
                    listDATA.Items.Clear();
                }
            }
            else
            {
                treeView1.Nodes.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Toolpars.formEntity.txtToPath + @"\Digiwin.ERP."
                                 + Toolpars.formEntity.txtNewTypeKey))
            {
                Process.Start(Toolpars.formEntity.txtToPath + @"\Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey);
            }
            else
            {
                MessageBox.Show("文件夹不存在~", "注意!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}