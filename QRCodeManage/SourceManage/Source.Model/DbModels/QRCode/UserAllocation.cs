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
    /// 分码表
    /// </summary>
    public class UserAllocation : CreatorBase<int>
    {

        /// <summary>
        /// 账户ID
        /// </summary>
        [Required]
        public int UaId { get; set; }

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
        /// 状态
        /// </summary>
        [Required]
        public int AllocationStare { get; set; }


      
    }
    /// <summary>
    /// 状态
    /// </summary>
    public enum AllocationStare
    {
        正常=1,
        禁用=2
    }
}
