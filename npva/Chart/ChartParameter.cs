using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace npva.Chart
{
    /// <summary>
    /// チャートの描画パラメータ
    /// </summary>
    class ChartParameter
    {
        /// <summary>
        /// 全部のサイズだとかのパラメータにかかる倍率(今のところ未使用)
        /// </summary>
        /// <remarks>
        /// これが1のとき、たとえばMerginTop=16だと上に16pxの隙間。2なら32pxの隙間。
        /// </remarks>
        public double GlobalScale = 1.0;

        /// <summary>
        /// 上マージン
        /// </summary>
        public double MergineTop = 16;
        /// <summary>
        /// 左マージン
        /// </summary>
        public double MergineLeft = 16;
        /// <summary>
        /// 下マージン
        /// </summary>
        public double MergineBottom = 24;
        /// <summary>
        /// 右マージン
        /// </summary>
        public double MergineRight = 64;

        /// <summary>
        /// 背景色
        /// </summary>
        public Color BackgroundColor = Color.White;

        /// <summary>
        /// 描画領域最小サイズ幅
        /// </summary>
        public double MinimumContentWidh = 128;
        /// <summary>
        /// 描画領域最小サイズ高さ
        /// </summary>
        public double MinimumContentHeight = 128;
    }

}
