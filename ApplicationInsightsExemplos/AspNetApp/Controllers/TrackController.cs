using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetApp.Controllers
{
    public class TrackController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Lanca404([FromBody]object body)
        {
            return Request.CreateResponse(HttpStatusCode.NotFound, $"Message: {body}");
        }
    }
}