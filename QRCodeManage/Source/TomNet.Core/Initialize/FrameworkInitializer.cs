using System;
using System.Collections.Generic;

using TomNet.Core.Configs;
using TomNet.Core.Dependency;
using TomNet.Core.Initialize;

using TomNet.Core.Security;
using TomNet.Utility;


namespace TomNet.Core
{
    /// <summary>
    /// 框架初始化
    /// </summary>
    public class FrameworkInitializer : IFrameworkInitializer
    {
        //基础模块，只初始化一次        
        private static bool _basicLoggingInitialized;
        private static bool _databaseInitialized;

        /// <summary>
        /// 开始执行框架初始化
        /// </summary>
        /// <param name="iocBuilder">依赖注入构建器</param>
        public void Initialize(IIocBuilder iocBuilder)
        {
            iocBuilder.CheckNotNull("iocBuilder");

            TomNetConfig config = TomNetConfig.Instance;

            //依赖注入初始化
            IServiceProvider provider = iocBuilder.Build();

            //日志功能初始化
            IBasicLoggingInitializer loggingInitializer = provider.GetService<IBasicLoggingInitializer>();
            if (!_basicLoggingInitialized && loggingInitializer != null)
            {
                loggingInitializer.Initialize(config.LoggingConfig);
                _basicLoggingInitialized = true;
            }

            //数据库初始化
            IDatabaseInitializer databaseInitializer = provider.GetService<IDatabaseInitializer>();
            if (!_databaseInitialized && databaseInitializer != null)
            {
                databaseInitializer.Initialize(config.DataConfig);
                _databaseInitialized = true;
            }


            //功能信息初始化
            IFunctionHandler functionHandler = provider.GetService<IFunctionHandler>();
            if (functionHandler != null)
            {
                functionHandler.Initialize();
            }
        }
    }
}