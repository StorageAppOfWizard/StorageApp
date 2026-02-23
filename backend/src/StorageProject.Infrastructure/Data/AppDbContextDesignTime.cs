using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using StorageProject.Infrasctructure.Data;

namespace StorageProject.Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = "User ID=root;Password=Lagavi30!;Host=dbproduct;Port=5432;Database=products;Pooling=true;MinPoolSize=0;MaxPoolSize=100;Connection Lifetime=0;";
            optionsBuilder.UseNpgsql(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}