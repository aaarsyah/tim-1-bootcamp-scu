namespace MyApp.WebAPI.Exceptions
{
    /// <summary>
    /// Exception untuk resource (berupa halaman atau user) yang tidak ditemukan<br />
    /// Tujuan: Untuk mengingatkan client bahwa yang ia cari tidak ada (atau kalau server tidak mau ngasih tahu)<br />
    /// HTTP Status: 404 Not Found<br />
    /// <br />
    /// Dipakai ketika:<br />
    /// - Halaman yang ingin dicari tidak ada<br />
    /// - User/Course yang ingin di-update/delete tidak ditemukan<br />
    /// - User/Course yang ingin diperlihatkan detilnya tidak ditemukan<br />
    /// <b>Tidak</b> dipakai ketika:<br />
    /// - Hasil query User/Course berupa list kosong (return empty result saja)<br />
    /// - Input yang diberikan tidak sesuai dengan format JSON yang ditentukan (gunakan ValidationException)<br />
    /// - User men-checkout barang yang tidak di keranjangnya (gunakan ValidationException)<br />
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
