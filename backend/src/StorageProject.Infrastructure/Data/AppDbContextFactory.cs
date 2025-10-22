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
            var connectionString = "Server=sqlserver-product,1433;Database=products;User Id=sa;Password=Lagavi30!;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
