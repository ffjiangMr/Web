using TomNet.Core.Configs;
using TomNet.Core.Dependency;
using TomNet.Core.Initialize;
using TomNet.SiteBase.Initialize;


namespace TomNet.Logging.Log4Net
{
    /// <summary>
    /// 服务映射信息集合扩展操作
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Log4Net日志功能相关映射信息
        /// </summary>
        public static void AddLog4NetServices(this IServiceCollection services)
        {
            if (TomNetConfig.LoggingConfigReseter == null)
            {
                TomNetConfig.LoggingConfigReseter = new Log4NetLoggingConfigReseter();
            }
            services.AddSingleton<IBasicLoggingInitializer, Log4NetLoggingInitializer>();
            services.AddSingleton<Log4NetLoggerAdapter>();
        }
    }
}