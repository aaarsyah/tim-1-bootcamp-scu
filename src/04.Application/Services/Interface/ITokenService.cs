using MyApp.Domain.Models;
using System.Security.Claims;

namespace MyApp.Application.Services;

/// <summary>
/// Token Service Interface
/// Mendefinisikan operasi untuk JWT token management
/// </summary>
public interface ITokenService
{
    string GenerateAccessToken(User user);
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    Task<bool> IsTokenValidAsync(string token);
    string GenerateRefreshToken();
    Task<string> GenerateEmailConfirmationTokenAsync();
    Task<string> GeneratePasswordResetTokenAsync();
}