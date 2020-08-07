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

        /// <summary>
        /// 部分別タブ(非表示の時一時的にここに避難する)
        /// </summary>
        TabPage partTab = null;

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
            //初期状態で表示されないタブ
            RemoveChartTab();
            RemovePartTab();

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
            if (cmbChartType.Items.Count > 0) cmbChartType.SelectedIndex = 0;
            cmbChartType.SelectedIndexChanged += (s, a) => redrawChart();

            //IDリスト
            foreach (var aid in analyzer.StoredAuthorInfo)
            {
                cmbUserId.Items.Add(aid);
            }

            //ユーザーの初期表示
            dlvTitleInfo.AuthorSummaryPvSums = Properties.Settings.Default.AuthorSummaryPvLength;
            var defaultAuthor = Properties.Settings.Default.StartupAuthor;
            if (analyzer.StoredAuthorInfo.Contains(defaultAuthor))
            {
                cmbUserId.Text = defaultAuthor;
                analyzer.Load(defaultAuthor);
                updateList();
            }

            //設定変更時イベント
            Properties.Settings.Default.SettingsSaving += Default_SettingsSaving;
        }

        /// <summary>
        /// 設定を変更したときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Default_SettingsSaving(object sender, CancelEventArgs e)
        {
            //著者情報の設定
            //dlvTitleInfo.AuthorSummaryDiffDays = (no prefference)
            dlvTitleInfo.AuthorSummaryPvSums = Properties.Settings.Default.AuthorSummaryPvLength;
            // チャートコンストラクタを再生成
            cmbChartType.Items.Clear();
            cmbChartType.Items.AddRange(Chart.ChartConstructor.CreateConstactors().ToArray());
            if (cmbChartType.Items.Count > 0) cmbChartType.SelectedIndex = 0;
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
                lblStatus.Text = enable ? "待機中" : "更新中";
                btnUpdate.Enabled = enable;
                cmbUserId.Enabled = enable;
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
            if(title.Score.Find(x=>x.PartPvChecked)!=null)
            {
                RestorePartTab();
                showPartPv(title);
            }
            else
            {
                RemovePartTab();
            }
        }

        /// <summary>
        /// 著者情報表示
        /// </summary>
        private void ShowUser()
        {
            if (current == null) return;

            lblTitle.Text = $"{current.Name}({current.ID})";

            var PvSums = "全期間";
            if (dlvTitleInfo.AuthorSummaryPvSums == 0) PvSums = "当日";
            if (dlvTitleInfo.AuthorSummaryPvSums > 0) PvSums = $"{dlvTitleInfo.AuthorSummaryPvSums}日前からの";


            if (dlvTitleInfo.AuthorSummaryDiffDays > 0)
            {
                lblUpdateInfo.Text = $"{current.CheckedDate} 時点 {PvSums}Pv +{dlvTitleInfo.AuthorSummaryDiffDays}日前とのスコア差分";
            }
            else
            {
                lblUpdateInfo.Text = $"{current.CheckedDate} 時点 {PvSums}Pv + スコア";
            }
            var totalSize = current.Titles.Sum(t => t.LatestScore?.Size);
            var totalPoints = current.Titles.Sum(t => t.LatestScore?.Points);
            lblSizeInfo.Text = $"{current.Titles.Count} 作投稿済み,  計 {totalSize:#,0} 文字, {totalPoints:#,0} pt.";

            RemoveChartTab();
            RemovePartTab();
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
        /// 部分別PV表示
        /// </summary>
        /// <param name="title"></param>
        private void showPartPv(DB.Title title)
        {
            var pv = new Dictionary<int, int>();
            foreach (var score in title.Score.Where(x => x.PartPvChecked))
            {
                foreach(var p in score.PartPv)
                {
                    if (pv.ContainsKey(p.Part))
                    {
                        pv[p.Part] += p.PageView;
                    }
                    else
                    {
                        pv[p.Part] = p.PageView;
                    }
                }
            }
            lvPartPv.Items.Clear();
            lvPartPv.Columns.Clear();
            lvPartPv.Columns.Add("部分");
            lvPartPv.Columns.Add("合計PV");
            foreach (var p in pv.OrderBy(x=>x.Key))
            {
                var lst = lvPartPv.Items.Add($"第{p.Key}部分");
                lst.SubItems.Add($"{p.Value:#,0}人");
            }
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

        /// <summary>
        /// 部分別タブ非表示
        /// </summary>
        private void RemovePartTab()
        {
            if (partTab == null)
            {
                partTab = tpPart;
                tabInfo.TabPages.Remove(partTab);
            }
        }

        /// <summary>
        /// 部分別タブ表示
        /// </summary>
        private void RestorePartTab()
        {
            if (partTab != null)
            {
                tabInfo.TabPages.Add(partTab);
                partTab = null;
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

        /// <summary>
        /// チャートの保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChartSaveAs_Click(object sender, EventArgs e)
        {
            var ofd = new SaveFileDialog();
            ofd.OverwritePrompt = true;
            ofd.Filter = "PNGファイル(*.png)|*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var f = ofd.FileName;
                var conf = Properties.Settings.Default;
                if (conf.ChartSaveAsIs)
                {
                    chartDrawer.SaveImage(f);
                }
                else
                {
                    var constractor = cmbChartType.SelectedItem as Chart.ChartConstructor;
                    if (constractor != null)
                    {
                        chartDrawer.SaveImage(constractor, f, conf.ChartSaveWidth, conf.ChartSaveHeight);
                    }
                    else
                    {
                        MessageBox.Show("グラフが選択されていません");
                    }
                }
            }
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

        /// <summary>
        /// 部分別PV取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdUpdatePPV_Click(object sender, EventArgs e)
        {
            var t = dlvTitleInfo.ArrangedTitle;
            if (t != null)
            {
                t = analyzer.getOriginalTitle(t);
                analyzer.AnalyzePartPv(t);
                analyzer.Save();
                updateList();
            }
        }

        /// <summary>
        /// 設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPref_Click(object sender, EventArgs e)
        {
            var dlg = new ConfigulationForm();
            dlg.Authors = analyzer.StoredAuthorInfo.ToArray();
            dlg.ShowDialog();
        }

        #endregion

        #region コンテキストメニュー

        /// <summary>
        /// 詳細ビュー用・表示前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmenuDetailList_Opening(object sender, CancelEventArgs e)
        {
            //作品/日付単一選択でなければ部位別PV表示は非表示
            if ((dlvTitleInfo.ArrangedTitle != null) && (dlvTitleInfo.SelectedDayScore == null))
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 部分別PV表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmenuBrowsePartPv_Click(object sender, EventArgs e)
        {
            var t = dlvTitleInfo.ArrangedTitle;

            var d = dlvTitleInfo.SelectedDayScore;
            if ((t != null) && (d != null))
            {
                var day = d.Date.ToString("yyyy-MM-dd");
                var url = $"https://kasasagi.hinaproject.com/access/chapter/ncode/{t.ID}/?date={day}";
                System.Diagnostics.Process.Start(url);
            }
        }


        #endregion

    }
}
