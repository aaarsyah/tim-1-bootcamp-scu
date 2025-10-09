namespace MyApp.WebAPI.Exceptions
{
    /// <summary>
    /// Base class for all custom API exceptions
    /// Purpose: Provide consistent error information structure
    /// 
    /// Why custom exceptions?
    /// 1. Better control over HTTP status codes
    /// 2. Consistent error response format
    /// 3. Additional context with error codes and details
    /// 4. Easier error handling in middleware
    /// 
    /// Example usage:
    /// throw new NotFoundException("Account not found", new { accountId = "ACC123" });
    /// </summary>
    public abstract class BaseApiException : Exception
    {
        /// <summary>
        /// HTTP status code to return
        /// Examples: 400 (Bad Request), 404 (Not Found), 500 (Internal Server Error)
        /// </summary>
        public int StatusCode { get; }

        /// <summary>
        /// Machine-readable error code
        /// Purpose: Frontend can handle specific errors programmatically
        /// Examples: "INSUFFICIENT_BALANCE", "ACCOUNT_NOT_FOUND"
        /// </summary>
        public string ErrorCode { get; }

        /// <summary>
        /// Additional error details (optional)
        /// Purpose: Provide context-specific information
        /// Example: { accountId: "ACC123", currentBalance: 100.00 }
        /// </summary>
        public object? Details { get; }

        /// <summary>
        /// Constructor without inner exception
        /// </summary>
        protected BaseApiException(int statusCode, string errorCode, string message, object? details = null)
            : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            Details = details;
        }

        /// <summary>
        /// Constructor with inner exception
        /// Purpose: Wrap lower-level exceptions with business context
        /// </summary>
        protected BaseApiException(int statusCode, string errorCode, string message, Exception innerException, object? details = null)
            : base(message, innerException)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            Details = details;
        }
    }
}
