using TomNet.Core;
using TomNet.Core.Dependency;
using TomNet.Utility;

using Owin;


namespace TomNet.App.Local.Initialize
{
    /// <summary>
    /// <see cref="IAppBuilder"/>初始化扩展
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// 初始化本地程序集框架
        /// </summary>
        public static IAppBuilder UseLocalInitialize(this IAppBuilder app, IIocBuilder iocBuilder)
        {
            iocBuilder.CheckNotNull("iocBuilder" );
            IFrameworkInitializer initializer = new FrameworkInitializer();
            initializer.Initialize(iocBuilder);
            return app;
        }
    }
}