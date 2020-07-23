using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace npva.DB
{
    /// <summary>
    /// 作品データ
    /// </summary>
    [XmlType("title")]
    public class Title
    {
        /// <summary>
        /// ID(Nコード)
        /// </summary>
        [XmlAttribute("id")]
        public string ID { get; set; }

        /// <summary>
        /// タイトル
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// 著者名
        /// </summary>
        [XmlAttribute("author")]
        public string Author { get; set; }

        /// <summary>
        /// 初回掲載日時
        /// </summary>
        [XmlAttribute("first")]
        public DateTime FirstUp { get; set; }

        /// <summary>
        /// 最終掲載日時
        /// </summary>
        [XmlAttribute("last")]
        public DateTime LastUp { get; set; }

        /// <summary>
        /// スコア情報最終確認日時
        /// </summary>
        [XmlAttribute("checked")]
        public DateTime LastCheck { get; set; }

        /// <summary>
        /// スコア情報
        /// </summary>
        /// <remarks>
        /// このリストを直接編集してはいけない。XmlSerializerでの利用の為編集可能としている。
        /// 編集した場合、日付に重複がなく日付順に並ぶようにNormalize()を実行すること。
        /// </remarks>
        [XmlElement("score")]
        public List<DailyScore> Score { get; set; } = new List<DailyScore>();

        /// <summary>
        /// 最終スコア情報
        /// </summary>
        public DailyScore LatestScore { get { return Score.FindLast(x => x.HasScoreInfo); } }

        /// <summary>
        /// n日前のスコア
        /// </summary>
        /// <param name="backlog"></param>
        /// <returns></returns>
        public DailyScore BackLogScore(int backlog)
        {
            var dt = DateTime.Now.Date.AddDays(-backlog);
            foreach(var e in Score)
            {
                if (e.HasScoreInfo && (e.Date.Date == dt))
                {
                    return e;
                }
            }
            return null;
        }

        /// <summary>
        /// PV
        /// </summary>
        public int PageView { get { return Score.Sum(x => x.PageView); } }

        /// <summary>
        /// ユニークPV
        /// </summary>
        public int UniquePageView { get { return Score.Sum(x => x.UniquePageView); } }

        /// <summary>
        /// 正規化（Scoreの日付の重複をなくし（後ろ優先）、日付順に並べる）
        /// </summary>
        public void Normalize()
        {
            //未実装です
            //※ちゃんと正しくMergeを使いこなしていれば基本的には不要な処理なので後回し。
            //  勝手にデータベースを書き換えたりするときに必要になる。
        }

        /// <summary>
        /// マージする
        /// </summary>
        /// <param name="merge">マージするもの</param>
        /// <remarks>
        /// この機能は、ロードした作品データにREST API分のデータをマージするための物なので、PV関連はノータッチ。
        /// </remarks>
        public void Merge(DB.Title merge)
        {
            //探す日付
            var mergeScore = merge.LatestScore;
            var findDate = mergeScore.Date.Date;

            //おんなじ日付があるか探す(後ろから探すのが早かろう)
            for (var i = Score.Count - 1; i >= 0; i--)
            {
                if (Score[i].Date.Date == findDate)
                {
                    Score[i].MergeScore(merge.LatestScore);
                    return;
                }
            }
            //見つからなかったので足す。
            Score.Add(merge.LatestScore);

            //日付を新しいほうにする
            if (LastCheck < merge.LastCheck)
            {
                LastCheck = merge.LastCheck;
            }
        }

        /// <summary>
        /// スコアデータ登録
        /// </summary>
        /// <param name="dailyValue"></param>
        /// <remarks>
        /// この機能は、ロードした作品データにPVデータをマージするための物なので、REST-API分のデータはノータッチ。
        /// </remarks>
        public void AddPageView(DailyScore pv, bool isUnique)
        {
            //探す日付
            var findDate = pv.Date.Date;

            //おんなじ日付があるか探す
            for (var i = 0; i < Score.Count; i++)
            {
                if (Score[i].Date.Date == findDate)
                {
                    //同じ日→マージ
                    if (isUnique)
                    {
                        Score[i].MergeUniquePageView(pv);
                    }
                    else
                    {
                        Score[i].MergePageView(pv);

                    }
                    return;
                }
                else if (Score[i].Date.Date > findDate)
                {
                    //過ぎた→インサート
                    Score.Insert(i, pv);
                    return;
                }
            }
            //最期に足す（普通はUpdate時のScoreデータがあるのでここには来ない)
            Score.Add(pv);
        }

        /// <summary>
        /// 表示(デバッグ用） Ncode + タイトル
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{ID} {Name}";
        }
    }
}
