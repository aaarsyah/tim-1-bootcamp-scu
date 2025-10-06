namespace WebApplication1.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        public string Name { get; set; } = string.Empty; // Default empty string untuk avoid null
        
        /// <summary>
        /// Deskripsi lengkap product - Optional, max 1000 characters
        /// Field ini bisa digunakan untuk SEO dan detail information
        /// </summary>
        public string Description { get; set; } = string.Empty;
        
        /// <summary>
        /// Harga product dalam decimal dengan precision 18,2
        /// Artinya: maksimal 16 digit sebelum koma, 2 digit setelah koma
        /// Contoh: 999999999999999.99
        /// </summary>
        public decimal Price { get; set; }
        
        
        /// <summary>
        /// URL untuk gambar product - Optional, max 500 characters
        /// Bisa berupa relative path atau absolute URL
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;
        
        /// <summary>
        /// Flag untuk menandakan apakah product masih aktif/tersedia
        /// true = active, false = discontinued/inactive
        /// Default true untuk product baru
        /// </summary>
        public bool IsActive { get; set; } = true;
        
        /// <summary>
        /// Timestamp kapan product ini dibuat
        /// Otomatis di-set ke UTC time saat object dibuat
        /// Di database akan menggunakan GETUTCDATE() sebagai default value
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// Timestamp kapan product ini terakhir di-update
        /// Otomatis di-update setiap kali ada perubahan data
        /// Di-handle di ProductDbContext.SaveChangesAsync()
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// Foreign Key yang menunjuk ke Category.Id
        /// Setiap product harus belong ke satu category
        /// Required field (tidak boleh null/0)
        /// </summary>
        public int CategoryId { get; set; }
        
        /// <summary>
        /// Navigation property untuk relationship ke Category
        /// Entity Framework akan otomatis populate ini saat include Category
        /// virtual keyword memungkinkan lazy loading (jika diaktifkan)
        /// null! artinya kita tahu ini tidak akan null di runtime
        /// </summary>
        public virtual Category Category { get; set; } = null!;
    }
}