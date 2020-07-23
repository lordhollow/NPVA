using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npva.Exporter
{
    /// <summary>
    /// エクスポータ
    /// </summary>
    abstract class Exporter
    {
        /// <summary>
        /// ファクトリメソッド
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Exporter> GetExporters()
        {
            yield return new AsIsExporter();
            yield return new MultipleCSVExporter();
        }

        /// <summary>
        /// 出力先のダイアログのモード
        /// </summary>
        public OutputPathSelectorType SelectorType { get; protected set; } = OutputPathSelectorType.File;

        /// <summary>
        /// フィルタ
        /// </summary>
        public string Filter { get; protected set; } = "*.*|すべて(*.*)";

        /// <summary>
        /// デフォルト拡張子
        /// </summary>
        public string DefaultExt { get; protected set; }

        /// <summary>
        /// 複数ファイルを受け付けるか？
        /// </summary>
        public bool AcceptMultipleTitle { get; protected set; } = true;

        /// <summary>
        /// 進捗
        /// </summary>
        public IProgress<float> Progress { get; set; } = new Progress<float>();

        /// <summary>
        /// エクスポートの開始
        /// </summary>
        /// <param name="author"></param>
        /// <param name="titles"></param>
        public bool StartExport(DB.Author author, IEnumerable<DB.Title> titles)
        {
            var ts = titles.ToList();
            if (ts.Count == 0)
            {
                throw new ExportTitleNotSelectedException();
            }
            else if ((ts.Count != 1) && (!AcceptMultipleTitle))
            {
                throw new ExporterNotAcceptMultiTitleException();
            }

            Progress.Report(0);

            var nowStr = DateTime.Now.ToString("yyyy-mm-dd");
            var defaultName = $"{nowStr} {author.ID}.{DefaultExt}";

            var exportPath = (SelectorType == OutputPathSelectorType.File) ? getFileNameByDialog(defaultName) : getFolderNameByDialog();

            if (string.IsNullOrEmpty(exportPath))
            {
                //cancel return
                return false;
            }

            bool ret =  ExportExecute(author, ts, exportPath);
            Progress.Report(1);

            return ret;
        }

        /// <summary>
        /// ファイル名選択ダイアログ表示
        /// </summary>
        /// <param name="defaultName"></param>
        /// <returns></returns>
        private string getFileNameByDialog(string defaultName)
        {
            var ofd = new System.Windows.Forms.SaveFileDialog();
            ofd.FileName = defaultName;
            ofd.Filter = Filter;
            if (ofd.ShowDialog()== System.Windows.Forms.DialogResult.OK)
            {
                return ofd.FileName;
            }
            return "";
        }

        /// <summary>
        /// フォルダ名選択ダイアログ表示
        /// </summary>
        /// <returns></returns>
        private string getFolderNameByDialog()
        {
            var ofd = new System.Windows.Forms.FolderBrowserDialog();
            if (ofd.ShowDialog()== System.Windows.Forms.DialogResult.OK)
            {
                return ofd.SelectedPath;
            }
            return "";
        }

        /// <summary>
        /// エクスポート実作業
        /// </summary>
        /// <param name="author">誰の</param>
        /// <param name="titles">何を(author.Titlesのサブセット)</param>
        /// <param name="outPath">どこに</param>
        /// <returns></returns>
        protected abstract bool ExportExecute(DB.Author author, List<DB.Title> titles, string outPath);
    }

    /// <summary>
    /// ファイルとディレクトリどちらを設定必要か
    /// </summary>
    enum OutputPathSelectorType
    {
        /// <summary>
        /// ファイルを選択
        /// </summary>
        File,
        /// <summary>
        /// ディレクトリ(フォルダ)を選択
        /// </summary>
        Directory,
    }

    /// <summary>
    /// 作品未選択例外
    /// </summary>
    class ExportTitleNotSelectedException : Exception { }

    /// <summary>
    /// 複数作品出力未対応例外
    /// </summary>
    class ExporterNotAcceptMultiTitleException : Exception { }

}
