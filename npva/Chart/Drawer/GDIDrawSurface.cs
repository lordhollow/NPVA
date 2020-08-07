using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace npva.Chart.Drawer
{
    /// <summary>
    /// GDIで描画する画面
    /// </summary>
    partial class GDIDrawSurface : UserControl
    {
        public GDIDrawSurface()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 作品
        /// </summary>
        DB.Title title;
        /// <summary>
        /// チャート
        /// </summary>
        IChart chart;
        /// <summary>
        /// 描画手段（GDI）
        /// </summary>
        GDIDrawContext drawContext = new GDIDrawContext();

        /// <summary>
        /// バックサーフェス
        /// </summary>
        Bitmap backSurface;

        /// <summary>
        /// バックサーフェスGDI
        /// </summary>
        Graphics backSurfaceGraphics;


        /// <summary>
        /// タイトル設定
        /// </summary>
        /// <param name="constractor">チャート構築アルゴリズム</param>
        public void Arrange(DB.Title title, ChartConstructor constractor)
        {
            this.title = title;
            if (constractor != null)
            {
                chart = ChartFactory.CreateChart();
                constractor.ConstractChart(chart, title);
                chart.Resize(ClientSize.Width, ClientSize.Height);
                //chart.AxisPointed += (s, a) => Console.WriteLine($"Logical {a.X}, {a.Y1}, {a.Y2}");
                Invalidate(ClientRectangle);
            }
        }

        /// <summary>
        /// チャートコンストラクタ切り替え
        /// </summary>
        /// <param name="constractor"></param>
        public void Arrange(ChartConstructor constractor)
        {
            if ((constractor != null) && (title != null))
            {
                chart = ChartFactory.CreateChart();
                constractor.ConstractChart(chart, title);
                chart.Resize(ClientSize.Width, ClientSize.Height);
                Invalidate(ClientRectangle);
            }
        }

        /// <summary>
        /// 画像を保存する(見たまま)
        /// </summary>
        /// <param name="f"></param>
        public void SaveImage(string f)
        {
            backSurface.Save(f, System.Drawing.Imaging.ImageFormat.Png);
        }

        /// <summary>
        /// 保存時のサイズ指定
        /// </summary>
        /// <param name="f"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        public void SaveImage(ChartConstructor constractor, string f, int w, int h)
        {
            //今表示している内容に悪影響を及ぼさないようにゼロから構築して保存
            var bmp = new Bitmap(w, h);
            var d = new GDIDrawContext();
            var g = Graphics.FromImage(bmp);
            var c = ChartFactory.CreateChart();

            constractor.ConstractChart(c, title);
            c.Resize(w, h);
            d.ClientRect = new RectangleF(0, 0, w, h);
            d.PaintHandler(g);
            c.Draw(d);
            bmp.Save(f, System.Drawing.Imaging.ImageFormat.Png);
        }

        /// <summary>
        /// ロード時初期化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GDIDrawSurface_Load(object sender, EventArgs e)
        {
            backSurface = new Bitmap(ClientSize.Width, ClientSize.Height);
            backSurfaceGraphics = Graphics.FromImage(backSurface);
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GDIDrawSurface_Paint(object sender, PaintEventArgs e)
        {
            if (chart == null) return;
            //ここからグラフの更新
            drawContext.ClientRect = new RectangleF(0, 0, Width, Height);
            drawContext.PaintHandler(backSurfaceGraphics);
            chart.Draw(drawContext);
            e.Graphics.DrawImage(backSurface, new Point());
        }

        /// <summary>
        /// リサイズ時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GDIDrawSurface_Resize(object sender, EventArgs e)
        {
            if (chart != null)
            {
                backSurface = new Bitmap(ClientSize.Width, ClientSize.Height);
                backSurfaceGraphics = Graphics.FromImage(backSurface);
                chart.Resize(ClientSize.Width, ClientSize.Height);
                Invalidate(ClientRectangle);
            }
        }

        /// <summary>
        /// マウス動かしたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GDIDrawSurface_MouseMove(object sender, MouseEventArgs e)
        {
            chart?.PointerSet(e.X, e.Y);
        }

    }
}
