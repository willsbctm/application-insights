using AspNetApp.Filters;
using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AspNetApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static string _apitrackBodyTelemetryConfigFilterEnabled = ConfigurationManager.AppSettings["ApiTrackBodyMvcTelemetryConfigFilterEnabled"];

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            if (TrackBodyTelemetryConfigApiFilter.ShouldBeEnabled(_apitrackBodyTelemetryConfigFilterEnabled))
                GlobalConfiguration.Configuration.Filters.Add(new TrackBodyTelemetryConfigApiFilter());
        }
    }
}
