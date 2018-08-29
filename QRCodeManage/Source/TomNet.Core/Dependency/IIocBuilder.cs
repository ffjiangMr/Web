using System;


namespace TomNet.Core.Dependency
{
    /// <summary>
    /// 定义依赖注入构建器，解析依赖注入服务映射信息进行构建
    /// </summary>
    public interface IIocBuilder
    {
        /// <summary>
        /// 获取 服务提供者
        /// </summary>
        IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 开始构建依赖注入映射
        /// </summary>
        /// <returns>服务提供者</returns>
        IServiceProvider Build();
    }
}