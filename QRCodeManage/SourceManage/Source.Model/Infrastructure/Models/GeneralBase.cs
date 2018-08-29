using System;
using TomNet.Core.Data;

namespace Source.Model.Infrastructure
{
    /// <summary>
    /// 普通的数据模型
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class GeneralBase<TKey> : EntityBase<TKey>, IGeneral
          where TKey : IEquatable<TKey>
    {
        public bool IsLocked { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedTime { get; set; }
        public GeneralBase()
        {
            IsLocked = false;
            IsDeleted = false;
            CreatedTime = DateTime.Now;
        }
    }
}
