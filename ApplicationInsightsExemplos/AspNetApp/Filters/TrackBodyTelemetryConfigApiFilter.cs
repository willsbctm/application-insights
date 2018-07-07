using System.Web.Http.Filters;

namespace AspNetApp.Filters
{
    // ActionFilterAttribute do namespace System.Web.Http.Filters é um filtro para actions dentro de uma ApiController
    public class TrackBodyTelemetryConfigApiFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var filterContext = actionExecutedContext.ActionContext;
            if (TrackBodyTelemetry.IsPutOrPost(filterContext.Request.Method.ToString()))
            {
                var requestBody = GetBodyFromRequest(actionExecutedContext);
                // Os dados do corpo não são adicionados no request =(
                TrackBodyTelemetry.AddBodyToTelemetryClient(requestBody);
            }

            base.OnActionExecuted(actionExecutedContext);
        }

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