using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace npva.DB
{
    /// <summary>
    /// 著者
    /// </summary>
    public class Author
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        [XmlAttribute("id")]
        public string ID { get; set; }

        /// <summary>
        /// 名前
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// 確認日
        /// </summary>
        [XmlAttribute("date")]
        public DateTime CheckedDate { get; set; }

        /// <summary>
        /// 作品リスト
        /// </summary>
        [XmlElement("title")]
        public List<Title> Titles { get; set; } = new List<Title>();

        /// <summary>
        /// このオブジェクトにlhsをマージする
        /// </summary>
        /// <param name="newData">今回取得したデータ</param>
        public void Merge(Author newData)
        {
            var dic = Titles.ToDictionary(x => x.ID);   //ncodeをキーにする

            foreach(var item in newData.Titles)
            {
                //すでにあるものならマージ
                if (dic.ContainsKey(item.ID))
                {
                    dic[item.ID].Merge(item);
                }
                //新しい物なら足す
                else
                {
                    Titles.Add(item);
                }
            }
            //作品並び替え
            Normalize();
        }

        /// <summary>
        /// 初回投稿でtitleをソート
        /// </summary>
        public void Normalize()
        {
            //初回投稿日でソート
            Titles.Sort((x, y) => (int)(x.FirstUp - y.FirstUp).TotalSeconds);
        }

        /// <summary>
        /// 著者名の解決
        /// </summary>
        public void ResolveAuthorName()
        {
            //Authorの名前を数える。。。
            var names = new Dictionary<string, int>();
            foreach(var name in Titles.Select(x=>x.Author))
            {
                if (names.ContainsKey(name))
                {
                    names[name]++;
                }
                else
                {
                    names[name] = 1;
                }
            }
            //利用実績が多い順に並べる
            var nameList = names.OrderByDescending(x => x.Value).Select(x => x.Key);
            Name = string.Join(", ", nameList);
        }

        /// <summary>
        /// インデクサ(参照のみ。ncode指定で作品データ取得）なければnull
        /// </summary>
        /// <param name="ncode"></param>
        /// <returns></returns>
        public Title this[string ncode]
        {
            get
            {
                ncode = ncode.ToUpper();
                return Titles?.FirstOrDefault(x => x.ID?.ToUpper() == ncode);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public void Save(string path)
        {
            using (var f = new System.IO.StreamWriter(path))
            {
                var ser = new XmlSerializer(typeof(DB.Author));
                ser.Serialize(f, this);
            }
        }

        /// <summary>
        /// シリアライズ/デシリアライズしてDeepCopyを作る
        /// </summary>
        /// <returns></returns>
        public Author Clone()
        {
            var sr = new XmlSerializer(typeof(DB.Author));

            var sb = new StringBuilder();
            using (var f = new System.IO.StringWriter(sb))
            {
                sr.Serialize(f, this);
            }
            using (var f = new System.IO.StringReader(sb.ToString()))
            {
                return sr.Deserialize(f) as DB.Author;
            }
        }
    }
}
