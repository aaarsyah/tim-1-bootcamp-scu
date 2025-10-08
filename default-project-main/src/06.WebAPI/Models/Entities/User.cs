namespace MyApp.WebAPI.Models.Entities
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
        /// Guid: Nama pengguna
        /// </summary>
        public string Guid { get; set; } = string.Empty;
        /// <summary>
        /// Name: Nama pengguna
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Email: Alamat email pengguna
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Password: Password pengguna
        /// TODO: Menunggu implementasi autentikasi
        /// </summary>
        public string Password { get; set; } = string.Empty;
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
