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
        /// Courses: Kelas-kelas pelajaran yang terkait
        /// </summary>
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        //TODO: Add image URL
    }
}

