using System;
using System.Web.Http;
using TomNet.Utility.Logging;
using TomNet.Web.Mvc.UI;
using TomNet.Web.WebApi.Messages;

namespace TomNet.Web.WebApi
{
    /// <summary>
    /// WebAPI的控制器基类
    /// </summary>

    public abstract class BaseApiController : ApiController
    {
        protected readonly ILogger Logger;

        protected BaseApiController()
        {
            Logger = LogManager.GetLogger(GetType());
        }
        /// <summary>
        /// 获取或设置 依赖注入服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

        protected TokenResult Token(AjaxResult result, string token)
        {
            return new TokenResult(result, token, Request);
        }
    }
}
