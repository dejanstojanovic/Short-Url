using System;

namespace ShortUrl.Data
{
    public interface IRepository<T> : IDisposable where T : class
    {
        bool ExistsKey(String key);
        bool ExistsUrl(String url);
        T FindKey(String key);
        T FindUrl(String url);
        T Add(T url);
    }
}
