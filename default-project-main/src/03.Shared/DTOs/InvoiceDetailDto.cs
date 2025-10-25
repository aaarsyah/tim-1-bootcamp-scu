namespace MyApp.Shared.DTOs
{
    /// <summary>
    /// Product data transfer object
    /// </summary>
    public class InvoiceDetailDto
    {
        /// <summary>
        /// Invoice Detail ID
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


        /// <summary>
        /// Whether Invoice is active
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Schedule (foreign key): Jadwal kelas yang terkait
        /// </summary>
        public int ScheduleId { get; set; }

        /// <summary>
        /// Nama Course
        /// </summary>
        public string CourseName { get; set; } = string.Empty;

        /// <summary>
        /// Nama Kategori Kelas
        /// </summary>
        public string CategoryName { get; set; } = string.Empty;

        /// <summary>
        /// Product price
        /// </summary>
        public decimal Price { get; set; }

        public List<DateOnly> ScheduleDates { get; set; } = new();
        public decimal TotalPrice { get; set; }
        
    }

    /// <summary>
    /// Create Invoice request
    /// </summary>
    public class CreateInvoiceDetailDto
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