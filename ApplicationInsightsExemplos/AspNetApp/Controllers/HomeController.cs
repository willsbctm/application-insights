using Microsoft.ApplicationInsights;
using System.Web.Mvc;

namespace AspNetApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            var telemetryClient = new TelemetryClient();
            telemetryClient.Context.Properties.Add("Home", "get na home");

            return View();
        }
    }
}
