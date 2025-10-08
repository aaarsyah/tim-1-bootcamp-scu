namespace MyApp.WebAPI.Exceptions
{
    /// <summary>
    /// Exception untuk input yang tidak sah<br />
    /// Tujuan: Untuk mengingatkan client bahwa aksi yang ingin dilakukan tidak benar<br />
    /// HTTP Status: 400 Bad Request<br />
    /// <br />
    /// Dipakai ketika:<br />
    /// - Input yang diberikan tidak sesuai dengan format JSON yang ditentukan<br />
    /// - Input yang diberikan sesuai format namun tidak benar (misal userid tidak sesuai)<br />
    /// - User/Course yang ingin di-update tidak ditemukan<br />
    /// <b>Tidak</b> dipakai ketika:<br />
    /// - User/Course yang di-query tidak ditemukan (return empty result saja)<br />
    /// <br />
    /// Contoh dengan error-error spesifik:<br />
    /// var errors = new Dictionary&lt;string, string[]&gt;<br />
    /// {<br />
    ///     ["Amount"] = new[] { "Amount must be greater than 0" },<br />
    ///     ["AccountNumber"] = new[] { "Account number is required" }<br />
    /// };<br />
    /// throw new ValidationException(errors);
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
