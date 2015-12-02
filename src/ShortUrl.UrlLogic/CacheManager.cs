using System;
using System.Web;
using System.Web.Caching;

namespace ShortUrl.Logic
{
    public static class CacheManager
    {
        public static T GetCached<T>(string cacheKey) where T : class
        {
            HttpContext httpContext = HttpContext.Current;
            if (httpContext != null)
            {
                return HttpContext.Current.Cache[cacheKey] as T;
            }
            return null;
        }

        public static bool TryAddToCache<T>(string cacheKey, T value, int timeout = 0)
        {
            HttpContext httpContext = HttpContext.Current;
            if (httpContext != null)
            {
                httpContext.Cache.Insert(cacheKey, value, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(ConfigManager.CacheTimeout));

            }
            return false;
        }

    }
}
