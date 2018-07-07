using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System.IO;
using System.Web;

namespace AspNetApp.Filters
{
    public class TrackBodyOnApplicationInsightsInitializer : ITelemetryInitializer
    {
        public void Initialize(ITelemetry telemetry)
        {
            if (!(telemetry is RequestTelemetry requestTelemetry)) return;

            if (VerbIsPostOrPut())
            {
                var body = GetBodyText();
                if (!string.IsNullOrEmpty(body))
                    // Os dados do corpo são adicionados no request =)
                    TrackBodyTelemetry.AddBodyToRequestTelemetry(requestTelemetry, body);
            }
        }

        private bool VerbIsPostOrPut()
        => HttpContext.Current != null
        && HttpContext.Current.Request != null
        && TrackBodyTelemetry.IsPutOrPost(HttpContext.Current.Request.HttpMethod);

        private string GetBodyText()
        {
            using (var reader = new StreamReader(HttpContext.Current.Request.InputStream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}