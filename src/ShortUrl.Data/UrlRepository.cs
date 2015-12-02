using System.Linq;

namespace ShortUrl.Data
{
    public class UrlRepository : IRepository<Models.ShortUrl>
    {
        private ShortUrl.Data.DatabaseContext dbContext;
        private bool disposing = false;
        public UrlRepository()
        {
            dbContext = new DatabaseContext();
        }

        public Models.ShortUrl Add(Models.ShortUrl url)
        {
           var result = dbContext.ShortUrls.Add(url);
            dbContext.SaveChanges();
            return result;
        }

        public bool ExistsUrl(string url)
        {
          return   dbContext.ShortUrls.Any(u => u.Url == url);
        }

        public bool ExistsKey(string key)
        {
            return dbContext.ShortUrls.Any(u => u.Key == key);
        }

        public Models.ShortUrl FindKey(string key)
        {
          return  dbContext.ShortUrls.Where(u => u.Key == key).FirstOrDefault();
        }

        public Models.ShortUrl FindUrl(string url)
        {
            return dbContext.ShortUrls.Where(u => u.Url == url).FirstOrDefault();
        }

        public void Dispose()
        {
            if (!disposing)
            {
                disposing = true;
                dbContext.Dispose();
            }
        }
    }
}
