using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Web;
using System.Web.Caching;
using System.Net;
using System.Net.Http;

namespace ShortUrl.Logic
{
    public class UrlManager
    {
        private ShortUrl.Data.Context dbContext;

        private bool CheckUrlAvailability
        {
            get
            {
                bool check;
                if(!bool.TryParse(ConfigurationManager.AppSettings["CheckUrlAvailability"], out check))
                {
                    check = false;
                }
                return check;
            }
        }

        private int CheckUrlAvailabilityTimeout
        {
            get
            {
                int timeout;
                if (!int.TryParse(ConfigurationManager.AppSettings["CheckUrlAvailabilityTimeout"], out timeout))
                {
                    timeout = 5;
                }
                return timeout;
            }
        }

        private int KeyLength
        {
            get
            {
                int keyLength;
               if( !int.TryParse(ConfigurationManager.AppSettings["KeyLength"], out keyLength))
                {
                    keyLength = 6;
                }
                return keyLength;
            }
        }

        private int CacheTimeout
        {
            get
            {
                int cacheTimeout;
                if(!int.TryParse(ConfigurationManager.AppSettings["CaheTimeout"], out cacheTimeout))
                {
                    cacheTimeout = 5;
                }
                return cacheTimeout;
            }
        }

        public UrlManager()
        {
            dbContext = new Data.Context();
        }

        public String AddShortUrl(string Url)
        {
            if (!string.IsNullOrWhiteSpace(Url))
            {
                Url = Url.Trim().ToLower();
            }

            String cached = null;
            cached = this.GetCached<String>(Url);
            if (!String.IsNullOrWhiteSpace(cached))
            {
                return cached;
            }

            try
            {
               var url = new Uri(Url);
            }
            catch(Exception ex)
            {
                throw new Exceptions.InvalidUrlException(ex);
            }

            if ((this.CheckUrlAvailability && this.HttpGetStatusCode(Url).Result == HttpStatusCode.OK) || !this.CheckUrlAvailability)
            {

                String newKey = null;
                while (string.IsNullOrEmpty(newKey))
                {
                    if (!dbContext.ShortUrls.Any(s => s.Url == Url))
                    {
                        newKey = Guid.NewGuid().ToString("N").Substring(0, this.KeyLength).ToLower();
                        dbContext.ShortUrls.Add(new Data.Models.ShortUrl() { Key = newKey, Url = Url, DateCreated = DateTime.Now });
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        var shortUrl = dbContext.ShortUrls.Where(s => s.Url == Url).FirstOrDefault();
                        if (shortUrl != null)
                        {
                            newKey = shortUrl.Key;
                        }
                    }
                }
                this.TryAddToCache<String>(Url, newKey);

                return newKey;
            }
            else
            {
                throw new Exceptions.UrlUnreachableException(null);
            }
        }

        public String GetUrl(String ShortUrlKey)
        {
            var url = dbContext.ShortUrls.Where(s => s.Key == ShortUrlKey).FirstOrDefault();
            if (url != null)
            {
                return url.Url;
            }
            else
            {
                throw new Exceptions.MissingKeyException(null);
            }
        }

        public  async Task<HttpStatusCode> HttpGetStatusCode(string Url)
        {
            try
            {
                var httpclient = new HttpClient();
                httpclient.Timeout = TimeSpan.FromSeconds(this.CacheTimeout);
                var response = await httpclient.GetAsync(Url, HttpCompletionOption.ResponseHeadersRead);

                string text = null;

                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    var bytes = new byte[10];
                    var bytesread = stream.Read(bytes, 0, 10);
                    stream.Close();

                    text = Encoding.UTF8.GetString(bytes);

                    Console.WriteLine(text);
                }

                return response.StatusCode;
            }
            catch (Exception ex)
            {
                return HttpStatusCode.NotFound;
            }
        }

        private T GetCached<T>(string cacheKey) where T : class
        {
            HttpContext httpContext = HttpContext.Current;
            if (httpContext != null)
            {
                return HttpContext.Current.Cache[cacheKey] as T;
            }
            return null;
        }

        private  bool TryAddToCache<T>(string cacheKey, T value, int timeout = 0)
        {
            HttpContext httpContext = HttpContext.Current;
            if (httpContext != null)
            {
                //httpContext.Cache.Add(cacheKey, value, null, DateTime.Now.AddMinutes(cacheTimeout), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                httpContext.Cache.Insert(cacheKey, value, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(this.CacheTimeout));

            }
            return false;
        }

    }
}
