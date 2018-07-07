using AspNetApp.Filters;
using System.Configuration;
using System.Web.Http;

namespace AspNetApp
{
    public static class WebApiConfig
    {
        private static string _apitrackBodyTelemetryConfigFilterEnabled = ConfigurationManager.AppSettings["ApiTrackBodyMvcTelemetryConfigFilterEnabled"];

        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            if (TrackBodyTelemetryConfigApiFilter.ShouldBeEnabled(_apitrackBodyTelemetryConfigFilterEnabled))
                config.Filters.Add(new TrackBodyTelemetryConfigApiFilter());
        }
    }
}
