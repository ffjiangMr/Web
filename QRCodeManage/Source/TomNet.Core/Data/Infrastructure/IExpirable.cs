using System;


namespace TomNet.Core.Data
{
    /// <summary>
    /// 定义可过期性，包含生效时间与过期时间
    /// </summary>
    public interface IExpirable
    {
        /// <summary>
        /// 获取或设置 生效时间
        /// </summary>
        DateTime? BeginTime { get; set; }

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        DateTime? EndTime { get; set; }
    }
}