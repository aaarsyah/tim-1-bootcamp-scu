using Microsoft.AspNetCore.Mvc;
using MyApp.Shared.DTOs;
using MyApp.WebAPI.Exceptions;
using MyApp.WebAPI.Models;
using System;
using System.Net;
using System.Text.Json;

namespace MyApp.WebAPI.Middleware
{
    /// <summary>
    /// Global Exception Handling Middleware
    /// Purpose: Catch ALL unhandled exceptions and convert to standard error responses
    /// 
    /// Why middleware?
    /// 1. Centralized error handling - One place for all errors
    /// 2. Consistent responses - Same format for all errors
    /// 3. No try-catch in controllers - Cleaner code
    /// 4. Security - Control what error info is exposed
    /// 5. Logging - Automatic error logging
    /// 
    /// How it works:
    /// 1. Request comes in
    /// 2. Try to process request
    /// 3. If exception occurs -> catch it here
    /// 4. Convert to standard error response
    /// 5. Return appropriate HTTP status code
    /// </summary>
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _environment;
        //The compiler told me to do this
        private readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true //lebih mudah dibaca
        };

        public GlobalExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionHandlingMiddleware> logger,
            IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        /// <summary>
        /// Process request and catch any exceptions
        /// Called for every request
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Pass request to next middleware/controller
                await _next(context);
            }
            catch (Exception exception)
            {
                // Something went wrong - handle it
                _logger.LogError(exception, "An unhandled exception occurred");
                await HandleExceptionAsync(context, exception);
            }
        }

        /// <summary>
        /// Convert exception to appropriate HTTP response
        /// Maps exception types to status codes and error responses
        /// </summary>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            
            var traceId = context.TraceIdentifier; // For tracking in logs
            ErrorResponse errorResponse;
            Dictionary<string, object?> extensions = new();
            ProblemDetails response;
            // ===== HANDLE DIFFERENT EXCEPTION TYPES =====

            switch (exception)
            {
                // ===== CUSTOM API EXCEPTIONS =====
                // Our business exceptions with predefined status codes
                case BaseApiException ex:
                    context.Response.StatusCode = ex.StatusCode;
                    // Special handling for validation errors with field-specific errors
                    if (ex is ValidationException validationEx &&
                        validationEx.Details is Dictionary<string, string[]> validationErrors)
                    {
                        errorResponse = new ValidationErrorResponse
                        {
                            ValidationErrors = validationErrors,
                            TraceId = traceId,
                            StackTrace = _environment.IsDevelopment() ? validationEx.StackTrace : null
                        };
                        extensions["ErrorCode"] = ex.ErrorCode;
                        response = new ProblemDetails()
                        {
                            Title = ex.Message,
                            Detail = errorResponse.ToString(),
                            Extensions = extensions
                        };
                    }
                    else
                    {
                        errorResponse = new ErrorResponse
                        {
                            Details = ex.Details,
                            TraceId = traceId,
                            // Show stack trace only in development
                            StackTrace = _environment.IsDevelopment() ? ex.StackTrace : null
                        };
                        extensions["ErrorCode"] = ex.ErrorCode;
                        response = new ProblemDetails()
                        {
                            Title = ex.Message,
                            Detail = errorResponse.ToString(),
                            Extensions = extensions
                        };
                    }
                    break;

                // ===== ARGUMENT NULL EXCEPTION =====
                // Required parameter is null
                case ArgumentNullException argumentNullException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse = new ErrorResponse
                    {
                        TraceId = traceId,
                        StackTrace = _environment.IsDevelopment() ? argumentNullException.StackTrace : null
                    };
                    extensions["ErrorCode"] = "ARGUMENT_NULL";
                    response = new ProblemDetails()
                    {
                        Title = "A required argument was null",
                        Detail = errorResponse.ToString(),
                        Extensions = extensions
                    };
                    break;

                // ===== ARGUMENT EXCEPTION =====
                // Invalid argument value
                case ArgumentException argumentException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse = new ErrorResponse
                    {
                        TraceId = traceId,
                        StackTrace = _environment.IsDevelopment() ? argumentException.StackTrace : null
                    };
                    extensions["ErrorCode"] = "ARGUMENT_INVALID";
                    response = new ProblemDetails()
                    {
                        Title = argumentException.Message,
                        Detail = errorResponse.ToString(),
                        Extensions = extensions
                    };
                    break;

                // ===== UNAUTHORIZED ACCESS =====
                // User doesn't have permission
                case UnauthorizedAccessException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    errorResponse = new ErrorResponse
                    {
                        TraceId = traceId
                        // No stack trace for security
                    };
                    extensions["ErrorCode"] = "UNAUTHORIZED";
                    response = new ProblemDetails()
                    {
                        Title = "Access denied",
                        Detail = errorResponse.ToString(),
                        Extensions = extensions
                    };
                    break;

                // ===== TIMEOUT EXCEPTION =====
                // Operation took too long
                case TimeoutException timeoutException:
                    context.Response.StatusCode = (int)HttpStatusCode.RequestTimeout;
                    errorResponse = new ErrorResponse
                    {
                        Details = timeoutException.Message,
                        TraceId = traceId,
                        StackTrace = _environment.IsDevelopment() ? timeoutException.StackTrace : null
                    };
                    extensions["ErrorCode"] = "TIMEOUT";
                    response = new ProblemDetails()
                    {
                        Title = "The request timed out",
                        Detail = errorResponse.ToString(),
                        Extensions = extensions
                    };
                    break;

                // ===== TASK CANCELED (TIMEOUT) =====
                // Async operation was cancelled due to timeout
                case TaskCanceledException taskCanceledException 
                    when taskCanceledException.InnerException is TimeoutException:
                    context.Response.StatusCode = (int)HttpStatusCode.RequestTimeout;
                    errorResponse = new ErrorResponse
                    {
                        TraceId = traceId,
                        StackTrace = _environment.IsDevelopment() ? taskCanceledException.StackTrace : null
                    };
                    extensions["ErrorCode"] = "TIMEOUT";
                    response = new ProblemDetails()
                    {
                        Title = "The request timed out",
                        Detail = errorResponse.ToString(),
                        Extensions = extensions
                    };
                    break;

                // ===== UNKNOWN/UNHANDLED EXCEPTIONS =====
                // Anything else = Internal Server Error
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    var message = _environment.IsDevelopment() ?
                            exception.Message : // Show details in dev
                            "An internal server error occurred"; // Generic message in production
                    errorResponse = new ErrorResponse
                    {
                        TraceId = traceId,
                        StackTrace = _environment.IsDevelopment() ? exception.StackTrace : null
                    };
                    extensions["ErrorCode"] = "INTERNAL_SERVER_ERROR";
                    response = new ProblemDetails()
                    {
                        Title = message,
                        Detail = errorResponse.ToString(),
                        Extensions = extensions
                    };
                    // Log full details for internal errors
                    _logger.LogError(exception, 
                        "Internal server error occurred. TraceId: {TraceId}", traceId);
                    break;
            }

            // Convert error response to JSON
            var jsonResponse = JsonSerializer.Serialize(response, jsonSerializerOptions);

            // Send error response
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
