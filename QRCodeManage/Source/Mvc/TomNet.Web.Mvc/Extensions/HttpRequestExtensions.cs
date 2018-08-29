using System.Web;

using TomNet.Utility.Extensions;


namespace TomNet.Web.Mvc.Extensions
{
    /// <summary>
    /// <see cref="HttpRequest"/>扩展辅助操作类
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// 获取IP地址
        /// </summary>
        public static string GetIpAddress(this HttpRequestBase request)
        {
            string result = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (result.IsNullOrEmpty())
            {
                result = request.ServerVariables["REMOTE_ADDR"];
            }
            return result;
        }
    }
}