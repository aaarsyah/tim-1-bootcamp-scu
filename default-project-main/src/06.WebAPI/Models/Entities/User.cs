using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MyApp.WebAPI.Models.Entities
{
    /// <summary>
    /// User: Representasi sebuah pengguna yang terdaftar<br />
    /// //One-to-Many dengan CartItem (One User, Many CartItem)
    /// </summary>
    public class User : IdentityUser<int>
    {
        // public string Email { get; set; } = string.Empty;
    

        // Refresh token untuk JWT authentication
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Name: Nama pengguna
        /// </summary>
        public string Name { get; set; } = string.Empty;
        public bool EmailConfirmed { get; set; }
        public string? EmailConfirmationToken { get; set; }
        public DateTime? EmailConfirmationTokenExpiry { get; set; }
        // Password Reset
        public string? PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpiry { get; set; }
        
        // Security
        public int FailedLoginAttempts { get; set; } //untuk fe
        public DateTime? LockoutEnd { get; set; } //gagal login
        
        // Last Login
        public DateTime? LastLoginAt { get; set; }
        
        /// <summary>
        /// CreatedAt: Tangal pembuatan pengguna
        /// Catatan: Digunakan pada page Admin
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// UpdatedAt: Tangal perubahan pengguna
        /// Catatan: Digunakan pada page Admin
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        
        // Navigation properties
        public virtual ICollection<UserClaim> UserClaims { get; set; } = new List<UserClaim>();
    }

     /// <summary>
    /// Role Entity - Extended IdentityRole
    /// Merepresentasikan role/peran dalam sistem
    /// </summary>
    public class Role : IdentityRole<int>
    {
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }


    public class UserClaim : IdentityUserClaim<int>
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation property
        public User User { get; set; } = null!;
    }
}