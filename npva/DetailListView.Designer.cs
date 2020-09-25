namespace npva
{
    partial class DetailListView
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
            this.lvDisplay = new npva.DoubleBufferedListView();
            this.chDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPCPv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMobilePv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSmpPv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTotalPv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chVote = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chBookmark = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chImpression = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chReview = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSeries = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDaily = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chWeekly = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMonthly = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chQuater = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chYear = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chGlobalPt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvDisplay
            // 
            this.lvDisplay.AllowColumnReorder = true;
            this.lvDisplay.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDate,
            this.chPCPv,
            this.chMobilePv,
            this.chSmpPv,
            this.chTotalPv,
            this.chVote,
            this.chBookmark,
            this.chImpression,
            this.chReview,
            this.chDaily,
            this.chWeekly,
            this.chMonthly,
            this.chQuater,
            this.chYear,
            this.chGlobalPt,
            this.chSeries,
            this.chSize});
            this.lvDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDisplay.FullRowSelect = true;
            this.lvDisplay.GridLines = true;
            this.lvDisplay.HideSelection = false;
            this.lvDisplay.Location = new System.Drawing.Point(0, 0);
            this.lvDisplay.Name = "lvDisplay";
            this.lvDisplay.Size = new System.Drawing.Size(513, 386);
            this.lvDisplay.TabIndex = 0;
            this.lvDisplay.UseCompatibleStateImageBehavior = false;
            this.lvDisplay.View = System.Windows.Forms.View.Details;
            // 
            // chDate
            // 
            this.chDate.Text = "Date";
            this.chDate.Width = 80;
            // 
            // chPCPv
            // 
            this.chPCPv.Text = "PC";
            // 
            // chMobilePv
            // 
            this.chMobilePv.Text = "携帯";
            // 
            // chSmpPv
            // 
            this.chSmpPv.Text = "スマホ";
            // 
            // chTotalPv
            // 
            this.chTotalPv.Text = "合計Pv";
            // 
            // chVote
            // 
            this.chVote.Text = "評価";
            // 
            // chBookmark
            // 
            this.chBookmark.Text = "ブクマ";
            // 
            // chImpression
            // 
            this.chImpression.Text = "感想";
            // 
            // chReview
            // 
            this.chReview.Text = "レビュー";
            // 
            // chSeries
            // 
            this.chSeries.Text = "話数";
            // 
            // chSize
            // 
            this.chSize.Text = "文字数";
            // 
            // chDaily
            // 
            this.chDaily.Text = "日pt";
            // 
            // chWeekly
            // 
            this.chWeekly.Text = "週pt";
            // 
            // chMonthly
            // 
            this.chMonthly.Text = "月pt";
            // 
            // chQuater
            // 
            this.chQuater.Text = "季pt";
            // 
            // chYear
            // 
            this.chYear.Text = "年pt";
            // 
            // chGlobalPt
            // 
            this.chGlobalPt.Text = "総pt";
            // 
            // DetailListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvDisplay);
            this.DoubleBuffered = true;
            this.Name = "DetailListView";
            this.Size = new System.Drawing.Size(513, 386);
            this.ResumeLayout(false);

        }

        #endregion

        private npva.DoubleBufferedListView lvDisplay;
        private System.Windows.Forms.ColumnHeader chDate;
        private System.Windows.Forms.ColumnHeader chPCPv;
        private System.Windows.Forms.ColumnHeader chMobilePv;
        private System.Windows.Forms.ColumnHeader chSmpPv;
        private System.Windows.Forms.ColumnHeader chTotalPv;
        private System.Windows.Forms.ColumnHeader chVote;
        private System.Windows.Forms.ColumnHeader chBookmark;
        private System.Windows.Forms.ColumnHeader chImpression;
        private System.Windows.Forms.ColumnHeader chReview;
        private System.Windows.Forms.ColumnHeader chSeries;
        private System.Windows.Forms.ColumnHeader chSize;
        private System.Windows.Forms.ColumnHeader chDaily;
        private System.Windows.Forms.ColumnHeader chWeekly;
        private System.Windows.Forms.ColumnHeader chMonthly;
        private System.Windows.Forms.ColumnHeader chQuater;
        private System.Windows.Forms.ColumnHeader chYear;
        private System.Windows.Forms.ColumnHeader chGlobalPt;
    }
}
