namespace MyApp.WebAPI.Models
{
    /// <summary>
    /// PaymentMethod: Representasi sebuah metode pembayaran<br />
    /// One-to-Many dengan Invoice (One PaymentMethod, Many Invoice)
    /// </summary>
    public class PaymentMethod
    {
        /// <summary>
        /// Id: Primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name: Nama metode pembayaran
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// LogoUrl: URL logo metode pembayaran
        /// </summary>
        public string LogoUrl { get; set; } = string.Empty;
        /// <summary>
        /// CreatedAt: Tangal pembuatan metode pembayaran
        /// Catatan: Digunakan pada page Admin
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// UpdatedAt: Tangal perubahan metode pembayaran
        /// Catatan: Digunakan pada page Admin
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}