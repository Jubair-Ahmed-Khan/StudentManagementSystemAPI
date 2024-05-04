namespace STMS.Presentation.MiddleWares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log request details (method, path, etc.)
  
            _logger.LogInformation("HTTP Request: {method} {path}",context.Request.Method,context.Request.Path);

            var startTime = DateTime.UtcNow;
            await _next(context);
            var endTime = DateTime.UtcNow;

            // Log response details (status code, duration, etc.)
            _logger.LogInformation("HTTP Response: {statusCode} {duration}:hh:mm:ss.fff)", context.Response.StatusCode, endTime - startTime);
        }
    }
}
