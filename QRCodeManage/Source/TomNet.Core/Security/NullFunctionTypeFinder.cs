using System;
using System.Linq;


namespace TomNet.Core.Security
{
    /// <summary>
    /// 空的功能信息类型查找器
    /// </summary>
    public class NullFunctionTypeFinder : IFunctionTypeFinder
    {
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
            return new Type[0];
        }
    }
}