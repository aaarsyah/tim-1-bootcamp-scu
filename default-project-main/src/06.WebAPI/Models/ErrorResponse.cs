using System.Text.Json;

namespace MyApp.WebAPI.Models
{
    /// <summary>
    /// Standard Error Response
    /// Purpose: Provide consistent error format across all API endpoints
    /// 
    /// Why standardize?
    /// 1. Frontend can handle errors consistently
    /// 2. Better debugging with trace IDs
    /// 3. Client-friendly error messages
    /// 4. Machine-readable error codes
    /// 
    /// Example response:
    /// {
    ///   "errorCode": "INSUFFICIENT_BALANCE",
    ///   "message": "Insufficient balance in account ACC001",
    ///   "details": { "available": 100.00, "required": 500.00 },
    ///   "timestamp": "2025-10-05T12:30:45Z",
    ///   "traceId": "abc123",
    ///   "stackTrace": null
    /// }
    /// </summary>
    public class ErrorResponse
    {
        // ErrorCode dan Message sudah ada di ApiResponse
        
        /// <summary>
        /// Additional error context (optional)
        /// Purpose: Provide specific details for debugging
        /// Example: { accountId: "ACC001", currentBalance: 100.00 }
        /// </summary>
        public object? Details { get; set; }
        
        /// <summary>
        /// When the error occurred
        /// ISO 8601 format
        /// </summary>
        public string Timestamp { get; set; } = DateTime.UtcNow.ToString("O");
        
        /// <summary>
        /// Unique request identifier
        /// Purpose: Correlate error with request logs
        /// Used for debugging in production
        /// </summary>
        public string TraceId { get; set; } = string.Empty;
        
        /// <summary>
        /// Stack trace (development only)
        /// Purpose: Detailed error information for developers
        /// Should be null in production for security
        /// </summary>
        public string? StackTrace { get; set; }

        /// <summary>
        /// Convert to JSON string
        /// </summary>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });
        }
    }

    /// <summary>
    /// Validation Error Response
    /// Purpose: Return field-specific validation errors
    /// 
    /// Extends ErrorResponse with validation details
    /// 
    /// Example:
    /// {
    ///   "errorCode": "VALIDATION_ERROR",
    ///   "message": "One or more validation errors occurred",
    ///   "validationErrors": {
    ///     "Amount": ["Amount must be greater than 0"],
    ///     "FromAccountNumber": ["Source account is required"]
    ///   }
    /// }
    /// </summary>
    public class ValidationErrorResponse : ErrorResponse
    {
        /// <summary>
        /// Dictionary of field names to error messages
        /// Key: Field name (e.g., "Amount", "FromAccountNumber")
        /// Value: Array of error messages for that field
        /// 
        /// Why array?
        /// A single field can have multiple validation errors
        /// Example: Email might be both required AND invalid format
        /// </summary>
        public Dictionary<string, string[]> ValidationErrors { get; set; } = new Dictionary<string, string[]>();
    }
}
