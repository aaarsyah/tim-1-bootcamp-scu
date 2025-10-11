namespace MyApp.WebAPI.Exceptions
{
    /// <summary>
    /// Exception untuk akses yang belum diotorisasi (misalnya belum login)<br />
    /// Tujuan: Untuk mengingatkan client bahwa server belum tahu siapa dia<br />
    /// HTTP Status: 401 Unauthorized<br />
    /// <br />
    /// Dipakai ketika:<br />
    /// - User yang belum login mencoba untuk melakukan checkout<br />
    /// 
    /// <b>Tidak</b> dipakai ketika:<br />
    /// - User yang login mencoba untuk mengakses halaman admin (gunakan PermissionException)<br />
    /// - Login gagal karena salah password/email/dll. (gunakan ValidationException)<br />
    /// <br />
    /// Contoh:<br />
    /// throw new AuthorizationException($"Please log in first to access your cart.");<br />
    /// </summary>
    public class AuthorizationException : BaseApiException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="message">Pesan mengenai exception</param>
        /// <param name="details">Informasi detil mengenai exception (bisa berupa List bila lebih dari satu)</param>
        public AuthorizationException(string message, object? details = null)
            : base(StatusCodes.Status401Unauthorized, "NOT_AUTHORIZED", message, details)
        {
        }
    }
}
