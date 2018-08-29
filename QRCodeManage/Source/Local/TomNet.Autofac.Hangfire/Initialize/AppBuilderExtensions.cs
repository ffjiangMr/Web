
using TomNet.Core;
using TomNet.Core.Dependency;
using TomNet.Utility;

using Owin;


namespace TomNet.Autofac.Hangfire.Initialize
{
    /// <summary>
    /// <see cref="IAppBuilder"/>初始化扩展
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// 初始化TomNet框架的Hangfire功能
        /// </summary>
        public static IAppBuilder UseTomNetHangfile(this IAppBuilder app, IIocBuilder iocBuilder)
        {
            iocBuilder.CheckNotNull("iocBuilder");
            IFrameworkInitializer initializer = new FrameworkInitializer();
            initializer.Initialize(iocBuilder);
            return app;
        }
    }
}
