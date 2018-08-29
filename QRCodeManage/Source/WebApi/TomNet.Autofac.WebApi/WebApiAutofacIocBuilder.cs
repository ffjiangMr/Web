using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dependencies;

using Autofac;
using Autofac.Integration.WebApi;

using TomNet.Core.Dependency;
using TomNet.Core.Security;
using TomNet.Web.WebApi.Initialize;


namespace TomNet.Autofac.Http
{
    /// <summary>
    /// WebApi-Autofac依赖注入初始化类
    /// </summary>
    public class WebApiAutofacIocBuilder : IocBuilderBase
    {
        /// <summary>
        /// 初始化一个<see cref="WebApiAutofacIocBuilder"/>类型的新实例
        /// </summary>
        /// <param name="services">服务信息集合</param>
        public WebApiAutofacIocBuilder(IServiceCollection services)
            : base(services)
        { }

        /// <summary>
        /// 添加自定义服务映射
        /// </summary>
        /// <param name="services">服务信息集合</param>
        protected override void AddCustomTypes(IServiceCollection services)
        {
            services.AddInstance(this);
            services.AddSingleton<IIocResolver, WebApiIocResolver>();
            services.AddSingleton<IFunctionHandler, WebApiFunctionHandler>();
            services.AddSingleton<IFunctionTypeFinder, WebApiControllerTypeFinder>();
            services.AddSingleton<IFunctionMethodInfoFinder, WebApiActionMethodInfoFinder>();
        }

        /// <summary>
        /// 构建服务并设置WebApi平台的Resolver
        /// </summary>
        /// <param name="services">服务映射信息集合</param>
        /// <param name="assemblies">要检索的程序集集合</param>
        /// <returns>服务提供者</returns>
        protected override IServiceProvider BuildAndSetResolver(IServiceCollection services, Assembly[] assemblies)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterApiControllers(assemblies).AsSelf().PropertiesAutowired();
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
            builder.RegisterWebApiModelBinderProvider();
            builder.Populate(services);
            IContainer container = builder.Build();
            IDependencyResolver resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
            return (IServiceProvider)resolver.GetService(typeof(IServiceProvider));
        }
    }
}