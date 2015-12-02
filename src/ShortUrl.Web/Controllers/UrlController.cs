using ShortUrl.Logic;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShortUrl.Web.Controllers
{
    public class UrlController : ApiController
    {
        [HttpGet]
        [Route("{key}")]
        public HttpResponseMessage Get(string key)
        {
            using (UrlManager urlManager = new UrlManager()) {
                var response = Request.CreateResponse(HttpStatusCode.Moved);
                String urlString = urlManager.GetUrl(key);
                if (!String.IsNullOrWhiteSpace(urlString))
                {
                    response.Headers.Location = new Uri(urlString);
                    return response;
                }
            }
            return null;
        }

        [HttpPost]
        [Route("short")]
        public HttpResponseMessage Post([FromBody] String url)
        {
            using (UrlManager urlManager = new UrlManager())
            {
                return Request.CreateResponse(HttpStatusCode.OK, urlManager.AddShortUrl(url));
            }
        }
    }
}
