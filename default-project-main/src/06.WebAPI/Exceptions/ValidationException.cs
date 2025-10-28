namespace MyApp.WebAPI.Exceptions
{
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
            : base(StatusCodes.Status400BadRequest, "VALIDATION_ERROR", message, details)
        {
        }
        /// <summary>
        /// Constructor untuk multiple error
        /// </summary>
        /// <param name="errors">Objek (berupa list otau dictionary) mengenai semua error-error spesifik</param>
        public ValidationException(Dictionary<string, string[]> errors)
            : base(StatusCodes.Status400BadRequest, "VALIDATION_ERROR", "One or more validation errors occurred.", errors)
        {
        }
    }
}
