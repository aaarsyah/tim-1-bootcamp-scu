using MyApp.Shared.DTOs;
using System.Net.Http.Headers;

namespace MyApp.BlazorUI.Services;

public interface IAuthService
{
    Task<AuthResponseDto?> LoginAsync(LoginRequestDto request);
    Task<bool> RegisterAsync(RegisterRequestDto request);
    Task<bool> ConfirmEmailAsync(ConfirmEmailRequestDto request);
    Task<bool> ChangePasswordAsync(AuthenticationHeaderValue authorization, ChangePasswordRequestDto request);
    Task<bool> ForgotPasswordAsync(ForgotPasswordRequestDto request);
    Task<bool> ResetPasswordAsync(ResetPasswordRequestDto request);
    Task LogoutAsync(AuthenticationHeaderValue authorization);
    Task<bool> IsLoggedInAsync();
    Task<bool> IsAdminAsync();
    Task<AuthenticationHeaderValue?> GetAuthAsync();
}