using Microsoft.AspNetCore.Identity;
using MyApp.WebAPI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyApp.WebAPI.Models.Entities
{
    /// <summary>
    /// User: Representasi sebuah pengguna yang terdaftar<br />
    /// One-to-Many dengan CartItem (One User, Many CartItem)
    /// </summary>
    public class User : AuditableEntity
    {
        /// <summary>
        /// IsActive: Apakah entity ini aktif?
        /// </summary>
        public bool IsActive { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? PasswordSalt { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; } = DateTime.MinValue;
        public string? EmailConfirmationToken { get; set; }
        public DateTime EmailConfirmationTokenExpiry { get; set; } = DateTime.MinValue;
        public string? PasswordResetToken { get; set; }
        public DateTime PasswordResetTokenExpiry { get; set; } = DateTime.MinValue;
        public int FailedLoginAttempts { get; set; }
        public DateTime? LockoutEnd { get; set; }
        // Last Login
        public DateTime? LastLoginAt { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; } = null!;

        // Navigation Properties
        public ICollection<UserClaim> UserClaims { get; set; } = new List<UserClaim>();
    }

    /// <summary>
    /// Role: Merepresentasikan role/peran dalam sistem
    /// </summary>
    public class Role : BaseEntity
{
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<User> Users { get; set; } = new List<User>();
    }


    public class UserClaim : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public string ClaimType { get; set; } = string.Empty;
        public string ClaimValue { get; set; } = string.Empty;
    }


}