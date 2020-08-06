namespace npva
{
    partial class NPVAMain
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

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NPVAMain));
            this.spLR = new System.Windows.Forms.SplitContainer();
            this.pL = new System.Windows.Forms.Panel();
            this.menuStripMain = new System.Windows.Forms.ToolStrip();
            this.cmdViewLog = new System.Windows.Forms.ToolStripButton();
            this.cmdExport = new System.Windows.Forms.ToolStripButton();
            this.cmdUpdateAllStoredUser = new System.Windows.Forms.ToolStripButton();
            this.cmdPVReparse = new System.Windows.Forms.ToolStripButton();
            this.cmdUpdatePPV = new System.Windows.Forms.ToolStripButton();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.cmbSortType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lstTitles = new System.Windows.Forms.ListBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.cmbUserId = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pR = new System.Windows.Forms.Panel();
            this.tabInfo = new System.Windows.Forms.TabControl();
            this.tpList = new System.Windows.Forms.TabPage();
            this.dlvTitleInfo = new npva.DetailListView();
            this.cmenuDetailList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmenuBrowsePartPv = new System.Windows.Forms.ToolStripMenuItem();
            this.tpChart = new System.Windows.Forms.TabPage();
            this.spChartLR = new System.Windows.Forms.SplitContainer();
            this.chartDrawer = new npva.Chart.Drawer.GDIDrawSurface();
            this.lvAnalyzed = new System.Windows.Forms.ListView();
            this.chTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMarkreDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbChartType = new System.Windows.Forms.ComboBox();
            this.tpPart = new System.Windows.Forms.TabPage();
            this.lvPartPv = new System.Windows.Forms.ListView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblSizeInfo = new System.Windows.Forms.Label();
            this.lblUpdateInfo = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.cmdPref = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.spLR)).BeginInit();
            this.spLR.Panel1.SuspendLayout();
            this.spLR.Panel2.SuspendLayout();
            this.spLR.SuspendLayout();
            this.pL.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.pR.SuspendLayout();
            this.tabInfo.SuspendLayout();
            this.tpList.SuspendLayout();
            this.cmenuDetailList.SuspendLayout();
            this.tpChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spChartLR)).BeginInit();
            this.spChartLR.Panel1.SuspendLayout();
            this.spChartLR.Panel2.SuspendLayout();
            this.spChartLR.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tpPart.SuspendLayout();
            this.SuspendLayout();
            // 
            // spLR
            // 
            this.spLR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spLR.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spLR.Location = new System.Drawing.Point(0, 0);
            this.spLR.Name = "spLR";
            // 
            // spLR.Panel1
            // 
            this.spLR.Panel1.Controls.Add(this.pL);
            // 
            // spLR.Panel2
            // 
            this.spLR.Panel2.Controls.Add(this.pR);
            this.spLR.Size = new System.Drawing.Size(1097, 458);
            this.spLR.SplitterDistance = 221;
            this.spLR.TabIndex = 2;
            // 
            // pL
            // 
            this.pL.Controls.Add(this.menuStripMain);
            this.pL.Controls.Add(this.lblAuthor);
            this.pL.Controls.Add(this.cmbSortType);
            this.pL.Controls.Add(this.label2);
            this.pL.Controls.Add(this.lstTitles);
            this.pL.Controls.Add(this.lblStatus);
            this.pL.Controls.Add(this.btnUpdate);
            this.pL.Controls.Add(this.cmbUserId);
            this.pL.Controls.Add(this.label1);
            this.pL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pL.Location = new System.Drawing.Point(0, 0);
            this.pL.Name = "pL";
            this.pL.Size = new System.Drawing.Size(221, 458);
            this.pL.TabIndex = 0;
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdViewLog,
            this.cmdExport,
            this.cmdUpdateAllStoredUser,
            this.cmdPVReparse,
            this.cmdUpdatePPV,
            this.cmdPref});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(221, 25);
            this.menuStripMain.TabIndex = 10;
            // 
            // cmdViewLog
            // 
            this.cmdViewLog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdViewLog.Image = ((System.Drawing.Image)(resources.GetObject("cmdViewLog.Image")));
            this.cmdViewLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdViewLog.Name = "cmdViewLog";
            this.cmdViewLog.Size = new System.Drawing.Size(23, 22);
            this.cmdViewLog.Text = "LogWindow";
            this.cmdViewLog.ToolTipText = "ログ画面を表示します";
            this.cmdViewLog.Click += new System.EventHandler(this.cmdViewLog_Click);
            // 
            // cmdExport
            // 
            this.cmdExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdExport.Image = ((System.Drawing.Image)(resources.GetObject("cmdExport.Image")));
            this.cmdExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdExport.Name = "cmdExport";
            this.cmdExport.Size = new System.Drawing.Size(23, 22);
            this.cmdExport.Text = "Export";
            this.cmdExport.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.cmdExport.ToolTipText = "データをエクスポートします";
            this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
            // 
            // cmdUpdateAllStoredUser
            // 
            this.cmdUpdateAllStoredUser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdUpdateAllStoredUser.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpdateAllStoredUser.Image")));
            this.cmdUpdateAllStoredUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdUpdateAllStoredUser.Name = "cmdUpdateAllStoredUser";
            this.cmdUpdateAllStoredUser.Size = new System.Drawing.Size(23, 22);
            this.cmdUpdateAllStoredUser.Text = "全更新";
            this.cmdUpdateAllStoredUser.ToolTipText = "すべての解析済みユーザーのデータを更新します";
            this.cmdUpdateAllStoredUser.Click += new System.EventHandler(this.cmdUpdateAllStoredUser_Click);
            // 
            // cmdPVReparse
            // 
            this.cmdPVReparse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdPVReparse.Image = ((System.Drawing.Image)(resources.GetObject("cmdPVReparse.Image")));
            this.cmdPVReparse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdPVReparse.Name = "cmdPVReparse";
            this.cmdPVReparse.Size = new System.Drawing.Size(23, 22);
            this.cmdPVReparse.Text = "Reparse";
            this.cmdPVReparse.ToolTipText = "全PVデータを再取得します。キャッシュがあればキャッシュのみ参照し、kasasagiへのアクセスは行いません。";
            this.cmdPVReparse.Click += new System.EventHandler(this.cmdPVReparse_Click);
            // 
            // cmdUpdatePPV
            // 
            this.cmdUpdatePPV.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdUpdatePPV.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpdatePPV.Image")));
            this.cmdUpdatePPV.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdUpdatePPV.Name = "cmdUpdatePPV";
            this.cmdUpdatePPV.Size = new System.Drawing.Size(23, 22);
            this.cmdUpdatePPV.Text = "日別PV取得";
            this.cmdUpdatePPV.Visible = false;
            this.cmdUpdatePPV.Click += new System.EventHandler(this.cmdUpdatePPV_Click);
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Location = new System.Drawing.Point(10, 100);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(53, 12);
            this.lblAuthor.TabIndex = 9;
            this.lblAuthor.Text = "<著者名>";
            // 
            // cmbSortType
            // 
            this.cmbSortType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSortType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSortType.FormattingEnabled = true;
            this.cmbSortType.Location = new System.Drawing.Point(120, 128);
            this.cmbSortType.Name = "cmbSortType";
            this.cmbSortType.Size = new System.Drawing.Size(94, 20);
            this.cmbSortType.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "作品一覧";
            // 
            // lstTitles
            // 
            this.lstTitles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstTitles.FormattingEnabled = true;
            this.lstTitles.IntegralHeight = false;
            this.lstTitles.ItemHeight = 12;
            this.lstTitles.Location = new System.Drawing.Point(12, 151);
            this.lstTitles.Name = "lstTitles";
            this.lstTitles.Size = new System.Drawing.Size(202, 295);
            this.lstTitles.TabIndex = 5;
            this.lstTitles.SelectedIndexChanged += new System.EventHandler(this.lstTitles_SelectedIndexChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 71);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(41, 12);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "待機中";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(148, 48);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(45, 23);
            this.btnUpdate.TabIndex = 3;
            this.btnUpdate.Text = "更新";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // cmbUserId
            // 
            this.cmbUserId.FormattingEnabled = true;
            this.cmbUserId.Location = new System.Drawing.Point(12, 48);
            this.cmbUserId.Name = "cmbUserId";
            this.cmbUserId.Size = new System.Drawing.Size(128, 20);
            this.cmbUserId.TabIndex = 2;
            this.cmbUserId.SelectedIndexChanged += new System.EventHandler(this.cmbUserId_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ユーザーID";
            // 
            // pR
            // 
            this.pR.Controls.Add(this.tabInfo);
            this.pR.Controls.Add(this.lblSizeInfo);
            this.pR.Controls.Add(this.lblUpdateInfo);
            this.pR.Controls.Add(this.lblTitle);
            this.pR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pR.Location = new System.Drawing.Point(0, 0);
            this.pR.Name = "pR";
            this.pR.Size = new System.Drawing.Size(872, 458);
            this.pR.TabIndex = 1;
            // 
            // tabInfo
            // 
            this.tabInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabInfo.Controls.Add(this.tpList);
            this.tabInfo.Controls.Add(this.tpChart);
            this.tabInfo.Controls.Add(this.tpPart);
            this.tabInfo.Location = new System.Drawing.Point(10, 78);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.SelectedIndex = 0;
            this.tabInfo.Size = new System.Drawing.Size(859, 377);
            this.tabInfo.TabIndex = 3;
            // 
            // tpList
            // 
            this.tpList.Controls.Add(this.dlvTitleInfo);
            this.tpList.Location = new System.Drawing.Point(4, 22);
            this.tpList.Name = "tpList";
            this.tpList.Padding = new System.Windows.Forms.Padding(3);
            this.tpList.Size = new System.Drawing.Size(851, 351);
            this.tpList.TabIndex = 0;
            this.tpList.Text = "List";
            this.tpList.UseVisualStyleBackColor = true;
            // 
            // dlvTitleInfo
            // 
            this.dlvTitleInfo.ContextMenuStrip = this.cmenuDetailList;
            this.dlvTitleInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dlvTitleInfo.Location = new System.Drawing.Point(3, 3);
            this.dlvTitleInfo.Name = "dlvTitleInfo";
            this.dlvTitleInfo.Size = new System.Drawing.Size(845, 345);
            this.dlvTitleInfo.TabIndex = 0;
            // 
            // cmenuDetailList
            // 
            this.cmenuDetailList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmenuBrowsePartPv});
            this.cmenuDetailList.Name = "cmenuDetailList";
            this.cmenuDetailList.Size = new System.Drawing.Size(214, 26);
            this.cmenuDetailList.Opening += new System.ComponentModel.CancelEventHandler(this.cmenuDetailList_Opening);
            // 
            // cmenuBrowsePartPv
            // 
            this.cmenuBrowsePartPv.Name = "cmenuBrowsePartPv";
            this.cmenuBrowsePartPv.Size = new System.Drawing.Size(213, 22);
            this.cmenuBrowsePartPv.Text = "(browser)この日の部分別Pv";
            this.cmenuBrowsePartPv.Click += new System.EventHandler(this.cmenuBrowsePartPv_Click);
            // 
            // tpChart
            // 
            this.tpChart.Controls.Add(this.spChartLR);
            this.tpChart.Controls.Add(this.panel1);
            this.tpChart.Location = new System.Drawing.Point(4, 22);
            this.tpChart.Name = "tpChart";
            this.tpChart.Padding = new System.Windows.Forms.Padding(3);
            this.tpChart.Size = new System.Drawing.Size(851, 351);
            this.tpChart.TabIndex = 1;
            this.tpChart.Text = "Chart";
            this.tpChart.UseVisualStyleBackColor = true;
            // 
            // spChartLR
            // 
            this.spChartLR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spChartLR.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.spChartLR.Location = new System.Drawing.Point(3, 27);
            this.spChartLR.Name = "spChartLR";
            // 
            // spChartLR.Panel1
            // 
            this.spChartLR.Panel1.Controls.Add(this.chartDrawer);
            // 
            // spChartLR.Panel2
            // 
            this.spChartLR.Panel2.BackColor = System.Drawing.Color.DimGray;
            this.spChartLR.Panel2.Controls.Add(this.lvAnalyzed);
            this.spChartLR.Panel2.Controls.Add(this.label3);
            this.spChartLR.Size = new System.Drawing.Size(845, 321);
            this.spChartLR.SplitterDistance = 639;
            this.spChartLR.TabIndex = 0;
            // 
            // chartDrawer
            // 
            this.chartDrawer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartDrawer.Location = new System.Drawing.Point(0, 0);
            this.chartDrawer.Name = "chartDrawer";
            this.chartDrawer.Size = new System.Drawing.Size(639, 321);
            this.chartDrawer.TabIndex = 0;
            // 
            // lvAnalyzed
            // 
            this.lvAnalyzed.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chTitle,
            this.chValue,
            this.chMarkreDate});
            this.lvAnalyzed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvAnalyzed.FullRowSelect = true;
            this.lvAnalyzed.GridLines = true;
            this.lvAnalyzed.HideSelection = false;
            this.lvAnalyzed.Location = new System.Drawing.Point(0, 0);
            this.lvAnalyzed.Name = "lvAnalyzed";
            this.lvAnalyzed.Size = new System.Drawing.Size(202, 321);
            this.lvAnalyzed.TabIndex = 1;
            this.lvAnalyzed.UseCompatibleStateImageBehavior = false;
            this.lvAnalyzed.View = System.Windows.Forms.View.Details;
            // 
            // chTitle
            // 
            this.chTitle.Text = "タイトル";
            this.chTitle.Width = 82;
            // 
            // chValue
            // 
            this.chValue.Text = "値";
            this.chValue.Width = 73;
            // 
            // chMarkreDate
            // 
            this.chMarkreDate.Text = "記録日";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "Analyzing...";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbChartType);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(845, 24);
            this.panel1.TabIndex = 1;
            // 
            // cmbChartType
            // 
            this.cmbChartType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChartType.FormattingEnabled = true;
            this.cmbChartType.Location = new System.Drawing.Point(3, 1);
            this.cmbChartType.Name = "cmbChartType";
            this.cmbChartType.Size = new System.Drawing.Size(220, 20);
            this.cmbChartType.TabIndex = 0;
            // 
            // tpPart
            // 
            this.tpPart.Controls.Add(this.lvPartPv);
            this.tpPart.Controls.Add(this.panel2);
            this.tpPart.Location = new System.Drawing.Point(4, 22);
            this.tpPart.Name = "tpPart";
            this.tpPart.Padding = new System.Windows.Forms.Padding(3);
            this.tpPart.Size = new System.Drawing.Size(851, 351);
            this.tpPart.TabIndex = 2;
            this.tpPart.Text = "部分別PV";
            this.tpPart.UseVisualStyleBackColor = true;
            // 
            // lvPartPv
            // 
            this.lvPartPv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPartPv.HideSelection = false;
            this.lvPartPv.Location = new System.Drawing.Point(3, 40);
            this.lvPartPv.Name = "lvPartPv";
            this.lvPartPv.Size = new System.Drawing.Size(845, 308);
            this.lvPartPv.TabIndex = 0;
            this.lvPartPv.UseCompatibleStateImageBehavior = false;
            this.lvPartPv.View = System.Windows.Forms.View.Details;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(845, 37);
            this.panel2.TabIndex = 1;
            // 
            // lblSizeInfo
            // 
            this.lblSizeInfo.AutoSize = true;
            this.lblSizeInfo.Location = new System.Drawing.Point(26, 47);
            this.lblSizeInfo.Name = "lblSizeInfo";
            this.lblSizeInfo.Size = new System.Drawing.Size(29, 12);
            this.lblSizeInfo.TabIndex = 2;
            this.lblSizeInfo.Text = "話数";
            // 
            // lblUpdateInfo
            // 
            this.lblUpdateInfo.AutoSize = true;
            this.lblUpdateInfo.Location = new System.Drawing.Point(26, 33);
            this.lblUpdateInfo.Name = "lblUpdateInfo";
            this.lblUpdateInfo.Size = new System.Drawing.Size(53, 12);
            this.lblUpdateInfo.TabIndex = 1;
            this.lblUpdateInfo.Text = "更新情報";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTitle.Location = new System.Drawing.Point(6, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(55, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "TITLE";
            // 
            // cmdPref
            // 
            this.cmdPref.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdPref.Image = ((System.Drawing.Image)(resources.GetObject("cmdPref.Image")));
            this.cmdPref.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdPref.Name = "cmdPref";
            this.cmdPref.Size = new System.Drawing.Size(23, 22);
            this.cmdPref.Text = "設定";
            this.cmdPref.Click += new System.EventHandler(this.cmdPref_Click);
            // 
            // NPVAMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 458);
            this.Controls.Add(this.spLR);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NPVAMain";
            this.Text = "PV解析";
            this.Load += new System.EventHandler(this.NPVAMain_Load);
            this.spLR.Panel1.ResumeLayout(false);
            this.spLR.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spLR)).EndInit();
            this.spLR.ResumeLayout(false);
            this.pL.ResumeLayout(false);
            this.pL.PerformLayout();
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.pR.ResumeLayout(false);
            this.pR.PerformLayout();
            this.tabInfo.ResumeLayout(false);
            this.tpList.ResumeLayout(false);
            this.cmenuDetailList.ResumeLayout(false);
            this.tpChart.ResumeLayout(false);
            this.spChartLR.Panel1.ResumeLayout(false);
            this.spChartLR.Panel2.ResumeLayout(false);
            this.spChartLR.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spChartLR)).EndInit();
            this.spChartLR.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tpPart.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pL;
        private System.Windows.Forms.ComboBox cmbSortType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstTitles;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.ComboBox cmbUserId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pR;
        private System.Windows.Forms.Label lblSizeInfo;
        private System.Windows.Forms.Label lblUpdateInfo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.TabControl tabInfo;
        private System.Windows.Forms.TabPage tpList;
        private System.Windows.Forms.TabPage tpChart;
        private DetailListView dlvTitleInfo;
        private System.Windows.Forms.ToolStrip menuStripMain;
        private System.Windows.Forms.ToolStripButton cmdViewLog;
        private System.Windows.Forms.SplitContainer spChartLR;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbChartType;
        private Chart.Drawer.GDIDrawSurface chartDrawer;
        private System.Windows.Forms.ToolStripButton cmdPVReparse;
        private System.Windows.Forms.ToolStripButton cmdExport;
        private System.Windows.Forms.SplitContainer spLR;
        private System.Windows.Forms.ToolStripButton cmdUpdateAllStoredUser;
        private System.Windows.Forms.ListView lvAnalyzed;
        private System.Windows.Forms.ColumnHeader chTitle;
        private System.Windows.Forms.ColumnHeader chValue;
        private System.Windows.Forms.ColumnHeader chMarkreDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip cmenuDetailList;
        private System.Windows.Forms.ToolStripMenuItem cmenuBrowsePartPv;
        private System.Windows.Forms.ToolStripButton cmdUpdatePPV;
        private System.Windows.Forms.TabPage tpPart;
        private System.Windows.Forms.ListView lvPartPv;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripButton cmdPref;
    }
}

