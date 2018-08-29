using System;
using System.Linq;

using TomNet.Core.Configs;
using TomNet.Core.Data;
using TomNet.Core.Dependency;
using TomNet.Data.Entity.Properties;
using TomNet.Utility.Extensions;


namespace TomNet.Data.Entity
{
    /// <summary>
    /// 服务映射集合扩展操作
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加数据层服务映射信息
        /// </summary>
        /// <param name="services">服务映射信息集合</param>
        public static void AddDataServices(this IServiceCollection services)
        {
            //添加上下文类型
            if (TomNetConfig.DataConfigReseter == null)
            {
                TomNetConfig.DataConfigReseter = new DataConfigReseter();
            }
            DataConfig config = TomNetConfig.Instance.DataConfig;
            Type[] contextTypes = config.ContextConfigs.Where(m => m.Enabled).Select(m => m.ContextType).ToArray();
            Type baseType = typeof(IUnitOfWork);
            foreach (var contextType in contextTypes)
            {
                if (!baseType.IsAssignableFrom(contextType))
                {
                    throw new InvalidOperationException(Resources.ContextTypeNotIUnitOfWorkType.FormatWith(contextType));
                }
                services.AddScoped(baseType, contextType);
                services.AddScoped(contextType);
            }

            //其他数据层初始化类型
            services.AddSingleton<IEntityMapperAssemblyFinder, EntityMapperAssemblyFinder>();
        }
    }
}