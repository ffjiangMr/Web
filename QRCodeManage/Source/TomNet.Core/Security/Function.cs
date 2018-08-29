using System;
using System.ComponentModel;

using TomNet.Utility.Data;


namespace TomNet.Core.Security
{
    /// <summary>
    /// 实体类——功能信息
    /// </summary>
    [Description("权限-功能信息")]
    public class Function : FunctionBase<Guid>
    {
        /// <summary>
        /// 初始化一个<see cref="Function"/>类型的新实例
        /// </summary>
        public Function()
        {
            Id = CombHelper.NewComb();
        }

    }
}