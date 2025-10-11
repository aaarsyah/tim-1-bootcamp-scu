namespace MyApp.WebAPI.Models.Entities
{
    /// <summary>
    /// Invoice: Representasi sebuah bukti pembelian<br />
    /// Many-to-One dengan User (Many Invoice, One user)
    /// Many-to-One dengan Schedule (Many Invoice, One Schedule)
    /// </summary>
    public class Invoice
    {
        /// <summary>
        /// Id: Primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// RefCode: Kode bukti pembelian
        /// </summary>
        public string RefCode { get; set; } = string.Empty;
        /// <summary>
        /// Date: Tanggal pembelian
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// User (foreign key): Pengguna yang terkait<br />
        /// Catatan: Dapat berupa null bila user dihapus
        /// </summary>
        public int? UserId { get; set; }
        /// <summary>
        /// User: Pengguna yang terkait<br />
        /// Catatan: Dapat berupa null bila user dihapus
        /// </summary>
        public virtual User? User { get; set; } = null!;
        /// <summary>
        /// PaymentMethodId (foreign key): Id metode pembayaran yang terkait
        /// </summary>
        public int? PaymentMethodId { get; set; }
        /// <summary>
        /// Virtual field untuk PaymentMethod: Metode pembayaran yang terkait
        /// </summary>
        public virtual PaymentMethod? PaymentMethod { get; set; } = null!;
        /// <summary>
        /// Virtual field untuk InvoiceDetails: Item-item dalam bukti pembelian yang terkait
        /// </summary>
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>(); //Collection navigation untuk mempermudah saja

    }
}
