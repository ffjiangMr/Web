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
   public class BaseDictionariesInfo: CreatorBase<int>
    {
        /// <summary>
        /// 健
        /// </summary>
        [Required, StringLength(50)]
        public string KeyName { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string ValueName { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Explain { get; set; }
    }

    public enum DicTypeEnum
    {
        计算=0,
        基础

    }
}
