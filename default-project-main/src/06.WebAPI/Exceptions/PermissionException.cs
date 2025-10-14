namespace MyApp.WebAPI.Exceptions
{
    /// <summary>
    /// Exception untuk akses yang tidak diperbolehkan<br />
    /// Tujuan: Untuk mengingatkan client bahwa ia dilarang untuk melakukan aksi tertentu<br />
    /// HTTP Status: 403 Forbidden<br />
    /// <br />
    /// Dipakai ketika:<br />
    /// - User yang login mencoba untuk mengakses halaman admin<br />
    /// <b>Tidak</b> dipakai ketika:<br />
    /// - User yang belum login mencoba untuk melakukan checkout <br />
    /// - Token yang dipakai user expired atau invalid (gunakan AuthenticationException)<br />
    /// Contoh:<br />
    /// throw new PermissionException($"Access is denied.");<br />
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
}
