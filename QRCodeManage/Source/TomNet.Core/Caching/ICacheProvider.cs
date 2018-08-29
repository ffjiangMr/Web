namespace TomNet.Core.Caching
{
    /// <summary>
    /// 缓存提供者约定，用于创建并管理缓存对象
    /// </summary>
    public interface ICacheProvider
    {
        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="regionName">缓存区域名称</param>
        /// <returns></returns>
        ICache GetCache(string regionName);

    }
}