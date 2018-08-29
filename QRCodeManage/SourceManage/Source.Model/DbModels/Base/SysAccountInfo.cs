
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Source.Model.Infrastructure;

namespace Source.Model.DbModels.Base
{
    /// <summary>
    /// 管理员账户表
    /// </summary>
    public class SysAccountInfo : CreatorBase<int>
    {
        /// <summary>
        /// 管理员名称
        /// </summary>
        [Required, StringLength(18)]
        public string SuName { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string Identity { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Explain { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        [Required, StringLength(50)]
        public string Login { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required, StringLength(50)]
        public string Password { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        public string Message { get; set; }

        public SysAccountInfo()
        {

            LastLoginTime = DateTime.Now;
        }
    }
    /// <summary>
    /// 权限枚举
    /// </summary>
    public enum AccountRoleEnum
    {
        超级管理员 = 0,
        一般管理员

    }

}
