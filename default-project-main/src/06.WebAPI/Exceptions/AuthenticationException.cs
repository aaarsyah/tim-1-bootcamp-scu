namespace MyApp.WebAPI.Exceptions
{
    /// <summary>
    /// Exception untuk akses yang belum diotorisasi (misalnya belum login)<br />
    /// Tujuan: Untuk mengingatkan client bahwa server tidak dapat authenticate client<br />
    /// HTTP Status: 401 Unauthorized<br />
    /// <br />
    /// Dipakai ketika:<br />
    /// - User yang belum login mencoba untuk melakukan checkout<br />
    /// - Token yang dipakai user expired atau invalid<br />
    /// <b>Tidak</b> dipakai ketika:<br />
    /// - User yang login mencoba untuk mengakses halaman admin (gunakan PermissionException)<br />
    /// - Login gagal karena salah password/email/dll. (gunakan ValidationException)<br />
    /// </summary>
    public class AuthenticationException : BaseApiException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="message">Pesan mengenai exception</param>
        /// <param name="details">Informasi detil mengenai exception (bisa berupa List bila lebih dari satu)</param>
         public AuthenticationException(string message, object? details = null)
            : base(StatusCodes.Status401Unauthorized, "NOT_AUTHORIZED", message, details)
        {
        }
    }
}
