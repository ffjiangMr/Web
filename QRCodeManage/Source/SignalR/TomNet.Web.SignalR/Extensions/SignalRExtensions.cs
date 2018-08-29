using Microsoft.AspNet.SignalR;


namespace TomNet.Web.SignalR.Extensions
{
    /// <summary>
    /// 扩展辅助操作类
    /// </summary>
    public static class SignalRExtensions
    {
        /// <summary>
        /// 获取请求的IP地址
        /// </summary>
        public static string GetRemoteIp(this IRequest request)
        {
            object value;
            if (request.Environment.TryGetValue("server.RemoteIpAddress", out value))
            {
                return value.ToString();
            }
            return string.Empty;
        }
    }
}