namespace MyApp.WebAPI.Models.Entities
{
    /// <summary>
    /// Entity class untuk merepresentasikan Category dalam database
    /// Class ini akan di-map ke tabel "Categories" oleh Entity Framework
    /// One-to-Many relationship dengan Product (1 Category -> Many Products)
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
        public string Name { get; set; } = string.Empty; // Default empty string untuk avoid null
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
        /// IsActive: Apakah kategori aktif?<br />
        /// Catatan: Digunakan pada page Admin
        /// </summary>
        public bool IsActive { get; set; } = true;
        /// <summary>
        /// CreatedAt: Tangal pembuatan kategori<br />
        /// Catatan: Digunakan pada page Admin
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// UpdatedAt: Tangal perubahan kategori<br />
        /// Catatan: Digunakan pada page Admin
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// Virtual field untuk Courses: Kelas-kelas pelajaran yang terkait
        /// </summary>
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}