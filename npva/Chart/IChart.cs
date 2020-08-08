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
    /// チャートのインターフェース
    /// </summary>
    /// <remarks>
    /// 物理座標/物理サイズ： 描画対象領域のピクセル数と1:1で対応する
    /// 論理座標/論理サイズ： 軸の１と1:1で対応する。系列の値そのもの。
    /// </remarks>
    interface IChart
    {
        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="context"></param>
        void Draw(IDrawContext context);

        /// <summary>
        /// 描画領域のサイズ変更。引数はpx。
        /// </summary>
        /// <param name="pysicalhWidth"></param>
        /// <param name="physicalHeight"></param>
        void Resize(double pysicalhWidth, double physicalHeight);

        /// <summary>
        /// X軸
        /// </summary>
        Axis AxisX { get; set; }
        /// <summary>
        /// Y軸
        /// </summary>
        Axis AxisY1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        Axis AxisY2 { get; set; }

        /// <summary>
        /// 系列データの設定。
        /// </summary>
        /// <param name="targetAxis">Y1かY2。XはInvalidOperatiom</param>
        /// <param name=""></param>
        void ArrangeSeries(AxisType targetAxis, Series series);

        /// <summary>
        /// 論理座標Xが真ん中に来るようにする
        /// </summary>
        /// <param name="x"></param>
        /// <remarks>
        /// 論理座標Xとは、「日」単位の表示の場合は最初からの日数、「週」単位の表示の場合は最初からの週数。
        /// </remarks>
        void ScrollX(double x);
        /// <summary>
        /// 論理座標Xが左端に来るようにする
        /// </summary>
        /// <param name="x"></param>
        void ScrollX_Left(double x);
        /// <summary>
        /// 論理座標Xが右端に来るようにする
        /// </summary>
        /// <param name="x"></param>
        void ScrollX_Right(double x);

        /// <summary>
        /// 描画する物を設定
        /// </summary>
        /// <param name="obj"></param>
        void AddDrawableObject(IDrawableObject obj);

        /// <summary>
        /// 描画するものを除く
        /// </summary>
        /// <param name="obj"></param>
        void RemoveDrawableObject(IDrawableObject obj);

        /// <summary>
        /// ポインター位置通知
        /// </summary>
        /// <param name="px"></param>
        /// <param name="py"></param>
        void PointerSet(double px, double py);

        /// <summary>
        /// 指した場所の情報
        /// </summary>
        /// <param name="logicalX"></param>
        /// <returns></returns>
        string GetPointedString(double logicalX);

        /// <summary>
        /// プロットエリア上にカーソルがあるときのイベント
        /// </summary>
        event EventHandler<AxisPointedEventArgs> AxisPointed;
    }

    /// <summary>
    /// 描画領域ポイントされたイベント
    /// </summary>
    class AxisPointedEventArgs : EventArgs
    {
        /// <summary>
        /// 論理X
        /// </summary>
        public double X = 0;
        /// <summary>
        /// 論理Y1
        /// </summary>
        public double Y1 = 0;
        /// <summary>
        /// 論理Y2
        /// </summary>
        public double Y2 = 0;
    }


    /// <summary>
    /// 描画可能なクラスにはこれをつけて描画メソッドを実装する
    /// </summary>
    interface IDrawableObject
    {
        void Draw(IDrawContext context);
    }



}
