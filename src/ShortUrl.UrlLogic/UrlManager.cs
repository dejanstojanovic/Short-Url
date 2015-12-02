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
            cached = CacheManager.GetCached<String>(Url);
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

            if ((ConfigManager.CheckUrlAvailability && this.HttpGetStatusCode(Url).Result == HttpStatusCode.OK) || !ConfigManager.CheckUrlAvailability)
            {

                String newKey = null;
                while (string.IsNullOrEmpty(newKey))
                {
                    if (!dbContext.ShortUrls.Any(s => s.Url == Url))
                    {
                        newKey = Guid.NewGuid().ToString("N").Substring(0, ConfigManager.KeyLength).ToLower();
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
                CacheManager.TryAddToCache<String>(Url, newKey);

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
                httpclient.Timeout = TimeSpan.FromSeconds(ConfigManager.CacheTimeout);
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


    }
}
