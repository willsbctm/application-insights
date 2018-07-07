using Microsoft.ApplicationInsights;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AspNetApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [HttpPost]
        public string Post(string body)
        {
            var telemetryClient = new TelemetryClient();
            telemetryClient.TrackEvent("Post", new Dictionary<string, string> { { "body", body } });

            return body;
        }
    }
}
