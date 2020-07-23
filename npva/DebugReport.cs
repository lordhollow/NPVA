using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npva
{
    /// <summary>
    /// デバッグ用レポート出力クラス
    /// </summary>
    class DebugReport
    {
        /// <summary>
        /// ログ
        /// </summary>
        static List<string> logs = new List<string>();

        /// <summary>
        /// 例外保存用
        /// </summary>
        /// <param name="e"></param>
        public static void ReportException(Exception e)
        {
            Log(e, e.Message);
        }

        /// <summary>
        /// ログ出す
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        public static void Log(object sender, string msg)
        {
            msg = $"{DateTime.Now} {msg}";
            logs.Add(msg);
            Console.WriteLine(msg);
            LogArrival?.Invoke(sender, new LogArrivalEventArgs { Message = msg });
        }

        /// <summary>
        /// ログ消す
        /// </summary>
        public static void LogClear()
        {
            logs.Clear();
        }

        /// <summary>
        /// ログ参照
        /// </summary>
        static public IEnumerable<string> Logs { get { return logs; } }

        /// <summary>
        /// なんかステータス
        /// </summary>
        public static string AnalyzerStatus { get; set; }

        /// <summary>
        /// ログ来たイベント
        /// </summary>
        public static event EventHandler<LogArrivalEventArgs> LogArrival;
    }

    /// <summary>
    /// ログ到着イベント
    /// </summary>
    class LogArrivalEventArgs : EventArgs
    {
        public string Message { get; set; }
    }

}
