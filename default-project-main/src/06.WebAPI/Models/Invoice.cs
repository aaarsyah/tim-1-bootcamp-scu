namespace MyApp.WebAPI.Models
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
        /// Date: Tanggal pembelian
        /// </summary>
        public DateOnly Date { get; set; }
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
        /// PaymentMethod (foreign key): Metode pembayaran yang terkait
        /// </summary>
        public int? PaymentMethodId { get; set; }
        /// <summary>
        /// PaymentMethod: Metode pembayaran yang terkait
        /// </summary>
        public PaymentMethod? PaymentMethod { get; set; } = null!;
        /// <summary>
        /// InvoiceDetails: Item-item dalam bukti pembelian yang terkait
        /// </summary>
        public ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>(); //Collection navigation not required

    }
}
