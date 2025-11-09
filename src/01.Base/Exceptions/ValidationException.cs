namespace MyApp.Base.Exceptions;

/// <summary>
/// Exception untuk input yang tidak sah, sehingga tidak dijalankan dan tidak mengubah sistem<br />
/// Tujuan: Untuk mengingatkan client untuk memperbaiki inputnya<br />
/// Umumnya digunakan pada endpoint POST<br />
/// HTTP Status: 400 Bad Request<br />
/// </summary>
public class ValidationException : BaseApiException
{
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="message">Pesan mengenai exception</param>
    /// <param name="details">Informasi detil mengenai exception (bisa berupa List bila lebih dari satu)</param>
    public ValidationException(string message, object? details = null)
        : base(400, "VALIDATION_ERROR", message, details) // StatusCodes.Status400BadRequest
    {
    }
    /// <summary>
    /// Constructor untuk multiple error
    /// </summary>
    /// <param name="errors">Objek (berupa list otau dictionary) mengenai semua error-error spesifik</param>
    public ValidationException(Dictionary<string, string[]> errors)
        : base(400, "VALIDATION_ERROR", "One or more validation errors occurred.", errors) // StatusCodes.Status400BadRequest
    {
    }
}
