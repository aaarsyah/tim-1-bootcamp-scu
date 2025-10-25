namespace MyApp.WebAPI.Models.Entities
{
    /// <summary>
    /// InvoiceDetail: Representasi sebuah item dalam bukti pembelian<br />
    /// Many-to-One dengan Invoice (Many InvoiceDetail, One Invoice)
    /// </summary>
    public class InvoiceDetail : BaseEntity
    {
        /// <summary>
        /// Invoice (foreign key): Bukti pembelian yang terkait
        /// </summary>
        public int InvoiceId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        /// <summary>
        /// Nama Kategori Kelas
        /// </summary>
        public string CategoryName { get; set; } = string.Empty;
        /// <summary>
        /// Product price
        /// </summary>
        public long Price { get; set; }

        public DateOnly ScheduleDate { get; set; }
    }
}
