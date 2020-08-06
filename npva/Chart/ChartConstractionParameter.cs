using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace npva.Chart
{
    /// <summary>
    /// チャート描画パラメータ
    /// </summary>
    class ChartConstractionParameter
    {
        /// <summary>
        /// ページビューの色
        /// </summary>
        public Color PageViewColor { get; set; } = Color.Blue;
        /// <summary>
        /// ユニークページビューの色
        /// </summary>
        public Color UniquePageViewColor { get; set; } = Color.DeepSkyBlue;

        /// <summary>
        /// ポイントの色の色
        /// </summary>
        public Color PointColor { get; set; } = Color.DeepPink;

        /// <summary>
        /// ページビュー移動平均の色
        /// </summary>
        public Color MovingAvgPageViewColor { get; set; } = Color.RoyalBlue;

        /// <summary>
        /// ユニークページビュー移動平均の色
        /// </summary>
        public Color MovingAvgUniquePageViewColor { get; set; } = Color.SkyBlue;

        /// <summary>
        /// PVの系列を含めない
        /// </summary>
        public bool ExcludePV { get; set; } = false;

        /// <summary>
        /// ユニークPVの系列を含めない
        /// </summary>
        public bool ExcludeUnique { get; set; } = false;

        /// <summary>
        /// スコアの系列を含めない
        /// </summary>
        public bool ExcludeScore { get; set; } = false;

        /// <summary>
        /// 移動平均を左だけでとる
        /// </summary>
        public bool MovingAverageByLeft { get; set; } = false;
    }
}
