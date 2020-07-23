using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using npva.DB;

namespace npva.Exporter
{
    /// <summary>
    /// AsIs(NPVA内部形式)で出力するエクスポータ
    /// </summary>
    class AsIsExporter : Exporter
    {
        /// <summary>
        /// 初期化
        /// </summary>
        public AsIsExporter()
        {
            SelectorType = OutputPathSelectorType.File;
            Filter = "*.xml|XMLファイル(*.xml)|*.*|すべて(*.*)";
            AcceptMultipleTitle = true;
            DefaultExt = "xml";
        }

        /// <summary>
        /// 表示名
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "XML(プロジェクト互換形式)";
        }

        /// <summary>
        /// 作品エクスポート
        /// </summary>
        /// <param name="author"></param>
        /// <param name="titles"></param>
        /// <param name="outPath"></param>
        /// <returns></returns>
        protected override bool ExportExecute(Author author, List<Title> titles, string outPath)
        {
            var exAuth = author.Clone();
            exAuth.Titles.RemoveAll(t => titles.Find(x => x.ID == t.ID) == null);
            exAuth.Save(outPath);
            return true;
        }
    }
}
