using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;
using System.Web.Http.Dependencies;
using TomNet.Core.Dependency;
using TomNet.Web.WebApi.Internal;

namespace TomNet.Web.WebApi.Initialize
{
    /// <summary>
    /// WebApi依赖注入对象解析器
    /// </summary>
    public class WebApiIocResolver : IIocResolver
    {
        /// <summary>
        /// 获取指定类型的实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        /// <summary>
        /// 获取指定类型的实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            IDependencyScope scope = CallContext.LogicalGetData(InternalConstants.RequestLifetimeScopeKey) as IDependencyScope;
            if (scope != null)
            {
                return scope.GetService(type);
            }
            return GlobalConfiguration.Configuration.DependencyResolver.GetService(type);
        }

        /// <summary>
        /// 获取指定类型的所有实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public IEnumerable<T> Resolves<T>()
        {
            return Resolves(typeof(T)).Cast<T>();
        }

        /// <summary>
        /// 获取指定类型的所有实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public IEnumerable<object> Resolves(Type type)
        {
            return GlobalConfiguration.Configuration.DependencyResolver.GetServices(type);
        }
    }
}