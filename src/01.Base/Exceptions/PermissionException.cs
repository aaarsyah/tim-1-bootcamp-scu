namespace MyApp.Base.Exceptions;

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
        : base(403, "DENIED", message, details) // StatusCodes.Status403Forbidden
    {
    }
}
public class AccountLockedException : BaseApiException
{
    public AccountLockedException(string message, object? details = null)
        : base(403, "ACCOUNT_LOCKED", message, details) // StatusCodes.Status403Forbidden
    {
    }
}
public class AccountInactiveException : BaseApiException
{
    public AccountInactiveException(string message, object? details = null)
        : base(403, "ACCOUNT_INACTIVE", message, details) // StatusCodes.Status403Forbidden
    {
    }
}
