using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using npva.DB;

namespace npva.Chart
{
    /// <summary>
    /// チャートコンストラクタの基底クラス
    /// </summary>
    abstract class ChartConstructor
    {
        /// <summary>
        /// ファクトリメソッド
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ChartConstructor> CreateConstactors()
        {
            var conf = Properties.Settings.Default;

            if (!conf.CCIgnoreAllTime)
            {
                if (!conf.CCIgnoreTotalPv) yield return new BasicPvPointChartConstractor(true, false);
                if (!conf.CCIgnoreDailyPv) yield return new BasicPvPointChartConstractor(false, false);
                if (!conf.CCIgnoreMovingAverage)
                {
                    yield return new BasicPvPointChartConstractor(false, true);
                    yield return new BasicPvPointChartConstractor(false, true) { MovingAverageSize = 30 };
                    yield return new BasicPvPointChartConstractor(false, true) { MovingAverageSize = 90 };
                    yield return new BasicPvPointChartConstractor(false, true) { MovingAverageSize = 180 };
                    yield return new BasicPvPointChartConstractor(false, true) { MovingAverageSize = 365 };
                }
            }
            if (!conf.CCIgnoreBacklog7)
            {
                if (!conf.CCIgnoreTotalPv) yield return new BasicPvPointChartConstractor(7, true, false);
                if (!conf.CCIgnoreDailyPv) yield return new BasicPvPointChartConstractor(7, false, false);
            }
            if (!conf.CCIgnoreBacklog30)
            {
                if (!conf.CCIgnoreTotalPv) yield return new BasicPvPointChartConstractor(30, true, false);
                if (!conf.CCIgnoreDailyPv) yield return new BasicPvPointChartConstractor(30, false, false);
                if (!conf.CCIgnoreMovingAverage) yield return new BasicPvPointChartConstractor(30, false, true);
            }
            if (!conf.CCIgnoreBacklog90)
            {
                if (!conf.CCIgnoreTotalPv) yield return new BasicPvPointChartConstractor(90, true, false);
                if (!conf.CCIgnoreDailyPv) yield return new BasicPvPointChartConstractor(90, false, false);
                if (!conf.CCIgnoreMovingAverage) yield return new BasicPvPointChartConstractor(90, false, true);
            }
            if (!conf.CCIgnoreBacklog180)
            {
                if (!conf.CCIgnoreTotalPv) yield return new BasicPvPointChartConstractor(180, true, false);
                if (!conf.CCIgnoreDailyPv) yield return new BasicPvPointChartConstractor(180, false, false);
                if (!conf.CCIgnoreMovingAverage) yield return new BasicPvPointChartConstractor(180, false, true);
            }
            if (!conf.CCIgnoreBacklog365)
            {
                if (!conf.CCIgnoreTotalPv) yield return new BasicPvPointChartConstractor(365, true, false);
                if (!conf.CCIgnoreDailyPv) yield return new BasicPvPointChartConstractor(365, false, false);
                if (!conf.CCIgnoreMovingAverage) yield return new BasicPvPointChartConstractor(365, false, true);
            }

            Parameter.ExcludePV = conf.ChartExcludePV;
            Parameter.ExcludeUnique = conf.ChartExcludeUnique;
            Parameter.ExcludeScore = conf.ChartExcludeScore;
            Parameter.MovingAverageByLeft = conf.MovingAverageByLeft;
        }

        /// <summary>
        /// パラメータ
        /// </summary>
        public static ChartConstractionParameter Parameter { get; set; } = new ChartConstractionParameter();

        /// <summary>
        /// チャートを構築する
        /// </summary>
        /// <param name="chart"></param>
        public abstract void ConstractChart(IChart chart, DB.Title title);

        /// <summary>
        /// 最大値の自動決定
        /// </summary>
        /// <param name="v"></param>
        /// <param name="min"></param>
        /// <returns></returns>
        public static double CounterAutoScaleMax(double v, double min)
        {
            if (v < min) return min;
            var b = Math.Pow(10, Math.Floor(Math.Log10(v)));
            var r = b;
            while (r < v)
            {
                r += b / 10;
            }
            return r;
        }

        /// <summary>
        /// X軸: min=0, max=FirstUpからの経過日数とする可変軸
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        protected static Axis getXAxisAllTime(DB.Title title)
        {
            return GetXAxisFromSpecificatedDate(title.FirstUp, DateTime.Now);
        }

