using System.IO;
using System.Web.Mvc;

namespace AspNetApp.Filters
{
    // ActionFilterAttribute do namespace System.Web.Mvc é um filtro para actions dentro de uma Mvc Controller
    public class TrackBodyTelemetryConfigMvcFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (TrackBodyTelemetry.IsPutOrPost(filterContext.HttpContext.Request.HttpMethod))
            {
                var requestBody = GetBodyFromRequest(filterContext);
                // Os dados do corpo não são adicionados no request =(
                TrackBodyTelemetry.AddBodyToTelemetryClient(requestBody);
            }

            base.OnActionExecuted(filterContext);
        }

        private string GetBodyFromRequest(ActionExecutedContext filterContext)
        {
            using (var stream = filterContext.HttpContext.Request.InputStream)
            {
                if (stream.CanSeek)
                    stream.Position = 0;

                using (var streamReader = new StreamReader(stream))
                {
                    var requestBody = streamReader.ReadToEnd();

                    return requestBody;
                }
            }
        }
    }
}