using MyApp.Shared.DTOs;

namespace MyApp.WebAPI.Services;

/// <summary>
/// Authentication Service Interface
/// Mendefinisikan operasi untuk authentication
/// </summary>
public interface IAuthenticationService
{
    Task<bool> RegisterAsync(RegisterRequestDto request);
    Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
    Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);
    Task<bool> LogoutAsync(int userId);
    Task<bool> ChangePasswordAsync(int userId, ChangePasswordRequestDto request);
    Task<bool> ForgotPasswordAsync(ForgotPasswordRequestDto request);
    Task<bool> ResetPasswordAsync(ResetPasswordRequestDto request);
    Task<bool> ConfirmEmailAsync(ConfirmEmailRequestDto request);
}
