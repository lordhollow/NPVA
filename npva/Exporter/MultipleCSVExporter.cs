using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using npva.DB;
using System.IO;

namespace npva.Exporter
{
    /// <summary>
    /// 複数ファイルCSVを出力するエクスポータ
    /// </summary>
    class MultipleCSVExporter : Exporter
    {
        /// <summary>
        /// 初期化
        /// </summary>
        public MultipleCSVExporter()
        {
            SelectorType = OutputPathSelectorType.Directory;
            Filter = "*.csv|csvファイル(*.csv)|*.*|すべて(*.*)";
            AcceptMultipleTitle = true;
            DefaultExt = "csv";
        }

        /// <summary>
        /// 表記
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "CSV(1作品1ファイル)";
        }

        /// <summary>
        /// 変換
        /// </summary>
        /// <param name="author">誰の</param>
        /// <param name="titles">何を</param>
        /// <param name="outPath">どこに</param>
        /// <returns></returns>
        protected override bool ExportExecute(Author author, List<Title> titles, string outPath)
        {
            //UTF-8で出力するので、タイトルにマルチバイト文字を入れないことで
            //データ部分には数字しか来ないので
            //ANSIのファイルとしてExcelでも普通に開けるようになるはず！
            var pickers = new List<TitleAndPicker>
            {
                new TitleAndPicker("date", x=>x.Date.ToString("yyyy/MM/dd")),
                new TitleAndPicker("PV(PC)", x=>simple(x.PC)),
                new TitleAndPicker("PV(Mobile)", x=>simple(x.Mobile)),
                new TitleAndPicker("PV(SmartPhone)", x=>simple(x.SmartPhone)),
                new TitleAndPicker("PV(Total)", x=>simple(x.PageView)),
                new TitleAndPicker("UniquePV(PC)", x=>simple(x.PCUnique)),
                new TitleAndPicker("UniquePV(Mobile)", x=>simple(x.MobileUnique)),
                new TitleAndPicker("UniquePV(SmartPhone)", x=>simple(x.SmartPhoneUnique)),
                new TitleAndPicker("UniquePV(Total)", x=>simple(x.UniquePageView)),
                new TitleAndPicker("Votes", x=>withDefault(x.Votes)),
                new TitleAndPicker("VoteScore", x=>withDefault(x.VoteScore)),
                new TitleAndPicker("AverageVoteScore", averageVote),
                new TitleAndPicker("Bookmarks", x=>withDefault(x.Bookmarks)),
                new TitleAndPicker("Impressions", x=>withDefault(x.Impressions)),
                new TitleAndPicker("Reviews", x=>withDefault(x.Reviews)),
                new TitleAndPicker("DailyPoint", x=>withDefault(x.DailyPoint)),
                new TitleAndPicker("WeeklyPoint", x=>withDefault(x.WeeklyPoint)),
                new TitleAndPicker("MonthlyPoint", x=>withDefault(x.MonthlyPoint)),
                new TitleAndPicker("QuarterPoint", x=>withDefault(x.QuarterPoint)),
                new TitleAndPicker("YearPoint", x=>withDefault(x.YearPoint)),
                new TitleAndPicker("Points", x=>withDefault(x.Points)),
                new TitleAndPicker("Series", x=>withDefault(x.Series)),
                new TitleAndPicker("Size", x=>withDefault(x.Size)),
            };

            foreach (var title in titles)
            {
                var dt = title.LastCheck.ToString("yyyyMMdd");
                var path = Path.Combine(outPath, $"{title.ID}_{dt}.{DefaultExt}");

                using (var file = new StreamWriter(path))
                {
                    //タイトル行
                    file.WriteLine(string.Join(",", pickers.Select(x => x.Title).ToArray()));

                    //スコア行
                    foreach (var score in title.Score)
                    {
                        file.WriteLine(string.Join(",", pickers.Select(x => x.Picker(score)).ToArray()));
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 単純に文字列化
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        static string simple(double v)
        {
            return v.ToString();
        }

        /// <summary>
        /// 負をデフォルト値として扱ってから文字列に、それ以外を数値に。
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        static string withDefault(double v)
        {
            if (v < 0) return "";
            return v.ToString();
        }

        /// <summary>
        /// 投票平均スコアを文字列化
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        static string averageVote(DailyScore score)
        {
            if(score.Votes<0)
            {
                return "";
            }
            else if (score.Votes == 0)
            {
                return "0.0";
            }
            else
            {
                var voteScore = score.VoteScore / (score.Votes * 2.0);
                return voteScore.ToString("0.0");
            }
        }

        /// <summary>
        /// 作品と文字列ピッカー
        /// </summary>
        class TitleAndPicker
        {
            /// <summary>
            /// 初期化
            /// </summary>
            /// <param name="t">作品</param>
            /// <param name="p">ピッカー</param>
            public TitleAndPicker(string t, Func<DailyScore, string> p)
            {
                Title = t;
                Picker = p;
            }
            
            /// <summary>
            /// 作品
            /// </summary>
            public string Title;

            /// <summary>
            /// ピッカー
            /// </summary>
            public Func<DailyScore, string> Picker;
        }

    }
}


