namespace Digiwin.ERP.XTEST.UI.Implement
{
    partial class ExportExcelForm
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
            this.digiwinLabelTextBox1 = new Digiwin.Common.UI.DigiwinLabelTextBox();
            this.digiwinButton1 = new Digiwin.Common.UI.DigiwinButton();
            this.digiwinButton2 = new Digiwin.Common.UI.DigiwinButton();
            this.digiwinButton3 = new Digiwin.Common.UI.DigiwinButton();
            this.SuspendLayout();
            // 
            // digiwinLabelTextBox1
            // 
            this.digiwinLabelTextBox1.Caption = "选择导出路径:";
            this.digiwinLabelTextBox1.CaptionSpace = 0;
            this.digiwinLabelTextBox1.Location = new System.Drawing.Point(104, 25);
            this.digiwinLabelTextBox1.Name = "digiwinLabelTextBox1";
            this.digiwinLabelTextBox1.Size = new System.Drawing.Size(257, 21);
            this.digiwinLabelTextBox1.TabIndex = 0;
            this.digiwinLabelTextBox1.ToolTipText = null;
            // 
            // digiwinButton1
            // 
            this.digiwinButton1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.digiwinButton1.Location = new System.Drawing.Point(358, 25);
            this.digiwinButton1.Name = "digiwinButton1";
            this.digiwinButton1.Size = new System.Drawing.Size(34, 21);
            this.digiwinButton1.TabIndex = 2;
            this.digiwinButton1.Text = "...";
            this.digiwinButton1.UseVisualStyleBackColor = true;
            this.digiwinButton1.Click += new System.EventHandler(this.digiwinButton1_Click);
            // 
            // digiwinButton2
            // 
            this.digiwinButton2.Location = new System.Drawing.Point(104, 65);
            this.digiwinButton2.Name = "digiwinButton2";
            this.digiwinButton2.Size = new System.Drawing.Size(75, 23);
            this.digiwinButton2.TabIndex = 3;
            this.digiwinButton2.Text = " 确认导出";
            this.digiwinButton2.UseVisualStyleBackColor = true;
            this.digiwinButton2.Click += new System.EventHandler(this.digiwinButton2_Click);
            // 
            // digiwinButton3
            // 
            this.digiwinButton3.Location = new System.Drawing.Point(317, 65);
            this.digiwinButton3.Name = "digiwinButton3";
            this.digiwinButton3.Size = new System.Drawing.Size(75, 23);
            this.digiwinButton3.TabIndex = 4;
            this.digiwinButton3.Text = "取消导出";
            this.digiwinButton3.UseVisualStyleBackColor = true;
            this.digiwinButton3.Click += new System.EventHandler(this.digiwinButton3_Click);
            // 
            // ExportExcelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 117);
            this.Controls.Add(this.digiwinButton3);
            this.Controls.Add(this.digiwinButton2);
            this.Controls.Add(this.digiwinButton1);
            this.Controls.Add(this.digiwinLabelTextBox1);
            this.Name = "ExportExcelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "导出EXCEL";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Digiwin.Common.UI.DigiwinLabelTextBox digiwinLabelTextBox1;
        private Digiwin.Common.UI.DigiwinButton digiwinButton1;
        private Digiwin.Common.UI.DigiwinButton digiwinButton2;
        private Digiwin.Common.UI.DigiwinButton digiwinButton3;
    }
}