using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace npva
{
    /// <summary>
    /// 詳細表示
    /// </summary>
    public partial class DetailListView : UserControl
    {
        /// <summary>
        /// 著者表示時に何日前のデータとの比較を表示するか
        /// </summary>
        /// <remarks>
        /// 0以下の時はその日のデータをそのまま表示。
        /// </remarks>
        public int AuthorSummaryDiffDays = 7;

        /// <summary>
        /// 著者表示時にPVを何日前から積算するか
        /// </summary>
        /// <remarks>
        /// 0以下の時は全期間
        /// </remarks>
        public int AuthorSummaryPvSums = 0;

        /// <summary>
        /// 携帯電話からのアクセスを示すカラムの番号
        /// </summary>
        const int indexOfMobileColumn = 2;

        /// <summary>
        /// 携帯電話からのアクセスを示すカラムの幅
        /// </summary>
        int DefaultMobileColumnWidth = 0;

        /// <summary>
        /// 初期化
        /// </summary>
        public DetailListView()
        {
            InitializeComponent();
            DefaultMobileColumnWidth = lvDisplay.Columns[indexOfMobileColumn].Width;
        }

        /// <summary>
        /// 著者情報表示
        /// </summary>
        /// <param name=""></param>
        public void Arrange(DB.Author author)
        {
            ArrangedAuthor = author;
            ArrangedTitle = null;

            chDate.Text = "Title";
            lvDisplay.BeginUpdate();
            lvDisplay.Items.Clear();
            var mobileColumnWidth = 0;
            foreach (var title in author.Titles)
            {
                var item = new ListViewItem($"{title.ID} {title.Name}");
                var score = title.LatestScore;
                var scoreD = AuthorSummaryDiffDays <= 0 ? null : title.GetScore(score.Date.AddDays(-1 * AuthorSummaryDiffDays));
                var pvScore = title.SumUpPv(AuthorSummaryPvSums);
                if (pvScore.Mobile != 0) mobileColumnWidth = DefaultMobileColumnWidth;
                if (score != null) registorScoreInfo(pvScore, score, scoreD, item);
                lvDisplay.Items.Add(item);
            }
            lvDisplay.Columns[indexOfMobileColumn].Width = mobileColumnWidth;
            lvDisplay.EndUpdate();
        }

        /// <summary>
        /// 作品データ表示
        /// </summary>
        /// <param name="title"></param>
        public void Arrange(DB.Title title)
        {
            ArrangedAuthor = null;
            ArrangedTitle = title;

            chDate.Text = "Date";
            lvDisplay.BeginUpdate();
            lvDisplay.Items.Clear();
            var mobileColumnWidth = 0;
            //総計
            {
                var item = new ListViewItem("total");
                var pv = new DB.DailyScore
                {
                    PC = title.Score.Sum(x => x.PC),
                    PCUnique = title.Score.Sum(x => x.PCUnique),
                    Mobile = title.Score.Sum(x => x.Mobile),
                    MobileUnique = title.Score.Sum(x => x.MobileUnique),
                    SmartPhone = title.Score.Sum(x => x.SmartPhone),
                    SmartPhoneUnique = title.Score.Sum(x => x.SmartPhoneUnique)
                };
                if (pv.Mobile != 0) mobileColumnWidth = DefaultMobileColumnWidth;
                var score = title.LatestScore;
                registorScoreInfo(pv, score, null, item);

                lvDisplay.Items.Add(item);
            }
            //個別(逆順)
            foreach (var score in (title.Score as IEnumerable<DB.DailyScore>).Reverse())
            {
                var item = new ListViewItem(score.Date.ToString("yyyy/MM/dd"));
                registorScoreInfo(score, score, null, item);
                lvDisplay.Items.Add(item);
            }

            lvDisplay.Columns[indexOfMobileColumn].Width = mobileColumnWidth;
            lvDisplay.EndUpdate();
        }

        /// <summary>
        /// 表示中のユーザー(作品表示中はNULL)
        /// </summary>
        public DB.Author ArrangedAuthor { get; private set; }

        /// <summary>
        /// 表示中の作品(ユーザー表示中はNULL)
        /// </summary>
        public DB.Title ArrangedTitle { get; private set; }

        /// <summary>
        /// 選ばれしもの
        /// </summary>
        public DB.DailyScore SelectedDayScore
        {
            get
            {
                if (lvDisplay.SelectedItems.Count == 1)
                {
                    return lvDisplay.SelectedItems[0].Tag as DB.DailyScore;
                }
                return null;
            }
        }

        /// <summary>
        /// 1項目分追加
        /// </summary>
        /// <param name="pv">PVデータを含むやつ</param>
        /// <param name="score">スコアデータを含むやつ</param>
        /// <param name="item">追加する対象ンリストビューアイテム</param>
        private static void registorScoreInfo(DB.DailyScore pv, DB.DailyScore score, DB.DailyScore diffScore, ListViewItem item)
        {
            item.Tag = score;
            item.UseItemStyleForSubItems = false;

            if (pv != null)
            {
                setPvColumnData(item, pv.PC, pv.PCUnique);
                setPvColumnData(item, pv.Mobile, pv.MobileUnique);
                setPvColumnData(item, pv.SmartPhone, pv.SmartPhoneUnique);
                setPvColumnData(item, pv.PageView, pv.UniquePageView);
            }
            else
            {
                item.SubItems.Add("no data");
                item.SubItems.Add("no data");
                item.SubItems.Add("no data");
                item.SubItems.Add("no data");
            }
            if (score.HasScoreInfo)
            {
                if ((diffScore == null) || (!diffScore.HasScoreInfo))
                {
                    //通常表示モード
                    if (score.Votes == 0)
                    {
                        item.SubItems.Add("none");
                    }
                    else
                    {
                        var voteScore = score.VoteAverage;
                        item.SubItems.Add($"{voteScore:0.0}({score.Votes}人)");
                    }
                    item.SubItems.Add($"{score.Bookmarks}");
                    item.SubItems.Add($"{score.Impressions}");
                    item.SubItems.Add($"{score.Reviews}");
                    item.SubItems.Add($"{score.DailyPoint}");
                    item.SubItems.Add($"{score.WeeklyPoint}");
                    item.SubItems.Add($"{score.MonthlyPoint}");
                    item.SubItems.Add($"{score.QuarterPoint}");
                    item.SubItems.Add($"{score.YearPoint}");
                    item.SubItems.Add($"{score.Points}");
                    item.SubItems.Add($"{score.Series}");
                    item.SubItems.Add($"{score.Size:#,0}");
                }
                else
                {
                    //比較表示モード
                    var signed = "+#,0;-#,0; ";
                    item.SubItems.Add($"{(score.VoteAverage - diffScore.VoteAverage).ToString("+#,0.0;-#,0.0")}({(score.Votes - diffScore.Votes).ToString("+#,0;-#,0;±0")}人)");
                    item.SubItems.Add($"{(score.Bookmarks - diffScore.Bookmarks).ToString(signed)}");
                    item.SubItems.Add($"{(score.Impressions - diffScore.Impressions).ToString(signed)}");
                    item.SubItems.Add($"{(score.Reviews - diffScore.Reviews).ToString(signed)}");
                    item.SubItems.Add($"{(score.DailyPoint - diffScore.DailyPoint).ToString(signed)}");
                    item.SubItems.Add($"{(score.WeeklyPoint - diffScore.WeeklyPoint).ToString(signed)}");
                    item.SubItems.Add($"{(score.MonthlyPoint - diffScore.MonthlyPoint).ToString(signed)}");
                    item.SubItems.Add($"{(score.QuarterPoint - diffScore.QuarterPoint).ToString(signed)}");
                    item.SubItems.Add($"{(score.YearPoint - diffScore.YearPoint).ToString(signed)}");
                    item.SubItems.Add($"{(score.Points - diffScore.Points).ToString(signed)}");
                    item.SubItems.Add($"{(score.Series - diffScore.Series).ToString(signed)}");
                    item.SubItems.Add($"{(score.Size - diffScore.Size).ToString(signed)}");
                }
            }
            else
            {
                for (var i = 0; i < 12; i++)
                {
                    item.SubItems.Add("");
                }
            }
            item.SubItems.Add($"{score.Event}");
        }

        /// <summary>
        /// PVのセルのデータ設定
        /// </summary>
        /// <param name="item">行</param>
        /// <param name="pv">Pv</param>
        /// <param name="unique">ユニークPv</param>
        /// <remarks>
        /// PV0ならUniqueは非表示＆グレーアウト
        /// </remarks>
        private static void setPvColumnData(ListViewItem item, int pv, int unique)
        {
            if (pv == 0)
            {
                item.SubItems.Add("0").ForeColor = Color.Gray;
            }
            else
            {
                item.SubItems.Add($"{pv:#,0}({unique})");
            }
        }
    }
}
