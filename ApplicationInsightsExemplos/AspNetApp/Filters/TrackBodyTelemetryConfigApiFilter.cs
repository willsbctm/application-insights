using System.Web.Http.Filters;

namespace AspNetApp.Filters
{
    // ActionFilterAttribute do namespace System.Web.Http.Filters é um filtro para actions dentro de uma ApiController
    public class TrackBodyTelemetryConfigApiFilter : ActionFilterAttribute
    {
        private readonly TrackBodyTelemetry _trackBodyTelemetry = new TrackBodyTelemetry();

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var filterContext = actionExecutedContext.ActionContext;
            if (_trackBodyTelemetry.IsPutOrPost(filterContext.Request.Method.ToString()))
            {
                var requestBody = GetBodyFromRequest(actionExecutedContext);
                _trackBodyTelemetry.AddBodyToTelemetry(requestBody);
            }

            base.OnActionExecuted(actionExecutedContext);
        }

        public static bool ShouldBeEnabled(string keyValue) => keyValue != null && keyValue == "true";

        private string GetBodyFromRequest(HttpActionExecutedContext actionExecutedContext)
        {
            var taskGetStream = actionExecutedContext.Request.Content.ReadAsStreamAsync();
            taskGetStream.Wait();

            string requestBody = null;
            using (var stream = taskGetStream.Result)
            {
                if (stream.CanSeek)
                    stream.Position = 0;

                var taskGetBody = actionExecutedContext.Request.Content.ReadAsStringAsync();
                taskGetBody.Wait();

                requestBody = taskGetBody.Result;
                taskGetBody.Dispose();
            }
            taskGetStream.Dispose();

            return requestBody;
        }
    }
}