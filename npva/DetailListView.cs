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
        /// 初期化
        /// </summary>
        public DetailListView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 著者情報表示
        /// </summary>
        /// <param name=""></param>
        public void Arrange(DB.Author author)
        {
            chDate.Text = "Title";
            lvDisplay.Items.Clear();
            lvDisplay.SuspendLayout();
            foreach (var title in author.Titles)
            {
                var item = new ListViewItem($"{title.ID} {title.Name}");
                var score = title.LatestScore;
                if (score != null) registorScoreInfo(score, score, item);
                lvDisplay.Items.Add(item);
            }
            lvDisplay.ResumeLayout();
        }

        /// <summary>
        /// 作品データ表示
        /// </summary>
        /// <param name="title"></param>
        public void Arrange(DB.Title title)
        {
            chDate.Text = "Date";
            lvDisplay.Items.Clear();
            lvDisplay.SuspendLayout();
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
                var score = title.LatestScore;
                registorScoreInfo(pv, score, item);

                lvDisplay.Items.Add(item);
            }
            //個別(逆順)
            foreach (var score in (title.Score as IEnumerable<DB.DailyScore>).Reverse())
            {
                var item = new ListViewItem(score.Date.ToString("yyyy/MM/dd"));
                registorScoreInfo(score, score, item);
                lvDisplay.Items.Add(item);
            }

            lvDisplay.ResumeLayout();
        }

        /// <summary>
        /// 1項目分追加
        /// </summary>
        /// <param name="pv">PVデータを含むやつ</param>
        /// <param name="score">スコアデータを含むやつ</param>
        /// <param name="item">追加する対象ンリストビューアイテム</param>
        private static void registorScoreInfo(DB.DailyScore pv, DB.DailyScore score, ListViewItem item)
        {
            if (pv != null)
            {
                item.SubItems.Add($"{pv.PC:#,0}({pv.PCUnique})");
                item.SubItems.Add($"{pv.Mobile:#,0}({pv.MobileUnique})");
                item.SubItems.Add($"{pv.SmartPhone:#,0}({pv.SmartPhoneUnique})");
                item.SubItems.Add($"{pv.PageView:#,0}({pv.UniquePageView})");

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
                if (score.Votes == 0)
                {
                    item.SubItems.Add("none");
                }
                else
                {
                    var voteScore = score.VoteScore / (score.Votes * 2.0);
                    item.SubItems.Add($"{voteScore:0.0}x{score.Votes}");
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
        }
    }
}
