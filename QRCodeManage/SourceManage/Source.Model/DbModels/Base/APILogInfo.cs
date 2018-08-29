using TomNet.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Source.Model.Infrastructure;

namespace Source.Model.DbModels.Base
{
   public class APILogInfo : GeneralBase<int>
    {
        /// <summary>
        /// 调用人的GUid
        /// </summary>
        [Required, StringLength(50)]
        public string UserGuid { get; set; }

        /// <summary>
        /// 调用用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 接口名
        /// </summary>
        public string ApiName { get; set; }
       

        /// <summary>
        /// 说明
        /// </summary>
        public string Explain { get; set; }
    }

    
}
