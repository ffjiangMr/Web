using System.Collections.Generic;
using System.Linq;

using TomNet.Core.Configs.ConfigFile;


namespace TomNet.Core.Configs
{
    /// <summary>
    /// 基础日志配置信息
    /// </summary>
    public class BasicLoggingConfig
    {
        /// <summary>
        /// 初始化一个<see cref="BasicLoggingConfig"/>类型的新实例
        /// </summary>
        public BasicLoggingConfig()
        {
            AdapterConfigs = new List<LoggingAdapterConfig>();
        }

        /// <summary>
        /// 初始化一个<see cref="BasicLoggingConfig"/>类型的新实例
        /// </summary>
        internal BasicLoggingConfig(BasicLoggingElement element)
        {
            AdapterConfigs = element.Adapters.OfType<LoggingAdapterElement>()
                .Select(adapter => new LoggingAdapterConfig(adapter)).ToList();
        }

        /// <summary>
        /// 获取或设置 日志适配器配置信息集合
        /// </summary>
        public ICollection<LoggingAdapterConfig> AdapterConfigs { get; set; }
    }
}