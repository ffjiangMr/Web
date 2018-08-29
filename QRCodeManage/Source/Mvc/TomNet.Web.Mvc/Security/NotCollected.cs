using System;
using System.Web.Mvc;

namespace TomNet.Web.Mvc.Security
{
    /// <summary>
    /// 不采集的方法
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NotCollectedAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }
}
