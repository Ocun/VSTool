namespace Digiwin.Chun.Views
{
    partial class ModiPkgForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.TypeKeyText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpenTo = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNewTypeKey = new System.Windows.Forms.TextBox();
            this.FromServer = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.CustomerText = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Menu;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 222);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(813, 56);
            this.panel1.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(709, 12);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 29);
            this.button1.TabIndex = 1;
            this.button1.Text = "整支借用";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(616, 12);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(85, 29);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "修改代码";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // TypeKeyText
            // 
            this.TypeKeyText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TypeKeyText.Location = new System.Drawing.Point(149, 78);
            this.TypeKeyText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TypeKeyText.Name = "TypeKeyText";
            this.TypeKeyText.Size = new System.Drawing.Size(557, 25);
            this.TypeKeyText.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(13, 86);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "借用TypeKey";
            // 
            // btnOpenTo
            // 
            this.btnOpenTo.FlatAppearance.BorderSize = 0;
            this.btnOpenTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenTo.ForeColor = System.Drawing.Color.DarkCyan;
            this.btnOpenTo.Location = new System.Drawing.Point(716, 78);
            this.btnOpenTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOpenTo.Name = "btnOpenTo";
            this.btnOpenTo.Size = new System.Drawing.Size(61, 25);
            this.btnOpenTo.TabIndex = 12;
            this.btnOpenTo.Text = "○";
            this.btnOpenTo.UseVisualStyleBackColor = true;
            this.btnOpenTo.Click += new System.EventHandler(this.BtnOpenTo_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(57, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "TypeKey";
            // 
            // txtNewTypeKey
            // 
            this.txtNewTypeKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtNewTypeKey.Location = new System.Drawing.Point(149, 42);
            this.txtNewTypeKey.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNewTypeKey.Name = "txtNewTypeKey";
            this.txtNewTypeKey.Size = new System.Drawing.Size(557, 25);
            this.txtNewTypeKey.TabIndex = 14;
            // 
            // FromServer
            // 
            this.FromServer.AutoSize = true;
            this.FromServer.Location = new System.Drawing.Point(149, 144);
            this.FromServer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FromServer.Name = "FromServer";
            this.FromServer.Size = new System.Drawing.Size(90, 19);
            this.FromServer.TabIndex = 15;
            this.FromServer.Text = "15服务器";
            this.FromServer.UseVisualStyleBackColor = true;
            this.FromServer.CheckedChanged += new System.EventHandler(this.FromServer_CheckedChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.CustomerText);
            this.splitContainer1.Panel1.Controls.Add(this.txtNewTypeKey);
            this.splitContainer1.Panel1.Controls.Add(this.FromServer);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.TypeKeyText);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.btnOpenTo);
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(813, 222);
            this.splitContainer1.SplitterDistance = 85;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 16;
            // 
            // button2
            // 
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("宋体", 8F);
            this.button2.Location = new System.Drawing.Point(716, 112);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(44, 25);
            this.button2.TabIndex = 18;
            this.button2.Text = " …";
            this.button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(45, 118);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "借用客户";
            // 
            // CustomerText
            // 
            this.CustomerText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.CustomerText.Location = new System.Drawing.Point(149, 111);
            this.CustomerText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CustomerText.Name = "CustomerText";
            this.CustomerText.Size = new System.Drawing.Size(557, 25);
            this.CustomerText.TabIndex = 16;
            this.CustomerText.TextChanged += new System.EventHandler(this.CustomerText_TextChanged);
            // 
            // ModiPkgForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(813, 278);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModiPkgForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "借用TypeKey（代码）";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ModiPkgForm_FormClosed);
            this.Load += new System.EventHandler(this.ModiPKGForm_Load);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox TypeKeyText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpenTo;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNewTypeKey;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox FromServer;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox CustomerText;
        private System.Windows.Forms.Button button2;
    }
}