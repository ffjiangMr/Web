using TomNet.Core.Configs;


namespace TomNet.Core.Initialize
{
    /// <summary>
    /// 定义基础日志初始化器，用于初始化基础日志功能
    /// </summary>
    public interface IBasicLoggingInitializer
    {
        /// <summary>
        /// 开始初始化基础日志
        /// </summary>
        /// <param name="config">日志配置信息</param>
        void Initialize(LoggingConfig config);
    }
}