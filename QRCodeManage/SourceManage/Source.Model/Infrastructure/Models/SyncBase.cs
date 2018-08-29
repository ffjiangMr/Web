using Source.Model.Infrastructure;
using System;

using TomNet.Core.Data;
using TomNet.Utility.Data;

namespace SourceModel.Infrastructure
{
    /// <summary>
    /// 用于同步的数据模型
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class SyncBase<TKey> : GeneralBase<TKey>, ISync
        where TKey : IEquatable<TKey>
    {
        public Guid SyncId { get; set; }
        public SyncBase()
        {
            SyncId = CombHelper.NewComb();
        }
    }
}
