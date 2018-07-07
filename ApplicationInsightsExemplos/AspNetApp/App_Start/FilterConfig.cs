using AspNetApp.Filters;
using System.Configuration;
using System.Web.Mvc;

namespace AspNetApp
{
    public class FilterConfig
    {
        private static string _mvctrackBodyTelemetryConfigFilterEnabled = ConfigurationManager.AppSettings["MvcTrackBodyTelemetryConfigFilterEnabled"];

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            if (TrackBodyTelemetryConfigMvcFilter.ShouldBeEnabled(_mvctrackBodyTelemetryConfigFilterEnabled))
                filters.Add(new TrackBodyTelemetryConfigMvcFilter());
        }
    }
}
