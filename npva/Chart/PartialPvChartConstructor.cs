using npva.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npva.Chart
{
    /// <summary>
    /// 部位別Pvグラフ表示のコンストラクター
    /// </summary>
    class PartialPvChartConstructor : ChartConstructor
    {
        /// <summary>
        /// いつからの累計？
        /// </summary>
        public DateTime Start { get; set; } = DateTime.MinValue;

        /// <summary>
        /// いつまで？
        /// </summary>
        public DateTime End { get; set; } = DateTime.MaxValue;

        public override void ConstractChart(IChart chart, Title title)
        {
            //データ作成
            var pvmap = new Dictionary<int, int>();
            foreach (var score in title.Score)
            {
                if ((score.PartPvChecked) && (score.Date >= Start) && (score.Date <= End))
                {
                    foreach (var ppv in score.PartPv)
                    {
                        if (pvmap.ContainsKey(ppv.Part))
                        {
                            pvmap[ppv.Part] += ppv.PageView;
                        }
                        else
                        {
                            pvmap[ppv.Part] = ppv.PageView;
                        }
                    }
                }
            }

            //系列
            var uv = new SimpleSeries { Color = Parameter.UniquePageViewColor, Name = "unique", DrawBarChart = true, BarPositionCenter = false };
            var maxPart = title.LatestScore.Series;
            var maxUv = 10;
            for (var i = 1; i <= maxPart; i++)
            {
                if (pvmap.ContainsKey(i))
                {
                    uv.Add(i, pvmap[i]);
                    if (maxUv < pvmap[i]) maxUv = pvmap[i];
                }
            }
            chart.ArrangeSeries(AxisType.YLeft, uv);

            //X軸
            var Xaxis = new FlexisbleAxis();
            Xaxis.SetLogicalSize(1, maxPart + 1);
            Xaxis.Type = AxisType.X;
            Xaxis.Translator = x => $"第{x}部分";
            Xaxis.TickEnumerator = a => PartPicker(a, maxPart);
            chart.AxisX = Xaxis;

            //Y軸
            chart.AxisY1 = chart.AxisY2 = getStandardYAxis(0, maxUv, 10, "");

        }

        public override string ToString()
        {
            return "部位別Pv累計";
        }

        static IEnumerable<Tuple<double, bool>> PartPicker(Axis a, int max)
        {
            var def = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(1000,500),
                new Tuple<int, int>(500,50),
                new Tuple<int, int>(100, 10),
                new Tuple<int, int>(50,5),
                new Tuple<int, int>(10,2),
                new Tuple<int, int>(1,1),
            };
            var p = 1;
            foreach (var d in def)
            {
                if (max > d.Item1)
                {
                    while (p <= max)
                    {
                        yield return new Tuple<double, bool>(p, true);
                        p += d.Item2;
                    }
                    break;
                }
            }
        }


    }
}
