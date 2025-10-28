namespace MyApp.Shared.DTOs
{
    /// <summary>
    /// Product data transfer object
    /// </summary>
    public class InvoiceDto
    {
        /// <summary>
        /// Invoice ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// RefCode name
        /// </summary>
        public string RefCode { get; set; } = string.Empty;

        /// <summary>
        /// TanggalBeli description
        /// </summary>
        public DateTime CreatedAt { get; set; }

        public long TotalPrice { get; set; }

        public int TotalCourse { get; set; }

        public string Email { get; set; } = string.Empty;

    }

    /// <summary>
    /// Create Invoice request
    /// </summary>
    public class CreateInvoiceDto
    {
        /// <summary>
        /// RefCode
        /// </summary>
        public string RefCode { get; set; } = string.Empty;

        /// <summary>
        /// TanggalBeli
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}