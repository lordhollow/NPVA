using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace npva.DB
{
    /// <summary>
    /// 部分別PV1日分
    /// </summary>
    [XmlType("ppv")]
    public class PartPv
    {
        /// <summary>
        /// 部分
        /// </summary>
        [XmlAttribute("n")]
        public int Part { get; set; }

        /// <summary>
        /// PV(人)
        /// </summary>
        [XmlAttribute("pv")]
        public int PageView { get; set; }

        /// <summary>
        /// 文字列
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Part}={PageView}";
        }
    }
}
