namespace Common.Implement.UI
{
    partial class ModiName
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt01 = new System.Windows.Forms.TextBox();
            this.txt02 = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblattention = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BatchcomBox = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(93, 65);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "类名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(72, 95);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "方法名";
            // 
            // txt01
            // 
            this.txt01.Location = new System.Drawing.Point(156, 59);
            this.txt01.Margin = new System.Windows.Forms.Padding(4);
            this.txt01.Name = "txt01";
            this.txt01.Size = new System.Drawing.Size(272, 25);
            this.txt01.TabIndex = 1;
            this.txt01.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt01_KeyPress);
            // 
            // txt02
            // 
            this.txt02.Location = new System.Drawing.Point(156, 95);
            this.txt02.Margin = new System.Windows.Forms.Padding(4);
            this.txt02.Name = "txt02";
            this.txt02.Size = new System.Drawing.Size(272, 25);
            this.txt02.TabIndex = 2;
            this.txt02.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt01_KeyPress);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(723, 14);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 29);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblattention
            // 
            this.lblattention.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblattention.AutoSize = true;
            this.lblattention.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblattention.ForeColor = System.Drawing.Color.Red;
            this.lblattention.Location = new System.Drawing.Point(226, 46);
            this.lblattention.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblattention.Name = "lblattention";
            this.lblattention.Size = new System.Drawing.Size(0, 18);
            this.lblattention.TabIndex = 7;
            this.lblattention.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Menu;
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 219);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(827, 56);
            this.panel1.TabIndex = 8;
            // 
            // BatchcomBox
            // 
            this.BatchcomBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BatchcomBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BatchcomBox.FormattingEnabled = true;
            this.BatchcomBox.Location = new System.Drawing.Point(156, 29);
            this.BatchcomBox.Name = "BatchcomBox";
            this.BatchcomBox.Size = new System.Drawing.Size(272, 23);
            this.BatchcomBox.TabIndex = 1;
            this.BatchcomBox.Visible = false;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(156, 127);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(272, 23);
            this.comboBox2.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(60, 29);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "批次类型";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(80, 130);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "时机点";
            // 
            // ModiName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(827, 275);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.BatchcomBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblattention);
            this.Controls.Add(this.txt02);
            this.Controls.Add(this.txt01);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModiName";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "请输入";
            this.Load += new System.EventHandler(this.ModiName_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txt01;
        public System.Windows.Forms.TextBox txt02;
        public System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.Label lblattention;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox BatchcomBox;
        private System.Windows.Forms.ComboBox comboBox2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label4;
    }
}