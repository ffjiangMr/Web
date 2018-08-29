using Source.Model.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source.Model.DbModels.QRCode
{
    /// <summary>
    /// 填充表
    /// </summary>
    public class UserNumber : CreatorBase<int>
    {
        /// <summary>
        /// 账户ID
        /// </summary>
        [Required]
        public int UaId { get; set; }

        /// <summary>
        /// 所属箱ID
        /// </summary>
        [Required]
        public int UNumBoxId { get; set; }

        /// <summary>
        /// 开始码
        /// </summary>
        [Required]
        public long StartNumber { get; set; }

        /// <summary>
        /// 结束码
        /// </summary>
        [Required]
        public long EndNumber { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Required]
        public int NumCount { get; set; }

        /// <summary>
        /// 码对应URL
        /// </summary>
        [Required]
        public string ValueURL { get; set; }


        /// <summary>
        /// 分组标识
        /// </summary>
        public string GroupGUID { get; set; }
    }
 
}
