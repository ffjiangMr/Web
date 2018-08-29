namespace TomNet.Core.Security
{
    /// <summary>
    /// 表示功能访问类型的枚举
    /// </summary>
    public enum FunctionType
    {
        /// <summary>
        /// 匿名用户可访问
        /// </summary>
        Anonymouse = 0,

        /// <summary>
        /// 登录用户可访问
        /// </summary>
        Logined = 1,

        /// <summary>
        /// 指定角色可访问
        /// </summary>
        RoleLimit = 2
    }
}