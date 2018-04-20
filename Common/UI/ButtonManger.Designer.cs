namespace Common.Implement.UI
{
    partial class ButtonManger
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.StopBtn = new System.Windows.Forms.Button();
            this.StartBtn = new System.Windows.Forms.Button();
            this.CopyBtn = new System.Windows.Forms.Button();
            this.RefreshBtn = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.StartClientBtn = new System.Windows.Forms.Button();
            this.CopyClientBtn = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // StopBtn
            // 
            this.StopBtn.BackgroundImage = global::Common.Implement.Properties.Resources.Stop;
            this.StopBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.StopBtn.FlatAppearance.BorderSize = 0;
            this.StopBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StopBtn.Font = new System.Drawing.Font("宋体", 10F);
            this.StopBtn.ForeColor = System.Drawing.Color.DarkGreen;
            this.StopBtn.Location = new System.Drawing.Point(206, 0);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(14, 21);
            this.StopBtn.TabIndex = 43;
            this.StopBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StopBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.StopBtn, "关闭服务端，客户端");
            this.StopBtn.UseVisualStyleBackColor = true;
            this.StopBtn.Click += new System.EventHandler(this.Stop_Click);
            // 
            // StartBtn
            // 
            this.StartBtn.BackgroundImage = global::Common.Implement.Properties.Resources.Start;
            this.StartBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.StartBtn.FlatAppearance.BorderSize = 0;
            this.StartBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartBtn.Font = new System.Drawing.Font("宋体", 10F);
            this.StartBtn.ForeColor = System.Drawing.Color.DarkGreen;
            this.StartBtn.Location = new System.Drawing.Point(71, 0);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(14, 21);
            this.StartBtn.TabIndex = 42;
            this.StartBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StartBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.StartBtn, "启动服务端");
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // CopyBtn
            // 
            this.CopyBtn.BackgroundImage = global::Common.Implement.Properties.Resources.Copy;
            this.CopyBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CopyBtn.FlatAppearance.BorderSize = 0;
            this.CopyBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CopyBtn.Font = new System.Drawing.Font("宋体", 10F);
            this.CopyBtn.ForeColor = System.Drawing.Color.DarkGreen;
            this.CopyBtn.Location = new System.Drawing.Point(246, 0);
            this.CopyBtn.Name = "CopyBtn";
            this.CopyBtn.Size = new System.Drawing.Size(14, 21);
            this.CopyBtn.TabIndex = 44;
            this.CopyBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CopyBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.CopyBtn, "复制服务端");
            this.CopyBtn.UseVisualStyleBackColor = true;
            this.CopyBtn.Click += new System.EventHandler(this.CopyBtn_Click);
            // 
            // RefreshBtn
            // 
            this.RefreshBtn.BackgroundImage = global::Common.Implement.Properties.Resources.Refresh;
            this.RefreshBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.RefreshBtn.FlatAppearance.BorderSize = 0;
            this.RefreshBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RefreshBtn.Font = new System.Drawing.Font("宋体", 10F);
            this.RefreshBtn.ForeColor = System.Drawing.Color.DarkGreen;
            this.RefreshBtn.Location = new System.Drawing.Point(226, 0);
            this.RefreshBtn.Name = "RefreshBtn";
            this.RefreshBtn.Size = new System.Drawing.Size(14, 21);
            this.RefreshBtn.TabIndex = 45;
            this.RefreshBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.RefreshBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.RefreshBtn, "重启客户端服务端");
            this.RefreshBtn.UseVisualStyleBackColor = true;
            this.RefreshBtn.Click += new System.EventHandler(this.RefreshBtn_Click);
            // 
            // StartClientBtn
            // 
            this.StartClientBtn.BackgroundImage = global::Common.Implement.Properties.Resources.Start_Client;
            this.StartClientBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.StartClientBtn.FlatAppearance.BorderSize = 0;
            this.StartClientBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartClientBtn.Font = new System.Drawing.Font("宋体", 10F);
            this.StartClientBtn.ForeColor = System.Drawing.Color.DarkGreen;
            this.StartClientBtn.Location = new System.Drawing.Point(139, 0);
            this.StartClientBtn.Name = "StartClientBtn";
            this.StartClientBtn.Size = new System.Drawing.Size(14, 21);
            this.StartClientBtn.TabIndex = 47;
            this.StartClientBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StartClientBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.StartClientBtn, "启动客户端");
            this.StartClientBtn.UseVisualStyleBackColor = true;
            this.StartClientBtn.Click += new System.EventHandler(this.StartClientBtn_Click);
            // 
            // CopyClientBtn
            // 
            this.CopyClientBtn.BackgroundImage = global::Common.Implement.Properties.Resources.Copy_Client;
            this.CopyClientBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CopyClientBtn.FlatAppearance.BorderSize = 0;
            this.CopyClientBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CopyClientBtn.Font = new System.Drawing.Font("宋体", 10F);
            this.CopyClientBtn.ForeColor = System.Drawing.Color.DarkGreen;
            this.CopyClientBtn.Location = new System.Drawing.Point(266, 0);
            this.CopyClientBtn.Name = "CopyClientBtn";
            this.CopyClientBtn.Size = new System.Drawing.Size(14, 21);
            this.CopyClientBtn.TabIndex = 48;
            this.CopyClientBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CopyClientBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.CopyClientBtn, "复制客户端");
            this.CopyClientBtn.UseVisualStyleBackColor = true;
            this.CopyClientBtn.Click += new System.EventHandler(this.CopyClientBtn_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(0, 0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(65, 20);
            this.comboBox1.TabIndex = 46;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(91, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 49;
            this.label1.Text = "Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 50;
            this.label2.Text = "Client";
            // 
            // ButtonManger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CopyClientBtn);
            this.Controls.Add(this.StartClientBtn);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.RefreshBtn);
            this.Controls.Add(this.CopyBtn);
            this.Controls.Add(this.StopBtn);
            this.Controls.Add(this.StartBtn);
            this.Name = "ButtonManger";
            this.Size = new System.Drawing.Size(282, 20);
            this.Load += new System.EventHandler(this.ButtonManger_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StopBtn;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.Button CopyBtn;
        private System.Windows.Forms.Button RefreshBtn;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button StartClientBtn;
        private System.Windows.Forms.Button CopyClientBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
