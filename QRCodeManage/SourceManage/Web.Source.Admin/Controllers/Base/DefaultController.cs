using Newtonsoft.Json;
using Source.Admin.Web.Models.ViewModel;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Source.Admin.Web.Common;
using Source.Admin.Web.Filters;
using TomNet.Web.Mvc;
using TomNet.Web.Mvc.UI;
using Source.Core.Contracts.Base;

namespace Source.Admin.Web.Controllers
{

    public class DefaultController : BaseController
    {
        public ISysAccountInfoContract SysAccountContract { set; get; }

       
        [GlobalAuthorization]
        public ActionResult Index()
        {

            LoginModel login = CookiesManagement.GetLoginModel(CookiesManagement.GetTicket());

             //UserInfoModel model = UserContract.Entities.Where(x => x.Id == login.Id).FirstOrDefault();
            ViewBag.LoginModel = login;
            return View();
        }
      

       /// <summary>
       /// 登陆页面显示
       /// </summary>
       /// <returns></returns>
        public ActionResult Login()
        {
           FormsAuthentication.SignOut();
            return View();
        }

        /// <summary>
        /// 登陆函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <param name="remember"></param>
        /// <returns></returns>
        public ActionResult LoginAsync(string name, string pass, int remember)
        {
            AjaxResult result = new AjaxResult();
            try
            {
                var query = (from m in SysAccountContract.Entities
                             where m.Login == name && m.Password == pass &&  m.IsDeleted ==false
                             select new LoginModel
                             {
                                 Id = m.Id,
                                 Name = m.SuName,
                                Role= m.Role
                             }).FirstOrDefault();

                if (query == null)
                {
                    result.Type = AjaxResultType.Error;
                    result.Content = "账号或密码错误！";
                }
                else
                {
                   

                    var data = JsonConvert.SerializeObject(query);
                    var ticket = new FormsAuthenticationTicket(
                                        1,
                                        name,
                                        DateTime.Now,
                                        DateTime.Now.Add(FormsAuthentication.Timeout),
                                        remember == 1,
                                        data);
                    var cookie = new HttpCookie(
                        FormsAuthentication.FormsCookieName,
                        FormsAuthentication.Encrypt(ticket));
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);

                    result.Type = AjaxResultType.Success;
                    result.Content = "登录成功！";

                    ///更新登陆时间
                    var updatedate = SysAccountContract.GetByKey(query.Id);
                    updatedate.LastLoginTime = DateTime.Now;
                    SysAccountContract.Update(updatedate);

                }
            }
            catch
            {
                result.Type = AjaxResultType.Error;
                result.Content = "系统异常！";
            }
            return JsonEx(result);
        }
    }
}