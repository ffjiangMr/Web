using System;

namespace Source.Model.Infrastructure
{
    /// <summary>
    /// 用于同步数据的模型接口
    /// </summary>
    public interface ISync : IGeneral
    {
        Guid SyncId { get; set; }
    }
}
