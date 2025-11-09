namespace MyApp.Base.Exceptions;

/// <summary>
/// Exception untuk resource (berupa halaman atau user) yang tidak ditemukan<br />
/// Tujuan: Untuk mengingatkan client bahwa yang ia cari tidak ada (atau kalau server tidak mau ngasih tahu)<br />
/// Umumnya digunakan pada endpoint GET<br />
/// HTTP Status: 404 Not Found<br />
/// </summary>
public class NotFoundException : BaseApiException
{
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="message">Pesan mengenai exception</param>
    /// <param name="details">Informasi detil mengenai exception (bisa berupa List bila lebih dari satu)</param>
    public NotFoundException(string message, object? details = null)
        : base(404, "NOT_FOUND", message, details) // StatusCodes.Status404NotFound
    {
    }
    public NotFoundException(string idname, int id)
        : base(404, "NOT_FOUND", $"{idname} {id} not found") // StatusCodes.Status404NotFound
    {
    }
    public NotFoundException(string key, string value)
        : base(404, "NOT_FOUND", $"{key} {value} not found") // StatusCodes.Status404NotFound
    {
    }
}
