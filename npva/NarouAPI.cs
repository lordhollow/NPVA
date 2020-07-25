using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using YamlDotNet.RepresentationModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace npva
{
    /// <summary>
    /// 小説家になろうのサイトから情報をとってくるAPI
    /// </summary>
    public class NarouAPI
    {
        /// <summary>なろうサイトのREST APIアドレス</summary>
        public string API = "https://api.syosetu.com/novelapi/api/";
        /// <summary>kasasagi(アクセス解析サイト)のベースアドレス</summary>
        public string Kasasagi = "https://kasasagi.hinaproject.com/access/daypv/ncode/";    // "(ncode)/?month=(yyyyMM)";
        /// <summary>KasasagiでユニークPv出すときのベースアドレス</summary>
        public string KasasagiUnique = "https://kasasagi.hinaproject.com/access/dayunique/ncode/";    // "(ncode)/?month=(yyyyMM)";
        /// <summary>なろうREST APIのofオプション</summary>
        public string Filter = "n-t-w-l-ka-ga-gp-dp-wp-mp-qp-yp-f-imp-r-a-ah-gf-gl";
        /// <summary>リクエストに使用するUA(設定しないと結果が返ってこない）</summary>
        public string UserAgent = "NPVAnalyzer 0.01";
        /// <summary>Kasasgiが処理中で結果を得られなかったときに入っている文字列</summary>
        public string PreparingMarker = "<meta http-equiv=\"refresh\"";
        /// <summary>Kasasagiで部位別PVを出すときのベースアドレス</summary>
        public string KasasagiPartPv = "https://kasasagi.hinaproject.com/access/chapter/ncode/";   // "(ncode)/?date=(yyyy-MM-dd)";
        /// <summary>部位別PVがまだ準備されていない(昨日)のやつだ</summary>
        public string PartPvNotPreparedMarker = "<p class=\"attention\">解析対象外です。</p><!-- attention -->";
        /// <summary>キャッシュ保存フォルダ</summary>
        public readonly string CachePath = Path.Combine(Application.StartupPath, "cache/");

        /// <summary>
        /// 著者情報取得
        /// </summary>
        /// <param name="authorId">ユーザーID</param>
        /// <returns>取得結果</returns>
        public DB.Author GetAuthorInfo(string authorId)
        {
            var apiUrl = $"{API}?userid={authorId}&of={Filter}&lim=500";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
            var result = client.GetAsync(apiUrl);
            if (result.Result.IsSuccessStatusCode)
            {
                var contentTask = result.Result.Content.ReadAsStringAsync();
                return parseAuthorInfo(contentTask.Result, authorId);
            }
            return null; ;
        }

        /// <summary>
        /// REST APIの戻り値(Yaml)を著者情報オブジェクトに変換
        /// </summary>
        /// <param name="yaml">レスポンス</param>
        /// <param name="authorId">誰の？</param>
        /// <returns>著者情報オブジェクト（今回APIで取得した分のみ）</returns>
        private DB.Author parseAuthorInfo(string yaml, string authorId)
        {
            if (string.IsNullOrEmpty(yaml)) return null;
            var a = new DB.Author();
            a.ID = authorId;

            //yamlの解釈
            var parser = new YamlStream();
            using (var tr = new StringReader(yaml))
            {
                parser.Load(tr);

                var sequence = (YamlSequenceNode)parser.Documents[0].RootNode;
                foreach(var yTitle in sequence.Skip(1))
                {
                    var title = YamlMapToTitleObj(yTitle as YamlMappingNode);
                    if (title!=null)
                    {
                        a.Titles.Add(title);
                    }
                }
            }

            return a;
        }

        /// <summary>
        /// Yamlの作品情報を作品情報オブジェクトに変換
        /// </summary>
        /// <param name="node"></param>
        /// <returns>作品情報オブジェクト</returns>
        private DB.Title YamlMapToTitleObj(YamlMappingNode node)
        {
            if (node == null) return null;
            var t = new DB.Title
            {
                ID = GetStringFromYamlMap(node, "ncode"),
                Name = GetStringFromYamlMap(node, "title"),
                Author = GetStringFromYamlMap(node, "writer"),

                FirstUp = GetDateFromYamlMap(node, "general_firstup"),
                LastUp = GetDateFromYamlMap(node, "general_lastup"),

                LastCheck = DateTime.Now
            };

            var score = new DB.DailyScore
            {
                Series = GetIntFromYamlMap(node, "general_all_no"),
                Size = GetIntFromYamlMap(node, "length"),
                ConversationRatio = GetIntFromYamlMap(node, "kaiwaritu"),
                Impressions = GetIntFromYamlMap(node, "impression_cnt"),
                Reviews = GetIntFromYamlMap(node, "review_cnt"),
                Votes = GetIntFromYamlMap(node, "all_hyoka_cnt"),
                VoteScore = GetIntFromYamlMap(node, "all_point"),
                Bookmarks = GetIntFromYamlMap(node, "fav_novel_cnt"),
                DailyPoint = GetIntFromYamlMap(node, "daily_point"),
                WeeklyPoint = GetIntFromYamlMap(node, "weekly_point"),
                MonthlyPoint = GetIntFromYamlMap(node, "monthly_point"),
                QuarterPoint = GetIntFromYamlMap(node, "quarter_point"),
                YearPoint = GetIntFromYamlMap(node, "yearly_point"),
                Points = GetIntFromYamlMap(node, "global_point"),

                Date = t.LastCheck.Date
            };
            DebugReport.Log(this, $"REST get info {t.ID} {t.Name}");

            t.Score.Add(score);
            return t;
        }

        /// <summary>
        /// YAMLの連想配列から文字列取得
        /// </summary>
        /// <param name="node">連想配列</param>
        /// <param name="key">キー</param>
        /// <returns>文字列</returns>
        private string GetStringFromYamlMap(YamlMappingNode node, string key)
        {
            var str = node.Children[new YamlScalarNode(key)].ToString();
            return str.Trim();
        }

        /// <summary>
        /// YAMLの連想配列から日付取得
        /// </summary>
        /// <param name="node">連想配列</param>
        /// <param name="key">キー</param>
        /// <returns>日付</returns>
        private DateTime GetDateFromYamlMap(YamlMappingNode node, string key)
        {
            var str = node.Children[new YamlScalarNode(key)].ToString();
            return DateTime.Parse(str);
        }

        /// <summary>
        /// YAMLの連想配列から整数取得
        /// </summary>
        /// <param name="node">連想配列</param>
        /// <param name="key">キー</param>
        /// <returns>整数</returns>
        private int GetIntFromYamlMap(YamlMappingNode node, string key)
        {
            var str = node.Children[new YamlScalarNode(key)].ToString();
            return int.Parse(str);
        }

        /// <summary>
        /// ぶっ壊れキャッシュ(準備中の物）を削除
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// メンテナンス用。通常はぶっ壊れキャッシュは保存もされないが、Kasasagi仕様変更でぶっ壊れ判定に失敗した後などに使う。
        /// </remarks>
        public void DeleteAllInvalidCache()
        {
            foreach(var file in Directory.GetFiles("cache"))
            {
                var txt = File.ReadAllText(file);
                if (txt.Contains(PreparingMarker))
                {
                    File.Delete(file);
                    DebugReport.Log(this, $"delete invalid cache {file}");
                }
            }
        }

        /// <summary>
        /// PCデータ取得
        /// </summary>
        /// <param name="title">作品</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="useCache">キャッシュから読んでいいか</param>
        /// <returns>Kasasgiへのアクセスを実施したらtrue</returns>
        /// <remarks>サイトへのDDOS対策のためtrueが返ってきた場合少し間をおいてから次の月/作品を確認することを推奨</remarks>
        public bool UpdatePVData(DB.Title title, int year, int month, bool useCache)
        {
            var status = $"pv {title.ID}\t{year:0000}{month:00}";
            DebugReport.AnalyzerStatus = status;

            string cacheFile;
            string apiUrl;

            bool useKasasagi = false;

            for (var i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    //通常
                    cacheFile = $"Kasasagi{title.ID}{year:0000}{month:00}.html";
                    apiUrl = $"{Kasasagi}{title.ID}/?month={year:0000}{month:00}";
                }
                else
                {
                    //ユニーク
                    cacheFile = $"KasasagiU{title.ID}{year:0000}{month:00}.html";
                    apiUrl = $"{KasasagiUnique}{title.ID}/?month={year:0000}{month:00}";
                }
                cacheFile = Path.Combine(CachePath, cacheFile);

                string html = null;

                if (useCache)
                {
                    if (File.Exists(cacheFile))
                    {
                        html = File.ReadAllText(cacheFile);
                    }
                }
                if (html == null)
                {
                    useKasasagi = true;
                    html = RequestKasasagi(cacheFile, apiUrl, x => x.Contains(PreparingMarker));
                }

                //パースする
                parsePvHTML(title, html, i != 0);
            }
            return useKasasagi;
        }

        /// <summary>
        /// Kasasagiにリクエスト
        /// </summary>
        /// <param name="cacheFile"></param>
        /// <param name="apiUrl"></param>
        /// <returns></returns>
        private string RequestKasasagi(string cacheFile, string apiUrl, Func<string, bool> error)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
            var task = client.GetAsync(apiUrl);
            if (task.Result.IsSuccessStatusCode)
            {
                var contentReadTask = task.Result.Content.ReadAsStringAsync();
                var html = contentReadTask.Result;
                if (error(html))
                {
                    //この時はキャッシュ保存しない
                    DebugReport.Log(this, $"kasasagi failed {apiUrl}");
                    return "";
                }
                else
                {
                    //キャッシュ保存する。
                    //データが安定していない当月分（と3日までは先月分も）は残さないほうが運用が楽かも
                    DebugReport.Log(this, $"kasasagi succeeded {apiUrl}");
                    if (!Directory.Exists(CachePath))
                    {
                        Directory.CreateDirectory(CachePath);
                    }
                    File.WriteAllText(cacheFile, html);
                }
                return html;
            }
            else
            {
                DebugReport.Log(this, $"kasasagi {task.Result.ReasonPhrase} {apiUrl}");
            }

            return "";
        }

        /// <summary>
        /// kasasagiのスクレイピング
        /// </summary>
        /// <param name="title">作品</param>
        /// <param name="html">コンテンツ</param>
        /// <param name="isUnique">ユニークのデータか？</param>
        private void parsePvHTML(DB.Title title, string html, bool isUnique)
        {
            var regTable = new Regex("\\<table class=\"access_per_day\"\\>(.+?)\\</table\\>", RegexOptions.Singleline);
            var regDate = new Regex("(\\d+)年(\\d+)月(\\d+)日");
            var regView = new Regex("\\<td class=\"item\"\\>([\\d,]+)人?\\</td\\>.+?\\<div class=\"(pc|mobile|smp)\"", RegexOptions.Singleline);
            var matches = regTable.Matches(html);
            foreach (Match match in matches)
            {
                var dayTable = match.Groups[1].Value;
                var dm = regDate.Match(dayTable);
                if (dm.Success)
                {
                    var date = new DateTime(int.Parse(dm.Groups[1].Value), int.Parse(dm.Groups[2].Value), int.Parse(dm.Groups[3].Value));
                    var tempPv = new DB.DailyScore();
                    tempPv.Date = date;
                    var vms = regView.Matches(dayTable);
                    foreach(Match vm in vms)
                    {
                        var ns = vm.Groups[1].Value;
                        ns = ns.Replace(",", "");
                        var pv = int.Parse(ns);
                        var cls = vm.Groups[2].Value;
                        setPv(isUnique, tempPv, pv, cls);
                    }
                    //作品データにマージ
                    title.AddPageView(tempPv, isUnique);
                }
            }
        }

        /// <summary>
        /// 1日データに値を設定
        /// </summary>
        /// <param name="isUnique"></param>
        /// <param name="tempPv"></param>
        /// <param name="pv"></param>
        /// <param name="cls"></param>
        private void setPv(bool isUnique, DB.DailyScore tempPv, int pv, string cls)
        {
            switch (cls)
            {
                case "pc":
                    if (isUnique)
                    {
                        tempPv.PCUnique = pv;
                    }
                    else
                    {
                        tempPv.PC = pv;
                    }
                    break;
                case "mobile":
                    if (isUnique)
                    {
                        tempPv.MobileUnique = pv;
                    }
                    else
                    {
                        tempPv.Mobile = pv;
                    }
                    break;
                case "smp":
                    if (isUnique)
                    {
                        tempPv.SmartPhoneUnique = pv;
                    }
                    else
                    {
                        tempPv.SmartPhone = pv;
                    }
                    break;
            }
        }

        /// <summary>
        /// 部位別PVデータ取得
        /// </summary>
        /// <param name="title">どの作品の</param>
        /// <param name="daily">いつ？</param>
        /// <remarks>
        /// ユニークが出てからしか更新されず、以降データ変動しない(と思われる）ので常にキャッシュが有効。
        /// そもそもPVがない日は取れないので、当日のScoreがないときは何もせずに帰る。
        /// </remarks>
        public bool GetPartPvData(DB.Title title, DB.DailyScore daily)
        {
            if (title == null) return false;
            if (daily == null) return false;
            var date = daily.Date;

            bool useKasasagi = false;
            var dt = date.ToString("yyyy-MM-dd");
            var url = $"{KasasagiPartPv}{title.ID}/?date={dt}";
            var cacheFile = $"KasasagiP{title.ID}{date.Year:0000}{date.Month:00}{date.Day:00}.html";
            cacheFile = Path.Combine(CachePath, cacheFile);

            string html = null;
            if (File.Exists(cacheFile))
            {
                html = File.ReadAllText(cacheFile);
            }
            if (html == null)
            {
                useKasasagi = true;
                html = RequestKasasagi(cacheFile, url, x => x.Contains(PreparingMarker) || x.Contains(PartPvNotPreparedMarker));
            }

            //パースする
            parsePartPvHTML(daily, html);

            return useKasasagi;
        }

        /// <summary>
        /// 部分別PVを解析
        /// </summary>
        /// <param name="title"></param>
        /// <param name="date"></param>
        /// <param name="html"></param>
        private void parsePartPvHTML(DB.DailyScore score, string html)
        {
            if (string.IsNullOrEmpty(html)) return;
            score.PartPv = new List<DB.PartPv>();
            score.PartPvChecked = true;
            var reg = new Regex(@">第(\d+)部分:(\d+)人");

            MatchCollection ms = reg.Matches(html);
            foreach(Match m in ms)
            {
                var p = int.Parse(m.Groups[1].Value);
                var pv = int.Parse(m.Groups[2].Value);
                score.PartPv.Add(new DB.PartPv { Part = p, PageView = pv });
            }
        }
    }
}
