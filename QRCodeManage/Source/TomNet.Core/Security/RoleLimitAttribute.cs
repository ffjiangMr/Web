using System;


namespace TomNet.Core.Security
{
    /// <summary>
    /// 指定功能只允许特定角色可以访问
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class RoleLimitAttribute : Attribute
    { }
}