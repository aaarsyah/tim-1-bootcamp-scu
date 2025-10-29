namespace MyApp.WebAPI.Middleware;

/// <summary>
/// Correlation ID Middleware
/// Purpose: Add unique identifier to each request for tracking across logs
/// 
/// Why correlation IDs?
/// 1. Track Request Flow - Follow single request through entire system
/// 2. Debugging - Find all logs related to specific request
/// 3. Distributed Tracing - Connect logs across microservices
/// 4. Support - Users can provide correlation ID when reporting issues
/// 
/// How it works:
/// 1. Check if request has correlation ID header
/// 2. If yes, use it (from external system/gateway)
/// 3. If no, generate new one
/// 4. Add to response headers (client can track it)
/// 5. Add to logging context (all logs will include it)
/// 
/// Example:
/// Request comes in without header
/// -> Generate: "abc12345"
/// -> All logs: "[abc12345] Processing transfer..."
/// -> Response header: "X-Correlation-ID: abc12345"
/// -> Client sees: "Transaction failed, correlation ID: abc12345"
/// </summary>
public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;
    private const string CorrelationIdHeader = "X-Correlation-ID";

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Get existing or generate new correlation ID
        var correlationId = GetOrGenerateCorrelationId(context);
        
        // Add to response headers (so client can see it)
        context.Response.Headers.Append(CorrelationIdHeader, correlationId);
        
        // Add to logging context (all logs in this request will include it)
        // Note: Requires Serilog.Context package
        using (Serilog.Context.LogContext.PushProperty("CorrelationId", correlationId))
        {
            // Process request
            await _next(context);
        }
    }

    /// <summary>
    /// Get correlation ID from request or generate new one
    /// </summary>
    private string GetOrGenerateCorrelationId(HttpContext context)
    {
        // Check if request already has correlation ID
        if (context.Request.Headers.TryGetValue(CorrelationIdHeader, out var correlationId))
        {
            return correlationId.FirstOrDefault() ?? GenerateCorrelationId();
        }
        
        // Generate new one
        return GenerateCorrelationId();
    }

    /// <summary>
    /// Generate unique correlation ID
    /// Format: First 8 characters of GUID (lowercase, no hyphens)
    /// Example: "a1b2c3d4"
    /// 
    /// Why short?
    /// - Easy to read and communicate
    /// - Still unique enough for tracking
    /// - Less storage in logs
    /// </summary>
    private string GenerateCorrelationId()
    {
        return Guid.NewGuid().ToString("N")[..8];
    }
}
