using Microsoft.EntityFrameworkCore;
using Products.Constants.Enums;
using Products.Domain.Entities;

namespace Products.Infrastructure.Data
{
    public class ZeissAppDbContext : DbContext
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<CodeTracker> CodeTrackers => Set<CodeTracker>();
        public ZeissAppDbContext(DbContextOptions<ZeissAppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ZeissAppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}