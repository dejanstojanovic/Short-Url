using System.Data.Entity;

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
