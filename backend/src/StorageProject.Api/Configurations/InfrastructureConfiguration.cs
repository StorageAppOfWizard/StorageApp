using Microsoft.EntityFrameworkCore;
using StorageProject.Domain.Contracts;
using StorageProject.Infrasctructure.Data;
using StorageProject.Infrastructure.Repositories;

namespace StorageProject.Api.Configurations
{
    public static class InfrastructureConfiguration
    {
        public static void AddInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("StorageContext");
            services.AddDbContext<AppDbContext>(options =>
                                                options.UseSqlServer(connectionString, provideOptions => provideOptions.EnableRetryOnFailure()));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
