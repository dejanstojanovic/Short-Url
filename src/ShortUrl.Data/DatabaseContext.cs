using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortUrl.Data
{
    internal class DatabaseContext: DbContext
    {
        public DatabaseContext() : base("ShortUrl")
        {
            Database.SetInitializer<DatabaseContext>(new CreateDatabaseIfNotExists<DatabaseContext>());
        }

        public DbSet<Models.ShortUrl> ShortUrls { get; set; }
    }
}
