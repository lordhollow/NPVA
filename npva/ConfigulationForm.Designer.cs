namespace npva
{
    partial class ConfigulationForm
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
            this.numSummalyLen = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.chkSummaryAll = new System.Windows.Forms.CheckBox();
            this.cmbStartupAuthor = new System.Windows.Forms.ComboBox();
            this.gbChartConstractor = new System.Windows.Forms.GroupBox();
            this.chkCCIgnoreAllTime = new System.Windows.Forms.CheckBox();
            this.chkCCIgnoreBacklog365 = new System.Windows.Forms.CheckBox();
            this.chkCCIgnoreBacklog180 = new System.Windows.Forms.CheckBox();
            this.chkCCIgnoreBacklog90 = new System.Windows.Forms.CheckBox();
            this.chkCCIgnoreBacklog30 = new System.Windows.Forms.CheckBox();
            this.chkCCIgnoreBacklog7 = new System.Windows.Forms.CheckBox();
            this.chkCCIgnoreDaily = new System.Windows.Forms.CheckBox();
            this.chkCCIgnoreTotal = new System.Windows.Forms.CheckBox();
            this.chkCCIgnoreMovingAvg = new System.Windows.Forms.CheckBox();
            this.gbChart = new System.Windows.Forms.GroupBox();
            this.chkChartMALeft = new System.Windows.Forms.CheckBox();
            this.chkChartIgnoreScore = new System.Windows.Forms.CheckBox();
            this.chkChartIgnoreUnique = new System.Windows.Forms.CheckBox();
            this.chkChartIgnorePV = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.gbChartSave = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numChartSaveH = new System.Windows.Forms.NumericUpDown();
            this.numChartSaveW = new System.Windows.Forms.NumericUpDown();
            this.rbChartSaveSized = new System.Windows.Forms.RadioButton();
            this.rbChartSaveAsIs = new System.Windows.Forms.RadioButton();
            this.gbPPV = new System.Windows.Forms.GroupBox();
            this.chkPPVWeekFromMonday = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numPPVDays = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numSummalyLen)).BeginInit();
            this.gbChartConstractor.SuspendLayout();
            this.gbChart.SuspendLayout();
            this.gbChartSave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numChartSaveH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChartSaveW)).BeginInit();
            this.gbPPV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPPVDays)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "起動時に表示するユーザーID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "ユーザー表示のPV積算期間";
            // 
            // numSummalyLen
            // 
            this.numSummalyLen.Location = new System.Drawing.Point(171, 46);
            this.numSummalyLen.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numSummalyLen.Name = "numSummalyLen";
            this.numSummalyLen.Size = new System.Drawing.Size(120, 19);
            this.numSummalyLen.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(297, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "日";
            // 
            // chkSummaryAll
            // 
            this.chkSummaryAll.AutoSize = true;
            this.chkSummaryAll.Location = new System.Drawing.Point(358, 47);
            this.chkSummaryAll.Name = "chkSummaryAll";
            this.chkSummaryAll.Size = new System.Drawing.Size(60, 16);
            this.chkSummaryAll.TabIndex = 4;
            this.chkSummaryAll.Text = "全期間";
            this.chkSummaryAll.UseVisualStyleBackColor = true;
            this.chkSummaryAll.CheckedChanged += new System.EventHandler(this.chkSummaryAll_CheckedChanged);
            // 
            // cmbStartupAuthor
            // 
            this.cmbStartupAuthor.FormattingEnabled = true;
            this.cmbStartupAuthor.Location = new System.Drawing.Point(170, 20);
            this.cmbStartupAuthor.Name = "cmbStartupAuthor";
            this.cmbStartupAuthor.Size = new System.Drawing.Size(121, 20);
            this.cmbStartupAuthor.TabIndex = 5;
            // 
            // gbChartConstractor
            // 
            this.gbChartConstractor.Controls.Add(this.chkCCIgnoreAllTime);
            this.gbChartConstractor.Controls.Add(this.chkCCIgnoreBacklog365);
            this.gbChartConstractor.Controls.Add(this.chkCCIgnoreBacklog180);
            this.gbChartConstractor.Controls.Add(this.chkCCIgnoreBacklog90);
            this.gbChartConstractor.Controls.Add(this.chkCCIgnoreBacklog30);
            this.gbChartConstractor.Controls.Add(this.chkCCIgnoreBacklog7);
            this.gbChartConstractor.Controls.Add(this.chkCCIgnoreDaily);
            this.gbChartConstractor.Controls.Add(this.chkCCIgnoreTotal);
            this.gbChartConstractor.Controls.Add(this.chkCCIgnoreMovingAvg);
            this.gbChartConstractor.Location = new System.Drawing.Point(20, 87);
            this.gbChartConstractor.Name = "gbChartConstractor";
            this.gbChartConstractor.Size = new System.Drawing.Size(606, 95);
            this.gbChartConstractor.TabIndex = 6;
            this.gbChartConstractor.TabStop = false;
            this.gbChartConstractor.Text = "チャートの候補";
            // 
            // chkCCIgnoreAllTime
            // 
            this.chkCCIgnoreAllTime.AutoSize = true;
            this.chkCCIgnoreAllTime.Location = new System.Drawing.Point(423, 66);
            this.chkCCIgnoreAllTime.Name = "chkCCIgnoreAllTime";
            this.chkCCIgnoreAllTime.Size = new System.Drawing.Size(146, 16);
            this.chkCCIgnoreAllTime.TabIndex = 12;
            this.chkCCIgnoreAllTime.Text = "全期間チャートを含めない";
            this.chkCCIgnoreAllTime.UseVisualStyleBackColor = true;
            // 
            // chkCCIgnoreBacklog365
            // 
            this.chkCCIgnoreBacklog365.AutoSize = true;
            this.chkCCIgnoreBacklog365.Location = new System.Drawing.Point(208, 66);
            this.chkCCIgnoreBacklog365.Name = "chkCCIgnoreBacklog365";
            this.chkCCIgnoreBacklog365.Size = new System.Drawing.Size(152, 16);
            this.chkCCIgnoreBacklog365.TabIndex = 11;
            this.chkCCIgnoreBacklog365.Text = "365日間チャートを含めない";
            this.chkCCIgnoreBacklog365.UseVisualStyleBackColor = true;
            // 
            // chkCCIgnoreBacklog180
            // 
            this.chkCCIgnoreBacklog180.AutoSize = true;
            this.chkCCIgnoreBacklog180.Location = new System.Drawing.Point(6, 66);
            this.chkCCIgnoreBacklog180.Name = "chkCCIgnoreBacklog180";
            this.chkCCIgnoreBacklog180.Size = new System.Drawing.Size(152, 16);
            this.chkCCIgnoreBacklog180.TabIndex = 10;
            this.chkCCIgnoreBacklog180.Text = "180日間チャートを含めない";
            this.chkCCIgnoreBacklog180.UseVisualStyleBackColor = true;
            // 
            // chkCCIgnoreBacklog90
            // 
            this.chkCCIgnoreBacklog90.AutoSize = true;
            this.chkCCIgnoreBacklog90.Location = new System.Drawing.Point(423, 42);
            this.chkCCIgnoreBacklog90.Name = "chkCCIgnoreBacklog90";
            this.chkCCIgnoreBacklog90.Size = new System.Drawing.Size(146, 16);
            this.chkCCIgnoreBacklog90.TabIndex = 9;
            this.chkCCIgnoreBacklog90.Text = "90日間チャートを含めない";
            this.chkCCIgnoreBacklog90.UseVisualStyleBackColor = true;
            // 
            // chkCCIgnoreBacklog30
            // 
            this.chkCCIgnoreBacklog30.AutoSize = true;
            this.chkCCIgnoreBacklog30.Location = new System.Drawing.Point(208, 42);
            this.chkCCIgnoreBacklog30.Name = "chkCCIgnoreBacklog30";
            this.chkCCIgnoreBacklog30.Size = new System.Drawing.Size(146, 16);
            this.chkCCIgnoreBacklog30.TabIndex = 8;
            this.chkCCIgnoreBacklog30.Text = "30日間チャートを含めない";
            this.chkCCIgnoreBacklog30.UseVisualStyleBackColor = true;
            // 
            // chkCCIgnoreBacklog7
            // 
            this.chkCCIgnoreBacklog7.AutoSize = true;
            this.chkCCIgnoreBacklog7.Location = new System.Drawing.Point(6, 42);
            this.chkCCIgnoreBacklog7.Name = "chkCCIgnoreBacklog7";
            this.chkCCIgnoreBacklog7.Size = new System.Drawing.Size(140, 16);
            this.chkCCIgnoreBacklog7.TabIndex = 7;
            this.chkCCIgnoreBacklog7.Text = "7日間チャートを含めない";
            this.chkCCIgnoreBacklog7.UseVisualStyleBackColor = true;
            // 
            // chkCCIgnoreDaily
            // 
            this.chkCCIgnoreDaily.AutoSize = true;
            this.chkCCIgnoreDaily.Location = new System.Drawing.Point(208, 18);
            this.chkCCIgnoreDaily.Name = "chkCCIgnoreDaily";
            this.chkCCIgnoreDaily.Size = new System.Drawing.Size(149, 16);
            this.chkCCIgnoreDaily.TabIndex = 2;
            this.chkCCIgnoreDaily.Text = "日別PVチャートを含めない";
            this.chkCCIgnoreDaily.UseVisualStyleBackColor = true;
            // 
            // chkCCIgnoreTotal
            // 
            this.chkCCIgnoreTotal.AutoSize = true;
            this.chkCCIgnoreTotal.Location = new System.Drawing.Point(6, 18);
            this.chkCCIgnoreTotal.Name = "chkCCIgnoreTotal";
            this.chkCCIgnoreTotal.Size = new System.Drawing.Size(149, 16);
            this.chkCCIgnoreTotal.TabIndex = 1;
            this.chkCCIgnoreTotal.Text = "累計PVチャートを含めない";
            this.chkCCIgnoreTotal.UseVisualStyleBackColor = true;
            // 
            // chkCCIgnoreMovingAvg
            // 
            this.chkCCIgnoreMovingAvg.AutoSize = true;
            this.chkCCIgnoreMovingAvg.Location = new System.Drawing.Point(423, 18);
            this.chkCCIgnoreMovingAvg.Name = "chkCCIgnoreMovingAvg";
            this.chkCCIgnoreMovingAvg.Size = new System.Drawing.Size(173, 16);
            this.chkCCIgnoreMovingAvg.TabIndex = 0;
            this.chkCCIgnoreMovingAvg.Text = "PV移動平均チャートを含めない";
            this.chkCCIgnoreMovingAvg.UseVisualStyleBackColor = true;
            // 
            // gbChart
            // 
            this.gbChart.Controls.Add(this.chkChartMALeft);
            this.gbChart.Controls.Add(this.chkChartIgnoreScore);
            this.gbChart.Controls.Add(this.chkChartIgnoreUnique);
            this.gbChart.Controls.Add(this.chkChartIgnorePV);
            this.gbChart.Location = new System.Drawing.Point(20, 188);
            this.gbChart.Name = "gbChart";
            this.gbChart.Size = new System.Drawing.Size(606, 65);
            this.gbChart.TabIndex = 7;
            this.gbChart.TabStop = false;
            this.gbChart.Text = "チャートの表示";
            // 
            // chkChartMALeft
            // 
            this.chkChartMALeft.AutoSize = true;
            this.chkChartMALeft.Location = new System.Drawing.Point(6, 42);
            this.chkChartMALeft.Name = "chkChartMALeft";
            this.chkChartMALeft.Size = new System.Drawing.Size(160, 16);
            this.chkChartMALeft.TabIndex = 3;
            this.chkChartMALeft.Text = "移動平均を左側のみで算出";
            this.chkChartMALeft.UseVisualStyleBackColor = true;
            // 
            // chkChartIgnoreScore
            // 
            this.chkChartIgnoreScore.AutoSize = true;
            this.chkChartIgnoreScore.Location = new System.Drawing.Point(423, 18);
            this.chkChartIgnoreScore.Name = "chkChartIgnoreScore";
            this.chkChartIgnoreScore.Size = new System.Drawing.Size(110, 16);
            this.chkChartIgnoreScore.TabIndex = 2;
            this.chkChartIgnoreScore.Text = "累計スコア非表示";
            this.chkChartIgnoreScore.UseVisualStyleBackColor = true;
            // 
            // chkChartIgnoreUnique
            // 
            this.chkChartIgnoreUnique.AutoSize = true;
            this.chkChartIgnoreUnique.Location = new System.Drawing.Point(208, 18);
            this.chkChartIgnoreUnique.Name = "chkChartIgnoreUnique";
            this.chkChartIgnoreUnique.Size = new System.Drawing.Size(112, 16);
            this.chkChartIgnoreUnique.TabIndex = 1;
            this.chkChartIgnoreUnique.Text = "ユニークPV非表示";
            this.chkChartIgnoreUnique.UseVisualStyleBackColor = true;
            // 
            // chkChartIgnorePV
            // 
            this.chkChartIgnorePV.AutoSize = true;
            this.chkChartIgnorePV.Location = new System.Drawing.Point(6, 18);
            this.chkChartIgnorePV.Name = "chkChartIgnorePV";
            this.chkChartIgnorePV.Size = new System.Drawing.Size(75, 16);
            this.chkChartIgnorePV.TabIndex = 0;
            this.chkChartIgnorePV.Text = "PV非表示";
            this.chkChartIgnorePV.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(551, 362);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // gbChartSave
            // 
            this.gbChartSave.Controls.Add(this.label5);
            this.gbChartSave.Controls.Add(this.label4);
            this.gbChartSave.Controls.Add(this.numChartSaveH);
            this.gbChartSave.Controls.Add(this.numChartSaveW);
            this.gbChartSave.Controls.Add(this.rbChartSaveSized);
            this.gbChartSave.Controls.Add(this.rbChartSaveAsIs);
            this.gbChartSave.Location = new System.Drawing.Point(20, 259);
            this.gbChartSave.Name = "gbChartSave";
            this.gbChartSave.Size = new System.Drawing.Size(606, 39);
            this.gbChartSave.TabIndex = 9;
            this.gbChartSave.TabStop = false;
            this.gbChartSave.Text = "チャートの画像保存";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(472, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "高さ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(344, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "幅";
            // 
            // numChartSaveH
            // 
            this.numChartSaveH.Location = new System.Drawing.Point(503, 14);
            this.numChartSaveH.Maximum = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.numChartSaveH.Name = "numChartSaveH";
            this.numChartSaveH.Size = new System.Drawing.Size(93, 19);
            this.numChartSaveH.TabIndex = 3;
            // 
            // numChartSaveW
            // 
            this.numChartSaveW.Location = new System.Drawing.Point(367, 14);
            this.numChartSaveW.Maximum = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.numChartSaveW.Name = "numChartSaveW";
            this.numChartSaveW.Size = new System.Drawing.Size(93, 19);
            this.numChartSaveW.TabIndex = 2;
            // 
            // rbChartSaveSized
            // 
            this.rbChartSaveSized.AutoSize = true;
            this.rbChartSaveSized.Location = new System.Drawing.Point(208, 17);
            this.rbChartSaveSized.Name = "rbChartSaveSized";
            this.rbChartSaveSized.Size = new System.Drawing.Size(76, 16);
            this.rbChartSaveSized.TabIndex = 1;
            this.rbChartSaveSized.TabStop = true;
            this.rbChartSaveSized.Text = "サイズ指定";
            this.rbChartSaveSized.UseVisualStyleBackColor = true;
            // 
            // rbChartSaveAsIs
            // 
            this.rbChartSaveAsIs.AutoSize = true;
            this.rbChartSaveAsIs.Location = new System.Drawing.Point(6, 17);
            this.rbChartSaveAsIs.Name = "rbChartSaveAsIs";
            this.rbChartSaveAsIs.Size = new System.Drawing.Size(86, 16);
            this.rbChartSaveAsIs.TabIndex = 0;
            this.rbChartSaveAsIs.TabStop = true;
            this.rbChartSaveAsIs.Text = "見たまま保存";
            this.rbChartSaveAsIs.UseVisualStyleBackColor = true;
            // 
            // gbPPV
            // 
            this.gbPPV.Controls.Add(this.numPPVDays);
            this.gbPPV.Controls.Add(this.label6);
            this.gbPPV.Controls.Add(this.chkPPVWeekFromMonday);
            this.gbPPV.Location = new System.Drawing.Point(20, 304);
            this.gbPPV.Name = "gbPPV";
            this.gbPPV.Size = new System.Drawing.Size(606, 52);
            this.gbPPV.TabIndex = 10;
            this.gbPPV.TabStop = false;
            this.gbPPV.Text = "部位別PV表示";
            // 
            // chkPPVWeekFromMonday
            // 
            this.chkPPVWeekFromMonday.AutoSize = true;
            this.chkPPVWeekFromMonday.Location = new System.Drawing.Point(6, 18);
            this.chkPPVWeekFromMonday.Name = "chkPPVWeekFromMonday";
            this.chkPPVWeekFromMonday.Size = new System.Drawing.Size(117, 16);
            this.chkPPVWeekFromMonday.TabIndex = 2;
            this.chkPPVWeekFromMonday.Text = "月曜日始まりにする";
            this.chkPPVWeekFromMonday.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(206, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "日間表示数";
            // 
            // numPPVDays
            // 
            this.numPPVDays.Location = new System.Drawing.Point(346, 17);
            this.numPPVDays.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numPPVDays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPPVDays.Name = "numPPVDays";
            this.numPPVDays.Size = new System.Drawing.Size(120, 19);
            this.numPPVDays.TabIndex = 11;
            this.numPPVDays.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ConfigulationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 402);
            this.Controls.Add(this.gbPPV);
            this.Controls.Add(this.gbChartSave);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbChart);
            this.Controls.Add(this.gbChartConstractor);
            this.Controls.Add(this.cmbStartupAuthor);
            this.Controls.Add(this.chkSummaryAll);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numSummalyLen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ConfigulationForm";
            this.Text = "設定";
            this.Load += new System.EventHandler(this.ConfigulationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numSummalyLen)).EndInit();
            this.gbChartConstractor.ResumeLayout(false);
            this.gbChartConstractor.PerformLayout();
            this.gbChart.ResumeLayout(false);
            this.gbChart.PerformLayout();
            this.gbChartSave.ResumeLayout(false);
            this.gbChartSave.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numChartSaveH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChartSaveW)).EndInit();
            this.gbPPV.ResumeLayout(false);
            this.gbPPV.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPPVDays)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numSummalyLen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkSummaryAll;
        private System.Windows.Forms.ComboBox cmbStartupAuthor;
        private System.Windows.Forms.GroupBox gbChartConstractor;
        private System.Windows.Forms.CheckBox chkCCIgnoreBacklog90;
        private System.Windows.Forms.CheckBox chkCCIgnoreBacklog30;
        private System.Windows.Forms.CheckBox chkCCIgnoreBacklog7;
        private System.Windows.Forms.CheckBox chkCCIgnoreDaily;
        private System.Windows.Forms.CheckBox chkCCIgnoreTotal;
        private System.Windows.Forms.CheckBox chkCCIgnoreMovingAvg;
        private System.Windows.Forms.CheckBox chkCCIgnoreBacklog180;
        private System.Windows.Forms.CheckBox chkCCIgnoreAllTime;
        private System.Windows.Forms.CheckBox chkCCIgnoreBacklog365;
        private System.Windows.Forms.GroupBox gbChart;
        private System.Windows.Forms.CheckBox chkChartMALeft;
        private System.Windows.Forms.CheckBox chkChartIgnoreScore;
        private System.Windows.Forms.CheckBox chkChartIgnoreUnique;
        private System.Windows.Forms.CheckBox chkChartIgnorePV;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox gbChartSave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numChartSaveH;
        private System.Windows.Forms.NumericUpDown numChartSaveW;
        private System.Windows.Forms.RadioButton rbChartSaveSized;
        private System.Windows.Forms.RadioButton rbChartSaveAsIs;
        private System.Windows.Forms.GroupBox gbPPV;
        private System.Windows.Forms.NumericUpDown numPPVDays;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkPPVWeekFromMonday;
    }
}