
using Digiwin.Chun.Common.Views;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VSTOOL));
            this.BtnCreate = new System.Windows.Forms.Button();
            this.BtnOpenTo = new System.Windows.Forms.Button();
            this.txtToPath = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.TxtPkGpath = new System.Windows.Forms.TextBox();
            this.btnP = new System.Windows.Forms.Button();
            this.btnG = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MyToolbar = new ToolBarManger();
            this.ModiCkb = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNewTypeKey = new System.Windows.Forms.TextBox();
            this.Industry = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.MyTreeView5 = new MyTreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.ToolsDescrition = new System.Windows.Forms.Label();
            this.PkgOpenTo = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SplitContainer2 = new System.Windows.Forms.SplitContainer();
            this.MyTreeView1 = new MyTreeView();
            this.ScrollPanel = new MyPanel();
            this.TreeView1 = new MyTreeView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).BeginInit();
            this.SplitContainer1.Panel1.SuspendLayout();
            this.SplitContainer1.Panel2.SuspendLayout();
            this.SplitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2)).BeginInit();
            this.SplitContainer2.Panel1.SuspendLayout();
            this.SplitContainer2.Panel2.SuspendLayout();
            this.SplitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnCreate
            // 
            this.BtnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCreate.BackColor = System.Drawing.SystemColors.Window;
            this.BtnCreate.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnCreate.ForeColor = System.Drawing.Color.Black;
            this.BtnCreate.Location = new System.Drawing.Point(868, 42);
            this.BtnCreate.Name = "BtnCreate";
            this.BtnCreate.Size = new System.Drawing.Size(75, 24);
            this.BtnCreate.TabIndex = 6;
            this.BtnCreate.Text = "生成";
            this.BtnCreate.UseVisualStyleBackColor = true;
            //this.BtnCreate.Click += new System.EventHandler(this.BtnCreate_Click);
            // 
            // BtnOpenTo
            // 
            this.BtnOpenTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOpenTo.FlatAppearance.BorderSize = 0;
            this.BtnOpenTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnOpenTo.Font = new System.Drawing.Font("新宋体", 9.5F);
            this.BtnOpenTo.ForeColor = System.Drawing.Color.DarkCyan;
            this.BtnOpenTo.Location = new System.Drawing.Point(754, 42);
            this.BtnOpenTo.Name = "BtnOpenTo";
            this.BtnOpenTo.Size = new System.Drawing.Size(38, 20);
            this.BtnOpenTo.TabIndex = 9;
            this.BtnOpenTo.Text = "○";
            this.BtnOpenTo.UseVisualStyleBackColor = true;
            //this.BtnOpenTo.Click += new System.EventHandler(this.BtnOpenTo_Click);
            // 
            // txtToPath
            // 
            this.txtToPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtToPath.Location = new System.Drawing.Point(21, 42);
            this.txtToPath.Name = "txtToPath";
            this.txtToPath.Size = new System.Drawing.Size(728, 21);
            this.txtToPath.TabIndex = 10;
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "请选择所在目录";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClear.ForeColor = System.Drawing.Color.Black;
            this.btnClear.Location = new System.Drawing.Point(868, 102);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 24);
            this.btnClear.TabIndex = 15;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            //this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpen.ForeColor = System.Drawing.Color.Black;
            this.btnOpen.Location = new System.Drawing.Point(868, 72);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 24);
            this.btnOpen.TabIndex = 19;
            this.btnOpen.Text = "OPEN";
            this.btnOpen.UseVisualStyleBackColor = true;
            //this.btnOpen.Click += new System.EventHandler(this.BtnOpen_Click);
            // 
            // TxtPkGpath
            // 
            this.TxtPkGpath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtPkGpath.Location = new System.Drawing.Point(21, 95);
            this.TxtPkGpath.Name = "TxtPkGpath";
            this.TxtPkGpath.Size = new System.Drawing.Size(728, 21);
            this.TxtPkGpath.TabIndex = 27;
            // 
            // btnP
            // 
            this.btnP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnP.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnP.FlatAppearance.BorderSize = 0;
            this.btnP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnP.ForeColor = System.Drawing.Color.YellowGreen;
            this.btnP.Location = new System.Drawing.Point(794, 42);
            this.btnP.Name = "btnP";
            this.btnP.Size = new System.Drawing.Size(25, 20);
            this.btnP.TabIndex = 30;
            this.btnP.Text = "◎";
            this.btnP.UseVisualStyleBackColor = true;
            this.btnP.Visible = false;
            //this.btnP.Click += new System.EventHandler(this.BtnP_Click);
            // 
            // btnG
            // 
            this.btnG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnG.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnG.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnG.FlatAppearance.BorderSize = 0;
            this.btnG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnG.ForeColor = System.Drawing.Color.YellowGreen;
            this.btnG.Location = new System.Drawing.Point(794, 95);
            this.btnG.Name = "btnG";
            this.btnG.Size = new System.Drawing.Size(25, 20);
            this.btnG.TabIndex = 31;
            this.btnG.Text = "◎";
            this.btnG.UseVisualStyleBackColor = true;
            this.btnG.Visible = false;
            //this.btnG.Click += new System.EventHandler(this.BtnG_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.MyToolbar);
            this.panel1.Controls.Add(this.ModiCkb);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtNewTypeKey);
            this.panel1.Controls.Add(this.Industry);
            this.panel1.Location = new System.Drawing.Point(-6, -10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(976, 84);
            this.panel1.TabIndex = 32;
            // 
            // MyToolbar
            // 
            this.MyToolbar.BackColor = System.Drawing.SystemColors.Window;
            this.MyToolbar.Location = new System.Drawing.Point(241, 56);
            this.MyToolbar.Name = "MyToolbar";
            this.MyToolbar.Size = new System.Drawing.Size(286, 20);
            this.MyToolbar.TabIndex = 42;
            this.MyToolbar.Toolpar = null;
            // 
            // ModiCkb
            // 
            this.ModiCkb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ModiCkb.AutoSize = true;
            this.ModiCkb.Location = new System.Drawing.Point(846, 35);
            this.ModiCkb.Name = "ModiCkb";
            this.ModiCkb.Size = new System.Drawing.Size(48, 16);
            this.ModiCkb.TabIndex = 39;
            this.ModiCkb.Text = "借用";
            this.ModiCkb.UseVisualStyleBackColor = true;
            //this.ModiCkb.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 34.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.BlueViolet;
            this.label2.Location = new System.Drawing.Point(6, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 62);
            this.label2.TabIndex = 0;
            this.label2.Text = "VSTool";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtNewTypeKey
            // 
            this.txtNewTypeKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewTypeKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewTypeKey.Location = new System.Drawing.Point(241, 29);
            this.txtNewTypeKey.Name = "txtNewTypeKey";
            this.txtNewTypeKey.Size = new System.Drawing.Size(538, 21);
            this.txtNewTypeKey.TabIndex = 2;
            //this.txtNewTypeKey.TextChanged += TxtNewTypeKey_TextChanged;
            // 
            // Industry
            // 
            this.Industry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Industry.AutoSize = true;
            this.Industry.Location = new System.Drawing.Point(785, 35);
            this.Industry.Name = "Industry";
            this.Industry.Size = new System.Drawing.Size(60, 16);
            this.Industry.TabIndex = 28;
            this.Industry.Text = "行业包";
            this.Industry.UseVisualStyleBackColor = true;
            //this.Industry.CheckedChanged += new System.EventHandler(this.Industry_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Window;
            this.panel3.Controls.Add(this.MyTreeView5);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(232, 467);
            this.panel3.TabIndex = 34;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel3_Paint);
            // 
            // MyTreeView5
            // 
            this.MyTreeView5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MyTreeView5.BackColor = System.Drawing.Color.White;
            this.MyTreeView5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MyTreeView5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MyTreeView5.DescriptionColor = System.Drawing.Color.Gray;
            this.MyTreeView5.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.MyTreeView5.Font = new System.Drawing.Font("新宋体", 11F);
            this.MyTreeView5.ForeColor = System.Drawing.Color.Black;
            this.MyTreeView5.FullRowSelect = true;
            this.MyTreeView5.HotTracking = true;
            this.MyTreeView5.ImeMode = System.Windows.Forms.ImeMode.On;
            this.MyTreeView5.IsCard = false;
            this.MyTreeView5.ItemHeight = 25;
            this.MyTreeView5.Location = new System.Drawing.Point(9, 23);
            this.MyTreeView5.Name = "MyTreeView5";
            this.MyTreeView5.NodeCheckBoxSize = new System.Drawing.Size(20, 20);
            this.MyTreeView5.NodeFont = null;
            this.MyTreeView5.NodeImageSize = new System.Drawing.Size(40, 40);
            this.MyTreeView5.PaddingSetting = new System.Drawing.Point(0, 4);
            this.MyTreeView5.ShowDescription = false;
            this.MyTreeView5.ShowLines = false;
            this.MyTreeView5.ShowPlusMinus = false;
            this.MyTreeView5.Size = new System.Drawing.Size(216, 444);
            this.MyTreeView5.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
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
            this.panel4.Controls.Add(this.BtnCreate);
            this.panel4.Controls.Add(this.btnP);
            this.panel4.Controls.Add(this.BtnOpenTo);
            this.panel4.Controls.Add(this.TxtPkGpath);
            this.panel4.Controls.Add(this.btnOpen);
            this.panel4.Controls.Add(this.btnClear);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 538);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(968, 156);
            this.panel4.TabIndex = 35;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel1.Location = new System.Drawing.Point(449, 130);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(31, 15);
            this.linkLabel1.TabIndex = 34;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "帮助";
            //this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            // 
            // ToolsDescrition
            // 
            this.ToolsDescrition.AutoSize = true;
            this.ToolsDescrition.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ToolsDescrition.ForeColor = System.Drawing.Color.DimGray;
            this.ToolsDescrition.Location = new System.Drawing.Point(19, 130);
            this.ToolsDescrition.Name = "ToolsDescrition";
            this.ToolsDescrition.Size = new System.Drawing.Size(427, 15);
            this.ToolsDescrition.TabIndex = 16;
            this.ToolsDescrition.Text = "继续使用表示你同意本工具的使用协议，并自行承担由此带来的风险，更多点击";
            // 
            // PkgOpenTo
            // 
            this.PkgOpenTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PkgOpenTo.FlatAppearance.BorderSize = 0;
            this.PkgOpenTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PkgOpenTo.Font = new System.Drawing.Font("新宋体", 9.5F);
            this.PkgOpenTo.ForeColor = System.Drawing.Color.DarkCyan;
            this.PkgOpenTo.Location = new System.Drawing.Point(754, 95);
            this.PkgOpenTo.Name = "PkgOpenTo";
            this.PkgOpenTo.Size = new System.Drawing.Size(38, 20);
            this.PkgOpenTo.TabIndex = 33;
            this.PkgOpenTo.Text = "○";
            this.PkgOpenTo.UseVisualStyleBackColor = true;
            //this.PkgOpenTo.Click += new System.EventHandler(this.BtnOpenTo_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(18, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 14);
            this.label4.TabIndex = 32;
            this.label4.Text = "借用目录";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(18, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "创建到";
            // 
            // SplitContainer1
            // 
            this.SplitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.SplitContainer1.IsSplitterFixed = true;
            this.SplitContainer1.Location = new System.Drawing.Point(-6, 73);
            this.SplitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.SplitContainer1.Name = "SplitContainer1";
            // 
            // SplitContainer1.Panel1
            // 
            this.SplitContainer1.Panel1.Controls.Add(this.panel2);
            this.SplitContainer1.Panel1MinSize = 600;
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.panel3);
            this.SplitContainer1.Size = new System.Drawing.Size(982, 467);
            this.SplitContainer1.SplitterDistance = 749;
            this.SplitContainer1.SplitterWidth = 1;
            this.SplitContainer1.TabIndex = 36;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.SplitContainer2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(749, 467);
            this.panel2.TabIndex = 33;
            // 
            // SplitContainer2
            // 
            this.SplitContainer2.BackColor = System.Drawing.SystemColors.Window;
            this.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.SplitContainer2.IsSplitterFixed = true;
            this.SplitContainer2.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer2.Name = "SplitContainer2";
            // 
            // SplitContainer2.Panel1
            // 
            this.SplitContainer2.Panel1.Controls.Add(this.MyTreeView1);
            // 
            // SplitContainer2.Panel2
            // 
            this.SplitContainer2.Panel2.Controls.Add(this.ScrollPanel);
            this.SplitContainer2.Panel2.Controls.Add(this.TreeView1);
            this.SplitContainer2.Size = new System.Drawing.Size(749, 467);
            this.SplitContainer2.SplitterDistance = 245;
            this.SplitContainer2.SplitterWidth = 1;
            this.SplitContainer2.TabIndex = 14;
            // 
            // MyTreeView1
            // 
            this.MyTreeView1.AllowDrop = true;
            this.MyTreeView1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.MyTreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MyTreeView1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MyTreeView1.DescriptionColor = System.Drawing.Color.DimGray;
            this.MyTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MyTreeView1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.MyTreeView1.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MyTreeView1.ForeColor = System.Drawing.Color.Black;
            this.MyTreeView1.FullRowSelect = true;
            this.MyTreeView1.HideSelection = false;
            this.MyTreeView1.HotTracking = true;
            this.MyTreeView1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.MyTreeView1.IsCard = false;
            this.MyTreeView1.ItemHeight = 50;
            this.MyTreeView1.Location = new System.Drawing.Point(0, 0);
            this.MyTreeView1.Name = "MyTreeView1";
            this.MyTreeView1.NodeCheckBoxSize = new System.Drawing.Size(20, 20);
            this.MyTreeView1.NodeFont = null;
            this.MyTreeView1.NodeImageSize = new System.Drawing.Size(40, 40);
            this.MyTreeView1.PaddingSetting = new System.Drawing.Point(0, 4);
            this.MyTreeView1.ShowDescription = true;
            this.MyTreeView1.ShowLines = false;
            this.MyTreeView1.ShowPlusMinus = false;
            this.MyTreeView1.Size = new System.Drawing.Size(245, 467);
            this.MyTreeView1.TabIndex = 14;
            //this.MyTreeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.MyTreeView1_NodeMouseClick);
            // 
            // ScrollPanel
            // 
            this.ScrollPanel.AllowDrop = true;
            this.ScrollPanel.AutoScroll = true;
            this.ScrollPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.ScrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScrollPanel.Location = new System.Drawing.Point(0, 0);
            this.ScrollPanel.Margin = new System.Windows.Forms.Padding(2);
            this.ScrollPanel.Name = "ScrollPanel";
            this.ScrollPanel.Size = new System.Drawing.Size(503, 467);
            this.ScrollPanel.TabIndex = 35;
            //this.ScrollPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.ScrollPanel_DragDrop);
            //this.ScrollPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.ScrollPanel_DragEnter);
            // 
            // TreeView1
            // 
            this.TreeView1.BackColor = System.Drawing.SystemColors.Window;
            this.TreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TreeView1.DescriptionColor = System.Drawing.Color.Empty;
            this.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeView1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.TreeView1.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TreeView1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.TreeView1.FullRowSelect = true;
            this.TreeView1.HideSelection = false;
            this.TreeView1.HotTracking = true;
            this.TreeView1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.TreeView1.IsCard = false;
            this.TreeView1.ItemHeight = 25;
            this.TreeView1.Location = new System.Drawing.Point(0, 0);
            this.TreeView1.Name = "TreeView1";
            this.TreeView1.NodeCheckBoxSize = new System.Drawing.Size(20, 20);
            this.TreeView1.NodeFont = null;
            this.TreeView1.NodeImageSize = new System.Drawing.Size(40, 40);
            this.TreeView1.PaddingSetting = new System.Drawing.Point(0, 0);
            this.TreeView1.ShowDescription = false;
            this.TreeView1.ShowLines = false;
            this.TreeView1.ShowPlusMinus = false;
            this.TreeView1.Size = new System.Drawing.Size(503, 467);
            this.TreeView1.TabIndex = 13;
            this.TreeView1.Visible = false;
            // 
            // VSTOOL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(968, 694);
            this.Controls.Add(this.SplitContainer1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(930, 657);
            this.Name = "VSTOOL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            //this.Load += new System.EventHandler(this.VSTOOL_Load);
            //this.ClientSizeChanged += new System.EventHandler(this.VSTOOL_ClientSizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.SplitContainer1.Panel1.ResumeLayout(false);
            this.SplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).EndInit();
            this.SplitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.SplitContainer2.Panel1.ResumeLayout(false);
            this.SplitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2)).EndInit();
            this.SplitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnCreate;
        private System.Windows.Forms.Button BtnOpenTo;
        private System.Windows.Forms.TextBox txtToPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox TxtPkGpath;
        private System.Windows.Forms.Button btnP;
        private System.Windows.Forms.Button btnG;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SplitContainer SplitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtNewTypeKey;
        private System.Windows.Forms.CheckBox Industry;
        private System.Windows.Forms.Label label1;
        private MyTreeView TreeView1;
        private System.Windows.Forms.SplitContainer SplitContainer2;
        private MyTreeView MyTreeView1;
        private MyPanel ScrollPanel;
        private MyTreeView MyTreeView5;
        private System.Windows.Forms.CheckBox ModiCkb;
        private System.Windows.Forms.Button PkgOpenTo;
        private System.Windows.Forms.Label ToolsDescrition;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private ToolBarManger MyToolbar;
    }
}

