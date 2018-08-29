using System;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using Source.Admin.Web.Models;
using Source.Admin.Web.Models.ViewModel;

namespace Source.Admin.Web.Common
{
    public class CookiesManagement
    {
        public static HttpCookie GetCookies()
        {
            try
            {
                string cookieName = FormsAuthentication.FormsCookieName;
                return HttpContext.Current.Request.Cookies[cookieName];
            }
            catch
            {
                return null;
            }

        }

        public static FormsAuthenticationTicket GetTicket()
        {
            try
            {
                var cookies = GetCookies();
                return cookies == null ? null : FormsAuthentication.Decrypt(cookies.Value);
            }
            catch
            {
                return null;
            }
        }
        public static FormsAuthenticationTicket GetTicket(HttpCookie cookies)
        {
            try
            {
                return  FormsAuthentication.Decrypt(cookies.Value);
            }
            catch
            {
                return null;
            }
        }
        public static LoginModel GetLoginModel(FormsAuthenticationTicket ticket)
        {
            try
            {
                return JsonConvert.DeserializeObject<LoginModel>(ticket.UserData);
            }
            catch
            {
                return null;
            }
        }

        public static void RefreshCookies(FormsAuthenticationTicket ticket, LoginModel model = null)
        {

            var cookies = GetCookies();
            if (cookies == null) return;
            if (ticket.IsPersistent) return;
            string userData = model == null ? "" : JsonConvert.SerializeObject(model);

            var nticket = new FormsAuthenticationTicket(
                                        1,
                                        ticket.Name,
                                        DateTime.Now,
                                        DateTime.Now.Add(FormsAuthentication.Timeout),
                                        ticket.IsPersistent,
                                        userData == "" ? ticket.UserData : userData);

            cookies.Value= FormsAuthentication.Encrypt(nticket);
            HttpContext.Current.Response.Cookies.Set(cookies);

        }
    }
}