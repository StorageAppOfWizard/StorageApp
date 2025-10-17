using StorageProject.Api.Middlewares;

namespace StorageProject.Api.Configurations
{
    public static class MiddlewareConfiguration
    {
        public static void UseCustomMiddleware(this WebApplication app)
        {
            app.UseMiddleware<LoggingMiddleware>();
            app.UseMiddleware<MiddlewareException>();
        }
    }
}
