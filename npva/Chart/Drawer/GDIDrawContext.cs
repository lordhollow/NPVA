using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace npva.Chart.Drawer
{
    /// <summary>
    /// GDIによる描画
    /// </summary>
    /// <remarks>
    /// まだまだ適当です
    /// </remarks>
    class GDIDrawContext : IDrawContext
    {
        /// <summary>
        /// 描画先グラフィック
        /// </summary>
        Graphics graphics;

        /// <summary>
        /// グラフィック設定
        /// </summary>
        /// <param name="g"></param>
        public void PaintHandler(Graphics g)
        {
            graphics = g;
        }

        /// <summary>
        /// クライアント領域(グラフィックのサイズと同じ)
        /// </summary>
        public RectangleF ClientRect { get; set; }

        /// <summary>
        /// プロットエリア
        /// </summary>
        public RectangleF PlotArea { get; set; }

        /// <summary>
        /// 消去
        /// </summary>
        public void Clear()
        {
            graphics.Clear(Color.White);
        }

        /// <summary>
        /// 線を引く
        /// </summary>
        /// <param name="p1">ここから</param>
        /// <param name="p2">ここまで</param>
        /// <param name="size">太さ</param>
        /// <param name="color">色</param>
        public void DrawLine(PointF p1, PointF p2, double size, Color color)
        {
            using (var pen = new Pen(color, (float)size))
            {
                graphics.DrawLine(pen, p1, p2);
            }
        }

        /// <summary>
        /// 点を打つ
        /// </summary>
        /// <param name="p">ここに</param>
        /// <param name="size">この大きさの</param>
        /// <param name="color">色</param>
        public void DrawPoint(PointF p, double size, Color color)
        {
            using (var brush = new SolidBrush(color))
            {
                graphics.FillRectangle(brush, new RectangleF((float)(p.X - size / 2), (float)(p.Y - size / 2), (float)size, (float)size));
            }
        }

        /// <summary>
        /// 多角形描画（未実装）
        /// </summary>
        /// <param name="points"></param>
        /// <param name="closeArea"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        /// <param name="fillColor"></param>
        public void DrawPolygon(IEnumerable<PointF> points, bool closeArea, double size, Color color, Color fillColor)
        {
        }

        /// <summary>
        /// 四角形描画
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="size">線の太さ</param>
        /// <param name="color">色</param>
        /// <param name="fillColor">塗りつぶしの色</param>
        public void DrawRect(RectangleF rect, double size, Color color, Color fillColor)
        {
            if (fillColor != Color.Transparent)
            {
                using (var brush = new SolidBrush(fillColor))
                {
                    graphics.FillRectangle(brush, rect);
                }
            }
            if (color != fillColor)
            {   //同色なら箱を書き直す必要はないので
                using (var pen = new Pen(color, 1))
                {
                    graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
                }
            }
        }

        /// <summary>
        /// 文字列描画
        /// </summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        /// <param name="h">文字の大きさ（現在無効）</param>
        /// <param name="color">色</param>
        /// <param name="s">文字列</param>
        /// <param name="hAlign">横位置</param>
        /// <param name="vAlign">縦位置</param>
        public void DrawString(double x, double y, double h, Color color, string s, StringAlignment hAlign, StringAlignment vAlign)
        {
            var font = SystemFonts.CaptionFont;
            if ((hAlign != StringAlignment.Near) || (vAlign != StringAlignment.Near))
            {
                var sz = graphics.MeasureString(s, font);
                if (hAlign == StringAlignment.Middle)
                {
                    x -= sz.Width / 2;
                }
                else if (hAlign == StringAlignment.Far)
                {
                    x -= sz.Width;
                }
                if (vAlign == StringAlignment.Middle)
                {
                    y -= sz.Height / 2;
                }
                else if (vAlign == StringAlignment.Far)
                {
                    y -= sz.Height;
                }
            }
            using (var brush = new SolidBrush(color))
            {
                graphics.DrawString(s, SystemFonts.CaptionFont, brush, (float)x, (float)y);
            }
        }

        /// <summary>
        /// クリップ領域設定(現在無効）
        /// </summary>
        /// <param name="clipArea"></param>
        public void SetClipingRect(RectangleF clipArea)
        {
        }
    }

}
