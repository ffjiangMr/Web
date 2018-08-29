using System;
using System.Configuration;

using TomNet.Core.Configs.ConfigFile;


namespace TomNet.Core.Configs
{
    /// <summary>
    /// TomNet配置类
    /// </summary>
    public sealed class TomNetConfig
    {
        private const string TomNetSectionName = "tomnet";
        private static readonly Lazy<TomNetConfig> InstanceLazy
            = new Lazy<TomNetConfig>(() => new TomNetConfig());

        /// <summary>
        /// 初始化一个新的<see cref="TomNetConfig"/>实例
        /// </summary>
        private TomNetConfig()
        {
            TomNetFrameworkSection section = (TomNetFrameworkSection)ConfigurationManager.GetSection(TomNetSectionName);
            if (section == null)
            {
                DataConfig = new DataConfig();
                LoggingConfig = new LoggingConfig();
                return;
            }
            DataConfig = new DataConfig(section.Data);
            LoggingConfig = new LoggingConfig(section.Logging);
        }

        /// <summary>
        /// 获取 配置类的单一实例
        /// </summary>
        public static TomNetConfig Instance
        {
            get
            {
                TomNetConfig config = InstanceLazy.Value;
                if (DataConfigReseter != null)
                {
                    config.DataConfig = DataConfigReseter.Reset(config.DataConfig);
                }
                if (LoggingConfigReseter != null)
                {
                    config.LoggingConfig = LoggingConfigReseter.Reset(config.LoggingConfig);
                }
                return config;
            }
        }

        /// <summary>
        /// 获取或设置 数据配置重置信息
        /// </summary>
        public static IDataConfigReseter DataConfigReseter { get; set; }

        /// <summary>
        /// 获取或设置 日志配置重置信息
        /// </summary>
        public static ILoggingConfigReseter LoggingConfigReseter { get; set; }

        /// <summary>
        /// 获取或设置 数据配置信息
        /// </summary>
        public DataConfig DataConfig { get; set; }

        /// <summary>
        /// 获取或设置 日志配置信息
        /// </summary>
        public LoggingConfig LoggingConfig { get; set; }
    }
}