using MyApp.Shared.DTOs;

namespace MyApp.BlazorUI.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginRequestDto request);
        Task<AuthResponseDto?> RegisterAsync(RegisterRequestDto request);
        Task<bool> ChangePasswordAsync(ChangePasswordRequestDto request);
        Task<bool> ForgotPasswordAsync(ForgotPasswordRequestDto request);
        Task<bool> ResetPasswordAsync(ResetPasswordRequestDto request);
        Task<UserDto?> GetCurrentUserAsync();
        Task LogoutAsync();
    }
    public class AuthService
    {
    }
}
