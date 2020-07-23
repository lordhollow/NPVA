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
        GDIDrawContext drawContext;

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
        /// ロード時初期化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GDIDrawSurface_Load(object sender, EventArgs e)
        {
            //特になし
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
            if (drawContext == null)
            {
                drawContext = new GDIDrawContext();
            }
            drawContext.ClientRect = new RectangleF(0, 0, Width, Height);
            drawContext.PaintHandler(e.Graphics);
            chart.Draw(drawContext);
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
