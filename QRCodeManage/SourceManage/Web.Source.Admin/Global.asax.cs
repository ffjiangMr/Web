using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using TomNet.Utility.Logging;

namespace Source.Admin.Web
{
    public class Global : HttpApplication
    {
        private ILogger _logger;
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            GetLogger();
            Exception ex = Server.GetLastError();
            _logger.Fatal("全局异常Application_Error", ex);
        }

        private void GetLogger()
        {
            if (_logger == null)
            {
                _logger = LogManager.GetLogger<Global>();
            }
        }
    }
}
