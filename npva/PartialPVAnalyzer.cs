using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using npva.DB;

namespace npva
{
    /// <summary>
    /// 部分別PV解釈
    /// </summary>
    class PartialPVAnalyzer
    {
        /// <summary>
        /// アナライザー列挙
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<PartialPVAnalyzer> CreatePartialPVAnalyzers()
        {
            yield return new PPVA_PartialTotal();
            yield return new PPVA_PartialByMonth();
            yield return new PPVA_PartialByMonth(6);
            yield return new PPVA_PartialByMonth(true);
            yield return new PPVA_PartialByMonth(true, 6);
        }

        /// <summary>
        /// このクラスの勝手なインスタンス化を阻止
        /// </summary>
        protected PartialPVAnalyzer() { }

        /// <summary>
        /// 解析結果をリストビューに入れる
        /// </summary>
        /// <param name="t"></param>
        /// <param name="listView"></param>
        public void Analyze(DB.Title t, ListView listView)
        {
            listView.Items.Clear();
            listView.Columns.Clear();
            analyze_execute(t, listView);
        }

        /// <summary>
        /// 解析結果をリストビューに入れる(入れる部分)
        /// </summary>
        /// <param name="t"></param>
        /// <param name="listView"></param>
        protected virtual void analyze_execute(DB.Title t, ListView listView)
        {

        }
    }


    /// <summary>
    /// 部位別総PV
    /// </summary>
    class PPVA_PartialTotal : PartialPVAnalyzer
    {

        protected override void analyze_execute(Title t, ListView listView)
        {
            var pv = new Dictionary<int, int>();
            foreach (var score in t.Score.Where(x => x.PartPvChecked))
            {
                foreach (var p in score.PartPv)
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
            listView.Items.Clear();
            listView.Columns.Clear();
            listView.Columns.Add("部分");
            listView.Columns.Add("合計PV");
            foreach (var p in pv.OrderBy(x => x.Key))
            {
                var lst = listView.Items.Add($"第{p.Key}部分");
                lst.SubItems.Add($"{p.Value:#,0}人");
            }
        }

        public override string ToString()
        {
            return "部位別総PV";
        }
    }

    /// <summary>
    /// 部位別PV/月間合計
    /// </summary>
    class PPVA_PartialByMonth : PartialPVAnalyzer
    {
        int range = 0;
        bool sumup = false;

        /// <summary>
        /// 全期間
        /// </summary>
        public PPVA_PartialByMonth()
        {
            range = 0;
        }

        /// <summary>
        /// 過去Nか月
        /// </summary>
        /// <param name="n"></param>
        public PPVA_PartialByMonth(int n)
        {
            range = n;
        }

        /// <summary>
        /// 累積
        /// </summary>
        /// <param name="sum"></param>
        public PPVA_PartialByMonth(bool sum)
        {
            sumup = sum;
            range = 0;
        }

        //過去Nか月累積
        public PPVA_PartialByMonth(bool sum, int n)
        {
            sumup = sum;
            range = n;
        }

        protected override void analyze_execute(Title t, ListView listView)
        {
            //総和数（TODO：：途中で挿話したり削除したらどうなるかはわからんです）
            var ms = t.LatestScore.Series;
            
            //全キーおよびカウンタ辞書
            var keys = getKeys(t);
            var ppvd = new Dictionary<string, int[]>();
            keys.ForEach(key => ppvd[key] = new int[ms + 1]);
 
            //解析
            countupPPV(t, ppvd);
            
            //累積化
            if (sumup) ResolveSumup(ppvd, keys, ms);

            //桁設定
            listView.Columns.Add("部分");

            keys.ForEach(x => listView.Columns.Add(x));

            //行設定(S=0は除外)
            for (var s = 1; s < ms + 1; s++)
            {
                var r = listView.Items.Add(s == 0 ? "index" : $"第{s}部分");
                foreach (var k in keys)
                {
                    if (ppvd.ContainsKey(k))
                    {
                        r.SubItems.Add(ppvd[k][s].ToString("#,0"));
                    }
                    else
                    {
                        r.SubItems.Add("");
                    }
                }
            }
        }

        /// <summary>
        /// 部分別を日付キーの辞書に話数の配列を入れて返す
        /// </summary>
        /// <param name="t">作品</param>
        /// <param name="ms">話数</param>
        /// <returns>カウント結果</returns>
        private void countupPPV(Title t, Dictionary<string, int[]> ppvd)
        {
            foreach (var score in t.Score.Where(x => x.PartPvChecked))
            {
                //scoreがrange以内か判定する
                if (score.Date < getFirstKeyDate(t)) continue;
                var mkey = DateToKey(score.Date);
                var pd = ppvd[mkey];
                var tpv = 0;
                foreach (var ppv in score.PartPv)
                {
                    tpv += ppv.PageView;
                    if (ppv.Part < pd.Length)
                    {
                        pd[ppv.Part] += ppv.PageView;
                    }
                }
                //部位別総計とその日のユニークの誤差＝目次のPV?なんか計算合わない（マイナスになるときがある）
                pd[0] = score.UniquePageView - tpv;
            }
        }

        /// <summary>
        /// 日付を月表記にする
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private string DateToKey(DateTime d)
        {
            return d.ToString("yyyy/MM");
        }

        /// <summary>
        /// 最初のキーになる日付を取得
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private DateTime getFirstKeyDate(Title t)
        {
            var firstDate = t.FirstUp.Date;
            if (range > 0)
            {
                firstDate = DateTime.Now.AddMonths(-range + 1);
            }
            firstDate = firstDate.AddDays(-firstDate.Day + 1);
            return firstDate;
        }

        /// <summary>
        /// 次のキーになる日付を取得
        /// </summary>
        /// <param name="firstDate"></param>
        /// <returns></returns>
        private DateTime getNextKeyDate(DateTime firstDate)
        {
            firstDate = firstDate.AddMonths(1);
            return firstDate;
        }

        /// <summary>
        /// 表示期間のキーリスト作成
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private List<string> getKeys(Title t)
        {
            var keys = new List<string>();
            DateTime date = getFirstKeyDate(t);
            while (date <= DateTime.Now.Date)
            {
                var k = DateToKey(date);
                keys.Add(k);
                date = getNextKeyDate(date);
            }
            return keys;
        }

        /// <summary>
        /// 加算解決
        /// </summary>
        /// <param name="ppvd"></param>
        private void ResolveSumup(Dictionary<string, int[]> ppvd, List<string> keys, int ms)
        {
            //2キー目以降から、直前のキーの値を加算していく。
            for (int i = 1; i < keys.Count; i++)
            {
                int[] pd = ppvd[keys[i - 1]];
                int[] cd = ppvd[keys[i]];

                //※pdとcdのindexの数は同じであるという前提があります

                for (var y = 0; y < pd.Length; y++)
                {
                    cd[y] += pd[y];
                }
            }
        }

        /// <summary>
        /// 表示名
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var r = range <= 0 ? "" : $"(過去{range}か月分)";
            var s = sumup ? "累積" : "月間";

            return $"{s}部位別PV{r}";
        }
    }

}
