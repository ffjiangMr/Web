using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace TomNet.Web.WebApi.Filters
{
    public abstract class BasicApiAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var attr = actionContext.ActionDescriptor
                .GetCustomAttributes<AllowAnonymousAttribute>()
                .OfType<AllowAnonymousAttribute>();

            bool isAnonymous = attr.Any(a => a is AllowAnonymousAttribute);

            if (isAnonymous)
            {
                base.OnActionExecuting(actionContext);
            }
            else
            {
                string token = string.Empty;

                if (actionContext.Request.Headers.Contains("Token"))
                    token = HttpUtility.UrlDecode(actionContext.Request.Headers.GetValues("Token").FirstOrDefault());
                if (!string.IsNullOrEmpty(token))
                {
                    if (ValidateToken(token))
                        base.OnActionExecuting(actionContext);
                    else
                        actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }
                else
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }
            }
        }

        public virtual bool ValidateToken(string token)
        {
            return token == "tommy";
        }
    }
}
