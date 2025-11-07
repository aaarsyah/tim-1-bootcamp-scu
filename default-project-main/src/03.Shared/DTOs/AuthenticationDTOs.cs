using System.ComponentModel.DataAnnotations;

namespace MyApp.Shared.DTOs;

/// <summary>
/// Request DTO untuk registrasi user baru
/// </summary>
public class RegisterRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string ConfirmNewPassword { get; set; } = string.Empty;
}

/// <summary>
/// Request DTO untuk login
/// </summary>
public class LoginRequestDto
{
    public string Email { get; set; } = string.Empty;
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
    public string PasswordResetToken { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string ConfirmNewPassword { get; set; } = string.Empty;
}

// Confirm Email Request (GET) = Flow After Register
public class ConfirmEmailRequestDto
{
    public string Email { get; set; } = string.Empty;
    public string EmailConfirmationToken { get; set; } = string.Empty;
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
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}

/// <summary>
/// Request DTO untuk change password (POST)
/// </summary>
public class ChangePasswordRequestDto
{
    public string Password { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
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
