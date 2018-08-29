using TomNet.Core.Dependency;


namespace TomNet.Core
{
    /// <summary>
    /// 框架初始化接口
    /// </summary>
    public interface IFrameworkInitializer
    {
        /// <summary>
        /// 开始执行框架初始化
        /// </summary>
        /// <param name="iocBuilder">依赖注入构建器</param>
        void Initialize(IIocBuilder iocBuilder);
    }
}