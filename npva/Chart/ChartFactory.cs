using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npva.Chart
{
    /// <summary>
    /// チャートファクトリ
    /// </summary>
    static class ChartFactory
    {
        /// <summary>
        /// ファクトリメソッド(あんまり意味ない）
        /// </summary>
        /// <returns></returns>
        public static IChart CreateChart()
        {
            return new Chart();
        }

    }
}
