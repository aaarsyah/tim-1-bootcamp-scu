using MyApp.WebAPI.Exceptions;
using MyApp.WebAPI.Models;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using Xunit.Sdk;

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

            ApiResponse<object> response;

            switch (exception)
            {
                case BaseApiException ex: // Semua exception dari BaseApiException
                    context.Response.StatusCode = ex.StatusCode;
                    response = ApiResponse<object>.ErrorResult(ex.ErrorCode,ex.Message);
                    // TODO: Add stack trace for development purposes
                    // TODO: Include error details for ValidationException
                    break;
                default:
                    response = ApiResponse<object>.ErrorResult("An error occurred while processing your request");
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
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