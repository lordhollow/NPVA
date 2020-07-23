using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using npva.DB;

namespace npva
{
    /// <summary>
    /// 作品リストの並べ替え方法
    /// </summary>
    class SortOrderEntry
    {
        public static IEnumerable<SortOrderEntry> CreateEntries()
        {
            yield return new SortOrderEntry_OrderByFirstUp();
            yield return new SortOrderEntry_OrderByFirstUpR();
            yield return new SortOrderEntry_OrderByLastUp();
            yield return new SortOrderEntry_OrderByLastUpR();
            yield return new SortOrderEntry_OrderByTotalPv();
            yield return new SortOrderEntry_OrderByTotalPvR();
            yield return new SortOrderEntry_OrderByImpressions();
            yield return new SortOrderEntry_OrderByImpressionsR();
            yield return new SortOrderEntry_OrderByReviews();
            yield return new SortOrderEntry_OrderByReviewsR();
            yield return new SortOrderEntry_OrderByBookmarks();
            yield return new SortOrderEntry_OrderByBookmarksR();
            yield return new SortOrderEntry_OrderByPoints();
            yield return new SortOrderEntry_OrderByPointsR();
            yield return new SortOrderEntry_OrderByVoteAverage();
            yield return new SortOrderEntry_OrderByVoteAverageR();
            yield return new SortOrderEntry();
        }

        public virtual IEnumerable<DB.Title> Order(IEnumerable<DB.Title> list)
        {
            return list;
        }
        public virtual string TitlePicker(DB.Title item)
        {
            return item.Name;
        }
        public override string ToString()
        {
            return "そのまま";
        }
    }

    class SortOrderEntry_OrderByFirstUp : SortOrderEntry
    {
        public override IEnumerable<Title> Order(IEnumerable<Title> list)
        {
            return list.OrderByDescending(x => x.FirstUp);
        }
        public override string TitlePicker(Title item)
        {
            return $"{item.Name}({item.FirstUp.ToShortDateString()})";
        }
        public override string ToString()
        {
            return "▼初回投稿";
        }
    }

    class SortOrderEntry_OrderByFirstUpR : SortOrderEntry
    {
        public override IEnumerable<Title> Order(IEnumerable<Title> list)
        {
            return list.OrderBy(x => x.FirstUp);
        }
        public override string TitlePicker(Title item)
        {
            return $"{item.Name}({item.FirstUp.ToShortDateString()})";
        }
        public override string ToString()
        {
            return "▲初回投稿";
        }
    }

    class SortOrderEntry_OrderByLastUp : SortOrderEntry
    {
        public override IEnumerable<Title> Order(IEnumerable<Title> list)
        {
            return list.OrderByDescending(x => x.LastUp);
        }
        public override string TitlePicker(Title item)
        {
            return $"{item.Name}({item.LastUp.ToShortDateString()})";
        }
        public override string ToString()
        {
            return "▼最終投稿";
        }
    }

    class SortOrderEntry_OrderByLastUpR : SortOrderEntry
    {
        public override IEnumerable<Title> Order(IEnumerable<Title> list)
        {
            return list.OrderBy(x => x.LastUp);
        }
        public override string TitlePicker(Title item)
        {
            return $"{item.Name}({item.LastUp.ToShortDateString()})";
        }
        public override string ToString()
        {
            return "▲最終投稿";
        }
    }

    class SortOrderEntry_OrderByTotalPv : SortOrderEntry
    {
        public override IEnumerable<Title> Order(IEnumerable<Title> list)
        {
            return list.OrderByDescending(x => x.PageView);
        }
        public override string TitlePicker(Title item)
        {
            return $"{item.Name}({item.PageView})";
        }
        public override string ToString()
        {
            return "▼総PV";
        }
    }

    class SortOrderEntry_OrderByTotalPvR : SortOrderEntry
    {
        public override IEnumerable<Title> Order(IEnumerable<Title> list)
        {
            return list.OrderBy(x => x.LatestScore?.PageView);
        }
        public override string TitlePicker(Title item)
        {
            return $"{item.Name}({item.LatestScore?.PageView})";
        }
        public override string ToString()
        {
            return "▲総PV";
        }
    }

    class SortOrderEntry_OrderByImpressions : SortOrderEntry
    {
        public override IEnumerable<Title> Order(IEnumerable<Title> list)
        {
            return list.OrderByDescending(x => x.LatestScore?.Impressions);
        }
        public override string TitlePicker(Title item)
        {
            return $"{item.Name}({item.LatestScore?.Impressions})";
        }
        public override string ToString()
        {
            return "▼コメント数";
        }
    }

