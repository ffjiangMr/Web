using System;
using System.Linq;
using System.Reflection;


namespace TomNet.Core.Security
{
    /// <summary>
    /// 空的方法查找器
    /// </summary>
    public class NullFunctionMethodInfoFinder : IFunctionMethodInfoFinder
    {
        /// <summary>
        /// 查找指定条件的方法信息
        /// </summary>
        /// <param name="type">控制器类型</param>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        public MethodInfo[] Find(Type type, Func<MethodInfo, bool> predicate)
        {
            return FindAll(type).Where(predicate).ToArray();
        }

        /// <summary>
        /// 从指定类型查找方法信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public MethodInfo[] FindAll(Type type)
        {
            return new MethodInfo[0];
        }
    }
}