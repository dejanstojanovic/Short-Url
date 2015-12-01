using System;
using System.Collections.Generic;
using System.Linq;
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
            var response = Request.CreateResponse(HttpStatusCode.Moved);
            String urlString = new ShortUrl.Logic.UrlManager().GetUrl(key);
            if (!String.IsNullOrWhiteSpace(urlString))
            {
                response.Headers.Location = new Uri(urlString);
                return response;
            }
            return null;
        }

        [HttpPost]
        [Route("short")]
        public HttpResponseMessage Post([FromBody] String url)
        {
                return Request.CreateResponse(HttpStatusCode.OK, new ShortUrl.Logic.UrlManager().AddShortUrl(url));
            
        }
    }
}
