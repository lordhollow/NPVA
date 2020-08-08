using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using npva.DB;

namespace npva.Chart
{
    /// <summary>
    /// 過去N日のPV(左軸)とポイント(右軸)のグラフ
    /// </summary>
    class BasicPvPointChartConstractor : ChartConstructor
    {
        /// <summary>
        /// 何日さかのぼり？（０は全期間）
        /// </summary>
        public int BackLogSize { get; private set; }

        /// <summary>
        /// PV累計？
        /// </summary>
        public bool PVSumUp { get; private set; }

        /// <summary>
        /// 移動平均モード(累計時無効)
        /// </summary>
        public bool MovingAvg { get; private set; }

        /// <summary>
        /// 移動平均サイズ
        /// </summary>
        public int MovingAverageSize { get; set; } = 7;

        /// <summary>
        /// 全期間？
        /// </summary>
        private bool IsAllTime { get; set; }

        /// <summary>
        /// 全期間
        /// </summary>
        /// <param name="PvSumup">累計モード</param>
        /// <param name="avg">移動平均モード(累計モードのほうが優先)</param>
        public BasicPvPointChartConstractor(bool PvSumup, bool avg)
        {
            BackLogSize = 0;
            PVSumUp = PvSumup;
            IsAllTime = true;
            MovingAvg = avg;
            if (PvSumup) MovingAvg = false;
        }

        /// <summary>
        /// 過去N日間
        /// </summary>
        /// <param name="backlog">区間長(LastCheckからの日数)</param>
        /// <param name="PvSumup">累計モード</param>
        /// <param name="avg">移動平均モード(累計モードのほうが優先)</param>
        public BasicPvPointChartConstractor(int backlog, bool PvSumup, bool avg)
        {
            BackLogSize = backlog;
            PVSumUp = PvSumup;
            IsAllTime = false;
            MovingAvg = avg;
            if (PvSumup) MovingAvg = false;
        }

        /// <summary>
        /// チャート作成
        /// </summary>
        /// <param name="chart"></param>
        /// <param name="title"></param>
        public override void ConstractChart(IChart chart, Title title)
        {
            DateTime BaseDate;
            if (IsAllTime)
            {
                BaseDate = title.FirstUp.Date;
                BackLogSize = (int)(title.LastCheck.Date - BaseDate.Date).TotalDays;
            }
            else
            {
                BaseDate = title.LastCheck.Date.AddDays(-BackLogSize);
            }


            //系列
            var sPv = new SimpleSeries() { Color = Parameter.PageViewColor, Name = "PageView" };
            if (!MovingAvg && !Parameter.ExcludePV) chart.ArrangeSeries(AxisType.YLeft, sPv);
            var sUPv = new SimpleSeries() { Color = Parameter.UniquePageViewColor, Name = "Unique" };
            if (!MovingAvg && !Parameter.ExcludeUnique) chart.ArrangeSeries(AxisType.YLeft, sUPv);
            var sPt = new SimpleSeries() { Color = Parameter.PointColor, Name="Points" };

            //累計グラフはゼロ点を設定しておく(初回にPV!=0になるところからしか線が出ないため
            if (PVSumUp)
            {
                sPv.Add(0, 0);
                sUPv.Add(0, 0);
            }

            double pv = 0;
            double upv = 0;
            int ll = 0;
            foreach (var score in title.Score)
            {
                if (score.Date >= BaseDate)   //基準日以降
                {
                    var l = (int)(score.Date.Date - BaseDate).TotalDays;
                    if (PVSumUp)
                    {
                        pv += score.PageView;
                        upv += score.UniquePageView;
                        sPv.Add(l, pv);
                        sUPv.Add(l, upv);

                    }
                    else
                    {
                        for (; ll < l; ll++)
                        {   //スコアない期間
                            sPv.Add(ll, 0);
                            sUPv.Add(ll, 0);
                        }
                        ll = l + 1;

                        sPv.Add(l, score.PageView);
                        sUPv.Add(l, score.UniquePageView);
                    }
                    if (score.HasScoreInfo)
                    {
                        sPt.Add(l, score.Points);
                    }
                }
            }

            double avgPvMax = 0;
            if (MovingAvg)
            {
                //七日間移動平均
                var avgPV = sPv.GetMovingAverageProjection(MovingAverageSize, Parameter.MovingAverageByLeft);
                avgPV.Color = Parameter.MovingAvgPageViewColor;
                avgPV.Name = "[Avg.]PageView";
                avgPV.ValueStringFormat = "#,0.0";
                //avgPV.Width = 1;
                if (!Parameter.ExcludePV) chart.ArrangeSeries(AxisType.YLeft, avgPV);
                avgPvMax = avgPV.FindMax().Value;

                var avgUPV = sUPv.GetMovingAverageProjection(MovingAverageSize, Parameter.MovingAverageByLeft);
                avgUPV.Color = Parameter.MovingAvgUniquePageViewColor;
                avgUPV.Name = "[Avg.]Unique";
                avgUPV.ValueStringFormat = "#,0.0";
                //avgUPV.Width = 1;
                if (!Parameter.ExcludeUnique) chart.ArrangeSeries(AxisType.YLeft, avgUPV);

            }

            //移動平均より後ろにポイントが来るようにここでチャートに追加
            if (!Parameter.ExcludeScore) chart.ArrangeSeries(AxisType.YRight, sPt);

            var pvMax = sPv.FindMax();  //PV最大値

            //X軸
            chart.AxisX = GetXAxisFromSpecificatedDate(BaseDate.Date, title.LastCheck.Date);

            //PV軸(y1)
            if (PVSumUp)
            {
                //累計時
                chart.AxisY1 = getStandardYAxis(0, pv, 100, "");
            }
            else if (MovingAvg)
            {
                //移動平均時
                chart.AxisY1 = getStandardYAxis(0, avgPvMax, 10, "");
            }
            else
            {
                //日別時
                chart.AxisY1 = getStandardYAxis(0, pvMax.Value, 10, "");
            }

            //PT軸(y2)
            var ptMax = sPt.FindMax();  //ポイント最大値
            chart.AxisY2 = getStandardYAxis(0, ptMax.Value, 100, "pt");
            chart.AxisY2.Parameter.Color = chart.AxisY2.Parameter.WideGridColor = Parameter.PointColor;
            chart.AxisY2.Parameter.DrawWideGrid = chart.AxisY2.Parameter.DrawNarrowGrid = false;

            //最大値マーカー
            if (!PVSumUp && !MovingAvg) //累計と移動平均では無効
            {
                chart.AddDrawableObject(GetMaxMarker(chart.AxisX, chart.AxisY1, (int)pvMax.Key, (int)pvMax.Value));
            }

            //最終更新日マーカー
            if (BaseDate < title.LastUp)
            {
                chart.AddDrawableObject(GetLastUpMarker(chart.AxisX, chart.AxisY1, (int)(title.LastUp.Date - BaseDate).TotalDays));
            }

            //初回更新マーカー必要？
            if (BaseDate < title.FirstUp)
            {
            }
        }

        /// <summary>
        /// 文字列表示（リスト表示用）
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var tm = IsAllTime ? "全期間" : $"{BackLogSize}日間";
            var sm = PVSumUp ? "累計" : "";
            var ma = MovingAvg ? $"{MovingAverageSize}日移動平均" : "";
            var po = " +Point";
            return $"{tm}{sm}PV{ma}{po}";
        }
    }

}
