using System;
using System.Collections.Generic;
using System.Reflection;

using Autofac;
using Autofac.Builder;
using Autofac.Core;

using TomNet.Autofac.Hangfire.Initialize;
using TomNet.Core.Dependency;
using TomNet.Utility.Extensions;


namespace TomNet.Autofac
{
    /// <summary>
    /// Autofac类型映射注册操作类
    /// </summary>
    public static class AutofacRegistration
    {
        /// <summary>
        /// 使用<see cref="ServiceDescriptor"/>映射信息进行类型注册
        /// </summary>
        /// <param name="builder">容器构建器</param>
        /// <param name="descriptors">类型映射描述信息集合</param>
        public static void Populate(this ContainerBuilder builder, IEnumerable<ServiceDescriptor> descriptors)
        {
            builder.RegisterType<IocServiceProvider>().As<IServiceProvider>().SingleInstance();

            RegisterInternal(builder, descriptors);
        }

        private static void RegisterInternal(ContainerBuilder builder, IEnumerable<ServiceDescriptor> descriptors)
        {
            foreach (ServiceDescriptor descriptor in descriptors)
            {
                if (descriptor.ImplementationType != null)
                {
                    TypeInfo serviceTypeInfo = descriptor.ServiceType.GetTypeInfo();
                    if (serviceTypeInfo.IsGenericTypeDefinition)
                    {
                        if (!descriptor.ServiceType.IsGenericAssignableFrom(descriptor.ImplementationType))
                        {
                            throw new InvalidOperationException("泛型类型“{0}”不能由类型“{1}”指派".FormatWith(descriptor.ServiceType,
                                descriptor.ImplementationType));
                        }
                        builder.RegisterGeneric(descriptor.ImplementationType)
                            .As(descriptor.ServiceType)
                            .AsSelf()
                            .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                            .ConfigureLifetimeStyle(descriptor.Lifetime);
                    }
                    else
                    {
                        if (!descriptor.ServiceType.IsAssignableFrom(descriptor.ImplementationType))
                        {
                            throw new InvalidOperationException("类型“{0}”不能由类型“{1}”指派".FormatWith(descriptor.ServiceType, descriptor.ImplementationType));
                        }
                        builder.RegisterType(descriptor.ImplementationType)
                            .As(descriptor.ServiceType)
                            .AsSelf()
                            .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                            .ConfigureLifetimeStyle(descriptor.Lifetime);
                    }
                }
                else if (descriptor.ImplementationFactory != null)
                {
                    IComponentRegistration registration = RegistrationBuilder.ForDelegate(descriptor.ServiceType,
                        (context, paramters) =>
                        {
                            IServiceProvider provider = context.Resolve<IServiceProvider>();
                            return descriptor.ImplementationFactory(provider);
                        })
                        .ConfigureLifetimeStyle(descriptor.Lifetime)
                        .CreateRegistration();
                    builder.RegisterComponent(registration);
                }
                else if (descriptor.ImplementationInstance != null)
                {
                    builder.RegisterInstance(descriptor.ImplementationInstance)
                        .As(descriptor.ServiceType)
                        .AsSelf()
                        .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                        .ConfigureLifetimeStyle(descriptor.Lifetime);
                }
            }
        }

        private static IRegistrationBuilder<object, T, TU> ConfigureLifetimeStyle<T, TU>(this IRegistrationBuilder<object, T, TU> builder,
            LifetimeStyle lifetime)
        {
            switch (lifetime)
            {
                case LifetimeStyle.Transient:
                    builder.InstancePerDependency();
                    break;
                case LifetimeStyle.Scoped:
                    builder.InstancePerBackgroundJob();
                    break;
                case LifetimeStyle.Singleton:
                    builder.SingleInstance();
                    break;
            }
            return builder;
        }
    }
}