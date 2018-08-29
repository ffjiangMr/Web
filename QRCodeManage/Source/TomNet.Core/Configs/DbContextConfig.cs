using System;

using TomNet.Core.Configs.ConfigFile;
using TomNet.Core.Properties;
using TomNet.Utility.Extensions;


namespace TomNet.Core.Configs
{
    /// <summary>
    /// TomNet数据上下文配置
    /// </summary>
    public class DbContextConfig
    {
        /// <summary>
        /// 初始化一个<see cref="DbContextConfig"/>类型的新实例
        /// </summary>
        public DbContextConfig()
        {
            Name = Guid.NewGuid().ToString();
            Enabled = true;
        }

        /// <summary>
        /// 初始化一个<see cref="DbContextConfig"/>类型的新实例
        /// </summary>
        internal DbContextConfig(ContextElement element)
        {
            Name = element.Name;
            Enabled = element.Enabled;
            ConnectionStringName = element.ConnectionStringName;
            ContextType = Type.GetType(element.ContextTypeName);
            if (ContextType == null)
            {
                throw new InvalidOperationException(Resources.ConfigFile_NameToTypeIsNull.FormatWith(element.ContextTypeName));
            }
            InitializerConfig = new DbContextInitializerConfig(element.DbContextInitializer);
        }
        
        /// <summary>
        /// 获取或设置 上下文名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 是否可用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 获取或设置 数据库连接串名称
        /// </summary>
        public string ConnectionStringName { get; set; }

        /// <summary>
        /// 获取或设置 数据上下文类型
        /// </summary>
        public Type ContextType { get; set; }

        /// <summary>
        /// 获取或设置 数据上下文初始化配置
        /// </summary>
        public DbContextInitializerConfig InitializerConfig { get; set; }
    }
}