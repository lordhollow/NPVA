using System;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Collections.Generic;


namespace npva.DB
{
    /// <summary>
    /// ある一日の情報
    /// </summary>
    [XmlType("daily")]
    public class DailyScore
    {
        /// <summary>
        /// データがないことを示す値
        /// </summary>
        public const int NoData = -1;

        /// <summary>
        /// 日付（日付のみ有効）
        /// </summary>
        [XmlAttribute("date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// PCアクセス数
        /// </summary>
        [XmlAttribute("p")]
        public int PC { get; set; }

        /// <summary>
        /// PCユニーク
        /// </summary>
        [XmlAttribute("up")]
        public int PCUnique { get; set; }

        /// <summary>
        /// モバイルアクセス数
        /// </summary>
        [XmlAttribute("m")]
        public int Mobile { get; set; }

        /// <summary>
        /// モバイルアクセスユニーク
        /// </summary>
        [XmlAttribute("um")]
        public int MobileUnique { get; set; }

        /// <summary>
        /// スマホアクセス数
        /// </summary>
        [XmlAttribute("s")]
        public int SmartPhone { get; set; }

        /// <summary>
        /// スマホユニーク
        /// </summary>
        [XmlAttribute("su")]
        public int SmartPhoneUnique { get; set; }

        /// <summary>
        /// トータル
        /// </summary>
        public int PageView { get { return PC + Mobile + SmartPhone; } }

        /// <summary>
        /// トータルユニーク
        /// </summary>
        public int UniquePageView { get { return PCUnique + MobileUnique + SmartPhoneUnique; } }

        /// <summary>
        /// 部分別PV取得済み？
        /// </summary>
        [XmlAttribute("ppv")]
        [DefaultValue(false)]
        public bool PartPvChecked { get; set; }

        /// <summary>
        /// 部分別PV
        /// </summary>
        [XmlElement("part")]
        public List<PartPv> PartPv { get; set; } = new List<PartPv>();

        // 以下は、データ更新を行った日の分だけしかデータがない

        /// <summary>
        /// スコア関連情報があるか？
        /// </summary>
        public bool HasScoreInfo { get { return Series != NoData; } }

        /// <summary>
        /// 話数
        /// </summary>
        [XmlAttribute("ga")]
        [DefaultValue(NoData)]
        public int Series { get; set; } = NoData;

        /// <summary>
        /// 文字数
        /// </summary>
        [XmlAttribute("l")]
        [DefaultValue(NoData)]
        public int Size { get; set; } = NoData;

        /// <summary>
        /// 会話率
        /// </summary>
        [XmlAttribute("ka")]
        [DefaultValue(NoData)]
        public int ConversationRatio { get; set; } = NoData;

        /// <summary>
        /// 感想の数
        /// </summary>
        [XmlAttribute("imp")]
        [DefaultValue(NoData)]
        public int Impressions { get; set; } = NoData;

        /// <summary>
        /// レビューの数
        /// </summary>
        [XmlAttribute("r")]
        [DefaultValue(NoData)]
        public int Reviews { get; set; } = NoData;

        /// <summary>
        /// 評価の数
        /// </summary>
        [XmlAttribute("ah")]
        [DefaultValue(NoData)]
        public int Votes { get; set; } = NoData;

        /// <summary>
        /// 評価点
        /// </summary>
        [XmlAttribute("a")]
        [DefaultValue(NoData)]
        public int VoteScore { get; set; } = NoData;

        /// <summary>
        /// ブックマークの数
        /// </summary>
        [XmlAttribute("f")]
        [DefaultValue(NoData)]
        public int Bookmarks { get; set; } = NoData;

        /// <summary>
        /// 日間ポイント
        /// </summary>
        [XmlAttribute("dp")]
        [DefaultValue(NoData)]
        public int DailyPoint { get; set; } = NoData;

        /// <summary>
        /// 週間ポイント
        /// </summary>
        [XmlAttribute("wp")]
        [DefaultValue(NoData)]
        public int WeeklyPoint { get; set; } = NoData;

        /// <summary>
        /// 月間ポイント
        /// </summary>
        [XmlAttribute("mp")]
        [DefaultValue(NoData)]
        public int MonthlyPoint { get; set; } = NoData;

        /// <summary>
        /// 季間ポイント
        /// </summary>
        [XmlAttribute("qp")]
        [DefaultValue(NoData)]
        public int QuarterPoint { get; set; } = NoData;

        /// <summary>
        /// 年間ポイント
        /// </summary>
        [XmlAttribute("yp")]
        [DefaultValue(NoData)]
        public int YearPoint { get; set; } = NoData;

        /// <summary>
        /// トータルポイント
        /// </summary>
        [XmlAttribute("gp")]
        [DefaultValue(NoData)]
        public int Points { get; set; } = NoData;

        /// <summary>
        /// PV部分のマージ
        /// </summary>
        /// <param name="pv"></param>
        public void MergePageView(DailyScore pv)
        {
            if (Date.Date != pv.Date.Date) throw new InvalidOperationException("Date Mismatch");

            PC = pv.PC;
            Mobile = pv.Mobile;
            SmartPhone = pv.SmartPhone;
            //ユニークは別HTMLから取るので別関数です
        }

        /// <summary>
        /// ユニークPV部分のマージ
        /// </summary>
        /// <param name="pv"></param>
        public void MergeUniquePageView(DailyScore pv)
        {
            if (Date.Date != pv.Date.Date) throw new InvalidOperationException("Date Mismatch");
            PCUnique = pv.PCUnique;
            MobileUnique = pv.MobileUnique;
            SmartPhoneUnique = pv.SmartPhoneUnique;
        }

        /// <summary>
        /// スコア部分のマージ
        /// </summary>
        /// <param name="score"></param>
        public void MergeScore(DailyScore score)
        {
            if (Date.Date != score.Date.Date) throw new InvalidOperationException("Date Mismatch");

            Series = score.Series;
            Size = score.Size;
            ConversationRatio = score.ConversationRatio;
            Impressions = score.Impressions;
            Reviews = score.Reviews;
            Votes = score.Votes;
            VoteScore = score.VoteScore;
            Bookmarks = score.Bookmarks;
            DailyPoint = score.DailyPoint;
            WeeklyPoint = score.WeeklyPoint;
            MonthlyPoint = score.MonthlyPoint;
            QuarterPoint = score.QuarterPoint;
            YearPoint = score.YearPoint;
            Points = score.Points;
        }

    }
}
