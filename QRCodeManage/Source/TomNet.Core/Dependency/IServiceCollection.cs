using System.Collections.Generic;


namespace TomNet.Core.Dependency
{
    /// <summary>
    /// 定义服务映射信息集合，用于装载注册类型映射的描述信息
    /// </summary>
    public interface IServiceCollection : IList<ServiceDescriptor>
    {
        /// <summary>
        /// 克隆创建当前集合的副本
        /// </summary>
        /// <returns></returns>
        IServiceCollection Clone();
    }
}