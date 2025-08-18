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
            var connectionString = "server=localhost;port=3306;database=dev;user=developer;password=Lagavi30!;";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
