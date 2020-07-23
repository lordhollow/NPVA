using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace npva
{
    static class Program
    {
        //hollow     435090
        //sengi      307964

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        /// <remarks>
        /// 基本的には、毎日決まった時間に -update 付きで起動してデータを蓄積して、
        /// 気が向いたときに普通に起動してUIで情報を確認する使い方。
        /// 決まった時間でなくてもいいが決まった時間のほうがAPI取得データ（感想とか）が２４時間ごとのデータになって安定する。
        /// （時刻データを持っていないので運用でカバー）
        /// 確認するユーザーを増やしたい時だけユーザーIDを入力して「更新」。
        /// 更新ボタンはいつでも有効だがあんまり使うケースはない。
        /// </remarks>
        [STAThread]
        static void Main()
        {
            bool update = false;
            foreach (var arg in Environment.GetCommandLineArgs())
            {
                if (arg == "-update")
                {
                    update = true;
                }
            }
            if (update)
            {
                DebugReport.LogArrival += (s, a) =>
                {
                    using (var f = System.IO.File.AppendText("log.txt"))
                    {
                        f.WriteLine(a.Message);
                    }
                };
                DebugReport.Log(null, "CUI Update Start.");
                var analyzer = new Analyzer();
                analyzer.UpdateAllStoredUser();
                DebugReport.Log(null, "CUI Update End.");
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new NPVAMain());
            }
        }
    }
}
