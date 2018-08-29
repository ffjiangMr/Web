using System;
using System.Linq;
using System.Reflection;

using Microsoft.AspNet.SignalR.Hubs;

using TomNet.Core.Reflection;
using TomNet.Core.Security;


namespace TomNet.Web.SignalR.Initialize
{
    /// <summary>
    /// SignalR Hub 类型查找器
    /// </summary>
    public class SignalRHubTypeFinder : IFunctionTypeFinder
    {
        /// <summary>
        /// 获取或设置 程序集查找器
        /// </summary>
        public IAllAssemblyFinder AssemblyFinder { get; set; }

        /// <summary>
        /// 查找指定条件的项
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        public Type[] Find(Func<Type, bool> predicate)
        {
            return FindAll().Where(predicate).ToArray();
        }

        /// <summary>
        /// 查找所有项
        /// </summary>
        /// <returns></returns>
        public Type[] FindAll()
        {
            Assembly[] assemblies = AssemblyFinder.FindAll();
            return assemblies.SelectMany(assembly => assembly.GetTypes()
                .Where(type => typeof(IHub).IsAssignableFrom(type) && !type.IsAbstract))
                .Distinct().ToArray();
        }
    }
}