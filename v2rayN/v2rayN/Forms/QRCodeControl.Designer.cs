namespace v2rayN.Forms
{
    partial class QRCodeControl
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.picQRCode = new System.Windows.Forms.PictureBox();
            this.chkShow = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.picQRCode)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUrl
            // 
            this.txtUrl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtUrl.Location = new System.Drawing.Point(0, 371);
            this.txtUrl.Multiline = true;
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.ReadOnly = true;
            this.txtUrl.Size = new System.Drawing.Size(356, 70);
            this.txtUrl.TabIndex = 0;
            // 
            // picQRCode
            // 
            this.picQRCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picQRCode.Location = new System.Drawing.Point(0, 16);
            this.picQRCode.Name = "picQRCode";
            this.picQRCode.Size = new System.Drawing.Size(356, 355);
            this.picQRCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picQRCode.TabIndex = 24;
            this.picQRCode.TabStop = false;
            // 
            // chkShow
            // 
            this.chkShow.AutoSize = true;
            this.chkShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkShow.Location = new System.Drawing.Point(0, 0);
            this.chkShow.Name = "chkShow";
            this.chkShow.Size = new System.Drawing.Size(356, 16);
            this.chkShow.TabIndex = 25;
            this.chkShow.Text = "显示分享内容";
            this.chkShow.UseVisualStyleBackColor = true;
            this.chkShow.CheckedChanged += new System.EventHandler(this.chkShow_CheckedChanged);
            // 
            // QRCodeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picQRCode);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.chkShow);
            this.Name = "QRCodeControl";
            this.Size = new System.Drawing.Size(356, 441);
            this.Load += new System.EventHandler(this.QRCodeControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picQRCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.PictureBox picQRCode;
        private System.Windows.Forms.CheckBox chkShow;
    }
}
