using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Source.Admin.Web.Models.ApiParams
{
    public class ReturnDataBoxGetnumber
    {
        /// <summary>
        /// 开始码
        /// </summary>
        public long StartNumber { get; set; }

        /// <summary>
        /// 结束码
        /// </summary>
        public long EndNumber { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public long NumCount { get; set; }
    }
}