using System.Linq;

using TomNet.Core.Configs;
using TomNet.Core.Reflection;


namespace TomNet.Data.Entity
{
    /// <summary>
    /// 数据配置信息重置类
    /// </summary>
    public class DataConfigReseter : IDataConfigReseter
    {
        /// <summary>
        /// 初始化一个<see cref="DataConfigReseter"/>类型的新实例
        /// </summary>
        public DataConfigReseter()
        {
            MapperAssemblyFinder = new EntityMapperAssemblyFinder()
            {
                AllAssemblyFinder = new DirectoryAssemblyFinder()
            };
        }

        /// <summary>
        /// 获取或设置 实体映射程序集查找器
        /// </summary>
        public IEntityMapperAssemblyFinder MapperAssemblyFinder { get; set; }

        /// <summary>
        /// 重置数据配置信息
        /// </summary>
        /// <param name="config">原始数据配置信息</param>
        /// <returns>重置后的数据配置信息</returns>
        public DataConfig Reset(DataConfig config)
        {
            //没有上下文，添加默认上下文
            if (!config.ContextConfigs.Any())
            {
                DbContextConfig contextConfig = GetDefaultDbContextConfig();
                config.ContextConfigs.Add(contextConfig);
            }
            return config;
        }

        /// <summary>
        /// 获取默认业务上下文配置信息
        /// </summary>
        /// <returns></returns>
        protected virtual DbContextConfig GetDefaultDbContextConfig()
        {
            return new DbContextConfig()
            {
                ConnectionStringName = "default",
                ContextType = typeof(DefaultDbContext),
                InitializerConfig = new DbContextInitializerConfig()
                {
                    InitializerType = typeof(DefaultDbContextInitializer),
                    EntityMapperAssemblies = MapperAssemblyFinder.FindAll()
                }
            };
        }
    }
}