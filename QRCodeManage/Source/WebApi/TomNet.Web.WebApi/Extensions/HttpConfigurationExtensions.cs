using System.Web.Http;


namespace TomNet.Web.WebApi.Extensions
{
    /// <summary>
    /// HttpConfiguration扩展
    /// </summary>
    public static class HttpConfigurationExtensions
    {
        /// <summary>
        /// 注册WebApi默认路由
        /// </summary>
        public static void MapDefaultRoutes(this HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            //config.Routes.MapHttpRoute("ActionApi", "api/{controller}/{action}/{id}", new { id = RouteParameter.Optional });
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
        }
    }
}