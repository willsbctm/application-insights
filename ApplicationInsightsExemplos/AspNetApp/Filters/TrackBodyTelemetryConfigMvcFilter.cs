﻿using System.Web.Mvc;

namespace AspNetApp.Filters
{
    public class TrackBodyTelemetryConfigMvcFilter : ActionFilterAttribute
    {
        private readonly TrackBodyTelemetry _trackBodyTelemetry = new TrackBodyTelemetry();

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (_trackBodyTelemetry.IsPutOrPost(filterContext.HttpContext.Request.HttpMethod))
            {
                var requestBody = _trackBodyTelemetry.GetRequestBody(filterContext.HttpContext.Request.InputStream);
                _trackBodyTelemetry.AddBodyToTelemetry(requestBody);
            }

            base.OnActionExecuted(filterContext);
        }

        public static bool ShouldBeEnabled(string keyValue) => keyValue != null && keyValue == "true";
    }
}