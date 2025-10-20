using StorageProject.Api.Extensions;

namespace StorageProject.Api.Configurations
{
    public static class PipelineConfiguration
    {
        public static void AddPipelineConfiguration(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.Services.ApplyMigrations();
            }

            app.UseCustomMiddleware();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}