        /// <summary>
        /// 指定日付区間軸
        /// </summary>
        /// <param name="dFrom"></param>
        /// <param name="dEnd"></param>
        /// <returns></returns>
        protected static Axis GetXAxisFromSpecificatedDate(DateTime dFrom, DateTime dEnd)
        {
            var axis = new FlexisbleAxis();
            dFrom = dFrom.Date;
            dEnd = dEnd.Date;
            axis.SetLogicalSize(0, (dEnd - dFrom).TotalDays);
            axis.Type = AxisType.X;
            axis.Translator = x => "'" + (dFrom.AddDays(x).ToString("yy/MM/dd"));
            axis.TickEnumerator = (a) => DateTickPicker(a, dFrom, dEnd);
            return axis;

        }

        /// <summary>
        /// Y軸。min～maxが入り、maxの最小値が保証されmaxギリギリちょい上まであるY軸。左で返るので右用は別途プロパティを変更すること。
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="minimumMax"></param>
        /// <returns></returns>
        protected static Axis getStandardYAxis(double min, double max, double minimumMax, string arranged)
        {
            var axis = new FlexisbleAxis();
            axis.SetLogicalSize(min, CounterAutoScaleMax(max, minimumMax));
            axis.Type = AxisType.YLeft;
            axis.Translator = x => ((long)x).ToString("#,0" + arranged);
            axis.TickEnumerator = StandardTickPicker;
            return axis;
        }

        /// <summary>
        /// 標準軸Tick
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        private static IEnumerable<Tuple<double, bool>> StandardTickPicker(Axis a)
        {
            var keta = Math.Pow(10, Math.Floor(Math.Log10(a.LogicalMax)));
            if (keta == a.LogicalMax) keta /= 10;   //ぴったりの時Floorが仕事してないので上に出る
            for (var v = keta; v < a.LogicalMax; v += keta)
            {
                yield return new Tuple<double, bool>(v, true);
            }
        }

        /// <summary>
        /// 日付軸のTickピッカー
        /// </summary>
        /// <param name="a">軸</param>
        /// <param name="dayOfFirst">最初の一日</param>
        /// <returns></returns>
        private static IEnumerable<Tuple<double, bool>> DateTickPicker(Axis a, DateTime dayOfFirst, DateTime dayOfEnd)
        {
            var logicalSize = a.LogicalMax - a.LogicalMin;  //すべてのケースで = LogicalMaxだとは思われ

            var def = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(365, 12),
                new Tuple<int, int>(180, 6),
                new Tuple<int, int>(90, 3),
                new Tuple<int, int>(0, 1)
            };

            var tick = new DateTime(dayOfFirst.Year, 1, 1);
            foreach (var d in def)
            {
                if (logicalSize > d.Item1)
                {
                    while (tick < dayOfEnd)
                    {
                        if (tick > dayOfFirst)
                        {
                            yield return new Tuple<double, bool>((tick - dayOfFirst).TotalDays, true);
                        }
                        tick = tick.AddMonths(d.Item2);
                    }
                    break;
                }
            }
        }


        /// <summary>
        /// 最大値マーカー
        /// </summary>
        /// <param name="chart"></param>
        /// <param name="maxDate"></param>
        /// <param name="maxPv"></param>
        /// <returns></returns>
        protected static IDrawableObject GetMaxMarker(Axis aX, Axis aY, int maxDate, int maxPv)
        {
            return new AxisDependantString(aX, aY)
            {
                LogicalX = maxDate,
                LogicalY = maxPv,
                Message = $"最大: {aY.Translator(maxPv)}\r\n{aX.Translator(maxDate)}",
                Color = System.Drawing.Color.Red,
                HolizontalAlign = StringAlignment.Near,
                VerticalAlign = StringAlignment.Near,
            };
        }

        /// <summary>
        /// 最終更新日マーカー
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        protected static IDrawableObject GetLastUpMarker(Axis aX, Axis aY, int date)
        {
            return new AxisDependantString(aX, aY)
            {
                LogicalX = date,
                LogicalY = 0,
                Message = $"\r\n最終更新:{aX.Translator(date)}",
                Color = System.Drawing.Color.Purple,
                HolizontalAlign = StringAlignment.Middle,
                VerticalAlign = StringAlignment.Near,
                DrawVerticalLine = true,
            };

        }
    }
    
}