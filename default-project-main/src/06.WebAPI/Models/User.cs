namespace MyApp.WebAPI.Models
{
    /// <summary>
    /// User: Representasi sebuah pengguna yang terdaftar<br />
    /// //One-to-Many dengan CartItem (One User, Many CartItem)
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id: Primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name: Nama pengguna
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Email: Alamat email pengguna
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Password: Password pengguna
        /// TODO: Menunggu implementasi autentikasi
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// IsActive: Apakah pengguna sudah konfirmasi email?
        /// Catatan: Digunakan pada konfirmasi email
        /// </summary>
        public bool IsActive { get; set; } = false;
        /// <summary>
        /// IsAdmin: Apakah pengguna adalah admin?
        /// </summary>
        public bool IsAdmin { get; set; } = false;
        /// <summary>
        /// CreatedAt: Tangal pembuatan pengguna
        /// Catatan: Digunakan pada page Admin
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// UpdatedAt: Tangal perubahan pengguna
        /// Catatan: Digunakan pada page Admin
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}
