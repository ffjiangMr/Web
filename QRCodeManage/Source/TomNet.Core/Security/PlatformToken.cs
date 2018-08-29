namespace TomNet.Core.Security
{
    /// <summary>
    /// 技术平台标识
    /// </summary>
    public enum PlatformToken
    {
        /// <summary>
        /// 标识当前平台为ASP.NET MVC
        /// </summary>
        Mvc,

        /// <summary>
        /// 标识当前平台为ASP.NET WebAPI
        /// </summary>
        WebApi,

        /// <summary>
        /// 标识当前平台为ASP.NET SignalR
        /// </summary>
        SignalR,

        /// <summary>
        /// 标识当前平台为本地程序
        /// </summary>
        Local
    }
}