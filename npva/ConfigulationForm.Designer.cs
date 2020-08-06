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
            this.chkCCIgnoreMovingAvg = new System.Windows.Forms.CheckBox();
            this.chkCCIgnoreTotal = new System.Windows.Forms.CheckBox();
            this.chkCCIgnoreDaily = new System.Windows.Forms.CheckBox();
            this.chkCCIgnoreBacklog7 = new System.Windows.Forms.CheckBox();
            this.chkCCIgnoreBacklog30 = new System.Windows.Forms.CheckBox();
            this.chkCCIgnoreBacklog90 = new System.Windows.Forms.CheckBox();
            this.chkCCIgnoreBacklog180 = new System.Windows.Forms.CheckBox();
            this.chkCCIgnoreBacklog365 = new System.Windows.Forms.CheckBox();
            this.chkCCIgnoreAllTime = new System.Windows.Forms.CheckBox();
            this.gbChart = new System.Windows.Forms.GroupBox();
            this.chkChartIgnorePV = new System.Windows.Forms.CheckBox();
            this.chkChartIgnoreUnique = new System.Windows.Forms.CheckBox();
            this.chkChartIgnoreScore = new System.Windows.Forms.CheckBox();
            this.chkChartMALeft = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numSummalyLen)).BeginInit();
            this.gbChartConstractor.SuspendLayout();
            this.gbChart.SuspendLayout();
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
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(541, 271);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ConfigulationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 314);
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
    }
}