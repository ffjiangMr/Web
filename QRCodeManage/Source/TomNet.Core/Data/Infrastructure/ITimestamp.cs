namespace TomNet.Core.Data
{
    /// <summary>
    /// 用于获取或设置 版本控制标识,用于处理并发
    /// </summary>
    public interface ITimestamp
    {
        /// <summary>
        /// 获取或设置 版本控制标识，用于处理并发
        /// </summary>
        byte[] Timestamp { get; set; }
    }
}