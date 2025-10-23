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
        public int? InvoiceId { get; set; }
        /// <summary>
        /// ScheduleId (foreign key): Id jadwal kelas yang terkait
        /// </summary>
        public int ScheduleId { get; set; }
        /// <summary>
        /// Virtual field untuk Schedule: Jadwal kelas yang terkait
        /// </summary>
        public virtual Schedule Schedule { get; set; } = null!;
    }
}
