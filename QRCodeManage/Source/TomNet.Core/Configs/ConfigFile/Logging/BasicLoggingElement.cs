using System.Configuration;


namespace TomNet.Core.Configs.ConfigFile
{
    /// <summary>
    /// 基础日志配置节点
    /// </summary>
    internal class BasicLoggingElement : ConfigurationElement
    {
        private const string AdapterKey = "adapters";

        /// <summary>
        /// 获取或设置 日志适配器配置节点集合
        /// </summary>
        [ConfigurationProperty(AdapterKey)]
        public virtual LoggingAdapterCollection Adapters
        {
            get { return (LoggingAdapterCollection)base[AdapterKey]; }
        }
    }
}