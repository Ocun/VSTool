
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
            this.ToPath = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.PkgPath = new System.Windows.Forms.TextBox();
            this.btnP = new System.Windows.Forms.Button();
            this.btnG = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.HeaderPanel = new Digiwin.Chun.Common.Views.MyPanel();
            this.OpenCode = new System.Windows.Forms.Button();
            this.MyToolbar = new Digiwin.Chun.Common.Views.ToolBarManger();
            this.ModiCkb = new System.Windows.Forms.CheckBox();
            this.txtNewTypeKey = new System.Windows.Forms.TextBox();
            this.Industry = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.RighteTreeView = new Digiwin.Chun.Common.Views.MyTreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.Version = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.ToolsDescrition = new System.Windows.Forms.Label();
            this.PkgOpenTo = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SplitContainer2 = new System.Windows.Forms.SplitContainer();
            this.NavTreeView = new Digiwin.Chun.Common.Views.MyTreeView();
            this.ScrollPanel = new Digiwin.Chun.Common.Views.MyPanel();
            this.TreeView1 = new Digiwin.Chun.Common.Views.MyTreeView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.HeaderPanel.SuspendLayout();
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
            this.BtnOpenTo.Size = new System.Drawing.Size(27, 20);
            this.BtnOpenTo.TabIndex = 9;
            this.BtnOpenTo.Text = "○";
            this.BtnOpenTo.UseVisualStyleBackColor = true;
            // 
            // ToPath
            // 
            this.ToPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ToPath.Location = new System.Drawing.Point(21, 42);
            this.ToPath.Name = "ToPath";
            this.ToPath.Size = new System.Drawing.Size(728, 21);
            this.ToPath.TabIndex = 10;
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
            // 
            // PkgPath
            // 
            this.PkgPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PkgPath.Location = new System.Drawing.Point(21, 95);
            this.PkgPath.Name = "PkgPath";
            this.PkgPath.Size = new System.Drawing.Size(728, 21);
            this.PkgPath.TabIndex = 27;
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
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.HeaderPanel);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(-6, -10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(976, 84);
            this.panel1.TabIndex = 32;
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HeaderPanel.Controls.Add(this.OpenCode);
            this.HeaderPanel.Controls.Add(this.MyToolbar);
            this.HeaderPanel.Controls.Add(this.ModiCkb);
            this.HeaderPanel.Controls.Add(this.txtNewTypeKey);
            this.HeaderPanel.Controls.Add(this.Industry);
            this.HeaderPanel.Location = new System.Drawing.Point(243, 15);
            this.HeaderPanel.Margin = new System.Windows.Forms.Padding(2);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(737, 66);
            this.HeaderPanel.TabIndex = 43;
            // 
            // OpenCode
            // 
            this.OpenCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenCode.FlatAppearance.BorderSize = 0;
            this.OpenCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenCode.Font = new System.Drawing.Font("新宋体", 9.5F);
            this.OpenCode.ForeColor = System.Drawing.Color.DarkCyan;
            this.OpenCode.Location = new System.Drawing.Point(572, 16);
            this.OpenCode.Name = "OpenCode";
            this.OpenCode.Size = new System.Drawing.Size(28, 20);
            this.OpenCode.TabIndex = 36;
            this.OpenCode.Text = "○";
            this.OpenCode.UseVisualStyleBackColor = true;
            // 
            // MyToolbar
            // 
            this.MyToolbar.BackColor = System.Drawing.SystemColors.Window;
            this.MyToolbar.Location = new System.Drawing.Point(0, 42);
            this.MyToolbar.Margin = new System.Windows.Forms.Padding(4);
            this.MyToolbar.Name = "MyToolbar";
            this.MyToolbar.Size = new System.Drawing.Size(286, 20);
            this.MyToolbar.TabIndex = 42;
            // 
            // ModiCkb
            // 
            this.ModiCkb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ModiCkb.AutoSize = true;
            this.ModiCkb.Location = new System.Drawing.Point(523, 42);
            this.ModiCkb.Name = "ModiCkb";
            this.ModiCkb.Size = new System.Drawing.Size(48, 16);
            this.ModiCkb.TabIndex = 39;
            this.ModiCkb.Text = "借用";
            this.ModiCkb.UseVisualStyleBackColor = true;
            // 
            // txtNewTypeKey
            // 
            this.txtNewTypeKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewTypeKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewTypeKey.Location = new System.Drawing.Point(0, 15);
            this.txtNewTypeKey.Name = "txtNewTypeKey";
            this.txtNewTypeKey.Size = new System.Drawing.Size(566, 21);
            this.txtNewTypeKey.TabIndex = 2;
            // 
            // Industry
            // 
            this.Industry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Industry.AutoSize = true;
            this.Industry.Location = new System.Drawing.Point(461, 42);
            this.Industry.Name = "Industry";
            this.Industry.Size = new System.Drawing.Size(60, 16);
            this.Industry.TabIndex = 28;
            this.Industry.Text = "行业包";
            this.Industry.UseVisualStyleBackColor = true;
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
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Window;
            this.panel3.Controls.Add(this.RighteTreeView);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(187, 467);
            this.panel3.TabIndex = 34;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel3_Paint);
            // 
            // RighteTreeView
            // 
            this.RighteTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RighteTreeView.BackColor = System.Drawing.Color.White;
            this.RighteTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RighteTreeView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RighteTreeView.DelImage = ((System.Drawing.Image)(resources.GetObject("RighteTreeView.DelImage")));
            this.RighteTreeView.DescriptionColor = System.Drawing.Color.Gray;
            this.RighteTreeView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.RighteTreeView.Font = new System.Drawing.Font("新宋体", 11F);
            this.RighteTreeView.ForeColor = System.Drawing.Color.Black;
            this.RighteTreeView.FullRowSelect = true;
            this.RighteTreeView.HotTracking = true;
            this.RighteTreeView.ImeMode = System.Windows.Forms.ImeMode.On;
            this.RighteTreeView.IsCard = false;
            this.RighteTreeView.ItemHeight = 25;
            this.RighteTreeView.Location = new System.Drawing.Point(9, 23);
            this.RighteTreeView.Name = "RighteTreeView";
            this.RighteTreeView.NodeCheckBoxSize = new System.Drawing.Size(20, 20);
            this.RighteTreeView.NodeFont = null;
            this.RighteTreeView.NodeImageSize = new System.Drawing.Size(40, 40);
            this.RighteTreeView.PaddingSetting = new System.Drawing.Point(0, 4);
            this.RighteTreeView.ShowDescription = false;
            this.RighteTreeView.ShowLines = false;
            this.RighteTreeView.ShowPlusMinus = false;
            this.RighteTreeView.Size = new System.Drawing.Size(172, 444);
            this.RighteTreeView.TabIndex = 15;
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
            this.panel4.Controls.Add(this.linkLabel2);
            this.panel4.Controls.Add(this.Version);
            this.panel4.Controls.Add(this.linkLabel1);
            this.panel4.Controls.Add(this.ToolsDescrition);
            this.panel4.Controls.Add(this.PkgOpenTo);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.ToPath);
            this.panel4.Controls.Add(this.btnG);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.BtnCreate);
            this.panel4.Controls.Add(this.btnP);
            this.panel4.Controls.Add(this.BtnOpenTo);
            this.panel4.Controls.Add(this.PkgPath);
            this.panel4.Controls.Add(this.btnOpen);
            this.panel4.Controls.Add(this.btnClear);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 538);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(968, 156);
            this.panel4.TabIndex = 35;
            // 
            // linkLabel2
            // 
            this.linkLabel2.ActiveLinkColor = System.Drawing.Color.DarkRed;
            this.linkLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel2.LinkColor = System.Drawing.Color.Red;
            this.linkLabel2.Location = new System.Drawing.Point(816, 132);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(43, 15);
            this.linkLabel2.TabIndex = 36;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "有更新";
            this.linkLabel2.Visible = false;
            // 
            // Version
            // 
            this.Version.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Version.AutoSize = true;
            this.Version.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Version.ForeColor = System.Drawing.Color.DimGray;
            this.Version.Location = new System.Drawing.Point(865, 132);
            this.Version.Name = "Version";
            this.Version.Size = new System.Drawing.Size(49, 15);
            this.Version.TabIndex = 35;
            this.Version.Text = "1.0.0.1.0";
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
            this.PkgOpenTo.Size = new System.Drawing.Size(27, 20);
            this.PkgOpenTo.TabIndex = 33;
            this.PkgOpenTo.Text = "○";
            this.PkgOpenTo.UseVisualStyleBackColor = true;
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
            this.SplitContainer1.SplitterDistance = 794;
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
            this.panel2.Size = new System.Drawing.Size(794, 467);
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
            this.SplitContainer2.Panel1.Controls.Add(this.NavTreeView);
            // 
            // SplitContainer2.Panel2
            // 
            this.SplitContainer2.Panel2.Controls.Add(this.ScrollPanel);
            this.SplitContainer2.Panel2.Controls.Add(this.TreeView1);
            this.SplitContainer2.Size = new System.Drawing.Size(794, 467);
            this.SplitContainer2.SplitterDistance = 245;
            this.SplitContainer2.SplitterWidth = 1;
            this.SplitContainer2.TabIndex = 14;
            // 
            // NavTreeView
            // 
            this.NavTreeView.AllowDrop = true;
            this.NavTreeView.BackColor = System.Drawing.Color.WhiteSmoke;
            this.NavTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NavTreeView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.NavTreeView.DelImage = ((System.Drawing.Image)(resources.GetObject("NavTreeView.DelImage")));
            this.NavTreeView.DescriptionColor = System.Drawing.Color.DimGray;
            this.NavTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NavTreeView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.NavTreeView.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NavTreeView.ForeColor = System.Drawing.Color.Black;
            this.NavTreeView.FullRowSelect = true;
            this.NavTreeView.HideSelection = false;
            this.NavTreeView.HotTracking = true;
            this.NavTreeView.ImeMode = System.Windows.Forms.ImeMode.On;
            this.NavTreeView.IsCard = false;
            this.NavTreeView.ItemHeight = 50;
            this.NavTreeView.Location = new System.Drawing.Point(0, 0);
            this.NavTreeView.Name = "NavTreeView";
            this.NavTreeView.NodeCheckBoxSize = new System.Drawing.Size(20, 20);
            this.NavTreeView.NodeFont = null;
            this.NavTreeView.NodeImageSize = new System.Drawing.Size(40, 40);
            this.NavTreeView.PaddingSetting = new System.Drawing.Point(0, 4);
            this.NavTreeView.ShowDescription = true;
            this.NavTreeView.ShowLines = false;
            this.NavTreeView.ShowPlusMinus = false;
            this.NavTreeView.Size = new System.Drawing.Size(245, 467);
            this.NavTreeView.TabIndex = 14;
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
            this.ScrollPanel.Size = new System.Drawing.Size(548, 467);
            this.ScrollPanel.TabIndex = 35;
            // 
            // TreeView1
            // 
            this.TreeView1.BackColor = System.Drawing.SystemColors.Window;
            this.TreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TreeView1.DelImage = ((System.Drawing.Image)(resources.GetObject("TreeView1.DelImage")));
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
            this.TreeView1.Size = new System.Drawing.Size(548, 467);
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
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(930, 655);
            this.Name = "VSTOOL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.HeaderPanel.ResumeLayout(false);
            this.HeaderPanel.PerformLayout();
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
        private System.Windows.Forms.TextBox ToPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox PkgPath;
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
        private MyTreeView NavTreeView;
        private MyPanel ScrollPanel;
        private MyTreeView RighteTreeView;
        private System.Windows.Forms.CheckBox ModiCkb;
        private System.Windows.Forms.Button PkgOpenTo;
        private System.Windows.Forms.Label ToolsDescrition;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private ToolBarManger MyToolbar;
        private MyPanel HeaderPanel;
        private System.Windows.Forms.Label Version;
        private System.Windows.Forms.Button OpenCode;
        private System.Windows.Forms.LinkLabel linkLabel2;
    }
}

