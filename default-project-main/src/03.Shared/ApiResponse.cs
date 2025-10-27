namespace MyApp.Shared.DTOs
{
    /// <summary>
    /// Standard API response wrapper
    /// </summary>
    /// <typeparam name="T">Response data type</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Machine-readable status code
        /// Purpose: Frontend can handle specific errors programmatically
        /// Examples: "NOT_FOUND", "SUCCESS", "VALIDATION_ERROR"
        /// </summary>
        public string StatusCode { get; set; } = string.Empty;
        /// <summary>
        /// Response data
        /// </summary>
        public T? Data { get; set; }
        /// <summary>
        /// Response message
        /// </summary>
        public string? ErrorMessage { get; set; }
        /// <summary>
        /// Error details if any
        /// </summary>
        public object? Errors { get; set; }
        /// <summary>
        /// Response timestamp
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Create successful response
        /// </summary>
        public static ApiResponse<T> SuccessResult(T? data)
        {
            return new ApiResponse<T>
            {
                StatusCode = "SUCCESS",
                Data = data
            };
        }
        /// <summary>
        /// Create successful response with no data
        /// </summary>
        public static ApiResponse<object> SuccessResult()
        {
            return new ApiResponse<object>
            {
                StatusCode = "SUCCESS",
                Data = null
            };
        }
        /// <summary>
        /// Create error response
        /// </summary>
        public static ApiResponse<T> ErrorResult(string message, object? errors = null)
        {
            return new ApiResponse<T>
            {
                ErrorMessage = message,
                Errors = errors
            };
        }
    }

    /// <summary>
    /// Paginated response wrapper
    /// </summary>
    /// <typeparam name="T">Response data type</typeparam>
    public class PaginatedResponse<T>
    {
        /// <summary>
        /// Paginated data
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// Current page number
        /// </summary>
        public int PageNumber { get; set; }
        
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }
        
        /// <summary>
        /// Total number of records
        /// </summary>
        public int TotalRecords { get; set; }
        
        /// <summary>
        /// Total number of pages
        /// </summary>
        public int TotalPages { get; set; }
        
        /// <summary>
        /// Whether there is a next page
        /// </summary>
        public bool HasNextPage { get; set; }
        
        /// <summary>
        /// Whether there is a previous page
        /// </summary>
        public bool HasPreviousPage { get; set; }

        /// <summary>
        /// Create paginated response
        /// </summary>
        public static PaginatedResponse<IEnumerable<TData>> Create<TData>(
            IEnumerable<TData> data, 
            int pageNumber, 
            int pageSize, 
            int totalRecords)
        {
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            
            return new PaginatedResponse<IEnumerable<TData>>
            {
                Data = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                HasNextPage = pageNumber < totalPages,
                HasPreviousPage = pageNumber > 1
            };
        }
    }

    /// <summary>
    /// Pagination parameters
    /// </summary>
    public class PaginationParameters
    {
        /// <summary>
        /// Page number (1-based)
        /// </summary>
        public int PageNumber { get; set; } = 1;
        
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; } = 10;
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public PaginationParameters()
        {
            PageNumber = 1;
            PageSize = 10;
        }
        
        /// <summary>
        /// Constructor with parameters
        /// </summary>
        public PaginationParameters(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 50 ? 50 : pageSize; // Limit max page size
        }
    }

    /// <summary>
    /// Product query parameters
    /// </summary>
    public class CourseQueryParameters : PaginationParameters
    {
        /// <summary>
        /// Search term
        /// </summary>
        public string? Search { get; set; }
        
        /// <summary>
        /// Filter by category ID
        /// </summary>
        public int? CategoryId { get; set; }
        
        /// <summary>
        /// Minimum price filter
        /// </summary>
        public long? MinPrice { get; set; }
        
        /// <summary>
        /// Maximum price filter
        /// </summary>
        public long? MaxPrice { get; set; }
        
        /// <summary>
        /// Sort field
        /// </summary>
        public string SortBy { get; set; } = "name";
        
        /// <summary>
        /// Sort direction
        /// </summary>
        public string SortDirection { get; set; } = "asc";
    }
}