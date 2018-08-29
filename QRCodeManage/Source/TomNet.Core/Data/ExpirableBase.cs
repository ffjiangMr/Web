using System;


namespace TomNet.Core.Data
{
    /// <summary>
    /// 可过期实体基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ExpirableBase<TKey> : EntityBase<TKey>, IExpirable 
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 获取或设置 生效时间
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 验证时间有效性
        /// </summary>
        public void ThrowIfTimeInvalid()
        {
            if (!BeginTime.HasValue || !EndTime.HasValue || BeginTime.Value <= EndTime.Value)
            {
                return;
            }
            throw new IndexOutOfRangeException("生效时间不能大于过期时间");
        }
    }
}