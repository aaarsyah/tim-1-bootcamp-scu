namespace MyApp.WebAPI.Models
{
    /// <summary>
    /// Category: Representasi sebuah kelas pelajaran<br />
    /// One-to-Many dengan Course (One Category, Many Course)
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Id: Primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name: Nama kategori
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// LongName: Nama panjang kategori<br />
        /// Catatan: Ditampilkan pada page List Menu Kelas
        /// </summary>
        public string LongName { get; set; } = string.Empty;
        /// <summary>
        /// Description: Deskripsi kategori<br />
        /// Catatan: Ditampilkan pada page List Menu Kelas
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// ImageUrl: URL gambar kategori<br />
        /// Catatan: Ditampilkan pada page Landing
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;
        /// <summary>
        /// IsActive: Apakah kategori aktif?
        /// Catatan: Digunakan pada page Admin
        /// </summary>
        public bool IsActive { get; set; } = true;
        /// <summary>
        /// CreatedAt: Tangal pembuatan kategori
        /// Catatan: Digunakan pada page Admin
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// UpdatedAt: Tangal perubahan kategori
        /// Catatan: Digunakan pada page Admin
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// Courses: Kelas-kelas pelajaran yang terkait
        /// </summary>
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}

