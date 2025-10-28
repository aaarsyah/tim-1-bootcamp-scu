using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace MyApp.WebAPI.Exceptions
{
    /// <summary>
    /// Exception untuk akses yang tidak diperbolehkan<br />
    /// Tujuan: Untuk mengingatkan client bahwa ia dilarang untuk melakukan aksi tertentu<br />
    /// Digunakan pada endpoint apa saja<br />
    /// HTTP Status: 403 Forbidden<br />
    /// </summary>
    public class PermissionException : BaseApiException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="message">Pesan mengenai exception</param>
        /// <param name="details">Informasi detil mengenai exception (bisa berupa List bila lebih dari satu)</param>
        public PermissionException(string message, object? details = null)
            : base(StatusCodes.Status403Forbidden, "DENIED", message, details)
        {
        }
        // Apa ya kegunaan innerException?
        //public PermissionException(string message, Exception innerException, object? details = null)
        //    : base(403, "DENIED", message, innerException, details)
        //{
        //}
    }
    public class AccountLockedException : BaseApiException
    {
        public AccountLockedException(string message, object? details = null)
            : base(StatusCodes.Status403Forbidden, "ACCOUNT_LOCKED", message, details)
        {
        }
    }
    public class AccountInactiveException : BaseApiException
    {
        public AccountInactiveException(string message, object? details = null)
            : base(StatusCodes.Status403Forbidden, "ACCOUNT_INACTIVE", message, details)
        {
        }
    }
}
