using System;

using Autofac;

using Microsoft.AspNet.SignalR.Hubs;


namespace TomNet.Autofac.SignalR
{
    /// <summary>
    /// Autofac注册扩展
    /// </summary>
    public static class RegisterExtensions
    {
        /// <summary>
        /// 注册生命周期作用域相关的类型
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterLifetimeHubManager(this ContainerBuilder builder)
        {
            bool flag = builder == null;
            if (flag)
            {
                throw new ArgumentNullException("builder");
            }
            builder.RegisterType<LifetimeHubManager>().SingleInstance();
            builder.RegisterType<AutofacHubActivator>().As<IHubActivator>().SingleInstance();
        }
    }
}