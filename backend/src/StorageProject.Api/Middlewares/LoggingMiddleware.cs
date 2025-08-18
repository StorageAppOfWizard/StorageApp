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
            // Log the request details
            _logger.LogInformation("Handling request: {Method} {Path}", context.Request.Method, context.Request.Path);
            // Call the next middleware in the pipeline
            await _next(context);
            // Log the response details
            _logger.LogInformation("Finished handling request. Response status code: {StatusCode}", context.Response.StatusCode);
        }
    }
}

