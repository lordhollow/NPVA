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
    /// <summary>
    /// ログ表示ウィンドウ
    /// </summary>
    public partial class LogForm : Form
    {
        public LogForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ロード時初期化（デバッグレポートにフック）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogForm_Load(object sender, EventArgs e)
        {
            lstLog.Items.AddRange(DebugReport.Logs.ToArray());
            DebugReport.LogArrival += DebugReport_LogArrival;  
        }

        /// <summary>
        /// 閉じるとき（デバッグレポートをアンフック）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DebugReport.LogArrival -= DebugReport_LogArrival;
        }

        /// <summary>
        /// ログが来た時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DebugReport_LogArrival(object sender, LogArrivalEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => lstLog.Items.Add(e.Message)));
            }
            else
            {
                lstLog.Items.Add(e.Message);
            }
        }

        /// <summary>
        /// ログ消去
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogClear_Click(object sender, EventArgs e)
        {
            lstLog.Items.Clear();
            DebugReport.LogClear();
        }
    }
}
