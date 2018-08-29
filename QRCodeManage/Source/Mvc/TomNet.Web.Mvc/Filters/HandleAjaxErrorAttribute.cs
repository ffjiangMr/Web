using System;
using System.Web.Mvc;

using TomNet.Web.Mvc.UI;


namespace TomNet.Web.Mvc.Filters
{
    /// <summary>
    /// 表示一个特性，该特性用于处理Ajax操作方法引发的异常，此特性应在<see cref="System.Web.Mvc.HandleErrorAttribute"/>之前调用。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class HandleAjaxErrorAttribute : FilterAttribute, IExceptionFilter
    {
        #region Implementation of IExceptionFilter

        /// <summary>
        /// 在发生异常时调用。
        /// </summary>
        /// <param name="filterContext">筛选器上下文。</param>
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult()
                {
                    Data = new AjaxResult("Ajax操作引发异常：" + filterContext.Exception.Message, AjaxResultType.Error),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                filterContext.ExceptionHandled = true;
            }
        }

        #endregion
    }
}