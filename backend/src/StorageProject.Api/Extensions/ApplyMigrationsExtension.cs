using Microsoft.EntityFrameworkCore;
using StorageProject.Infrasctructure.Data;

namespace StorageProject.Api.Extensions
{
    public static class ApplyMigrationsExtension
    {
        public static void ApplyMigrations(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var sp = scope.ServiceProvider;
            try
            {
                var context = sp.GetRequiredService<AppDbContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = sp.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "Erro ao aplicar migrations");
            }
        }
    }
}
