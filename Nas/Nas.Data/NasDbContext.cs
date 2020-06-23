using Microsoft.EntityFrameworkCore;

namespace Nas.Data
{
    public class NasDbContext : DbContext
    {
        public NasDbContext(DbContextOptions<NasDbContext> options)
            : base(options)
        { }

        public DbSet<Uri> Uris { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UriConfiguration());
        }
    }
}