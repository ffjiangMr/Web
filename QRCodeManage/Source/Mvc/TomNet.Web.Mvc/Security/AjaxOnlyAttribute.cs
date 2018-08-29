using System;
using System.Web.Mvc;

using TomNet.Web.Mvc.Properties;


namespace TomNet.Web.Mvc.Security
{
    /// <summary>
    /// 限制当前功能只允许以Ajax的方式来访问
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AjaxOnlyAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Called before an action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new ContentResult
                {
                    Content = Resources.Mvc_ActionAttribute_AjaxOnlyMessage
                };
            }
        }
    }
}