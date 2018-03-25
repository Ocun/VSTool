
namespace VSTool
{
    partial class VSTOOL
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("节点3");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("节点0", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("节点4");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("节点1", new System.Windows.Forms.TreeNode[] {
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("节点2");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VSTOOL));
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnOpenTo = new System.Windows.Forms.Button();
            this.txtToPath = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnCreateCS = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btncopypkg = new System.Windows.Forms.Button();
            this.txtPKGpath = new System.Windows.Forms.TextBox();
            this.btnP = new System.Windows.Forms.Button();
            this.btnG = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl01 = new System.Windows.Forms.Label();
            this.rbModi = new System.Windows.Forms.RadioButton();
            this.RBBusiness = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.RBBatch = new System.Windows.Forms.RadioButton();
            this.txtNewTypeKey = new System.Windows.Forms.TextBox();
            this.RBReport = new System.Windows.Forms.RadioButton();
            this.Industry = new System.Windows.Forms.CheckBox();
            this.btncopydll = new System.Windows.Forms.Button();
            this.btncopyUIdll = new System.Windows.Forms.Button();
            this.btnKill = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.listDATA = new System.Windows.Forms.ListBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.treeView1 = new Common.Implement.UI.MyTreeView();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.myTreeView1 = new Common.Implement.UI.MyTreeView();
            this.scrollPanel = new Common.Implement.UI.MyPanel();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.myTreeView2 = new Common.Implement.UI.MyTreeView();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.myTreeView3 = new Common.Implement.UI.MyTreeView();
            this.myTreeView4 = new Common.Implement.UI.MyTreeView();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.scrollPanel.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.BackColor = System.Drawing.SystemColors.Window;
            this.btnCreate.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCreate.ForeColor = System.Drawing.Color.Black;
            this.btnCreate.Location = new System.Drawing.Point(1107, 32);
            this.btnCreate.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(100, 32);
            this.btnCreate.TabIndex = 6;
            this.btnCreate.Text = "生成";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnOpenTo
            // 
            this.btnOpenTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenTo.FlatAppearance.BorderSize = 0;
            this.btnOpenTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenTo.Location = new System.Drawing.Point(955, 35);
            this.btnOpenTo.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenTo.Name = "btnOpenTo";
            this.btnOpenTo.Size = new System.Drawing.Size(44, 29);
            this.btnOpenTo.TabIndex = 9;
            this.btnOpenTo.Text = "...";
            this.btnOpenTo.UseVisualStyleBackColor = true;
            this.btnOpenTo.Click += new System.EventHandler(this.btnOpenTo_Click);
            // 
            // txtToPath
            // 
            this.txtToPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtToPath.Enabled = false;
            this.txtToPath.Location = new System.Drawing.Point(39, 38);
            this.txtToPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtToPath.Name = "txtToPath";
            this.txtToPath.Size = new System.Drawing.Size(907, 25);
            this.txtToPath.TabIndex = 10;
            // 
            // btnCreateCS
            // 
            this.btnCreateCS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateCS.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCreateCS.ForeColor = System.Drawing.Color.Black;
            this.btnCreateCS.Location = new System.Drawing.Point(1107, 69);
            this.btnCreateCS.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreateCS.Name = "btnCreateCS";
            this.btnCreateCS.Size = new System.Drawing.Size(100, 32);
            this.btnCreateCS.TabIndex = 12;
            this.btnCreateCS.Text = "生成类";
            this.btnCreateCS.UseVisualStyleBackColor = true;
            this.btnCreateCS.Click += new System.EventHandler(this.btnCreateCS_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClear.ForeColor = System.Drawing.Color.Black;
            this.btnClear.Location = new System.Drawing.Point(1107, 141);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 32);
            this.btnClear.TabIndex = 15;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpen.ForeColor = System.Drawing.Color.Black;
            this.btnOpen.Location = new System.Drawing.Point(1107, 105);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(100, 32);
            this.btnOpen.TabIndex = 19;
            this.btnOpen.Text = "OPEN";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btncopypkg
            // 
            this.btncopypkg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btncopypkg.FlatAppearance.BorderSize = 0;
            this.btncopypkg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncopypkg.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btncopypkg.ForeColor = System.Drawing.Color.ForestGreen;
            this.btncopypkg.Location = new System.Drawing.Point(955, 106);
            this.btncopypkg.Margin = new System.Windows.Forms.Padding(4);
            this.btncopypkg.Name = "btncopypkg";
            this.btncopypkg.Size = new System.Drawing.Size(85, 29);
            this.btncopypkg.TabIndex = 24;
            this.btncopypkg.Text = "复制PKG代码";
            this.btncopypkg.UseVisualStyleBackColor = true;
            this.btncopypkg.Click += new System.EventHandler(this.btncopypkg_Click);
            // 
            // txtPKGpath
            // 
            this.txtPKGpath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPKGpath.Enabled = false;
            this.txtPKGpath.Location = new System.Drawing.Point(39, 105);
            this.txtPKGpath.Margin = new System.Windows.Forms.Padding(4);
            this.txtPKGpath.Name = "txtPKGpath";
            this.txtPKGpath.Size = new System.Drawing.Size(907, 25);
            this.txtPKGpath.TabIndex = 27;
            // 
            // btnP
            // 
            this.btnP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnP.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnP.FlatAppearance.BorderSize = 0;
            this.btnP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnP.ForeColor = System.Drawing.Color.YellowGreen;
            this.btnP.Location = new System.Drawing.Point(1007, 35);
            this.btnP.Margin = new System.Windows.Forms.Padding(4);
            this.btnP.Name = "btnP";
            this.btnP.Size = new System.Drawing.Size(33, 26);
            this.btnP.TabIndex = 30;
            this.btnP.Text = "◎";
            this.btnP.UseVisualStyleBackColor = true;
            this.btnP.Visible = false;
            this.btnP.Click += new System.EventHandler(this.btnP_Click);
            // 
            // btnG
            // 
            this.btnG.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnG.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnG.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnG.FlatAppearance.BorderSize = 0;
            this.btnG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnG.ForeColor = System.Drawing.Color.YellowGreen;
            this.btnG.Location = new System.Drawing.Point(1048, 105);
            this.btnG.Margin = new System.Windows.Forms.Padding(4);
            this.btnG.Name = "btnG";
            this.btnG.Size = new System.Drawing.Size(33, 26);
            this.btnG.TabIndex = 31;
            this.btnG.Text = "◎";
            this.btnG.UseVisualStyleBackColor = true;
            this.btnG.Click += new System.EventHandler(this.btnG_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.lbl01);
            this.panel1.Controls.Add(this.rbModi);
            this.panel1.Controls.Add(this.RBBusiness);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.RBBatch);
            this.panel1.Controls.Add(this.txtNewTypeKey);
            this.panel1.Controls.Add(this.RBReport);
            this.panel1.Controls.Add(this.Industry);
            this.panel1.Controls.Add(this.btncopydll);
            this.panel1.Controls.Add(this.btncopyUIdll);
            this.panel1.Controls.Add(this.btnKill);
            this.panel1.Location = new System.Drawing.Point(-8, -12);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1228, 104);
            this.panel1.TabIndex = 32;
            // 
            // lbl01
            // 
            this.lbl01.AutoSize = true;
            this.lbl01.BackColor = System.Drawing.Color.Transparent;
            this.lbl01.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl01.Location = new System.Drawing.Point(237, 19);
            this.lbl01.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl01.Name = "lbl01";
            this.lbl01.Size = new System.Drawing.Size(79, 20);
            this.lbl01.TabIndex = 1;
            this.lbl01.Text = "Typekey";
            // 
            // rbModi
            // 
            this.rbModi.AutoSize = true;
            this.rbModi.BackColor = System.Drawing.Color.Transparent;
            this.rbModi.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbModi.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.rbModi.Location = new System.Drawing.Point(560, 76);
            this.rbModi.Margin = new System.Windows.Forms.Padding(4);
            this.rbModi.Name = "rbModi";
            this.rbModi.Size = new System.Drawing.Size(65, 22);
            this.rbModi.TabIndex = 21;
            this.rbModi.TabStop = true;
            this.rbModi.Text = "修改";
            this.rbModi.UseVisualStyleBackColor = false;
            this.rbModi.CheckedChanged += new System.EventHandler(this.rbModi_CheckedChanged);
            // 
            // RBBusiness
            // 
            this.RBBusiness.AutoSize = true;
            this.RBBusiness.BackColor = System.Drawing.Color.Transparent;
            this.RBBusiness.Checked = true;
            this.RBBusiness.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RBBusiness.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.RBBusiness.Location = new System.Drawing.Point(240, 76);
            this.RBBusiness.Margin = new System.Windows.Forms.Padding(4);
            this.RBBusiness.Name = "RBBusiness";
            this.RBBusiness.Size = new System.Drawing.Size(65, 22);
            this.RBBusiness.TabIndex = 16;
            this.RBBusiness.TabStop = true;
            this.RBBusiness.Text = "建档";
            this.RBBusiness.UseVisualStyleBackColor = false;
            this.RBBusiness.CheckedChanged += new System.EventHandler(this.RBBusiness_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.BlueViolet;
            this.label2.Location = new System.Drawing.Point(24, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(205, 60);
            this.label2.TabIndex = 0;
            this.label2.Text = "VSTOOL";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // RBBatch
            // 
            this.RBBatch.AutoSize = true;
            this.RBBatch.BackColor = System.Drawing.Color.Transparent;
            this.RBBatch.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RBBatch.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.RBBatch.Location = new System.Drawing.Point(360, 76);
            this.RBBatch.Margin = new System.Windows.Forms.Padding(4);
            this.RBBatch.Name = "RBBatch";
            this.RBBatch.Size = new System.Drawing.Size(65, 22);
            this.RBBatch.TabIndex = 17;
            this.RBBatch.Text = "批次";
            this.RBBatch.UseVisualStyleBackColor = false;
            this.RBBatch.CheckedChanged += new System.EventHandler(this.RBBatch_CheckedChanged);
            // 
            // txtNewTypeKey
            // 
            this.txtNewTypeKey.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNewTypeKey.Location = new System.Drawing.Point(251, 42);
            this.txtNewTypeKey.Margin = new System.Windows.Forms.Padding(4);
            this.txtNewTypeKey.Name = "txtNewTypeKey";
            this.txtNewTypeKey.Size = new System.Drawing.Size(593, 25);
            this.txtNewTypeKey.TabIndex = 2;
            this.txtNewTypeKey.TextChanged += new System.EventHandler(this.txtNewTypeKey_TextChanged);
            // 
            // RBReport
            // 
            this.RBReport.AutoSize = true;
            this.RBReport.BackColor = System.Drawing.Color.Transparent;
            this.RBReport.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RBReport.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.RBReport.Location = new System.Drawing.Point(453, 76);
            this.RBReport.Margin = new System.Windows.Forms.Padding(4);
            this.RBReport.Name = "RBReport";
            this.RBReport.Size = new System.Drawing.Size(65, 22);
            this.RBReport.TabIndex = 18;
            this.RBReport.Text = "报表";
            this.RBReport.UseVisualStyleBackColor = false;
            this.RBReport.CheckedChanged += new System.EventHandler(this.RBReport_CheckedChanged);
            // 
            // Industry
            // 
            this.Industry.AutoSize = true;
            this.Industry.Location = new System.Drawing.Point(756, 74);
            this.Industry.Margin = new System.Windows.Forms.Padding(4);
            this.Industry.Name = "Industry";
            this.Industry.Size = new System.Drawing.Size(74, 19);
            this.Industry.TabIndex = 28;
            this.Industry.Text = "行业包";
            this.Industry.UseVisualStyleBackColor = true;
            this.Industry.CheckedChanged += new System.EventHandler(this.Industry_CheckedChanged);
            // 
            // btncopydll
            // 
            this.btncopydll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncopydll.ForeColor = System.Drawing.Color.ForestGreen;
            this.btncopydll.Location = new System.Drawing.Point(952, 42);
            this.btncopydll.Margin = new System.Windows.Forms.Padding(4);
            this.btncopydll.Name = "btncopydll";
            this.btncopydll.Size = new System.Drawing.Size(100, 29);
            this.btncopydll.TabIndex = 22;
            this.btncopydll.Text = "Copydll";
            this.btncopydll.UseVisualStyleBackColor = true;
            this.btncopydll.Click += new System.EventHandler(this.btncopydll_Click);
            // 
            // btncopyUIdll
            // 
            this.btncopyUIdll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncopyUIdll.ForeColor = System.Drawing.Color.ForestGreen;
            this.btncopyUIdll.Location = new System.Drawing.Point(844, 42);
            this.btncopyUIdll.Margin = new System.Windows.Forms.Padding(4);
            this.btncopyUIdll.Name = "btncopyUIdll";
            this.btncopyUIdll.Size = new System.Drawing.Size(100, 29);
            this.btncopyUIdll.TabIndex = 23;
            this.btncopyUIdll.Text = "CopyUIdll";
            this.btncopyUIdll.UseVisualStyleBackColor = true;
            this.btncopyUIdll.Click += new System.EventHandler(this.btncopyUIdll_Click);
            // 
            // btnKill
            // 
            this.btnKill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKill.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnKill.Location = new System.Drawing.Point(1060, 42);
            this.btnKill.Margin = new System.Windows.Forms.Padding(4);
            this.btnKill.Name = "btnKill";
            this.btnKill.Size = new System.Drawing.Size(100, 29);
            this.btnKill.TabIndex = 25;
            this.btnKill.Text = "Kill";
            this.btnKill.UseVisualStyleBackColor = true;
            this.btnKill.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Window;
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.listDATA);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(248, 584);
            this.panel3.TabIndex = 34;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(7, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "摘要";
            // 
            // listDATA
            // 
            this.listDATA.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listDATA.BackColor = System.Drawing.SystemColors.Window;
            this.listDATA.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listDATA.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listDATA.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listDATA.ForeColor = System.Drawing.Color.DarkGreen;
            this.listDATA.FormattingEnabled = true;
            this.listDATA.ItemHeight = 15;
            this.listDATA.Location = new System.Drawing.Point(12, 29);
            this.listDATA.Margin = new System.Windows.Forms.Padding(4);
            this.listDATA.Name = "listDATA";
            this.listDATA.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listDATA.Size = new System.Drawing.Size(232, 542);
            this.listDATA.Sorted = true;
            this.listDATA.TabIndex = 14;
            this.listDATA.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listDATA_DrawItem);
            this.listDATA.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.listDATA_MeasureItem);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.SystemColors.Menu;
            this.panel4.Controls.Add(this.treeView1);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.txtToPath);
            this.panel4.Controls.Add(this.btnG);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.btnCreate);
            this.panel4.Controls.Add(this.btnP);
            this.panel4.Controls.Add(this.btnOpenTo);
            this.panel4.Controls.Add(this.btnCreateCS);
            this.panel4.Controls.Add(this.txtPKGpath);
            this.panel4.Controls.Add(this.btncopypkg);
            this.panel4.Controls.Add(this.btnOpen);
            this.panel4.Controls.Add(this.btnClear);
            this.panel4.Location = new System.Drawing.Point(-8, 675);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1229, 198);
            this.panel4.TabIndex = 35;
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.SystemColors.Window;
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.treeView1.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.treeView1.FullRowSelect = true;
            this.treeView1.HideSelection = false;
            this.treeView1.HotTracking = true;
            this.treeView1.ImageHeight = 25;
            this.treeView1.ImageWidth = 25;
            this.treeView1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.treeView1.IsCard = false;
            this.treeView1.ItemHeight = 25;
            this.treeView1.Location = new System.Drawing.Point(489, 139);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4);
            this.treeView1.Name = "treeView1";
            this.treeView1.NodeFont = null;
            this.treeView1.NodeImageSize = new System.Drawing.Size(20, 20);
            this.treeView1.NodeOffset = 0;
            treeNode1.Name = "节点3";
            treeNode1.Text = "节点3";
            treeNode2.Name = "节点0";
            treeNode2.Text = "节点0";
            treeNode3.Name = "节点4";
            treeNode3.Text = "节点4";
            treeNode4.Name = "节点1";
            treeNode4.Text = "节点1";
            treeNode5.Name = "节点2";
            treeNode5.Text = "节点2";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode4,
            treeNode5});
            this.treeView1.PaddingSet = 0;
            this.treeView1.ShowLines = false;
            this.treeView1.ShowPlusMinus = false;
            this.treeView1.Size = new System.Drawing.Size(432, 194);
            this.treeView1.TabIndex = 13;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(35, 84);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 18);
            this.label4.TabIndex = 32;
            this.label4.Text = "源码位置";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(35, 16);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "创建位置";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(-8, 91);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            this.splitContainer1.Panel1MinSize = 600;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Size = new System.Drawing.Size(1237, 584);
            this.splitContainer1.SplitterDistance = 988;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 36;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.splitContainer2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(988, 584);
            this.panel2.TabIndex = 33;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.myTreeView1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.scrollPanel);
            this.splitContainer2.Size = new System.Drawing.Size(988, 584);
            this.splitContainer2.SplitterDistance = 245;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 14;
            // 
            // myTreeView1
            // 
            this.myTreeView1.BackColor = System.Drawing.SystemColors.Menu;
            this.myTreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.myTreeView1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.myTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myTreeView1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.myTreeView1.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myTreeView1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.myTreeView1.FullRowSelect = true;
            this.myTreeView1.HideSelection = false;
            this.myTreeView1.HotTracking = true;
            this.myTreeView1.ImageHeight = 25;
            this.myTreeView1.ImageWidth = 25;
            this.myTreeView1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.myTreeView1.IsCard = false;
            this.myTreeView1.ItemHeight = 50;
            this.myTreeView1.Location = new System.Drawing.Point(0, 0);
            this.myTreeView1.Margin = new System.Windows.Forms.Padding(4);
            this.myTreeView1.Name = "myTreeView1";
            this.myTreeView1.NodeFont = null;
            this.myTreeView1.NodeImageSize = new System.Drawing.Size(20, 20);
            this.myTreeView1.NodeOffset = 0;
            this.myTreeView1.PaddingSet = 2;
            this.myTreeView1.ShowLines = false;
            this.myTreeView1.ShowPlusMinus = false;
            this.myTreeView1.Size = new System.Drawing.Size(245, 584);
            this.myTreeView1.TabIndex = 14;
            this.myTreeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.myTreeView1_NodeMouseClick);
            // 
            // scrollPanel
            // 
            this.scrollPanel.AutoScroll = true;
            this.scrollPanel.Controls.Add(this.splitContainer3);
            this.scrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scrollPanel.Location = new System.Drawing.Point(0, 0);
            this.scrollPanel.Name = "scrollPanel";
            this.scrollPanel.Size = new System.Drawing.Size(742, 584);
            this.scrollPanel.TabIndex = 35;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.myTreeView2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Panel2Collapsed = true;
            this.splitContainer3.Size = new System.Drawing.Size(742, 584);
            this.splitContainer3.SplitterDistance = 132;
            this.splitContainer3.SplitterWidth = 1;
            this.splitContainer3.TabIndex = 34;
            // 
            // myTreeView2
            // 
            this.myTreeView2.BackColor = System.Drawing.SystemColors.Window;
            this.myTreeView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.myTreeView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myTreeView2.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.myTreeView2.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myTreeView2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.myTreeView2.FullRowSelect = true;
            this.myTreeView2.HotTracking = true;
            this.myTreeView2.ImageHeight = 25;
            this.myTreeView2.ImageWidth = 25;
            this.myTreeView2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.myTreeView2.IsCard = true;
            this.myTreeView2.ItemHeight = 65;
            this.myTreeView2.Location = new System.Drawing.Point(0, 0);
            this.myTreeView2.Margin = new System.Windows.Forms.Padding(4);
            this.myTreeView2.Name = "myTreeView2";
            this.myTreeView2.NodeFont = null;
            this.myTreeView2.NodeImageSize = new System.Drawing.Size(20, 20);
            this.myTreeView2.NodeOffset = 0;
            this.myTreeView2.PaddingSet = 3;
            this.myTreeView2.Scrollable = false;
            this.myTreeView2.ShowLines = false;
            this.myTreeView2.ShowPlusMinus = false;
            this.myTreeView2.Size = new System.Drawing.Size(742, 584);
            this.myTreeView2.TabIndex = 33;
            this.myTreeView2.SetAutoScrollEvent += new Common.Implement.UI.MyTreeView.SetAutoScrollHandler(this.myTreeView2_SetAutoScrollEvent);
            this.myTreeView2.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.myTreeView2_AfterCheck);
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.IsSplitterFixed = true;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.myTreeView3);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.myTreeView4);
            this.splitContainer4.Size = new System.Drawing.Size(96, 100);
            this.splitContainer4.SplitterDistance = 45;
            this.splitContainer4.SplitterWidth = 1;
            this.splitContainer4.TabIndex = 0;
            // 
            // myTreeView3
            // 
            this.myTreeView3.BackColor = System.Drawing.SystemColors.Window;
            this.myTreeView3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.myTreeView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myTreeView3.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.myTreeView3.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myTreeView3.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.myTreeView3.FullRowSelect = true;
            this.myTreeView3.HotTracking = true;
            this.myTreeView3.ImageHeight = 25;
            this.myTreeView3.ImageWidth = 25;
            this.myTreeView3.ImeMode = System.Windows.Forms.ImeMode.On;
            this.myTreeView3.IsCard = true;
            this.myTreeView3.ItemHeight = 65;
            this.myTreeView3.Location = new System.Drawing.Point(0, 0);
            this.myTreeView3.Margin = new System.Windows.Forms.Padding(4);
            this.myTreeView3.Name = "myTreeView3";
            this.myTreeView3.NodeFont = null;
            this.myTreeView3.NodeImageSize = new System.Drawing.Size(20, 20);
            this.myTreeView3.NodeOffset = 0;
            this.myTreeView3.PaddingSet = 3;
            this.myTreeView3.Scrollable = false;
            this.myTreeView3.ShowLines = false;
            this.myTreeView3.ShowPlusMinus = false;
            this.myTreeView3.Size = new System.Drawing.Size(45, 100);
            this.myTreeView3.TabIndex = 34;
            this.myTreeView3.SetAutoScrollEvent += new Common.Implement.UI.MyTreeView.SetAutoScrollHandler(this.myTreeView2_SetAutoScrollEvent);
            this.myTreeView3.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.myTreeView2_AfterCheck);
            // 
            // myTreeView4
            // 
            this.myTreeView4.BackColor = System.Drawing.SystemColors.Window;
            this.myTreeView4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.myTreeView4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myTreeView4.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.myTreeView4.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myTreeView4.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.myTreeView4.FullRowSelect = true;
            this.myTreeView4.HotTracking = true;
            this.myTreeView4.ImageHeight = 25;
            this.myTreeView4.ImageWidth = 25;
            this.myTreeView4.ImeMode = System.Windows.Forms.ImeMode.On;
            this.myTreeView4.IsCard = true;
            this.myTreeView4.ItemHeight = 65;
            this.myTreeView4.Location = new System.Drawing.Point(0, 0);
            this.myTreeView4.Margin = new System.Windows.Forms.Padding(4);
            this.myTreeView4.Name = "myTreeView4";
            this.myTreeView4.NodeFont = null;
            this.myTreeView4.NodeImageSize = new System.Drawing.Size(20, 20);
            this.myTreeView4.NodeOffset = 0;
            this.myTreeView4.PaddingSet = 3;
            this.myTreeView4.Scrollable = false;
            this.myTreeView4.ShowLines = false;
            this.myTreeView4.ShowPlusMinus = false;
            this.myTreeView4.Size = new System.Drawing.Size(50, 100);
            this.myTreeView4.TabIndex = 35;
            this.myTreeView4.SetAutoScrollEvent += new Common.Implement.UI.MyTreeView.SetAutoScrollHandler(this.myTreeView2_SetAutoScrollEvent);
            this.myTreeView4.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.myTreeView2_AfterCheck);
            // 
            // VSTOOL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1219, 868);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1234, 845);
            this.Name = "VSTOOL";
            this.Load += new System.EventHandler(this.VSTOOL_Load);
            this.ClientSizeChanged += new System.EventHandler(this.VSTOOL_ClientSizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.scrollPanel.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnOpenTo;
        private System.Windows.Forms.TextBox txtToPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnCreateCS;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btncopypkg;
        private System.Windows.Forms.TextBox txtPKGpath;
        private System.Windows.Forms.Button btnP;
        private System.Windows.Forms.Button btnG;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbl01;
        private System.Windows.Forms.TextBox txtNewTypeKey;
        private System.Windows.Forms.CheckBox Industry;
        private System.Windows.Forms.Button btnKill;
        private System.Windows.Forms.Button btncopyUIdll;
        private System.Windows.Forms.ListBox listDATA;
        private System.Windows.Forms.Button btncopydll;
        private System.Windows.Forms.RadioButton rbModi;
        private System.Windows.Forms.RadioButton RBBusiness;
        private System.Windows.Forms.RadioButton RBBatch;
        private System.Windows.Forms.RadioButton RBReport;
        private System.Windows.Forms.Label label1;
        private Common.Implement.UI.MyTreeView treeView1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Common.Implement.UI.MyTreeView myTreeView1;
        private Common.Implement.UI.MyTreeView myTreeView2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private Common.Implement.UI.MyTreeView myTreeView3;
        private Common.Implement.UI.MyTreeView myTreeView4;
        private Common.Implement.UI.MyPanel scrollPanel;
    }
}

