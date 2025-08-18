using Microsoft.EntityFrameworkCore;
using StorageProject.Domain.Entity;

namespace StorageProject.Infrasctructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands{ get; set; }
        public DbSet<Category> Categories{ get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Status)
                .HasConversion<string>();
        }

    }
}
