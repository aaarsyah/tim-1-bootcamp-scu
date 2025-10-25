using System.ComponentModel.DataAnnotations;

namespace MyApp.Shared.DTOs
{
    /// <summary>
    /// Request DTO untuk registrasi user baru
    /// </summary>
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
        // special character yang dimaksud merujuk pada karakter yang terdaftar disini: https://owasp.org/www-community/password-special-characters
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[ -/:-@[-`{-~]).+$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number and one special character")]
        public string NewPassword { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        public string ConfirmNewPassword { get; set; } = string.Empty;

        //ketika registrasi email-confirm = false
        //ketika confirm-email -> email-confirm = true
        //isAdmin (role) = hanya dapat diubah pada tampilan Admin
    }

    /// <summary>
    /// Request DTO untuk login
    /// </summary>
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; } = false; //checkbox remember me
    }

    /// <summary>
    /// Request DTO untuk FORGOT PASSWORD (POST)
    /// </summary>
    public class ForgotPasswordRequestDto
    {
        public string Email { get; set; } = string.Empty;
    }

    // Reset Password Request (GET) = Link
    public class ResetPasswordRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
        // special character yang dimaksud merujuk pada karakter yang terdaftar disini: https://owasp.org/www-community/password-special-characters
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[ -/:-@[-`{-~]).+$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number and one special character")]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }

    // Confirm Email Request (GET) = Flow After Register
    public class ConfirmEmailRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
    }

    /// <summary>
    /// Response DTO untuk authentication
    /// </summary>
    public class AuthResponseDto
    {
        // Status "Success" sudah ada di ApiResponse bernama StatusCode
        public string Message { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime AccessTokenExpiry { get; set; }
        public UserDto? User { get; set; }
    }

    /// <summary>
    /// DTO untuk profil user
    /// </summary>
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool EmailConfirmed { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<string> Roles { get; set; } = new List<string>(); //Roles = isAdmin
        public List<ClaimDto> Claims { get; set; } = new List<ClaimDto>();
    }

    /// <summary>
    /// DTO untuk claim
    /// </summary>
    public class RoleDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO untuk claim
    /// </summary>
    public class ClaimDto
    {
        public string Type { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    /// <summary>
    /// Request DTO untuk refresh token
    /// </summary>
    public class RefreshTokenRequestDto
    {
        [Required]
        public string AccessToken { get; set; } = string.Empty;
        
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }

    /// <summary>
    /// Request DTO untuk change password (POST)
    /// </summary>
    public class ChangePasswordRequestDto
    {
        [Required]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
        // special character yang dimaksud merujuk pada karakter yang terdaftar disini: https://owasp.org/www-community/password-special-characters
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[ -/:-@[-`{-~]).+$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number and one special character")]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }

    /// <summary>
    /// Request DTO untuk assign role
    /// </summary>
    public class RoleRequestDto
    {
        [Required]
        public string RoleName { get; set; } = string.Empty;
    }
}
