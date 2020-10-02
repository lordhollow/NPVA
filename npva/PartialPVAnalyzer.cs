using System;
using System.Collections.Generic;
using System.Drawing;
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

            var conf = Properties.Settings.Default;
            if (!conf.PPVWeekFromMonday)
            {
                yield return new PPVA_PartialByMonth(PPVA_PartialByMonth.RangeType.WeekFromSunday, false, 6);
            }
            else
            {
                yield return new PPVA_PartialByMonth(PPVA_PartialByMonth.RangeType.WeekFromMonday, false, 6);
            }
            yield return new PPVA_PartialByMonth(PPVA_PartialByMonth.RangeType.Day, false, conf.PPVDays);
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
        RangeType rangeType = RangeType.Month;
        Func<DateTime, string> DateToKey;
        Func<Title, DateTime> getFirstKeyDate;
        Func<DateTime, DateTime> getNextKeyDate;

        /// <summary>
        /// rangeが表す区間
        /// </summary>
        public enum RangeType
        {
            /// <summary>
            /// 過去n月
            /// </summary>
            Month,
            /// <summary>
            /// 過去n週(週は日曜日はじまり)
            /// </summary>
            WeekFromSunday,
            /// <summary>
            /// 過去n週(週は月曜日はじまり)
            /// </summary>
            WeekFromMonday,
            /// <summary>
            /// 過去n日
            /// </summary>
            Day,
        }

        /// <summary>
        /// 全期間
        /// </summary>
        public PPVA_PartialByMonth()
        {
            range = 0;
            construct();
        }

        /// <summary>
        /// 過去Nか月
        /// </summary>
        /// <param name="n"></param>
        public PPVA_PartialByMonth(int n)
        {
            range = n;
            construct();
        }

        /// <summary>
        /// 累積
        /// </summary>
        /// <param name="sum"></param>
        public PPVA_PartialByMonth(bool sum)
        {
            sumup = sum;
            range = 0;
            construct();
        }

        /// <summary>
        /// 過去Nか月累積
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="n"></param>
        public PPVA_PartialByMonth(bool sum, int n)
        {
            sumup = sum;
            range = n;
            construct();
        }

        /// <summary>
        /// フルオプション
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sum"></param>
        /// <param name="n"></param>
        public PPVA_PartialByMonth(RangeType type, bool sum, int n)
        {
            rangeType = type;
            sumup = sum;
            range = n;
            construct();
        }

        /// <summary>
        /// 初期化(キー関連のデリゲート設定)
        /// </summary>
        private void construct()
        {
            switch (rangeType)
            {
                case RangeType.WeekFromSunday:
                    DateToKey = DateToKey_WeekFromSunday;
                    getFirstKeyDate = getFirstKeyDate_WeekFromSunday;
                    getNextKeyDate = getNextKeyDate_Week;
                    break;
                case RangeType.WeekFromMonday:
                    DateToKey = DateToKey_WeekFromMonday;
                    getFirstKeyDate = getFirstKeyDate_WeekFromMonday;
                    getNextKeyDate = getNextKeyDate_Week;
                    break;
                case RangeType.Day:
                    DateToKey = DateToKey_Day;
                    getFirstKeyDate = getFirstKeyDate_Day;
                    getNextKeyDate = getNextKeyDate_Day;
                    break;
                default:
                    DateToKey = DateToKey_Month;
                    getFirstKeyDate = getFirstKeyDate_Month;
                    getNextKeyDate = getNextKeyDate_Month;
                    break;
            }
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
            var columnMax = countupPPV(t, ppvd);

            //累積化
            if (sumup) ResolveSumup(ppvd, keys, ms);

            //桁設定
            listView.Columns.Add("部分");
            keys.ForEach(x => listView.Columns.Add(x));

            //行設定(S=0は除外)
            for (var s = 1; s < ms + 1; s++)
            {
                var r = listView.Items.Add(s == 0 ? "index" : $"第{s}部分");
                r.UseItemStyleForSubItems = sumup;
                foreach (var k in keys)
                {
                    if (ppvd.ContainsKey(k))
                    {
                        var subitem = r.SubItems.Add(ppvd[k][s].ToString("#,0"));
                        //累積でないときヒートマップ化する
                        if (!sumup)
                        {
                            subitem.BackColor = GetHeatmapColor(columnMax, ppvd[k][s]);
                        }
                    }
                    else
                    {
                        r.SubItems.Add("");
                    }
                }
            }
        }

        /// <summary>
        /// ヒートマップの色を取得
        /// </summary>
        /// <param name="columnMax"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        private Color GetHeatmapColor(int columnMax, int v)
        {
            var ratio = v * 1.0 / columnMax;
            var level = (int)(255.0 * ratio);
            level = 255 - level;
            return Color.FromArgb(255, level, level);
        }

        /// <summary>
        /// 部分別を日付キーの辞書に話数の配列を入れて返す
        /// </summary>
        /// <param name="t">作品</param>
        /// <param name="ppvd">カウント結果（期間表示文字をキーとした、部位別PV配列）</param>
        /// <returns>カウント結果に含まれる数字で最大の物（ただしその最小値は100）</returns>
        private int countupPPV(Title t, Dictionary<string, int[]> ppvd)
        {
            var max = 10;
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
                        if (max < pd[ppv.Part]) max = pd[ppv.Part];
                    }
                }
                //部位別総計とその日のユニークの誤差＝目次のPV?なんか計算合わない（マイナスになるときがある）
                pd[0] = score.UniquePageView - tpv;
            }
            return max;
        }

        /// <summary>
        /// 日付を月表記にする(Month)
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private string DateToKey_Month(DateTime d)
        {
            return d.ToString("yyyy/MM");
        }

        /// <summary>
        /// 日付を月表記にする(Week(Sun))
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private string DateToKey_WeekFromSunday(DateTime d)
        {
            d = d.AddDays(-(int)d.DayOfWeek);
            return d.ToString("yyyy/MM/dd");
        }

        /// <summary>
        /// 
        /// 日付を月表記にする(Week(Mun))
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private string DateToKey_WeekFromMonday(DateTime d)
        {
            d = d.AddDays(-(int)d.DayOfWeek + 1);
            return d.ToString("yyyy/MM/dd");
        }

        /// <summary>
        /// 日付を月表記にする(Week,Day)
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private string DateToKey_Day(DateTime d)
        {
            return d.ToString("yyyy/MM/dd");
        }
        
        /// <summary>
        /// 最初のキーになる日付を取得(Month)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private DateTime getFirstKeyDate_Month(Title t)
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
        /// 最初のキーになる日付を取得(Week(Sun))
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private DateTime getFirstKeyDate_WeekFromSunday(Title t)
        {
            var firstDate = t.FirstUp.Date;
            if (range > 0)
            {
                firstDate = DateTime.Now.Date.AddDays(((range - 1) * -7) + 1);
            }
            //DayOfWeekはSundayが0。日曜日始まりはそのまま引けばよい。
            var w = firstDate.DayOfWeek;
            firstDate = firstDate.AddDays(-(int)w);
            return firstDate;
        }

        /// <summary>
        /// 最初のキーになる日付を取得(Week(Sun))
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private DateTime getFirstKeyDate_WeekFromMonday(Title t)
        {
            var firstDate = t.FirstUp.Date;
            if (range > 0)
            {
                firstDate = DateTime.Now.Date.AddDays(((range - 1) * -7) + 1);
            }
            //DayOfWeekはSundayが0。月曜始まりは1加算
            var w = firstDate.DayOfWeek;
            firstDate = firstDate.AddDays(-(int)w + 1);
            return firstDate;
        }

        /// <summary>
        /// 最初のキーになる日付を取得(Day)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private DateTime getFirstKeyDate_Day(Title t)
        {
            var firstDate = t.FirstUp.Date;
            if (range > 0)
            {
                firstDate = DateTime.Now.Date.AddDays(-range+1);
            }
            return firstDate;
        }

        /// <summary>
        /// 次のキーになる日付を取得(Month)
        /// </summary>
        /// <param name="firstDate"></param>
        /// <returns></returns>
        private DateTime getNextKeyDate_Month(DateTime firstDate)
        {
            firstDate = firstDate.AddMonths(1);
            return firstDate;
        }

        /// <summary>
        /// 次のキーになる日付を取得(Week)
        /// </summary>
        /// <param name="firstDate"></param>
        /// <returns></returns>
        private DateTime getNextKeyDate_Week(DateTime firstDate)
        {
            firstDate = firstDate.AddDays(7);
            return firstDate;
        }
        
        /// <summary>
        /// 次のキーになる日付を取得(Day)
        /// </summary>
        /// <param name="firstDate"></param>
        /// <returns></returns>
        private DateTime getNextKeyDate_Day(DateTime firstDate)
        {
            firstDate = firstDate.AddDays(1);
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
            var rangeScript = "月間";
            var rangeUnit = "か月分";

            switch (rangeType)
            {
                case RangeType.WeekFromSunday:
                case RangeType.WeekFromMonday:
                    rangeScript = "週間";
                    rangeUnit = "週間";
                    break;
                case RangeType.Day:
                    rangeScript = "日間";
                    rangeUnit = "日分";
                    break;
            }


            var r = range <= 0 ? "" : $"(過去{range}{rangeUnit})";
            var s = sumup ? "累積" : rangeScript;

            return $"{s}部位別PV{r}";
        }
    }

}
