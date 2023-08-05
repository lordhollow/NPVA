using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace npva.Chart
{
    /// <summary>
    /// 系列データ(規定)
    /// </summary>
    class Series
    {
        /// <summary>
        /// 系列名
        /// </summary>
        public string Name { get; set; } = "no name";

        /// <summary>
        /// 描画処理(サブクラスで実装）
        /// </summary>
        /// <param name="context">描画コンテキスト</param>
        /// <param name="axisX">X軸</param>
        /// <param name="axisY">Y軸</param>
        public virtual void Draw(IDrawContext context, Axis axisX, Axis axisY)
        {
        }

        /// <summary>
        /// 指定論理位置の値を名前と一緒に文字列として得る
        /// </summary>
        /// <param name="logicalX"></param>
        /// <returns></returns>
        public virtual string GetValueString(double logicalX)
        {
            return Name;
        }

        /// <summary>
        /// 座標返還
        /// </summary>
        /// <param name="outPos"></param>
        /// <param name="logicalX"></param>
        /// <param name="logicalY"></param>
        /// <param name="axisX"></param>
        /// <param name="axisY"></param>
        /// <param name="plotBase">プロットエリア左上</param>
        protected void setPhisicalPos(ref PointF outPos, double logicalX, double logicalY, Axis axisX, Axis axisY, PointF plotBase)
        {
            outPos.X = axisX.LogicalValueToPhysicalValue(logicalX, plotBase);
            outPos.Y = axisY.LogicalValueToPhysicalValue(logicalY, plotBase);
        }
    }

    /// <summary>
    /// 系列データ(ジェネリック)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class GenericSeries<T> : Series
    {
        protected List<KeyValuePair<double, T>> items = new List<KeyValuePair<double, T>>();

        public void Add(double logicalX, T value)
        {
            var e = new KeyValuePair<double, T>(logicalX, value);
            items.Add(e);
        }
    }

    /// <summary>
    /// 通常データ系列。線グラフを書く。
    /// </summary>
    class SimpleSeries : GenericSeries<double>
    {
        /// <summary>
        /// この色で描画
        /// </summary>
        public Color Color = Color.Black;

        /// <summary>
        /// 線の幅
        /// </summary>
        public double Width = 2;

        /// <summary>
        /// GetValueStringで使う値のフォーマット
        /// </summary>
        public string ValueStringFormat = "#,0";

        /// <summary>
        /// 棒グラフで書く(falseの時は折れ線)
        /// </summary>
        public bool DrawBarChart { get; set; } = false;

        /// <summary>
        /// バー描画するときセンタリングされるかどうか
        /// </summary>
        public bool BarPositionCenter { get; set; } = true;


        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="context"></param>
        /// <param name="axisX"></param>
        /// <param name="axisY"></param>
        public override void Draw(IDrawContext context, Axis axisX, Axis axisY)
        {
            if (items.Count == 0) return;
            if (DrawBarChart)
            {
                drawBarChart(context, axisX, axisY);
            }
            else
            {
                drawLineChart(context, axisX, axisY);
            }
        }

        private void drawLineChart(IDrawContext context, Axis axisX, Axis axisY)
        {
            var plotBase = new PointF(context.PlotArea.Left, context.PlotArea.Top);
            var startP = new PointF();
            var endP = new PointF();
            setPhisicalPos(ref endP, items[0].Key, items[0].Value, axisX, axisY, plotBase);

            if (items.Count == 1)
            {
                context.DrawPoint(endP, Width, Color);
                return;
            }
            startP = endP;
            for (var i = 1; i < items.Count; i++)
            {
                setPhisicalPos(ref endP, items[i].Key, items[i].Value, axisX, axisY, plotBase);
                //前から1pxも動いてなかったら線を引かない
                if (((int)(startP.X - endP.X) != 0) || ((int)(startP.Y - endP.Y) != 0))
                {
                    context.DrawLine(startP, endP, Width, Color);
                    startP = endP;
                }
            }
        }

        private void drawBarChart(IDrawContext context, Axis axisX, Axis axisY)
        {
            var plotBase = new PointF(context.PlotArea.Left, context.PlotArea.Top);

            //まずx=1の幅を求める
            float dx = 0;
            {
                const int mergin = 2;               //マージン
                const float minWidh = 1f;           //最低幅
                var pX1 = new PointF();
                setPhisicalPos(ref pX1, axisX.LogicalMin + 1, 0, axisX, axisY, plotBase);  //x=1の場所
                dx = (pX1.X - plotBase.X - mergin); //1要素の幅(原点とx=1の差)
                if (dx <= minWidh) dx = minWidh;    //最低幅クリップ
            }
            var offset = BarPositionCenter ? dx / 2 : 0;

            //全部描く
            for (var i = 0; i < items.Count; i++)
            {
                if (items[i].Value > 0) //ゼロは書かない
                {
                    //箱座標
                    PointF pTop = new PointF();
                    setPhisicalPos(ref pTop, items[i].Key, items[i].Value, axisX, axisY, plotBase);
                    var rect = new RectangleF(pTop.X - offset, pTop.Y, dx, context.PlotArea.Bottom - pTop.Y);
                    //左端より出てる分を切り捨てる
                    if (rect.Left < plotBase.X)
                    {
                        var sub = plotBase.X - rect.Left;
                        rect.X = plotBase.X;
                        rect.Width -= sub;
                    }
                    //右端より出てる分を切り捨てる
                    if (rect.Right > context.PlotArea.Right)
                    {
                        rect.Width -= (rect.Right - context.PlotArea.Right);
                    }
                    //箱描画
                    context.DrawRect(rect, 1, Color, Color);
                }
            }

        }

        /// <summary>
        /// 指定位置文字列化
        /// </summary>
        /// <param name="logicalX"></param>
        /// <returns></returns>
        public override string GetValueString(double logicalX)
        {
            //logicalX以下の最大値を近傍とする。
            //これは、累計なら過去の最も近い日と同じ値、個別なら一致する日付があるはずで、
            //もっとも古いデータより以前のscoreはデータなしとすればよいため。
            KeyValuePair<double, double>? dt = null;
            foreach (var d in items)
            {
                if (d.Key <= logicalX)
                {
                    dt = d;
                }
                else
                {
                    break;
                }
            }
            if (dt != null)
            {
                return $"{Name}: " + dt.Value.Value.ToString(ValueStringFormat);
            }
            else
            {
                return $"{Name}: no data";
            }
        }

        /// <summary>
        /// 最大値を探す
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<double, double> FindMax()
        {
            if (items.Count == 0) return new KeyValuePair<double, double>(-1, 0);
            if (items.Count == 1) return items[0];
            var m = items[0];
            for (var i = 1; i < items.Count; i++)
            {
                if (items[i].Value > m.Value)
                {
                    m = items[i];
                }
            }
            return m;
        }

        /// <summary>
        /// 移動平均を新しい系列にして出す！
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        /// <remarks>
        /// 現時点では簡単のためにkey値ではなく系列データのサイズを指定するようにしています
        /// </remarks>
        public SimpleSeries GetMovingAverageProjection(int size, bool leftOnly)
        {
            var series = new SimpleSeries();
            for (var i = 0; i < items.Count; i++)
            {
                var avg = leftOnly ? getMovingAverageLeft(i, size) : getMovingAverage(i, size);
                if (avg != null)
                {
                    series.Add(items[i].Key, avg.Value);
                }
            }
            return series;
        }

        /// <summary>
        /// とある区間の移動平均（左側サンプル）
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        double? getMovingAverageLeft(int pos, int size)
        {
            //範囲外NULL
            if (pos < size) return null;
            if (pos > items.Count) return null;

            var sum = 0.0;
            for (var i = 0; i < size; i++)
            {
                sum += items[pos - i].Value;
            }
            return sum / size;
        }

        /// <summary>
        /// とある区間の移動平均(両側サンプル)
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        double? getMovingAverage(int pos, int size)
        {
            var half = size / 2;
            //データが足りてないので計算できない区画はNULL
            if (pos - half < 0) return null;
            if (pos + half >= items.Count) return null;

            //幅が奇数の時
            if ((size % 2) == 1)
            {
                var sum = 0.0;
                for (var i = pos - half; i < pos + half; i++)
                {
                    sum += items[i].Value;
                }
                return sum / size;
            }
            //幅が偶数の時
            else
            {
                var sum = items[pos - half].Value + items[pos + half].Value;
                for (var i = pos - half + 1; i < pos + half - 1; i++)
                {
                    sum += items[i].Value * 2;
                }
                return sum / (size + size - 2);
            }
        }
    }


    /// <summary>
    /// 配列データ。積み上げグラフを描く。
    /// </summary>
    class ArraySeries : GenericSeries<double[]>
    {
    }

    /// <summary>
    /// 幅があるデータ。箱線グラフを書く。
    /// </summary>
    class RangeSeries : GenericSeries<RangeSeriesItem>
    {
    }



    /// <summary>
    /// 幅があるデータ（１項目分）。
    /// </summary>
    struct RangeSeriesItem
    {
        /// <summary>
        /// 最小
        /// </summary>
        public double Min { get; set; }
        /// <summary>
        /// 最大
        /// </summary>
        public double Max { get; set; }
        /// <summary>
        /// 代表
        /// </summary>
        public double Value { get; set; }
    }

}
