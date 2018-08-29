using TomNet.Core.Data;

namespace Source.Model.Infrastructure
{
    /// <summary>
    /// 常用的数据模型接口 - 带创建时间，是否锁定，是否可回收
    /// </summary>
    public interface IGeneral : ICreatedTime, ILockable, IRecyclable
    {

    }
}
