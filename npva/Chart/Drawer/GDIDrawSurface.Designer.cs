namespace npva.Chart.Drawer
{
    partial class GDIDrawSurface
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GDIDrawSurface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "GDIDrawSurface";
            this.Size = new System.Drawing.Size(217, 224);
            this.Load += new System.EventHandler(this.GDIDrawSurface_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GDIDrawSurface_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GDIDrawSurface_MouseMove);
            this.Resize += new System.EventHandler(this.GDIDrawSurface_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
