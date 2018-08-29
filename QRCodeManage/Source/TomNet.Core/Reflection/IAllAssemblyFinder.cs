using TomNet.Core.Dependency;


namespace TomNet.Core.Reflection
{
    /// <summary>
    /// 定义所有程序集查找器
    /// </summary>
    public interface IAllAssemblyFinder : IAssemblyFinder, ISingletonDependency
    { }
}