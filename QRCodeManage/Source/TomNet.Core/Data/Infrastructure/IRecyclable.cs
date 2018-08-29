namespace TomNet.Core.Data
{
    /// <summary>
    /// 表示实体将启用回收站机制，包含逻辑删除属性，运行逻辑如下：
    /// 1.实体删除时，将执行逻辑删除，而非物理删除
    /// 2.正常数据筛选时，将自动过滤已逻辑删除的信息
    /// 3.实体还原时，必须已逻辑删除
    /// 4.实体物理删除时，必须已逻辑删除
    /// </summary>
    public interface IRecyclable
    {
        /// <summary>
        /// 获取或设置 是否已逻辑删除
        /// </summary>
        bool IsDeleted { get; set; }
    }


    /// <summary>
    /// 回收站操作类型
    /// </summary>
    public enum RecycleOperation
    {
        /// <summary>
        /// 逻辑删除
        /// </summary>
        LogicDelete,

        /// <summary>
        /// 还原
        /// </summary>
        Restore,

        /// <summary>
        /// 物理删除
        /// </summary>
        PhysicalDelete
    }
}