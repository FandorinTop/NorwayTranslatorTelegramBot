using CacheEntities;
using Microsoft.EntityFrameworkCore;

namespace CacheDomainContext
{
    public class CacheDbContext : DbContext
    {
        public CacheDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CacheTgMessage> CachedMessages { get; set; }
    }
}