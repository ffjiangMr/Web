using System;


namespace TomNet.Core.Security
{
    /// <summary>
    /// 指定功能需要登录才能访问
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class LoginedAttribute : Attribute
    { }
}