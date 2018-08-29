using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Source.Admin.Web.Common;

namespace Source.Admin.Web.Filters
{
    public class GlobalAuthorization : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var attr = filterContext.ActionDescriptor.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>();

            bool isAnonymous = attr.Any(a => a is AllowAnonymousAttribute);

            if (isAnonymous)
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                try
                {
                    var cookies = CookiesManagement.GetCookies();
                    var ticket = CookiesManagement.GetTicket(cookies);
                    var model = CookiesManagement.GetLoginModel(ticket);

                    if (cookies == null || ticket == null || model == null)
                    {
                        var route = new RouteValueDictionary(new { controller = "Default", action = "Login" });
                        filterContext.Result = new RedirectToRouteResult("Default", route);
                        return;
                    }

                    if (!ticket.IsPersistent && ticket.Expired)
                    {
                        var route = new RouteValueDictionary(new { controller = "Default", action = "Login" });
                        filterContext.Result = new RedirectToRouteResult("Default", route);
                        return;
                    }

                    //此处加权限验证
                    CookiesManagement.RefreshCookies(ticket);
                    
                }
                catch
                {
                    var route = new RouteValueDictionary(new { controller = "Error", action = "Unauthorized" });
                    filterContext.Result = new RedirectToRouteResult("Default", route);
                    return;
                }
            }
        }

    }
}
