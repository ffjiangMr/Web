using System;
using System.Text;
using System.Web.Mvc;

using TomNet.Utility.Logging;
using TomNet.Web.Mvc.UI;


namespace TomNet.Web.Mvc
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public abstract class BaseController : Controller
    {
        protected readonly ILogger Logger;

        protected BaseController()
        {
            Logger = LogManager.GetLogger(GetType());
        }

        /// <summary>
        /// 获取或设置 依赖注入服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// Called when an unhandled exception occurs in the action.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnException(ExceptionContext filterContext)
        {
            Exception exception = filterContext.Exception;
            Logger.Error(exception.Message, exception);
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var message = "Ajax请求异常：";
                if (exception is HttpAntiForgeryException)
                {
                    message += "安全性验证失败。<br>请刷新页面重试，详情请查看系统日志。";
                }
                else
                {
                    message += exception.Message;
                }
                filterContext.Result = Json(new AjaxResult(message, AjaxResultType.Error));
                filterContext.ExceptionHandled = true;
            }
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResultEx
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

        /// <summary>
        /// 返回JsonResult      
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="behavior">行为</param>
        /// <param name="dateFormatStr">json中dateTime类型的格式</param>
        /// <returns>Json</returns>
        protected JsonResult JsonEx(object data, JsonRequestBehavior behavior, string dateFormatStr = "yyyy-MM-dd")
        {
            return new JsonResultEx
            {
                Data = data,
                JsonRequestBehavior = behavior,
                DateFormatString = dateFormatStr
            };
        }

        /// <summary>
        /// 返回JsonResult     
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="dateFormatStr">数据格式</param>
        /// <returns>Json</returns>
        protected JsonResult JsonEx(object data, string dateFormatStr)
        {
            return new JsonResultEx
            {
                Data = data,
                DateFormatString = dateFormatStr
            };
        }
        /// <summary>
        /// 返回JsonResult     
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>Json</returns>
        protected JsonResult JsonEx(object data)
        {
            return new JsonResultEx
            {
                Data = data
            };
        }
    }
}