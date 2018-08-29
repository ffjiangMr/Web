using System.Web.Mvc;

using TomNet.Utility.Extensions;


namespace TomNet.Web.Mvc.Extensions
{
    /// <summary>
    /// Controller相关扩展方法
    /// </summary>
    public static class ControllerContextExtensions
    {
        /// <summary>
        /// 获取Area名
        /// </summary>
        /// <param name="context">MVC控制器上下文</param>
        /// <returns></returns>
        public static string GetAreaName(this ControllerContext context)
        {
            string area = null;
            object value;
            if (context.RequestContext.RouteData.DataTokens.TryGetValue("area", out value))
            {
                area = (string)value;
                if (area.IsNullOrWhiteSpace())
                {
                    area = null;
                }
            }
            return area;
        }

        /// <summary>
        /// 获取Controller名
        /// </summary>
        /// <param name="context">MVC控制器上下文</param>
        /// <returns></returns>
        public static string GetControllerName(this ControllerContext context)
        {
            return context.RequestContext.RouteData.Values["controller"].ToString();
        }

        /// <summary>
        /// 获取Action名
        /// </summary>
        /// <param name="context">MVC控制器上下文</param>
        /// <returns></returns>
        public static string GetActionName(this ControllerContext context)
        {
            return context.RequestContext.RouteData.Values["action"].ToString();
        }
    }
}