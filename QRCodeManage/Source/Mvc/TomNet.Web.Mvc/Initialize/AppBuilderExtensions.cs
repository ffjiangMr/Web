using System.Web.Mvc;

using TomNet.Core;
using TomNet.Core.Dependency;
using TomNet.Utility;
using TomNet.Web.Mvc.Binders;

using Owin;


namespace TomNet.Web.Mvc.Initialize
{
    /// <summary>
    /// <see cref="IAppBuilder"/>初始化扩展
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// 初始化Mvc框架
        /// </summary>
        public static IAppBuilder UseTomNetMvc(this IAppBuilder app, IIocBuilder iocBuilder)
        {
            iocBuilder.CheckNotNull("iocBuilder");

            ModelBinders.Binders.Add(typeof(string), new StringTrimModelBinder());

            IFrameworkInitializer initializer = new FrameworkInitializer();
            initializer.Initialize(iocBuilder);
            return app;
        }
    }
}