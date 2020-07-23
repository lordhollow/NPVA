using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace npva.Chart
{

    /// <summary>
    /// 軸
    /// </summary>
    class Axis : IDrawableObject
    {
        /// <summary>
        /// 物理サイズ
        /// </summary>
        protected double physicalSize = 0;

        /// <summary>
        /// 倍率。1論理サイズ当たりの物理サイズ。
        /// </summary>
        public double Scale { get; private set; }

        /// <summary>
        /// 軸の種類
        /// </summary>
        public AxisType Type { get; set; }

        /// <summary>
        /// 論理最小
        /// </summary>
        public double LogicalMin { get; private set; }

        /// <summary>
        /// 論理最大
        /// </summary>
        public double LogicalMax { get; private set; }

        /// <summary>
        /// 物理最大幅
        /// </summary>
        public double PhysicalMax { get { return (LogicalMax - LogicalMin) * Scale; } }

        /// <summary>
        /// 論理値→表示名称変換
        /// </summary>
        public Func<double, string> Translator = x => x.ToString();

        /// <summary>
        /// TickのEnumerator(戻り値のタプルは論理値＋LargeTickかどうか)
        /// </summary>
        public Func<Axis, IEnumerable<Tuple<double, bool>>> TickEnumerator;

        /// <summary>
        /// 描画パラメータ
        /// </summary>
        public AxisDrawParameter Parameter = new AxisDrawParameter();

        /// <summary>
        /// 論理最大・最小の設定
        /// </summary>
        /// <param name="logicalMin"></param>
        /// <param name="logicalMax"></param>
        public void SetLogicalSize(double logicalMin, double logicalMax)
        {
            if (logicalMax == logicalMin) logicalMax = logicalMin + 1;

            LogicalMin = logicalMin;
            LogicalMax = logicalMax;
        }

        /// <summary>
        /// 物理オフセット(左端または下端の物理座標)
        /// </summary>
        public double PhysicalOffset { get; set; }

        /// <summary>
        /// 物理サイズ設定
        /// </summary>
        /// <param name="sz"></param>
        public void SetPhysicalSize(double sz)
        {
            physicalSize = sz;
            Scale = RecalcScale();
        }

        /// <summary>
        /// スケール再計算
        /// </summary>
        /// <returns></returns>
        protected virtual double RecalcScale()
        {
            return Scale;
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="context"></param>
        public void Draw(IDrawContext context)
        {
            if (Type == AxisType.X)
            {
                drawAxisX(context);
            }
            else
            {
                drawAxisY(context);
            }
        }

        /// <summary>
        /// X軸として描画
        /// </summary>
        /// <param name="context"></param>
        void drawAxisX(IDrawContext context)
        {
            //軸線
            var startPos = new PointF(context.PlotArea.Left, context.PlotArea.Bottom);
            var endPos = new PointF(context.PlotArea.Right, context.PlotArea.Bottom);
            context.DrawLine(startPos, endPos, 1, Parameter.Color);

            //キャプション
            context.DrawString(startPos.X, startPos.Y, Parameter.Font.Height, Parameter.Color, Translator(LogicalMin), StringAlignment.Middle, StringAlignment.Near);
            context.DrawString(endPos.X, endPos.Y, Parameter.Font.Height, Parameter.Color, Translator(LogicalMax), StringAlignment.Middle, StringAlignment.Near);

            //途中の線とキャプション
            if (TickEnumerator != null)
            {
                foreach (var tickInfo in TickEnumerator(this))
                {
                    var c = tickInfo.Item2 ? Parameter.WideGridColor : Parameter.NarrowGridColor;
                    var e = tickInfo.Item2 ? Parameter.DrawWideGrid : Parameter.DrawNarrowGrid;
                    var w = tickInfo.Item2 ? Parameter.DrawWideTickCaption : Parameter.DrawNarrowTickCaption;
                    var x = LogicalValueToPhysicalValue(tickInfo.Item1, context.PlotArea.Location);
                    if (e) context.DrawLine(new PointF(x, context.PlotArea.Top), new PointF(x, context.PlotArea.Bottom), 1, c);
                    if (w) context.DrawString(x, context.PlotArea.Bottom, Parameter.Font.Height, c, Translator(tickInfo.Item1), StringAlignment.Middle, StringAlignment.Near);

                }
            }

        }

        /// <summary>
        /// Y軸として描画
        /// </summary>
        /// <param name="context"></param>
        void drawAxisY(IDrawContext context)
        {
            //軸線
            var startPos = new PointF();
            var endPos = new PointF();
            var hAlign = StringAlignment.Far;
            startPos.Y = context.PlotArea.Top;
            endPos.Y = context.PlotArea.Bottom;
            if (Type == AxisType.YLeft)
            {
                startPos.X = endPos.X = context.PlotArea.Left;
                hAlign = StringAlignment.Far;
            }
            else
            {
                startPos.X = endPos.X = context.PlotArea.Right;
                hAlign = StringAlignment.Near;
            }
            context.DrawLine(startPos, endPos, 1, Parameter.Color);

            //キャプション
            context.DrawString(startPos.X, startPos.Y, Parameter.Font.Height, Parameter.Color, Translator(LogicalMax), hAlign, StringAlignment.Middle);
            context.DrawString(endPos.X, endPos.Y, Parameter.Font.Height, Parameter.Color, Translator(LogicalMin), hAlign, StringAlignment.Middle);

            //途中の線とキャプション
            if ((TickEnumerator != null))
            {
                foreach (var tickInfo in TickEnumerator(this))
                {
                    var c = tickInfo.Item2 ? Parameter.WideGridColor : Parameter.NarrowGridColor;
                    var e = tickInfo.Item2 ? Parameter.DrawWideGrid : Parameter.DrawNarrowGrid;
                    var w = tickInfo.Item2 ? Parameter.DrawWideTickCaption : Parameter.DrawNarrowTickCaption;
                    var y = LogicalValueToPhysicalValue(tickInfo.Item1, context.PlotArea.Location);
                    if (e) context.DrawLine(new PointF(context.PlotArea.Left, y), new PointF(context.PlotArea.Right, y), 1, c);
                    if (w) context.DrawString(startPos.X, y, Parameter.Font.Height, c, Translator(tickInfo.Item1), hAlign, StringAlignment.Middle);
                }
            }
        }

        /// <summary>
        /// 論理→物理変換
        /// </summary>
        /// <param name="logical">論理座標</param>
        /// <param name="plotBase">プロット領域左上</param>
        /// <returns></returns>
        public float LogicalValueToPhysicalValue(double logical, PointF plotBase)
        {
            var p = 0.0;
            if (Type == AxisType.X)
            {
                p = (logical - LogicalMin) * Scale + plotBase.X - PhysicalOffset;
            }
            else
            {
                p = (LogicalMax - logical) * Scale + plotBase.Y - PhysicalOffset;
            }
            return (float)p;
        }

        /// <summary>
        /// 物理→論理変換
        /// </summary>
        /// <param name="physical"></param>
        /// <param name="plotBase"></param>
        /// <returns></returns>
        public float PhysicalValueToLogicalValue(double physical, PointF plotBase)
        {
            var p = 0.0;
            if (Type==AxisType.X)
            {
                p = LogicalMin + ((physical - plotBase.X + PhysicalOffset) / Scale);
            }
            else
            {
                p = LogicalMax - ((physical - plotBase.Y + PhysicalOffset) / Scale);
            }
            return (float)p;
        }
    }




    /// <summary>
    /// 最大・最小を設定することによって論理サイズが変動するタイプの軸。全域表示のX軸や、普通のY軸。
    /// </summary>
    class FlexisbleAxis : Axis
    {
        protected override double RecalcScale()
        {
            return physicalSize / (LogicalMax - LogicalMin);
        }
    }

    /// <summary>
    /// 1論理サイズあたりの物理サイズが固定されているタイプの軸。日ごとや週ごと表示のX軸など。
    /// </summary>
    class FixedAxis : Axis
    {
        double fixedScale = 32;

        protected override double RecalcScale()
        {
            return fixedScale;
        }
    }


}
