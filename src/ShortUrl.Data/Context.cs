using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortUrl.Data
{
    public class Context: DbContext
    {
        public Context() : base("ShortUrl")
        {
            Database.SetInitializer<Context>(new CreateDatabaseIfNotExists<Context>());
        }

        public DbSet<Models.ShortUrl> ShortUrls { get; set; }
    }
}
