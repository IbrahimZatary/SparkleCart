using System.Net;
using System.Text.Json;

namespace SparkeApp.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger,
            IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Continue to next middleware / controller
                await _next(context);
            }
            catch (Exception ex)
            {
                // Catch ANY unhandled exception
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            // Log the error
            _logger.LogError(ex, "An unhandled exception occurred on {Path}", context.Request.Path);

            // Determine status code based on exception type
            var statusCode = GetStatusCode(ex);

            // Prepare response (hide details in production)
            var response = new
            {
                error = GetErrorMessage(ex),
                statusCode = statusCode,
                timestamp = DateTime.UtcNow,
                path = context.Request.Path,
                // Only show stack trace in development
                stackTrace = _env.IsDevelopment() ? ex.StackTrace : null
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private int GetStatusCode(Exception ex)
        {
            return ex switch
            {
                ArgumentException => (int)HttpStatusCode.BadRequest,           
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized, // 401
                InvalidOperationException => (int)HttpStatusCode.BadRequest,   
                KeyNotFoundException => (int)HttpStatusCode.NotFound,    // 404
                _ => (int)HttpStatusCode.InternalServerError    // 500
            };
        }

        private string GetErrorMessage(Exception ex)
        {
            if (_env.IsDevelopment())
            {
                return ex.Message;
            }

            return ex switch
            {
                ArgumentException => "Invalid input ",
                UnauthorizedAccessException => "You are not authorized here ",
                KeyNotFoundException => "The requested resource was not found",
                _ => "An unexpected error occurred."
            };
        }
    }
}
