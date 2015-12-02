using System;

namespace ShortUrl.Data
{
    public interface IRepository<T> : IDisposable where T : class
    {
        bool Exists(String url);
        T Find(String key);
        T Add(T url);
    }
}
