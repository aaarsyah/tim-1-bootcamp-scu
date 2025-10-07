namespace MyApp.WebAPI.Models
{
    /// <summary>
    /// Course: Representasi sebuah kelas pelajaran<br />
    /// One-to-Many dengan Schedule (One Course, Many Schedules)<br />
    /// Many-to-One dengan Category (Many Courses, One Category)
    /// </summary>
    public class Course
    {
        /// <summary>
        /// Id: Primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name: Nama kelas pelajaran
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Description: Deskripsi kelas pelajaran<br />
        /// Catatan: Ditampilkan pada page Detail Kelas
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// ImageUrl: URL gambar kelas pelajaran
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;
        /// <summary>
        /// Price: Harga kelas dalam IDR
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// IsActive: Apakah kelas pelajaran aktif?
        /// Catatan: Digunakan pada page Admin
        /// </summary>
        public bool IsActive { get; set; } = true;
        /// <summary>
        /// CreatedAt: Tangal pembuatan kelas pelajaran
        /// Catatan: Digunakan pada page Admin
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// UpdatedAt: Tangal perubahan kelas pelajaran
        /// Catatan: Digunakan pada page Admin
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// Category: Kategori kelas pelajaran yang terkait
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// Virtual field for Category
        /// </summary>
        public virtual Category Category { get; set; } = null!;
        /// <summary>
        /// Schedules: Jadwal-jadwal kelas yang terkait
        /// </summary>
        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    }
}
