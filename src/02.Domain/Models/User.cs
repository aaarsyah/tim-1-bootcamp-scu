using MyApp.Base;

namespace MyApp.Domain.Models;

/// <summary>
/// Representasi sebuah pengguna yang terdaftar<br />
/// One-to-Many dengan CartItem (One User, Many CartItem)<br />
/// Many-to-Many dengan Role (Many User, Many Role) pada entity UserRole
/// </summary>
public class User : AuditableEntity
{
    /// <summary>
    /// IsActive: Apakah entity ini aktif?
    /// </summary>
    public bool IsActive { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty; //Salt termasuk dalam password hash
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

    // Navigation Properties
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public virtual ICollection<UserClaim> UserClaims { get; set; } = new List<UserClaim>();
}

/// <summary>
/// Representasi sebuah peran seorang pengguna<br />
/// Many-to-Many dengan User (Many Role, Many User) pada entity UserRole
/// </summary>
public class Role : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public virtual ICollection<RoleClaim> RoleClaims { get; set; } = new List<RoleClaim>();
}
/// <summary>
/// Entity penghubung antara User dan Role
/// </summary>
public class UserRole : AuditableEntity
{
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
    public int RoleId { get; set; }
    public virtual Role Role { get; set; } = null!;
}
/// <summary>
/// Representasi sebuah Claim seorang pengguna, yakni sebuah informasi mengenai pengguna<br />
/// Misal: Tangal lahir, Nomor referral, dll.
/// </summary>
public class UserClaim : AuditableEntity
{
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
    public string ClaimType { get; set; } = string.Empty;
    public string ClaimValue { get; set; } = string.Empty;
}
/// <summary>
/// Representasi sebuah Claim sebuah role, yakni sebuah informasi mengenai role<br />
/// Misal: Permission (CanEditUsers, CanAddCourses, dll.)
/// </summary>
public class RoleClaim : AuditableEntity
{
    public int RoleId { get; set; }
    public virtual Role Role { get; set; } = null!;
    public string ClaimType { get; set; } = string.Empty;
    public string ClaimValue { get; set; } = string.Empty;
}

// Audit Log untuk tracking
public class AuditLog : BaseEntity
{
    public string Action { get; set; } = string.Empty;
    /// <summary>
    /// CreatedAt: Tanggal dan waktu entity dibuat
    /// </summary>
    public DateTime TimeOfAction { get; set; } = DateTime.UtcNow;
    public string EntityName { get; set; } = string.Empty;
    public int? EntityId { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string IpAddress { get; set; } = string.Empty;
    public string UserAgent { get; set; } = string.Empty;
}