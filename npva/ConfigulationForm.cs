using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace npva
{
    public partial class ConfigulationForm : Form
    {
        public string[] Authors
        {
            get { return cmbStartupAuthor.Items.Cast<string>().ToArray(); }
            set
            {
                cmbStartupAuthor.Items.Clear();
                cmbStartupAuthor.Items.AddRange(value);
            }
        }


        public ConfigulationForm()
        {
            InitializeComponent();
        }

        private void ShowCurrent()
        {
            var conf = Properties.Settings.Default;
            cmbStartupAuthor.Text = conf.StartupAuthor;
            if (conf.AuthorSummaryPvLength<0)
            {
                chkSummaryAll.Checked = true;
                numSummalyLen.Value = 0;
            }
            else
            {
                chkSummaryAll.Checked = false;
                numSummalyLen.Value = conf.AuthorSummaryPvLength;
            }
            //CC
            chkCCIgnoreTotal.Checked = conf.CCIgnoreTotalPv;
            chkCCIgnoreDaily.Checked = conf.CCIgnoreDailyPv;
            chkCCIgnoreMovingAvg.Checked = conf.CCIgnoreMovingAverage;
            chkCCIgnoreBacklog7.Checked = conf.CCIgnoreBacklog7;
            chkCCIgnoreBacklog30.Checked = conf.CCIgnoreBacklog30;
            chkCCIgnoreBacklog90.Checked = conf.CCIgnoreBacklog90;
            chkCCIgnoreBacklog180.Checked = conf.CCIgnoreBacklog180;
            chkCCIgnoreBacklog365.Checked = conf.CCIgnoreBacklog365;
            chkCCIgnoreAllTime.Checked = conf.CCIgnoreAllTime;
            //Chart
            chkChartIgnorePV.Checked = conf.ChartExcludePV;
            chkChartIgnoreUnique.Checked = conf.ChartExcludeUnique;
            chkChartIgnoreScore.Checked = conf.ChartExcludeScore;
            chkChartMALeft.Checked = conf.MovingAverageByLeft;
            //ChartSave
            if (conf.ChartSaveAsIs) rbChartSaveAsIs.Checked = true; else rbChartSaveSized.Checked = true;
            numChartSaveW.Value = conf.ChartSaveWidth;
            numChartSaveH.Value = conf.ChartSaveHeight;
        }

        private void Commit()
        {
            var conf = Properties.Settings.Default;
            conf.StartupAuthor = cmbStartupAuthor.Text;
            conf.AuthorSummaryPvLength = chkSummaryAll.Checked ? -1 : (int)numSummalyLen.Value;
            //CC
            conf.CCIgnoreTotalPv = chkCCIgnoreTotal.Checked;
            conf.CCIgnoreDailyPv = chkCCIgnoreDaily.Checked;
            conf.CCIgnoreMovingAverage = chkCCIgnoreMovingAvg.Checked;
            conf.CCIgnoreBacklog7 = chkCCIgnoreBacklog7.Checked;
            conf.CCIgnoreBacklog30 = chkCCIgnoreBacklog30.Checked;
            conf.CCIgnoreBacklog90 = chkCCIgnoreBacklog90.Checked;
            conf.CCIgnoreBacklog180 = chkCCIgnoreBacklog180.Checked;
            conf.CCIgnoreBacklog365 = chkCCIgnoreBacklog365.Checked;
            conf.CCIgnoreAllTime = chkCCIgnoreAllTime.Checked;
            //Chart
            conf.ChartExcludePV = chkChartIgnorePV.Checked;
            conf.ChartExcludeUnique = chkChartIgnoreUnique.Checked;
            conf.ChartExcludeScore = chkChartIgnoreScore.Checked;
            conf.MovingAverageByLeft = chkChartMALeft.Checked;
            //ChartSave
            conf.ChartSaveAsIs = rbChartSaveAsIs.Checked;
            conf.ChartSaveWidth = (int)numChartSaveW.Value;
            conf.ChartSaveHeight = (int)numChartSaveH.Value;

            conf.Save();
        }


        private void ConfigulationForm_Load(object sender, EventArgs e)
        {
            ShowCurrent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Commit();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void chkSummaryAll_CheckedChanged(object sender, EventArgs e)
        {
            numSummalyLen.Enabled = !chkSummaryAll.Checked;
        }
    }
}