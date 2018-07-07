using System.Web.Http.Filters;

namespace AspNetApp.Filters
{
    public class TrackBodyTelemetryConfigApiFilter : ActionFilterAttribute
    {
        private readonly TrackBodyTelemetry _trackBodyTelemetry = new TrackBodyTelemetry();

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var filterContext = actionExecutedContext.ActionContext;
            if (_trackBodyTelemetry.IsPutOrPost(filterContext.Request.Method.ToString()))
            {
                var requestBody = _trackBodyTelemetry.GetRequestBody(actionExecutedContext.Request.Content.ReadAsStreamAsync().Result);
                _trackBodyTelemetry.AddBodyToTelemetry(requestBody);
            }

            base.OnActionExecuted(actionExecutedContext);
        }

        public static bool ShouldBeEnabled(string keyValue) => keyValue != null && keyValue == "true";
    }
}