using Microsoft.AspNet.SignalR;

using TomNet.Core;
using TomNet.Core.Dependency;
using TomNet.Utility;

using Owin;


namespace TomNet.Web.SignalR.Initialize
{
    /// <summary>
    /// <see cref="IAppBuilder"/>初始化扩展
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// 初始化SignalR框架
        /// </summary>
        public static IAppBuilder UseTomNetSignalR(this IAppBuilder app, IIocBuilder iocBuilder)
        {
            iocBuilder.CheckNotNull("iocBuilder");
            IFrameworkInitializer initializer = new FrameworkInitializer();
            initializer.Initialize(iocBuilder);
            return app;
        }

        /// <summary>
        /// 初始化SignalR
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IAppBuilder ConfigureSignalR(this IAppBuilder app)
        {
            app.MapSignalR(new HubConfiguration()
            {
                EnableDetailedErrors = true
            });
            return app;
        }
    }
}