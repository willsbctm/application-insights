using AspNetApp.Filters;
using Microsoft.ApplicationInsights.Extensibility;
using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AspNetApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static string _instrumentationKey = ConfigurationManager.AppSettings["InstrumentationKey"];
        private static string _trackBodyOnApplicationInsightsInitializerEnabled = ConfigurationManager.AppSettings["TrackBodyOnApplicationInsightsInitializerEnabled"];


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            TelemetryConfiguration.Active.InstrumentationKey = _instrumentationKey;

            if (TrackBodyTelemetry.IsActive(_trackBodyOnApplicationInsightsInitializerEnabled))
                TelemetryConfiguration.Active.TelemetryInitializers.Add(new TrackBodyOnApplicationInsightsInitializer());
        }
    }
}
