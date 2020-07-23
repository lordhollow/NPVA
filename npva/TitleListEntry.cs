using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npva
{
    /// <summary>
    /// 作品とその表示名のペア
    /// </summary>
    class TitleListEntry
    {
        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="title">作品データ</param>
        /// <param name="displayName">表示名</param>
        public TitleListEntry(DB.Title title, string displayName)
        {
            Title = title;
            DisplayName = displayName;
        }

        /// <summary>
        /// 作品データ
        /// </summary>
        public DB.Title Title { get; private set; }

        /// <summary>
        /// 表示名
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// 文字列表記(表示名）
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return DisplayName;
        }
    }
}
