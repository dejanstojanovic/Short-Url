using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShortUrl.Data
{
    public interface IRepository<T> : IDisposable where T : class
    {
        bool Exists(String url);
        T Find(String key);
        T Add(T url);
    }
}
