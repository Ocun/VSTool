
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VSTOOL));
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnOpenTo = new System.Windows.Forms.Button();
            this.txtToPath = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.txtPKGpath = new System.Windows.Forms.TextBox();
            this.btnP = new System.Windows.Forms.Button();
            this.btnG = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ModiCkb = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNewTypeKey = new System.Windows.Forms.TextBox();
            this.Industry = new System.Windows.Forms.CheckBox();
            this.btncopydll = new System.Windows.Forms.Button();
            this.btncopyUIdll = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.myTreeView5 = new Common.Implement.UI.MyTreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.ToolsDescrition = new System.Windows.Forms.Label();
            this.PkgOpenTo = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.myTreeView1 = new Common.Implement.UI.MyTreeView();
            this.scrollPanel = new Common.Implement.UI.MyPanel();
            this.treeView1 = new Common.Implement.UI.MyTreeView();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.BackColor = System.Drawing.SystemColors.Window;
            this.btnCreate.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCreate.ForeColor = System.Drawing.Color.Black;
            this.btnCreate.Location = new System.Drawing.Point(1158, 52);
            this.btnCreate.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(100, 29);
            this.btnCreate.TabIndex = 6;
            this.btnCreate.Text = "生成";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.BtnCreate_Click);
            // 
            // btnOpenTo
            // 
            this.btnOpenTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenTo.FlatAppearance.BorderSize = 0;
            this.btnOpenTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenTo.Font = new System.Drawing.Font("新宋体", 10F);
            this.btnOpenTo.ForeColor = System.Drawing.Color.DarkCyan;
            this.btnOpenTo.Location = new System.Drawing.Point(1006, 52);
            this.btnOpenTo.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenTo.Name = "btnOpenTo";
            this.btnOpenTo.Size = new System.Drawing.Size(51, 30);
            this.btnOpenTo.TabIndex = 9;
            this.btnOpenTo.Text = "○";
            this.btnOpenTo.UseVisualStyleBackColor = true;
            this.btnOpenTo.Click += new System.EventHandler(this.BtnOpenTo_Click);
            // 
            // txtToPath
            // 
            this.txtToPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtToPath.Location = new System.Drawing.Point(28, 52);
            this.txtToPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtToPath.Name = "txtToPath";
            this.txtToPath.Size = new System.Drawing.Size(969, 25);
            this.txtToPath.TabIndex = 10;
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "请选择所在目录";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClear.ForeColor = System.Drawing.Color.Black;
            this.btnClear.Location = new System.Drawing.Point(1158, 126);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 29);
            this.btnClear.TabIndex = 15;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpen.ForeColor = System.Drawing.Color.Black;
            this.btnOpen.Location = new System.Drawing.Point(1158, 89);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(100, 29);
            this.btnOpen.TabIndex = 19;
            this.btnOpen.Text = "OPEN";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.BtnOpen_Click);
            // 
            // txtPKGpath
            // 
            this.txtPKGpath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPKGpath.Location = new System.Drawing.Point(28, 119);
            this.txtPKGpath.Margin = new System.Windows.Forms.Padding(4);
            this.txtPKGpath.Name = "txtPKGpath";
            this.txtPKGpath.Size = new System.Drawing.Size(969, 25);
            this.txtPKGpath.TabIndex = 27;
            // 
            // btnP
            // 
            this.btnP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnP.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnP.FlatAppearance.BorderSize = 0;
            this.btnP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnP.ForeColor = System.Drawing.Color.YellowGreen;
            this.btnP.Location = new System.Drawing.Point(1058, 52);
            this.btnP.Margin = new System.Windows.Forms.Padding(4);
            this.btnP.Name = "btnP";
            this.btnP.Size = new System.Drawing.Size(33, 25);
            this.btnP.TabIndex = 30;
            this.btnP.Text = "◎";
            this.btnP.UseVisualStyleBackColor = true;
            this.btnP.Visible = false;
            this.btnP.Click += new System.EventHandler(this.BtnP_Click);
            // 
            // btnG
            // 
            this.btnG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnG.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnG.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnG.FlatAppearance.BorderSize = 0;
            this.btnG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnG.ForeColor = System.Drawing.Color.YellowGreen;
            this.btnG.Location = new System.Drawing.Point(1058, 119);
            this.btnG.Margin = new System.Windows.Forms.Padding(4);
            this.btnG.Name = "btnG";
            this.btnG.Size = new System.Drawing.Size(33, 25);
            this.btnG.TabIndex = 31;
            this.btnG.Text = "◎";
            this.btnG.UseVisualStyleBackColor = true;
            this.btnG.Visible = false;
            this.btnG.Click += new System.EventHandler(this.BtnG_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.ModiCkb);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtNewTypeKey);
            this.panel1.Controls.Add(this.Industry);
            this.panel1.Controls.Add(this.btncopydll);
            this.panel1.Controls.Add(this.btncopyUIdll);
            this.panel1.Location = new System.Drawing.Point(-8, -12);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1300, 104);
            this.panel1.TabIndex = 32;
            // 
            // ModiCkb
            // 
            this.ModiCkb.AutoSize = true;
            this.ModiCkb.Location = new System.Drawing.Point(333, 74);
            this.ModiCkb.Margin = new System.Windows.Forms.Padding(4);
            this.ModiCkb.Name = "ModiCkb";
            this.ModiCkb.Size = new System.Drawing.Size(59, 19);
            this.ModiCkb.TabIndex = 39;
            this.ModiCkb.Text = "借用";
            this.ModiCkb.UseVisualStyleBackColor = true;
            this.ModiCkb.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 34.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.BlueViolet;
            this.label2.Location = new System.Drawing.Point(8, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(227, 76);
            this.label2.TabIndex = 0;
            this.label2.Text = "VSTool";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtNewTypeKey
            // 
            this.txtNewTypeKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewTypeKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewTypeKey.Location = new System.Drawing.Point(251, 42);
            this.txtNewTypeKey.Margin = new System.Windows.Forms.Padding(4);
            this.txtNewTypeKey.Name = "txtNewTypeKey";
            this.txtNewTypeKey.Size = new System.Drawing.Size(799, 25);
            this.txtNewTypeKey.TabIndex = 2;
            this.txtNewTypeKey.TextChanged += new System.EventHandler(this.TxtNewTypeKey_TextChanged);
            // 
            // Industry
            // 
            this.Industry.AutoSize = true;
            this.Industry.Location = new System.Drawing.Point(251, 74);
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
            this.btncopydll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btncopydll.FlatAppearance.BorderSize = 0;
            this.btncopydll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncopydll.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btncopydll.ForeColor = System.Drawing.Color.DarkGreen;
            this.btncopydll.Location = new System.Drawing.Point(1164, 62);
            this.btncopydll.Margin = new System.Windows.Forms.Padding(4);
            this.btncopydll.Name = "btncopydll";
            this.btncopydll.Size = new System.Drawing.Size(120, 31);
            this.btncopydll.TabIndex = 22;
            this.btncopydll.Text = "复制DLL";
            this.btncopydll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btncopydll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btncopydll.UseVisualStyleBackColor = true;
            this.btncopydll.Click += new System.EventHandler(this.Btncopydll_Click);
            // 
            // btncopyUIdll
            // 
            this.btncopyUIdll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btncopyUIdll.FlatAppearance.BorderSize = 0;
            this.btncopyUIdll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncopyUIdll.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btncopyUIdll.ForeColor = System.Drawing.Color.DarkGreen;
            this.btncopyUIdll.Location = new System.Drawing.Point(1164, 23);
            this.btncopyUIdll.Margin = new System.Windows.Forms.Padding(4);
            this.btncopyUIdll.Name = "btncopyUIdll";
            this.btncopyUIdll.Size = new System.Drawing.Size(128, 35);
            this.btncopyUIdll.TabIndex = 23;
            this.btncopyUIdll.Text = "复制客户端DLL";
            this.btncopyUIdll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btncopyUIdll.UseVisualStyleBackColor = true;
            this.btncopyUIdll.Click += new System.EventHandler(this.BtncopyUIdll_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Window;
            this.panel3.Controls.Add(this.myTreeView5);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(256, 584);
            this.panel3.TabIndex = 34;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel3_Paint);
            // 
            // myTreeView5
            // 
            this.myTreeView5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myTreeView5.BackColor = System.Drawing.Color.White;
            this.myTreeView5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.myTreeView5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.myTreeView5.DescriptionColor = System.Drawing.Color.Gray;
            this.myTreeView5.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.myTreeView5.Font = new System.Drawing.Font("新宋体", 11F);
            this.myTreeView5.ForeColor = System.Drawing.Color.Black;
            this.myTreeView5.FullRowSelect = true;
            this.myTreeView5.HotTracking = true;
            this.myTreeView5.ImeMode = System.Windows.Forms.ImeMode.On;
            this.myTreeView5.IsCard = false;
            this.myTreeView5.ItemHeight = 25;
            this.myTreeView5.Location = new System.Drawing.Point(12, 29);
            this.myTreeView5.Margin = new System.Windows.Forms.Padding(4);
            this.myTreeView5.Name = "myTreeView5";
            this.myTreeView5.NodeFont = null;
            this.myTreeView5.NodeImageSize = new System.Drawing.Size(20, 20);
            this.myTreeView5.PaddingSetting = new System.Drawing.Point(0, 4);
            this.myTreeView5.ShowDescription = false;
            this.myTreeView5.ShowLines = false;
            this.myTreeView5.ShowPlusMinus = false;
            this.myTreeView5.Size = new System.Drawing.Size(235, 555);
            this.myTreeView5.TabIndex = 15;
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
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Menu;
            this.panel4.Controls.Add(this.linkLabel1);
            this.panel4.Controls.Add(this.ToolsDescrition);
            this.panel4.Controls.Add(this.PkgOpenTo);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.txtToPath);
            this.panel4.Controls.Add(this.btnG);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.btnCreate);
            this.panel4.Controls.Add(this.btnP);
            this.panel4.Controls.Add(this.btnOpenTo);
            this.panel4.Controls.Add(this.txtPKGpath);
            this.panel4.Controls.Add(this.btnOpen);
            this.panel4.Controls.Add(this.btnClear);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 673);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1291, 195);
            this.panel4.TabIndex = 35;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(602, 163);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(37, 15);
            this.linkLabel1.TabIndex = 34;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "帮助";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            // 
            // ToolsDescrition
            // 
            this.ToolsDescrition.AutoSize = true;
            this.ToolsDescrition.ForeColor = System.Drawing.Color.DimGray;
            this.ToolsDescrition.Location = new System.Drawing.Point(25, 163);
            this.ToolsDescrition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ToolsDescrition.Name = "ToolsDescrition";
            this.ToolsDescrition.Size = new System.Drawing.Size(532, 15);
            this.ToolsDescrition.TabIndex = 16;
            this.ToolsDescrition.Text = "继续使用表示你同意本工具的使用协议，并自行承担由此带来的风险，更多点击";
            // 
            // PkgOpenTo
            // 
            this.PkgOpenTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PkgOpenTo.FlatAppearance.BorderSize = 0;
            this.PkgOpenTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PkgOpenTo.Font = new System.Drawing.Font("新宋体", 10F);
            this.PkgOpenTo.ForeColor = System.Drawing.Color.DarkCyan;
            this.PkgOpenTo.Location = new System.Drawing.Point(1006, 119);
            this.PkgOpenTo.Margin = new System.Windows.Forms.Padding(4);
            this.PkgOpenTo.Name = "PkgOpenTo";
            this.PkgOpenTo.Size = new System.Drawing.Size(51, 30);
            this.PkgOpenTo.TabIndex = 33;
            this.PkgOpenTo.Text = "○";
            this.PkgOpenTo.UseVisualStyleBackColor = true;
            this.PkgOpenTo.Click += new System.EventHandler(this.BtnOpenTo_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(24, 98);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 18);
            this.label4.TabIndex = 32;
            this.label4.Text = "借用目录";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(24, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "创建到";
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
            this.splitContainer1.Size = new System.Drawing.Size(1309, 584);
            this.splitContainer1.SplitterDistance = 1052;
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
            this.panel2.Size = new System.Drawing.Size(1052, 584);
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
            this.splitContainer2.Panel2.Controls.Add(this.treeView1);
            this.splitContainer2.Size = new System.Drawing.Size(1052, 584);
            this.splitContainer2.SplitterDistance = 245;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 14;
            // 
            // myTreeView1
            // 
            this.myTreeView1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.myTreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.myTreeView1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.myTreeView1.DescriptionColor = System.Drawing.Color.DimGray;
            this.myTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myTreeView1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.myTreeView1.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myTreeView1.ForeColor = System.Drawing.Color.Black;
            this.myTreeView1.FullRowSelect = true;
            this.myTreeView1.HideSelection = false;
            this.myTreeView1.HotTracking = true;
            this.myTreeView1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.myTreeView1.IsCard = false;
            this.myTreeView1.ItemHeight = 50;
            this.myTreeView1.Location = new System.Drawing.Point(0, 0);
            this.myTreeView1.Margin = new System.Windows.Forms.Padding(4);
            this.myTreeView1.Name = "myTreeView1";
            this.myTreeView1.NodeFont = null;
            this.myTreeView1.NodeImageSize = new System.Drawing.Size(20, 20);
            this.myTreeView1.PaddingSetting = new System.Drawing.Point(0, 4);
            this.myTreeView1.ShowDescription = true;
            this.myTreeView1.ShowLines = false;
            this.myTreeView1.ShowPlusMinus = false;
            this.myTreeView1.Size = new System.Drawing.Size(245, 584);
            this.myTreeView1.TabIndex = 14;
            this.myTreeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.MyTreeView1_NodeMouseClick);
            // 
            // scrollPanel
            // 
            this.scrollPanel.AutoScroll = true;
            this.scrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scrollPanel.Location = new System.Drawing.Point(0, 0);
            this.scrollPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.scrollPanel.Name = "scrollPanel";
            this.scrollPanel.Size = new System.Drawing.Size(806, 584);
            this.scrollPanel.TabIndex = 35;
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.SystemColors.Window;
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.DescriptionColor = System.Drawing.Color.Empty;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.treeView1.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.treeView1.FullRowSelect = true;
            this.treeView1.HideSelection = false;
            this.treeView1.HotTracking = true;
            this.treeView1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.treeView1.IsCard = false;
            this.treeView1.ItemHeight = 25;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4);
            this.treeView1.Name = "treeView1";
            this.treeView1.NodeFont = null;
            this.treeView1.NodeImageSize = new System.Drawing.Size(20, 20);
            this.treeView1.PaddingSetting = new System.Drawing.Point(0, 0);
            this.treeView1.ShowDescription = false;
            this.treeView1.ShowLines = false;
            this.treeView1.ShowPlusMinus = false;
            this.treeView1.Size = new System.Drawing.Size(806, 584);
            this.treeView1.TabIndex = 13;
            this.treeView1.Visible = false;
            // 
            // VSTOOL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1291, 868);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1234, 811);
            this.Name = "VSTOOL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnOpenTo;
        private System.Windows.Forms.TextBox txtToPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnOpen;
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
        private System.Windows.Forms.TextBox txtNewTypeKey;
        private System.Windows.Forms.CheckBox Industry;
        private System.Windows.Forms.Button btncopyUIdll;
        private System.Windows.Forms.Button btncopydll;
        private System.Windows.Forms.Label label1;
        private Common.Implement.UI.MyTreeView treeView1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Common.Implement.UI.MyTreeView myTreeView1;
        private Common.Implement.UI.MyPanel scrollPanel;
        private Common.Implement.UI.MyTreeView myTreeView5;
        private System.Windows.Forms.CheckBox ModiCkb;
        private System.Windows.Forms.Button PkgOpenTo;
        private System.Windows.Forms.Label ToolsDescrition;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

