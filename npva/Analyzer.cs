using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace npva
{
    /// <summary>
    /// アナライザ
    /// </summary>
    class Analyzer
    {
        /// <summary>
        ///　データベースの保存場所
        /// </summary>
        public readonly string StoragePath = System.IO.Path.Combine(Application.StartupPath, "db/");

        /// <summary>
        /// アクセス解析のページにアクセスする頻度(秒)
        /// </summary>
        /// <remarks>
        /// DDOSしないためのマナーとしての設定です。
        /// </remarks>
        public int KasasagiIntervalSec = 1;

        /// <summary>
        /// 現在触っているデータ
        /// </summary>
        private DB.Author authorInfo;

        /// <summary>
        /// カレントのクローンを取得
        /// </summary>
        /// <remarks>
        /// 非同期メソッドの追加により、Currentにいつ手が入るかわからない状態になっているため、
        /// ＵＩスレッドではこのオブジェクトを使うべきである。
        /// </remarks>
        public DB.Author Current
        {
            get
            {
                if (authorInfo == null) return null;
                return authorInfo.Clone();
            }
        }

        /// <summary>
        /// オリジナルの作品データ
        /// </summary>
        /// <param name="t"></param>
        /// <returns>見つからなかったらNULL</returns>
        /// <remarks>
        /// UIデータはauthorInfoのCloneを利用するのを基本とするが、
        /// 全体でなく作品単位で更新などをしようとするとき、更新できなくなってしまうので、
        /// それを解決するためにこれを使ってauthorInfoの同一作品を得てそれを使う。
        /// 同一性はNコードで見ている。
        /// </remarks>
        public DB.Title getOriginalTitle(DB.Title t)
        {
            return authorInfo.Titles.Find(x => x.ID == t.ID);
        }

        /// <summary>
        /// 分析開始
        /// </summary>
        public event EventHandler AnalyzingStart;

        /// <summary>
        /// 分析終了
        /// </summary>
        public event EventHandler AnalyzingEnd;

        /// <summary>
        /// PVのURLを最期に叩いた時間
        /// </summary>
        public static DateTime LastKasasagiDate = new DateTime();

        /// <summary>
        /// 保存されているデータの一覧を取得
        /// </summary>
        public IEnumerable<string> StoredAuthorInfo
        {
            get
            {
                var reg = new Regex(@"^\d+$", RegexOptions.IgnoreCase);
                if (Directory.Exists(StoragePath))
                {
                    foreach (var file in Directory.GetFiles(StoragePath, "*.xml", SearchOption.TopDirectoryOnly))
                    {
                        var d = Path.GetFileNameWithoutExtension(file);
                        if (reg.IsMatch(d))
                        {
                            yield return d;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 指定IDのデータをロード
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Load(string id)
        {
            lock (lockObj)
            {
                var path = Path.Combine(StoragePath, $"{id}.xml");
                if (File.Exists(path))
                {
                    using (var f = new StreamReader(path))
                    {
                        var ser = new XmlSerializer(typeof(DB.Author));
                        var auth = ser.Deserialize(f) as DB.Author;
                        if (auth != null)
                        {
                            authorInfo = auth;
                            DebugReport.Log(this, $"Author DB Loaded {path}");
                            return true;
                        }
                    }
                }
                DebugReport.Log(this, $"Author DB Load failed {path}");
            }
            return false;
        }

        /// <summary>
        /// 現在のデータをセーブ
        /// </summary>
        public void Save()
        {
            lock (lockObj)
            {
                var path = Path.Combine(StoragePath, $"{authorInfo.ID}.xml");
                if (!Directory.Exists(StoragePath))
                {
                    DebugReport.Log(this, $"DB folder Created {path}");
                    Directory.CreateDirectory(StoragePath);
                }
                authorInfo.Save(path);
                DebugReport.Log(this, $"Author DB wrote {path}");
            }
        }

        /// <summary>
        /// 全員分更新
        /// </summary>
        /// <returns></returns>
        public void UpdateAllStoredUser()
        {
            AnalyzingLockContext(() =>
            {
                var users = StoredAuthorInfo.ToArray();
                var currentUID = authorInfo?.ID;
                foreach (var user in users)
                {
                    UpdateUserData(user, true);
                    Save();
                }
                if (!string.IsNullOrEmpty(currentUID))
                {
                    Load(currentUID);
                }
            });
        }

        /// <summary>
        /// 基本情報更新
        /// </summary>
        /// <param name="id">ユーザーID</param>
        public void UpdateUserData(string id, bool updatePv)
        {
            AnalyzingLockContext(() =>
            {
                //カレントと違うものなら、一度消してロードする。
                if((authorInfo == null) || (authorInfo.ID != id))
                {
                    authorInfo = null;
                    Load(id);
                }

                //解析API準備
                var narou = new NarouAPI();
                var info = narou.GetAuthorInfo(id);

                //PVデータ初回参照
                if (authorInfo == null)
                {
                    authorInfo = info;
                    authorInfo.Normalize();

                    if (updatePv) GetPVData(true);
                }
                //当月度&先月度分PVデータ参照
                else
                {
                    //マージ
                    authorInfo.Merge(info);

                    if (updatePv)
                    {
                        //今月
                        var dt = DateTime.Now;
                        GetPVData(dt.Year, dt.Month, false);

                        //今日が1,2,3日であれば先月も見る(ユニークが反映されてないかもなので）
                        if (dt.Day <= 3)
                        {
                            //先月
                            dt = dt.AddMonths(-1);
                            GetPVData(dt.Year, dt.Month, false);
                        }
                    }
                }

                //著者名解決
                authorInfo.ResolveAuthorName();
                authorInfo.CheckedDate = DateTime.Now;
            });
        }

        /// <summary>
        /// 全員分更新（非同期）
        /// </summary>
        /// <returns></returns>
        public async Task UpdateAllStoredUserAsync()
        {
            var task = Task.Run(() => UpdateAllStoredUser());
            await task;

        }

        /// <summary>
        /// 基本情報更新(非同期)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task UpdateBasicInfoAsync(string id)
        {
            var task = Task.Run(() => UpdateUserData(id, true));
            await task;
        }

        /// <summary>
        /// PVデータ取り込み(全作品全期間)(非同期)
        /// </summary>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public async Task GetPVDataAsync(bool useCache)
        {
            var task = Task.Run(() => GetPVData(useCache));
            await task;
        }

        /// <summary>
        /// 日別PV取得(非同期）
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task AnalyzePartPvAsync(DB.Title t)
        {
            var task = Task.Run(() => AnalyzePartPv(t));
            await task;
        }

        /// <summary>
        /// PVデータ取り込み（全作品の初投稿月～今月）
        /// </summary>
        public void GetPVData(bool useCache)
        {
            AnalyzingLockContext(() =>
            {
                foreach (var title in authorInfo.Titles)
                {
                    GetPVData(title, useCache);
                }
            });
        }

        /// <summary>
        /// PVデータ取り込み（指定作品の初投稿月～今月）
        /// </summary>
        public void GetPVData(DB.Title title, bool useCache)
        {
            AnalyzingLockContext(() =>
            {
                var dt = DateTime.Now;
                var endDt = new DateTime(dt.Year, dt.Month, 1); //当月1日
                var startDt = new DateTime(title.FirstUp.Year, title.FirstUp.Month, 1);//初投稿月1日

                do
                {
                    GetPVData(title, startDt.Year, startDt.Month, useCache);  //更新・・・
                    startDt = startDt.AddMonths(1); //翌月へ
                } while (startDt <= endDt);
            });
        }

        /// <summary>
        /// PVデータを取り込み(全作品月指定)
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        public void GetPVData(int year, int month, bool useCache)
        {
            AnalyzingLockContext(() =>
            {
                foreach (var title in authorInfo.Titles)
                {
                    GetPVData(title, year, month, useCache);
                }
            });
        }

        /// <summary>
        /// PVデータ取り込み（タイトル・月指定）
        /// </summary>
        /// <param name="title"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        public void GetPVData(DB.Title title, int year, int month, bool useCache)
        {
            AnalyzingLockContext(() =>
            {
                var waitFor = LastKasasagiDate.AddSeconds(KasasagiIntervalSec);
                var waitMsec = (waitFor - DateTime.Now).TotalMilliseconds;
                if (waitMsec > 0)
                {
                    DebugReport.Log(this, $"kasasagi wait {waitMsec}");
                    System.Threading.Thread.Sleep((int)waitMsec);
                }
                var narou = new NarouAPI();
                var useKasasagi = narou.UpdatePVData(title, year, month, useCache);
                if (useKasasagi)
                {
                    LastKasasagiDate = DateTime.Now;
                }
            });
        }

        /// <summary>
        /// 部分別PV取得(全日程）
        /// </summary>
        /// <param name="title"></param>
        public void AnalyzePartPv(DB.Title title)
        {
            AnalyzingLockContext(() =>
            {
                foreach(var s in title.Score)
                {
                    if (!s.PartPvChecked && (s.PageView!=0))
                    {
                        AnalyzePartPv(title, s);
                    }
                }
            });
        }

        /// <summary>
        /// 部分別PV取得
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        public void AnalyzePartPv(DB.Title title, int year, int month, int day)
        {
            //複数の操作に分かれるものではないのでロックしない
            var daily = title.GetScore(new DateTime(year, month, day));
            AnalyzePartPv(title, daily);
        }

        /// <summary>
        /// 部分別PV取得
        /// </summary>
        /// <param name="title"></param>
        /// <param name="daily"></param>
        public void AnalyzePartPv(DB.Title title, DB.DailyScore daily)
        {
            AnalyzingLockContext(() =>
            {
                if ((daily == null) || (daily.PageView == 0) || (daily.Series == 1)) return;   //見るまでもないやつ(PV=0または1話しかない)
                if (daily.PartPvChecked) return;    //取得済みならもうしない

                var waitFor = LastKasasagiDate.AddSeconds(KasasagiIntervalSec);
                var waitMsec = (waitFor - DateTime.Now).TotalMilliseconds;
                if (waitMsec > 0)
                {
                    DebugReport.Log(this, $"kasasagi wait {waitMsec}");
                    System.Threading.Thread.Sleep((int)waitMsec);
                }
                var narou = new NarouAPI();
                var useKasasagi = narou.GetPartPvData(title, daily);
                if (useKasasagi)
                {
                    LastKasasagiDate = DateTime.Now;
                }
            });
        }

        /// <summary>
        /// 作品分析情報列挙
        /// </summary>
        /// <param name="title">作品データ</param>
        /// <returns>
        /// 作品データから得られるいろんな情報をキーバリューペアで返す
        /// </returns>
        public IEnumerable<AnalyzedValues> AnalyzeTitle(DB.Title title)
        {
            var signed = "+#,0;-#,0";
            double d;
            DateTime dt;

            yield return new AnalyzedValues("Nコード", title.ID);

            yield return new AnalyzedValues("PV", title.PageView.ToString("#,0"));
            yield return new AnalyzedValues("ユニークPV", title.UniquePageView.ToString("#,0"));
            yield return new AnalyzedValues("ポイント", title.LatestScore.Points.ToString("#,0"));
            yield return new AnalyzedValues("初回投稿から", ((DateTime.Now.Date - title.FirstUp.Date).TotalDays.ToString() + "日"), title.FirstUp.ToString("yyyy/MM/dd hh:mm"));
            yield return new AnalyzedValues("最終投稿から", ((DateTime.Now.Date - title.LastUp.Date).TotalDays.ToString() + "日"), title.FirstUp.ToString("yyyy/MM/dd hh:mm"));
            var writingDays = (title.LastUp.Date - title.FirstUp.Date).TotalDays + 1;
            yield return new AnalyzedValues("投稿期間", writingDays.ToString() + "日");
            d = getMaxXxx(title.Score, x => x.PageView, out dt);
            yield return new AnalyzedValues("最大PV", d.ToString("#,0"), dt.ToString("yyyy/MM/dd"));
            d = getMaxXxx(title.Score, x => x.UniquePageView, out dt);
            yield return new AnalyzedValues("最大ユニーク", d.ToString("#,0"), dt.ToString("yyyy/MM/dd"));
            yield return new AnalyzedValues("平均PV", getAverageXxx(title.Score, x => x.PageView).ToString("#,0.0"));
            yield return new AnalyzedValues("平均ユニーク", getAverageXxx(title.Score, x => x.UniquePageView).ToString("#,0.0"));
            //最新スコア
            var ls = title.LatestScore;
            yield return new AnalyzedValues("《最新》");
            yield return new AnalyzedValues("話数", ls.Series.ToString("#,0"));
            yield return new AnalyzedValues("文字数", ls.Size.ToString("#,0"));
            yield return new AnalyzedValues("一話平均文字数", (ls.Size / (float)ls.Series).ToString("#,0.0"));
            yield return new AnalyzedValues("ブクマ", ls.Bookmarks.ToString("#,0"));
            yield return new AnalyzedValues("感想", ls.Impressions.ToString("#,0"));
            yield return new AnalyzedValues("レビュー", ls.Reviews.ToString("#,0"));
            yield return new AnalyzedValues("評価数", ls.Votes.ToString("#,0"));
            yield return new AnalyzedValues("評価点", ls.VoteScore.ToString("#,0"));
            var av = ls.Votes == 0 ? 0 : ls.VoteScore / (ls.Votes * 2.0);
            yield return new AnalyzedValues("平均評価", av.ToString("0.0"));
            yield return new AnalyzedValues("日間Pt", ls.DailyPoint.ToString("#,0"));
            yield return new AnalyzedValues("月間Pt", ls.MonthlyPoint.ToString("#,0"));
            yield return new AnalyzedValues("季間Pt", ls.QuarterPoint.ToString("#,0"));
            yield return new AnalyzedValues("年間Pt", ls.YearPoint.ToString("#,0"));
            {
                var aS = writingDays / ls.Series;
                yield return new AnalyzedValues("投稿間隔", aS.ToString("#,0.0") + "日");
                var aL = ls.Size / writingDays;
                yield return new AnalyzedValues("速度", aL.ToString("#,0.0") + "文字/日");
            }
            //過去n日
            foreach (var bs in new int[] { 7, 30, 90, 180, 360 })
            {
                yield return new AnalyzedValues($"《過去{bs}日》");
                var ln = title.BackLogScore(bs - 1);
                var lns = title.Score.Where(x => x.Date > (DateTime.Now.Date.AddDays(-bs))).ToList();
                if (ln != null)
                {
                    d = ls.Points - ln.Points;
                    yield return new AnalyzedValues("ポイント", d.ToString(signed));
                    d = ls.Series - ln.Series;
                    yield return new AnalyzedValues("投稿話数", d.ToString(signed));
                    var aS = (double)bs / d;
                    yield return new AnalyzedValues("投稿間隔", aS.ToString("#,0.0") + "日");
                    d = ls.Size - ln.Size;
                    yield return new AnalyzedValues("文字数", d.ToString(signed));
                    var aL = d / (double)bs;
                    yield return new AnalyzedValues("速度", aL.ToString("#,0.0") + "文字/日");
                    d = ls.Bookmarks - ln.Bookmarks;
                    yield return new AnalyzedValues("ブクマ", d.ToString(signed));
                    d = ls.Impressions - ln.Impressions;
                    yield return new AnalyzedValues("感想", d.ToString(signed));
                    d = ls.Reviews - ln.Reviews;
                    yield return new AnalyzedValues("レビュー", d.ToString(signed));
                    d = ls.Votes - ln.Votes;
                    yield return new AnalyzedValues("評価数", d.ToString(signed));
                    d = ls.VoteScore - ln.VoteScore;
                    yield return new AnalyzedValues("評価点", d.ToString(signed));
                }
                d = lns.Sum(x => x.PageView);
                yield return new AnalyzedValues("PV", d.ToString("#,0"));
                d = lns.Sum(x => x.UniquePageView);
                yield return new AnalyzedValues("ユニークPV", d.ToString("#,0"));
                d = getMaxXxx(lns, x => x.PageView, out dt);
                yield return new AnalyzedValues("最大PV", d.ToString("#,0"), dt.ToString("yyyy/MM/dd"));
                d = getMaxXxx(lns, x => x.UniquePageView, out dt);
                yield return new AnalyzedValues("最大ユニーク", d.ToString("#,0"), dt.ToString("yyyy/MM/dd"));
                yield return new AnalyzedValues("平均PV", getAverageXxx(lns, x => x.PageView).ToString("#,0.0"));
                yield return new AnalyzedValues("平均ユニーク", getAverageXxx(lns, x => x.UniquePageView).ToString("#,0.0"));
            }
        }

        /// <summary>
        /// XXXの最大値を取得
        /// </summary>
        /// <param name="list">毎日データリスト</param>
        /// <param name="keySelector">XXXの値を返すセレクタ</param>
        /// <param name="date">いつのが最大か</param>
        /// <returns>最大値</returns>
        private double getMaxXxx(IEnumerable<DB.DailyScore> list, Func<DB.DailyScore, double> keySelector, out DateTime date)
        {
            double d = 0;
            date = DateTime.Now;
            foreach (var s in list)
            {
                var v = keySelector(s);
                if (v > d)
                {
                    d = v;
                    date = s.Date.Date;
                }
            }
            return d;
        }

        /// <summary>
        /// XXXの平均値を取得(データがなければゼロ）
        /// </summary>
        /// <param name="list">毎日データリスト</param>
        /// <param name="keySelector">XXXの値を返すセレクタ</param>
        /// <returns></returns>
        private double getAverageXxx(IEnumerable<DB.DailyScore> list, Func<DB.DailyScore, double> keySelector)
        {
            double d = 0;
            bool f = true;
            DateTime first = new DateTime();
            DateTime last = new DateTime();
            foreach (var s in list)
            {
                if (f)
                {
                    first = s.Date;
                    f = false;
                }
                d += keySelector(s);
                last = s.Date;
            }
            var cnt = (last.Date - first.Date).TotalDays + 1;
            return f ? 0 : d / cnt; //データが一個もないときfがtrueのままなので。
        }

        /// <summary>
        /// 指定データのイベント文字列更新
        /// </summary>
        /// <param name="ncode"></param>
        /// <param name="date"></param>
        /// <param name="eventString"></param>
        /// <returns></returns>
        public bool Edit(string ncode, DateTime date, string eventString)
        {
            bool ret = false;
            AnalyzingLockContext(() =>
            {
                var score = authorInfo[ncode][date];
                if (score != null)
                {
                    if (score.Event != eventString)
                    {
                        score.Event = eventString;
                        Save();
                    }
                }
            });

            return ret;
        }


        /// <summary>
        /// アナライザの内部処理の順序性利用ロックコンテキスト内で実行
        /// </summary>
        /// <param name="a"></param>
        /// <remarks>
        /// このクラスの副作用のあるPublicメソッドはすべて処理本体をaに詰めて実行する必要がある。
        /// </remarks>
        private void AnalyzingLockContext(Action a)
        {
            lock(lockObj)
            {
                notifyStartAnalyze();
                a();
                notifyEndAnalyze();
            }
        }


        /// <summary>
        /// 実行中判定用ロックカウンタ
        /// </summary>
        private int analyzingCounter = 0;
        /// <summary>
        /// ロックオブジェクト
        /// </summary>
        private readonly object lockObj = new object();

        /// <summary>
        /// 実行開始通知(未実行からの実行時のみ）
        /// </summary>
        void notifyStartAnalyze()
        {
            if (analyzingCounter == 0) AnalyzingStart?.Invoke(this, EventArgs.Empty);
            analyzingCounter++;
        }

        /// <summary>
        /// 実行終了通知（実行中から未実行になるときのみ）
        /// </summary>
        void notifyEndAnalyze()
        {
            analyzingCounter--;
            if (analyzingCounter == 0) AnalyzingEnd?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// 分析値
    /// </summary>
    class AnalyzedValues
    {
        /// <summary>
        /// タイトルのみ設定
        /// </summary>
        /// <param name="name"></param>
        public AnalyzedValues(string name)
        {
            ItemName = name;
            Value = MarkedDate = "";
        }

        /// <summary>
        /// タイトルと値設定
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public AnalyzedValues(string name, string value)
        {
            ItemName = name;
            Value = value;
            MarkedDate = "";
        }

        /// <summary>
        /// タイトルと値と記録日設定
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="marked"></param>
        public AnalyzedValues(string name, string value, string marked)
        {
            ItemName = name;
            Value = value;
            MarkedDate = marked;
        }
        /// <summary>
        /// 項目名
        /// </summary>
        public string ItemName { get; private set; }
        /// <summary>
        /// 値
        /// </summary>
        public string Value { get; private set; }
        /// <summary>
        /// 記録日
        /// </summary>
        public string MarkedDate { get; private set; }
    }
}
