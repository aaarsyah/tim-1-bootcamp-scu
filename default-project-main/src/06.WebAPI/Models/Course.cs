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
        /// Price: Harga kelas dalam IDR
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// Category: Kategori kelas pelajaran yang terkait
        /// </summary>
        public Category Category { get; set; } = null;
        /// <summary>
        /// Schedules: Jadwal-jadwal kelas yang terkait
        /// </summary>
        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
        //TODO: Add image URL
    }
}
