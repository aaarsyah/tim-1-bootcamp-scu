namespace MyApp.WebAPI.Exceptions
{
    /// Base class untuk semua custom API exceptions<br />
    /// Tujuan: Supaya semua exception dari API tetap konsisten<br />
    /// <br />
    /// Mengapa?<br />
    /// 1. Dapat menggunakan error code untuk dibaca frontend<br />
    /// 2. Error yang konsisten untuk seluruh API<br />
    /// 3. Informasi detil mengenai error<br />
    /// 4. Lebih mudah dihandle di middleware<br />
    /// <br />
    /// Semua class exception baru yang dihasilkan oleh API harus inherit dari kelas ini<br />
    /// Contoh kasus:<br />
    /// Create: throw ValidationException bila menyalahi database constraint (Ada username yang sama atau email tidak valid)<br />
    /// Read (get/query list): seharusnya tidak throw apapun, return list kosong bila hasil query kosong<br />
    /// Update: throw NotFoundException bila record yang ingin diupdate tidak ditemukan, atau ValidationException bila menyalahi database constraint<br />
    /// Delete: throw NotFoundException bila record yang ingin didelete tidak ditemukan, atau ValidationException bila menyalahi database constraint (misal foreign key ON DELETE RESTRICT)<br />
    /// Get by id/name: throw NotFoundException bila record yang ingin didapatkan tidak ditemukan.
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
