using TomNet.Core.Configs;
using TomNet.Core.Initialize;
using TomNet.Utility.Logging;


namespace TomNet.SiteBase.Initialize
{
    /// <summary>
    /// log4net日志初始化器，用于初始化基础日志功能
    /// </summary>
    public class Log4NetLoggingInitializer : LoggingInitializerBase, IBasicLoggingInitializer
    {
        /// <summary>
        /// 开始初始化基础日志
        /// </summary>
        /// <param name="config">日志配置信息</param>
        public void Initialize(LoggingConfig config)
        {
            LogManager.SetEntryInfo(config.EntryConfig.Enabled, config.EntryConfig.EntryLogLevel);
            foreach (LoggingAdapterConfig adapterConfig in config.BasicLoggingConfig.AdapterConfigs)
            {
                SetLoggingFromAdapterConfig(adapterConfig);
            }
        }
    }
}