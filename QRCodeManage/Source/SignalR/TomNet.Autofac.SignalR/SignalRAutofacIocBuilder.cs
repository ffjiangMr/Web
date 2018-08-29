using System;
using System.Reflection;
using System.Runtime.Remoting.Messaging;

using Autofac;
using Autofac.Integration.SignalR;

using Microsoft.AspNet.SignalR;

using TomNet.Core.Dependency;
using TomNet.Core.Security;
using TomNet.Web.SignalR.Initialize;


namespace TomNet.Autofac.SignalR
{
    /// <summary>
    /// SignalR-Autofac依赖注入初始化类
    /// </summary>
    public class SignalRAutofacIocBuilder : IocBuilderBase
    {
        /// <summary>
        /// 初始化一个<see cref="SignalRAutofacIocBuilder"/>类型的新实例
        /// </summary>
        /// <param name="services">服务信息集合</param>
        public SignalRAutofacIocBuilder(IServiceCollection services)
            : base(services)
        { }

        /// <summary>
        /// 添加自定义服务映射
        /// </summary>
        /// <param name="services">服务信息集合</param>
        protected override void AddCustomTypes(IServiceCollection services)
        {
            services.AddInstance(this);
            services.AddSingleton<IIocResolver, SignalRIocResolver>();
            services.AddSingleton<IFunctionHandler, SignalRFunctionHandler>();
            services.AddSingleton<IFunctionTypeFinder, SignalRHubTypeFinder>();
            services.AddSingleton<IFunctionMethodInfoFinder, SignalRHubMethodInfoFinder>();
        }

        /// <summary>
        /// 构建服务并设置SignalR平台的Resolver
        /// </summary>
        /// <param name="services">服务映射信息集合</param>
        /// <param name="assemblies">要检索的程序集集合</param>
        protected override IServiceProvider BuildAndSetResolver(IServiceCollection services, Assembly[] assemblies)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterHubs(assemblies).AsSelf().PropertiesAutowired();
            builder.RegisterLifetimeHubManager();
            builder.Populate(services);
            IContainer container = builder.Build();
            IDependencyResolver resolver = new AutofacDependencyResolver(container);
            GlobalHost.DependencyResolver = resolver;
            SignalRIocResolver.LifetimeResolveFunc = type =>
            {
                ILifetimeScope scope = CallContext.LogicalGetData(LifetimeHubManager.LifetimeScopeKey) as ILifetimeScope;
                if (scope == null)
                {
                    return null;
                }
                return scope.ResolveOptional(type);
            };
            return resolver.Resolve<IServiceProvider>();
        }
    }
}