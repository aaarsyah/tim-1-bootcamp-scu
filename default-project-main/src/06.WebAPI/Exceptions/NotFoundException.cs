namespace MyApp.WebAPI.Exceptions
{
    /// <summary>
    /// Exception untuk resource (berupa halaman) yang tidak ditemukan<br />
    /// Tujuan: Untuk mengingatkan client bahwa ia tersesat<br />
    /// HTTP Status: 404 Not Found<br />
    /// <br />
    /// Dipakai ketika:<br />
    /// - Halaman yang ingin dicari tidak ada<br />
    /// <b>Tidak</b> dipakai ketika:<br />
    /// - User/Course yang di-query tidak ditemukan (return empty result saja)<br />
    /// - User/Course yang ingin di-update tidak ditemukan (gunakan ValidationException)<br />
    /// Contoh:<br />
    /// throw new NotFoundException($"Page not found.");<br />
    /// </summary>
    public class NotFoundException : BaseApiException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="message">Pesan mengenai exception</param>
        /// <param name="details">Informasi detil mengenai exception (bisa berupa List bila lebih dari satu)</param>
        public NotFoundException(string message, object? details = null)
            : base(StatusCodes.Status404NotFound, "NOT_FOUND", message, details)
        {
        }
    }
}
