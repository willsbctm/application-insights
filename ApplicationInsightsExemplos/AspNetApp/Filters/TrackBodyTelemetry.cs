using Microsoft.ApplicationInsights;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace AspNetApp.Filters
{
    public class TrackBodyTelemetry
    {
        private const string _dimension = "body";
        private readonly IList<string> _methodsAlloweds = new List<string> { HttpMethod.Put.ToString(), HttpMethod.Post.ToString() };

        public bool IsPutOrPost(string method)
            => _methodsAlloweds.Contains(method);

        public string GetRequestBody(Stream inputStream)
        {
            using (var streamReader = new StreamReader(inputStream))
            {
                return streamReader.ReadToEnd();
            }
        }

        public void AddBodyToTelemetry(string requestBody)
        {
            if (!string.IsNullOrWhiteSpace(requestBody))
            {
                var telemetryClient = new TelemetryClient();
                telemetryClient.Context.Properties.Add(_dimension, requestBody);
            }
        }
    }
}