    class SortOrderEntry_OrderByImpressionsR : SortOrderEntry
    {
        public override IEnumerable<Title> Order(IEnumerable<Title> list)
        {
            return list.OrderBy(x => x.LatestScore?.Impressions);
        }
        public override string TitlePicker(Title item)
        {
            return $"{item.Name}({item.LatestScore?.Impressions})";
        }
        public override string ToString()
        {
            return "▲コメント数";
        }
    }

    class SortOrderEntry_OrderByReviews : SortOrderEntry
    {
        public override IEnumerable<Title> Order(IEnumerable<Title> list)
        {
            return list.OrderByDescending(x => x.LatestScore?.Reviews);
        }
        public override string TitlePicker(Title item)
        {
            return $"{item.Name}({item.LatestScore?.Reviews})";
        }
        public override string ToString()
        {
            return "▼レビュー数";
        }
    }

    class SortOrderEntry_OrderByReviewsR : SortOrderEntry
    {
        public override IEnumerable<Title> Order(IEnumerable<Title> list)
        {
            return list.OrderBy(x => x.LatestScore?.Reviews);
        }
        public override string TitlePicker(Title item)
        {
            return $"{item.Name}({item.LatestScore?.Reviews})";
        }
        public override string ToString()
        {
            return "▲レビュー数";
        }
    }

    class SortOrderEntry_OrderByBookmarks : SortOrderEntry
    {
        public override IEnumerable<Title> Order(IEnumerable<Title> list)
        {
            return list.OrderByDescending(x => x.LatestScore?.Bookmarks);
        }
        public override string TitlePicker(Title item)
        {
            return $"{item.Name}({item.LatestScore?.Bookmarks})";
        }
        public override string ToString()
        {
            return "▼ブクマ数";
        }
    }

    class SortOrderEntry_OrderByBookmarksR : SortOrderEntry
    {
        public override IEnumerable<Title> Order(IEnumerable<Title> list)
        {
            return list.OrderBy(x => x.LatestScore?.Bookmarks);
        }
        public override string TitlePicker(Title item)
        {
            return $"{item.Name}({item.LatestScore?.Bookmarks})";
        }
        public override string ToString()
        {
            return "▲ブクマ数";
        }
    }

    class SortOrderEntry_OrderByPoints : SortOrderEntry
    {
        public override IEnumerable<Title> Order(IEnumerable<Title> list)
        {
            return list.OrderByDescending(x => x.LatestScore?.Points);
        }
        public override string TitlePicker(Title item)
        {
            return $"{item.Name}({item.LatestScore?.Points})";
        }
        public override string ToString()
        {
            return "▼ポイント";
        }
    }

    class SortOrderEntry_OrderByPointsR : SortOrderEntry
    {
        public override IEnumerable<Title> Order(IEnumerable<Title> list)
        {
            return list.OrderBy(x => x.LatestScore?.Points);
        }
        public override string TitlePicker(Title item)
        {
            return $"{item.Name}({item.LatestScore?.Points})";
        }
        public override string ToString()
        {
            return "▲ポイント";
        }
    }

    class SortOrderEntry_OrderByVoteAverage : SortOrderEntry
    {
        public override IEnumerable<Title> Order(IEnumerable<Title> list)
        {
            return list.OrderByDescending(x => x.LatestScore?.Votes == 0 ? 0 : x.LatestScore?.VoteScore / (double)x.LatestScore?.Votes);
        }
        public override string TitlePicker(Title item)
        {
            var sc = item.LatestScore?.Votes == 0 ? 0 : item.LatestScore?.VoteScore / (item.LatestScore?.Votes * 2.0);
            return $"{item.Name}({sc:0.0})";
        }
        public override string ToString()
        {
            return "▼平均評価";
        }
    }

    class SortOrderEntry_OrderByVoteAverageR : SortOrderEntry
    {
        public override IEnumerable<Title> Order(IEnumerable<Title> list)
        {
            return list.OrderBy(x => x.LatestScore?.Votes == 0 ? 0 : x.LatestScore?.VoteScore / (double)x.LatestScore?.Votes);
        }
        public override string TitlePicker(Title item)
        {
            var sc = item.LatestScore?.Votes == 0 ? 0 : item.LatestScore?.VoteScore / (item.LatestScore?.Votes * 2.0);
            return $"{item.Name}({sc:0.0})";
        }
        public override string ToString()
        {
            return "▲平均評価";
        }
    }
}
