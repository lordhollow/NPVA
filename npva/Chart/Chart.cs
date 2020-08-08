using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npva.Chart
{
    /// <summary>
    /// チャート
    /// </summary>
    class Chart : IChart
    {
        /// <summary>物理幅</summary>
        double pW;
        /// <summary>物理高</summary>
        double pH;
        /// <summary>描画範囲</summary>
        System.Drawing.RectangleF plotArea;

        /// <summary>描画する物</summary>
        List<IDrawableObject> drawObjs;
        /// <summary>X軸</summary>
        Axis axisX;
        /// <summary>左Y軸</summary>
        Axis axisY1;
        /// <summary>右Y軸</summary>
        Axis axisY2;
        /// <summary>系列</summary>
        List<Tuple<AxisType, Series>> seriesList = new List<Tuple<AxisType, Series>>();


        /// <summary>X軸</summary>
        public Axis AxisX { get { return axisX; } set { axisX = value; value.Type = AxisType.X; } }
        /// <summary>左Y軸</summary>
        public Axis AxisY1 { get { return axisY1; } set { axisY1 = value; value.Type = AxisType.YLeft; } }
        /// <summary>右Y軸</summary>
        public Axis AxisY2 { get { return axisY2; } set { axisY2 = value; value.Type = AxisType.YRight; } }

        /// <summary>描画パラメータ</summary>
        public ChartParameter Parameter = new ChartParameter();

        /// <summary>
        /// 描画域刺されたイベント
        /// </summary>
        public event EventHandler<AxisPointedEventArgs> AxisPointed;

        /// <summary>
        /// 系列の割り当て
        /// </summary>
        /// <param name="targetAxis"></param>
        /// <param name="series"></param>
        public void ArrangeSeries(AxisType targetAxis, Series series)
        {
            var p = new Tuple<AxisType, Series>(targetAxis, series);
            seriesList.Add(p);
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="context"></param>
        public void Draw(IDrawContext context)
        {
            context.Clear();
            context.PlotArea = plotArea;

            //context.DrawRect(plotArea, 1, System.Drawing.Color.Gray, System.Drawing.Color.Gray);

            axisX?.Draw(context);
            axisY1?.Draw(context);
            axisY2?.Draw(context);
            foreach (var s in seriesList)
            {
                s.Item2.Draw(context, AxisX, s.Item1 == AxisType.YLeft ? AxisY1 : AxisY2);
            }

            if (drawObjs != null)
            {
                foreach (var obj in drawObjs)
                {
                    obj.Draw(context);
                }
            }
        }

        /// <summary>
        /// サイズ変更
        /// </summary>
        /// <param name="phisicalWidth"></param>
        /// <param name="phisicalHeight"></param>
        public void Resize(double phisicalWidth, double phisicalHeight)
        {
            pW = phisicalWidth;
            pH = phisicalHeight;

            //幅
            var axisWidth = phisicalWidth - Parameter.MergineLeft - Parameter.MergineRight;
            axisWidth -= axisY1 == null ? 0 : axisY1.Parameter.TitleWidth;
            axisWidth -= axisY2 == null ? 0 : axisY2.Parameter.TitleWidth;
            if (axisWidth < Parameter.MinimumContentWidh) axisWidth = Parameter.MinimumContentWidh;
            AxisX?.SetPhysicalSize(axisWidth);
            //高さ
            var axisHeight = phisicalHeight - Parameter.MergineTop - Parameter.MergineBottom;
            axisHeight -= AxisX.Parameter.Font.Size;    //これ単位がよくわからない
            if (axisHeight < Parameter.MinimumContentHeight) axisHeight = Parameter.MinimumContentHeight;
            axisY1?.SetPhysicalSize(axisHeight);
            axisY2?.SetPhysicalSize(axisHeight);

            var pAreaX = Parameter.MergineLeft + (axisY1 == null ? 0 : axisY1.Parameter.TitleWidth);
            var pAreaY = Parameter.MergineTop;

            plotArea = new System.Drawing.RectangleF((float)pAreaX, (float)pAreaY, (float)axisWidth, (float)axisHeight);
        }

        /// <summary>
        /// x地点表示
        /// </summary>
        /// <param name="x"></param>
        public void ScrollX(double x)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// x地点を左端に
        /// </summary>
        /// <param name="x"></param>
        public void ScrollX_Left(double x)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// x地点を右端に
        /// </summary>
        /// <param name="x"></param>
        public void ScrollX_Right(double x)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 描画する物を設定
        /// </summary>
        /// <param name="obj"></param>
        public void AddDrawableObject(IDrawableObject obj)
        {
            if (drawObjs == null)
            {
                drawObjs = new List<IDrawableObject>();
            }
            drawObjs.Add(obj);
        }

        /// <summary>
        /// 描画するものを除く
        /// </summary>
        /// <param name="obj"></param>
        public void RemoveDrawableObject(IDrawableObject obj)
        {
            if (drawObjs == null) return;
            drawObjs.Remove(obj);
        }

        /// <summary>
        /// ポインタセット
        /// </summary>
        /// <param name="px"></param>
        /// <param name="py"></param>
        public void PointerSet(double px, double py)
        {
            if (AxisPointed != null)
            {
                if (plotArea.Contains((float)px, (float)py))
                {
                    var arg = new AxisPointedEventArgs
                    {
                        X = axisX != null ? axisX.PhysicalValueToLogicalValue(px, plotArea.Location) : 0,
                        Y1 = axisY1 != null ? axisY1.PhysicalValueToLogicalValue(py, plotArea.Location) : 0,
                        Y2 = axisY1 != null ? axisY2.PhysicalValueToLogicalValue(py, plotArea.Location) : 0
                    };

                    AxisPointed.Invoke(this, arg);
                }
            }
        }

        /// <summary>
        /// 指定論理X軸に対応する系列のデータを取得
        /// </summary>
        /// <param name="logicalX">論理</param>
        public string GetPointedString(double logicalX)
        {
            var x = axisX.Translator(logicalX) + Environment.NewLine;
            foreach(var series in seriesList)
            {
                x += series.Item2.GetValueString(logicalX) + Environment.NewLine;
            }
            return x;
        }
    }

}
