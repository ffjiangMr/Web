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
    /// 用户表
    /// </summary>
    public class UserAccount : CreatorBase<int>
    {
        /// <summary>
        /// 来源应用名称
        /// </summary>
        [Required, StringLength(18)]
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
        /// 状态
        /// </summary>
        public int UserStare { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Explain { get; set; }
        /// <summary>
        /// 协约唯一标识
        /// </summary>
        [Required, StringLength(50)]
        public string GuIdNumber { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required, StringLength(50)]
        public string Password { get; set; }
    }
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserStare
    {
        申请=1,
        通过=2,
        拒绝=3,
        禁用=4
    }
}
