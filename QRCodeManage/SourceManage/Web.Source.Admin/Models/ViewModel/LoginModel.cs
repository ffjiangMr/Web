using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Source.Admin.Web.Models.ViewModel
{
    public class LoginModel
    {
        public int Id { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
    }
}