using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using System.Collections.Generic;
using System.Net.Http;

namespace AspNetApp.Filters
{
    public class TrackBodyTelemetry
    {
        private const string _dimension = "body";
        private static IList<string> _methodsAlloweds = new List<string> { HttpMethod.Put.ToString(), HttpMethod.Post.ToString() };

        public static bool IsActive(string key) => key != null && key == "true";

        public static bool IsPutOrPost(string method)
            => _methodsAlloweds.Contains(method);

        public static void AddBodyToTelemetryClient(string requestBody)
        {
            if (!string.IsNullOrWhiteSpace(requestBody))
                new TelemetryClient().Context.Properties.Add(_dimension, requestBody);
        }

        public static void AddBodyToRequestTelemetry(RequestTelemetry requestTelemetry, string requestBody)
        {
            if (!string.IsNullOrWhiteSpace(requestBody))
                requestTelemetry.Context.Properties.Add(_dimension, requestBody);
        }
    }
}