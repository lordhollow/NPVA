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
    /// メインウィンドウ
    /// </summary>
    /// <remarks>
    /// ツールバーのアイコンは28x28を使ったよ
    /// </remarks>
    public partial class NPVAMain : Form
    {
        //アナライザ
        private Analyzer analyzer;
        //Analyzerカレントのクローン
        private DB.Author current;

        /// <summary>
        /// チャートタブ(非表示の時一時的にここに避難する)
        /// </summary>
        TabPage chartTab = null;

        public NPVAMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ロード時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NPVAMain_Load(object sender, EventArgs e)
        {
            //アナライザー
            analyzer = new Analyzer();
            analyzer.AnalyzingStart += Analyzer_AnalyzingStart;
            analyzer.AnalyzingEnd += Analyzer_AnalyzingEnd;

            //ソートオブジェクトの準備
            cmbSortType.Items.AddRange(SortOrderEntry.CreateEntries().ToArray());
            cmbSortType.SelectedIndex = 0;
            cmbSortType.SelectedIndexChanged += (s, a) => updateList();

            //チャートコンストラクタの準備
            cmbChartType.Items.AddRange(Chart.ChartConstructor.CreateConstactors().ToArray());
            cmbChartType.SelectedIndex = 0;
            cmbChartType.SelectedIndexChanged += (s, a) => redrawChart();

            //IDリスト
            foreach (var aid in analyzer.StoredAuthorInfo)
            {
                cmbUserId.Items.Add(aid);
            }
        }

        /// <summary>
        /// Analyzerの分析開始イベントハンドラ（分析開始するメニューを無効化）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Analyzer_AnalyzingStart(object sender, EventArgs e)
        {
            SetCommandEnable(false);
        }

        /// <summary>
        /// Analyzerの分析終了イベントハンドラ（分析開始するメニューを有効化）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Analyzer_AnalyzingEnd(object sender, EventArgs e)
        {
            SetCommandEnable(true);
        }

        /// <summary>
        /// 分析開始するメニューの有効/無効設定
        /// </summary>
        /// <param name="enable"></param>
        private void SetCommandEnable(bool enable)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => SetCommandEnable(enable)));
            }
            else
            {
                if (enable)
                {
                    lblStatus.Text = "待機中";
                    btnUpdate.Enabled = true;
                }
                else
                {
                    btnUpdate.Enabled = false;
                    lblStatus.Text = "更新中";
                }
            }
        }

        /// <summary>
        /// 作品一覧更新
        /// </summary>
        private void updateList()
        {
            lstTitles.Items.Clear();
            current = analyzer.Current;

            if (current == null) return;
            var order = cmbSortType.SelectedItem as SortOrderEntry;
            if (order == null) order = new SortOrderEntry();

            foreach (var title in order.Order(current.Titles))
            {
                var item = new TitleListEntry(title, order.TitlePicker(title));
                lstTitles.Items.Add(item);
            }
            lblAuthor.Text = current.Name;
            ShowUser();
        }

        /// <summary>
        /// グラフ変更
        /// </summary>
        private void redrawChart()
        {
            chartDrawer.Arrange(cmbChartType.SelectedItem as Chart.ChartConstructor);
        }

        /// <summary>
        /// 作品詳細表示
        /// </summary>
        /// <param name="title"></param>
        private void ShowTitle(DB.Title title)
        {
            if (title == null)
            {
                lblTitle.Text = "タイトルがぬるぽ";
                return;
            }

            var score = title.LatestScore;

            lblTitle.Text = $"{title.Name} [{title.Author}]";
            lblUpdateInfo.Text = $"初回更新：{title.FirstUp}, 最終更新：{title.LastUp}";
            lblSizeInfo.Text = $"{score.Series} 話投稿済み,  {score.Size:#,0} 文字, {score.Points:#,0} pt.";

            RestoreChartTab();
            startAnalyze(title);
            dlvTitleInfo.Arrange(title);
            chartDrawer.Arrange(title, cmbChartType.SelectedItem as Chart.ChartConstructor);
        }

        /// <summary>
        /// 著者情報表示
        /// </summary>
        private void ShowUser()
        {
            if (current == null) return;

            lblTitle.Text = $"{current.Name}({current.ID})";
            lblUpdateInfo.Text = $"{current.CheckedDate} 時点 当日情報";
            var totalSize = current.Titles.Sum(t => t.LatestScore?.Size);
            var totalPoints = current.Titles.Sum(t => t.LatestScore?.Points);
            lblSizeInfo.Text = $"{current.Titles.Count} 作投稿済み,  計 {totalSize:#,0} 文字, {totalPoints:#,0} pt.";

            RemoveChartTab();
            dlvTitleInfo.Arrange(current);
        }

        /// <summary>
        /// 作品データ取得(チャートの右にあるやつ）
        /// </summary>
        /// <param name="title"></param>
        private void startAnalyze(DB.Title title)
        {
            lvAnalyzed.Visible = false;
            lvAnalyzed.Items.Clear();
            lvAnalyzed.Items.AddRange(analyzer.AnalyzeTitle(title).Select(x =>
            {
                var dt = new ListViewItem(x.ItemName);
                dt.SubItems.Add(x.Value);
                dt.SubItems.Add(x.MarkedDate);
                if (string.IsNullOrEmpty(x.Value))
                {
                    dt.BackColor = Color.SkyBlue;
                }
                return dt;

            }).ToArray());
            lvAnalyzed.Visible = true;
        }

        /// <summary>
        /// チャートタブ非表示
        /// </summary>
        private void RemoveChartTab()
        {
            if (chartTab == null)
            {
                chartTab = tpChart;
                tabInfo.TabPages.Remove(chartTab);
            }

        }
        /// <summary>
        /// チャートタブ表示
        /// </summary>
        private void RestoreChartTab()
        {
            if (chartTab != null)
            {
                tabInfo.TabPages.Add(chartTab);
                chartTab = null;
            }
        }

        #region UI操作イベント

        /// <summary>
        /// 更新ボタン押した
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            var id = cmbUserId.Text;
            await analyzer.UpdateBasicInfoAsync(id);
            analyzer.Save();
            updateList();
        }

        /// <summary>
        /// 作品リスト押した
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstTitles_SelectedIndexChanged(object sender, EventArgs e)
        {
            var entry = lstTitles.SelectedItem as TitleListEntry;
            if (entry != null)
            {
                var title = entry.Title;
                if (title != null)
                {
                    ShowTitle(title);
                }
                else
                {
                    ShowUser();
                }
            }
        }

        /// <summary>
        /// 著者選択代えた
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbUserId_SelectedIndexChanged(object sender, EventArgs e)
        {
            analyzer.Load(cmbUserId.Text);
            updateList();
        }

        #endregion

        #region ツールバーボタン

        LogForm logForm;

        /// <summary>
        /// ログウィンドウ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdViewLog_Click(object sender, EventArgs e)
        {
            if (logForm == null)
            {
                logForm = new LogForm();
                logForm.FormClosed += (s, a) => logForm = null;
                logForm.Show();
            }
            logForm.Activate();
        }

        /// <summary>
        /// エクスポート
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExport_Click(object sender, EventArgs e)
        {
            if (current != null)
            {
                var exporter = new ExportForm();
                exporter.Arrange(current);
                exporter.ShowDialog();
            }
        }

        /// <summary>
        /// 全ユーザーデータ更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void cmdUpdateAllStoredUser_Click(object sender, EventArgs e)
        {
            await analyzer.UpdateAllStoredUserAsync();
            updateList();
        }

        /// <summary>
        /// PVデータリパース
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPVReparse_Click(object sender, EventArgs e)
        {
            analyzer.GetPVData(true);
            analyzer.Save();
            updateList();
        }

        #endregion
    }
}
