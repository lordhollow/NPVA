using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using npva.DB;

namespace npva
{
    /// <summary>
    /// 部分別PV解釈
    /// </summary>
    class PartialPVAnalyzer
    {
        /// <summary>
        /// アナライザー列挙
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<PartialPVAnalyzer> CreatePartialPVAnalyzers()
        {
            yield return new PPVA_PartialTotal();
        }

        /// <summary>
        /// このクラスの勝手なインスタンス化を阻止
        /// </summary>
        protected PartialPVAnalyzer() { }

        /// <summary>
        /// 解析結果をリストビューに入れる
        /// </summary>
        /// <param name="t"></param>
        /// <param name="listView"></param>
        public virtual void Analyze(DB.Title t, ListView listView)
        {

        }
    }


    /// <summary>
    /// 部位別総PV
    /// </summary>
    class PPVA_PartialTotal : PartialPVAnalyzer
    {

        public override void Analyze(Title t, ListView listView)
        {
            var pv = new Dictionary<int, int>();
            foreach (var score in t.Score.Where(x => x.PartPvChecked))
            {
                foreach (var p in score.PartPv)
                {
                    if (pv.ContainsKey(p.Part))
                    {
                        pv[p.Part] += p.PageView;
                    }
                    else
                    {
                        pv[p.Part] = p.PageView;
                    }
                }
            }
            listView.Items.Clear();
            listView.Columns.Clear();
            listView.Columns.Add("部分");
            listView.Columns.Add("合計PV");
            foreach (var p in pv.OrderBy(x => x.Key))
            {
                var lst = listView.Items.Add($"第{p.Key}部分");
                lst.SubItems.Add($"{p.Value:#,0}人");
            }
        }

        public override string ToString()
        {
            return "部位別総PV";
        }
    }

}
