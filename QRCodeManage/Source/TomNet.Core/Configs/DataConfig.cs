using System.Collections.Generic;
using System.Linq;

using TomNet.Core.Configs.ConfigFile;


namespace TomNet.Core.Configs
{
    /// <summary>
    /// 数据配置信息
    /// </summary>
    public class DataConfig
    {
        /// <summary>
        /// 初始化一个<see cref="DataConfig"/>类型的新实例
        /// </summary>
        public DataConfig()
        {
            ContextConfigs = new List<DbContextConfig>();
        }

        /// <summary>
        /// 初始化一个<see cref="DataConfig"/>类型的新实例
        /// </summary>
        internal DataConfig(DataElement element)
        {
            ContextConfigs = element.Contexts.OfType<ContextElement>()
                .Select(context => new DbContextConfig(context)).ToList();
        }

        /// <summary>
        /// 获取或设置 上下文配置信息集合
        /// </summary>
        public ICollection<DbContextConfig> ContextConfigs { get; set; }
    }
}