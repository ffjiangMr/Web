using System;
using System.Reflection;
using System.Web.Mvc;

using Autofac;
using Autofac.Integration.Mvc;

using TomNet.Core.Dependency;
using TomNet.Core.Security;
using TomNet.Web.Mvc.Initialize;


namespace TomNet.Autofac.Mvc
{
    /// <summary>
    /// Mvc-Autofac依赖注入初始化类
    /// </summary>
    public class MvcAutofacIocBuilder : IocBuilderBase
    {
        /// <summary>
        /// 初始化一个<see cref="MvcAutofacIocBuilder"/>类型的新实例
        /// </summary>
        /// <param name="services">服务信息集合</param>
        public MvcAutofacIocBuilder(IServiceCollection services)
            : base(services)
        { }

        /// <summary>
        /// 添加自定义服务映射
        /// </summary>
        /// <param name="services">服务信息集合</param>
        protected override void AddCustomTypes(IServiceCollection services)
        {
            services.AddInstance(this);
            services.AddSingleton<IIocResolver, MvcIocResolver>();
            services.AddSingleton<IFunctionHandler, MvcFunctionHandler>();
            services.AddSingleton<IFunctionTypeFinder, MvcControllerTypeFinder>();
            services.AddSingleton<IFunctionMethodInfoFinder, MvcActionMethodInfoFinder>();
        }

        /// <summary>
        /// 构建服务并设置MVC平台的Resolver
        /// </summary>
        /// <param name="services">服务映射信息集合</param>
        /// <param name="assemblies">程序集集合</param>
        /// <returns>服务提供者</returns>
        protected override IServiceProvider BuildAndSetResolver(IServiceCollection services, Assembly[] assemblies)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterControllers(assemblies).AsSelf().PropertiesAutowired();
            builder.RegisterFilterProvider();
            builder.Populate(services);
            IContainer container = builder.Build();
            AutofacDependencyResolver resolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(resolver);
            MvcIocResolver.GlobalResolveFunc = t => resolver.ApplicationContainer.Resolve(t);
            return resolver.GetService<IServiceProvider>();
        }
    }
}