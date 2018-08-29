namespace TomNet.Core.Configs
{
    /// <summary>
    /// TomNet数据配置信息重置类
    /// </summary>
    public interface IDataConfigReseter
    {
        /// <summary>
        /// 重置数据配置信息
        /// </summary>
        /// <param name="config">原始数据配置信息</param>
        /// <returns>重置后的数据配置信息</returns>
        DataConfig Reset(DataConfig config);
    }
}