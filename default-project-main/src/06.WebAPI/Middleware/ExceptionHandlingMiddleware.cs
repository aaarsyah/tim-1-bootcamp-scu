using System.Net;
using System.Text.Json;
using MyApp.WebAPI.Models;

namespace MyApp.WebAPI.Middleware
{
    /// <summary>
    /// Global exception handling middleware
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Process HTTP context
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            
            var response = new ApiResponse<object>();

            switch (exception)
            {
                case ArgumentException ex:
                    response = ApiResponse<object>.ErrorResult(ex.Message);
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                
                case KeyNotFoundException ex:
                    response = ApiResponse<object>.ErrorResult(ex.Message);
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                
                case InvalidOperationException ex:
                    response = ApiResponse<object>.ErrorResult(ex.Message);
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                
                case UnauthorizedAccessException ex:
                    response = ApiResponse<object>.ErrorResult("Unauthorized access");
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                
                default:
                    response = ApiResponse<object>.ErrorResult("An error occurred while processing your request");
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(jsonResponse);
        }
    }

    /// <summary>
    /// Extension method to register middleware
    /// </summary>
    public static class ErrorHandlingMiddlewareExtensions
    {
        /// <summary>
        /// Use error handling middleware
        /// </summary>
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}