namespace MyApp.WebAPI.Models
{
    /// <summary>
    /// InvoiceDetail: Representasi sebuah item dalam bukti pembelian<br />
    /// Many-to-One dengan Invoice (Many InvoiceDetail, One Invoice)
    /// </summary>
    public class InvoiceDetail
    {
        /// <summary>
        /// Id: Primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Schedule: Jadwal kelas yang terkait
        /// </summary>
        public Schedule Schedule { get; set; } = null;
    }
}
