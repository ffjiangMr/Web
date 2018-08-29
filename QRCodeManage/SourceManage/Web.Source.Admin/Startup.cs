using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Owin;
using Owin;
using TomNet.Autofac.Hangfire.Initialize;
using TomNet.Autofac.Http;
using TomNet.Autofac.Mvc;
using TomNet.Autofac.SignalR;
using TomNet.Core.Caching;
using TomNet.Core.Dependency;
using TomNet.Data.Entity;
using TomNet.Logging.Log4Net;
using TomNet.Web.Mvc.Initialize;
using TomNet.Web.SignalR.Initialize;
using TomNet.Web.WebApi.Initialize;

[assembly: OwinStartup(typeof(Source.Admin.Web.Startup))]
namespace Source.Admin.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
            ICacheProvider provider = new RuntimeMemoryCacheProvider();
            CacheManager.SetProvider(provider, CacheLevel.First);

            IServicesBuilder builder = new ServicesBuilder();
            IServiceCollection services = builder.Build();
            services.AddLog4NetServices();
            services.AddDataServices();

            IIocBuilder mvcIocBuilder = new MvcAutofacIocBuilder(services);
            app.UseTomNetMvc(mvcIocBuilder);
            IIocBuilder apiIocBuilder = new WebApiAutofacIocBuilder(services);
            app.UseTomNetWebApi(apiIocBuilder);
            app.UseTomNetSignalR(new SignalRAutofacIocBuilder(services));

            app.ConfigureWebApi();
            app.ConfigureSignalR();

            IIocBuilder hangfireBuilder = new HangfireAutofacIocBuilder(services);
            app.UseTomNetHangfile(hangfireBuilder);
            GlobalConfiguration.Configuration.UseMemoryStorage();
            app.UseHangfireDashboard();
            app.UseHangfireServer(new BackgroundJobServerOptions() { WorkerCount = 1 });
        }
    }
}
