using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Source.Admin.Web.Models.ApiParams
{
    public class RegisterModel
    {
        /// <summary>
        /// 来源应用名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 所属行业
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        
        /// <summary>
        /// 说明
        /// </summary>
        public string Explain { get; set; }
        /// <summary>
        /// 协约唯一标识
        /// </summary>
        public string GuIdNumber { get; set; }
    }
}