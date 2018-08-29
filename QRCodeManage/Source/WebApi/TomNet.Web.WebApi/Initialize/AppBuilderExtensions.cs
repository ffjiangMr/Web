using System.Net.Http.Formatting;
using System.Web.Http;

using Microsoft.Owin.Security.OAuth;

using TomNet.Core;
using TomNet.Core.Dependency;
using TomNet.Utility;
using TomNet.Web.WebApi.Filters;

using Owin;
using TomNet.Web.WebApi.Handlers;

namespace TomNet.Web.WebApi.Initialize
{
    /// <summary>
    /// <see cref="IAppBuilder"/>初始化扩展
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// 初始化WebApi框架
        /// </summary>
        public static IAppBuilder UseTomNetWebApi(this IAppBuilder app, IIocBuilder iocBuilder)
        {
            iocBuilder.CheckNotNull("iocBuilder");
            IFrameworkInitializer initializer = new FrameworkInitializer();
            initializer.Initialize(iocBuilder);
            return app;
        }

        /// <summary>
        /// 初始化WebApi
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IAppBuilder ConfigureWebApi(this IAppBuilder app)
        {
            HttpConfiguration config = GlobalConfiguration.Configuration;

            //注册请求生命周期Scope的处理器
            config.MessageHandlers.Add(new RequestLifetimeScopeHandler());

            //全局异常处理
            config.Filters.Add(new ExceptionHandlingAttribute());
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.EnsureInitialized();
            return app;
        }
    }
}