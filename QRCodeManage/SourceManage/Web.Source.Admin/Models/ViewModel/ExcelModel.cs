using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Source.Admin.Web.Models.ViewModel
{
    public class ExcelModel
    {
        
        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string NumberCode { get; set; }
     
    }

    public class ParamsExcel
    {
        /// <summary>
        /// 开始码
        /// </summary>
        public long StartNumber { get; set; }

        /// <summary>
        /// 结束码
        /// </summary>
        public long EndNumber { get; set; }


    }

}