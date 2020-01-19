using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.ZYT
{
    public class BaiduAddressEntity
    {
        /// <summary>
        /// 返回码状态表
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public AddresResult result { get; set; }
    }
    public class AddresResult
    {
        /// <summary>
        /// 拆分
        /// </summary>
        public AddressComponent addressComponent { get; set; }
    }
    public class AddressComponent
    {
        /// <summary>
        /// 省
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// 市、区
        /// </summary>
        public string city { get; set; }
    }
}
