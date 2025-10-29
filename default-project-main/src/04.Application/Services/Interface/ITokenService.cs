using MyApp.Domain.Models;
using System.Security.Claims;

namespace MyApp.WebAPI.Services;

/// <summary>
/// Token Service Interface
/// Mendefinisikan operasi untuk JWT token management
/// </summary>
public interface ITokenService
{
    Task<string> GenerateAccessTokenAsync(User user);
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    Task<bool> IsTokenValidAsync(string token);
    Task<string> GenerateRefreshTokenAsync();
    Task<string> GenerateEmailConfirmationTokenAsync();
    Task<string> GeneratePasswordResetTokenAsync();
}