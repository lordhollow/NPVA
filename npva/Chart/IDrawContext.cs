using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace npva.Chart
{
    /// <summary>
    /// 描画API。GDIだったりなんだったりで仮想化する。
    /// </summary>
    interface IDrawContext
    {
        /// <summary>
        /// クライアント領域矩形(物理座標(たぶん左上は0,0))
        /// </summary>
        RectangleF ClientRect { get; set; }

        /// <summary>
        /// プロット領域矩形(物理座標）
        /// </summary>
        RectangleF PlotArea { get; set; }

        /// <summary>
        /// 全消し
        /// </summary>
        void Clear();

        /// <summary>
        /// クリッピング領域の設定。この領域内にしか書けない。
        /// </summary>
        /// <param name="clipArea"></param>
        void SetClipingRect(RectangleF clipArea);

        /// <summary>
        /// 文字列の描画
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="h"></param>
        /// <param name="color"></param>
        /// <param name="s"></param>
        void DrawString(double x, double y, double h, Color color, string s, StringAlignment hAlign,StringAlignment vAlign);

        /// <summary>
        /// 線を引く
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        void DrawLine(PointF p1, PointF p2, double size, Color color);

        /// <summary>
        /// 矩形を描く
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        /// <param name="fillColor"></param>
        void DrawRect(RectangleF rect, double size, Color color, Color fillColor);

        /// <summary>
        /// 多角形を描く
        /// </summary>
        /// <param name="points"></param>
        /// <param name="closeArea"></param>
        /// <param name="color"></param>
        /// <param name="fillColor"></param>
        void DrawPolygon(IEnumerable<PointF> points, bool closeArea, double size, Color color, Color fillColor);

        /// <summary>
        /// 点を打つ
        /// </summary>
        /// <param name="p"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        void DrawPoint(PointF p, double size, Color color);
    }

    
    /// <summary>
    /// 文字列位置
    /// </summary>
    enum StringAlignment
    {
        /// <summary>
        /// 近いほう（左か上）
        /// </summary>
        Near,
        /// <summary>
        /// 真ん中
        /// </summary>
        Middle,
        /// <summary>
        /// 遠いほう（右か下）
        /// </summary>
        Far,
    }
}
