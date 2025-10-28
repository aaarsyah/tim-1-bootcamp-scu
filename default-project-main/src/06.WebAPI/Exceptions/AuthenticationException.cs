namespace MyApp.WebAPI.Exceptions
{
    /// <summary>
    /// Exception untuk akses yang belum diotorisasi (misalnya belum login)<br />
    /// Tujuan: Untuk mengingatkan client bahwa server tidak dapat authenticate client karena belum ada credential<br />
    /// Umumnya digunakan ketika HTTP request tidak mengandung Authentication header<br />
    /// HTTP Status: 401 Unauthorized<br />
    /// </summary>
    public class AuthenticationException : BaseApiException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="message">Pesan mengenai exception</param>
        /// <param name="details">Informasi detil mengenai exception (bisa berupa List bila lebih dari satu)</param>
        public AuthenticationException(string errorCode, string message, object? details = null)
            : base(StatusCodes.Status401Unauthorized, errorCode, message, details)
        {
        }
    }
    public class TokenInvalidException : AuthenticationException
    {
        public TokenInvalidException(object? details = null)
            : base("INVALID_TOKEN", "Token is invalid", details)
        {
        }
    }
    
}
