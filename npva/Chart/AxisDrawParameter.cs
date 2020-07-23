using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace npva.Chart
{
    /// <summary>
    /// 軸描画パラメータ
    /// </summary>
    class AxisDrawParameter
    {
        /// <summary>
        /// 目盛り幅どのぐらい取っておくか（縦軸のみ）。左右マージンに加算。
        /// </summary>
        public double TitleWidth = 50;

        /// <summary>
        /// 分割線を描く
        /// </summary>
        public bool DrawWideGrid = true;

        /// <summary>
        /// 細かい分割線を描く
        /// </summary>
        public bool DrawNarrowGrid = false;

        /// <summary>
        /// 分割線のキャプション（ワイド）
        /// </summary>
        public bool DrawWideTickCaption = true;

        /// <summary>
        /// 分割線のキャプション（ナロー）
        /// </summary>
        public bool DrawNarrowTickCaption = false;

        /// <summary>
        /// 細かい分割線の最小間隔(物理）
        /// </summary>
        public int NarrowTickMinimumPhysicalSize = 32;

        /// <summary>
        /// 枠線
        /// </summary>
        public Color Color = Color.Black;

        /// <summary>
        /// 大分割線
        /// </summary>
        public Color WideGridColor = Color.Gray;

        /// <summary>
        /// 小分割線
        /// </summary>
        public Color NarrowGridColor = Color.LightGray;

        /// <summary>
        /// 文字フォント
        /// </summary>
        public Font Font = SystemFonts.DialogFont;
    }

}
