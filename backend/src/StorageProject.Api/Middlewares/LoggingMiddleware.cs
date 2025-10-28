using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace StorageProject.Api.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();

            try
            {
                _logger.LogInformation("Handling request: {Method} {Path}", context.Request.Method, context.Request.Path);
                await _next(context);

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception while handling request: {Method} {Path}/", context.Request.Method, context.Request.Path);
                throw;
            }
            finally
            {
                sw.Stop();
                _logger.LogInformation("Finished request {Method} {Path} responded {StatusCode} in {ElapsedMilliseconds}ms",
                    context.Request.Method,
                    context.Request.Path,
                    context.Response.StatusCode,
                    sw.ElapsedMilliseconds
                );
            }
        }
    }
}

