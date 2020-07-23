using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace npva.Chart
{
    /// <summary>
    /// 論理座標で場所を決める文字列ラベル
    /// </summary>
    class AxisDependantString : IDrawableObject
    {
        /// <summary>
        /// 軸を指定してオブジェクトを作成
        /// </summary>
        /// <param name="axisX">依存軸X</param>
        /// <param name="axisY">依存軸Y</param>
        public AxisDependantString(Axis axisX, Axis axisY)
        {
            AxisX = axisX;
            AxisY = axisY;
        }

        /// <summary>依存軸X</summary>
        Axis AxisX;
        /// <summary>依存軸Y</summary>
        Axis AxisY;

        /// <summary>
        /// 表示位置X
        /// </summary>
        public double LogicalX { get; set; }
        /// <summary>
        /// 表示位置Y
        /// </summary>
        public double LogicalY { get; set; }
        /// <summary>
        /// 表示する文字列
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// ラベル及び線の色
        /// </summary>
        public Color Color = Color.Gray;

        /// <summary>
        /// 座標に対する文字列の水平位置
        /// </summary>
        public StringAlignment HolizontalAlign { get; set; } = StringAlignment.Near;
        /// <summary>
        /// 座標に対する文字列の垂直位置
        /// </summary>
        public StringAlignment VerticalAlign { get; set; } = StringAlignment.Near;

        /// <summary>
        /// 水平線を引くか
        /// </summary>
        public bool DrawHolizontalLine { get; set; } = false;
        /// <summary>
        /// 垂直線を引くか
        /// </summary>
        public bool DrawVerticalLine { get; set; } = false;

        /// <summary>
        /// 描画処理の実施
        /// </summary>
        /// <param name="context"></param>
        public void Draw(IDrawContext context)
        {
            //位置決め
            var x = AxisX.LogicalValueToPhysicalValue(LogicalX, context.PlotArea.Location);
            var y = AxisY.LogicalValueToPhysicalValue(LogicalY, context.PlotArea.Location);
            //ラベル
            context.DrawString(x, y, 16, Color, Message, HolizontalAlign, VerticalAlign);
            //水平線
            if (DrawHolizontalLine)
            {
                context.DrawLine(new PointF(context.PlotArea.Left, y), new PointF(context.PlotArea.Right, y), 1, Color);
            }
            //垂直線
            if (DrawVerticalLine)
            {
                context.DrawLine(new PointF(x, context.PlotArea.Top), new PointF(x, context.PlotArea.Bottom), 1, Color);
            }
        }
    }

}
