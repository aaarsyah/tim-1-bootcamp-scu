// Import Entity Framework Core untuk database operations
using Microsoft.EntityFrameworkCore;
// Import DbContext untuk database operations
using MyApp.Infrastructure.Data;
// Import Models untuk entities dan response wrappers
using MyApp.Domain.Models;
// Import Logging untuk log aplikasi
using Microsoft.Extensions.Logging;
// Import Configuration untuk akses konfigurasi aplikasi
using MyApp.Infrastructure.Configuration;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace MyApp.Application.Services;

/// <summary>
/// Token Service Implementation
/// Handles JWT token generation, validation, and refresh token management
/// 
/// Key Features:
/// - Generate JWT access tokens with claims
/// - Generate secure refresh tokens
/// - Validate tokens
/// - Extract claims from expired tokens (for refresh)
/// </summary>
public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly ILogger<TokenService> _logger;

    public TokenService(JwtSettings jwtSettings, ILogger<TokenService> logger)
    {
        _jwtSettings = jwtSettings;
        _logger = logger;
    }

    /// <summary>
    /// Generate JWT Access Token
    /// Token berisi user information dan claims sebagai payload
    /// </summary>
    public string GenerateAccessToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        if (string.IsNullOrEmpty(_jwtSettings.SecretKey))
        {
            throw new InvalidOperationException("JWT Secret Key not configured");
        }
        var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
        

        // Build claims dari user
        var claims = BuildClaims(user);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes), //Expired
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    /// <summary>
    /// Get Principal from Expired Token
    /// Extract user claims dari expired token untuk proses refresh
    /// </summary>
    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = _jwtSettings.ValidateAudience,
            ValidateIssuer = _jwtSettings.ValidateIssuer,
            ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
            ValidateLifetime = false, // Tidak validate lifetime karena expired
            ValidIssuer = _jwtSettings.Issuer,
            ValidAudience = _jwtSettings.Audience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        
        try
        {
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || 
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to get principal from expired token");
            return null;
        }
    }

    /// <summary>
    /// Validate Token
    /// Cek apakah token valid dan belum expired
    /// </summary>
    public async Task<bool> IsTokenValidAsync(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuer = _jwtSettings.Issuer,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidAudience = _jwtSettings.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(_jwtSettings.ClockSkew)
            };

            tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
            return await Task.FromResult(true);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Token validation failed");
            return await Task.FromResult(false);
        }
    }

    /// <summary>
    /// Build Claims untuk JWT Token
    /// Menambahkan informasi user, roles, dan custom claims ke token
    /// </summary>
    private List<Claim> BuildClaims(User user)
    {
        var claims = new List<Claim>
        {
            // Perlu diperhatikan bahwa JwtRegisteredClaimNames.Sub akan dipetakan secara otomatis ke System.Security.Claims.ClaimTypes.NameIdentifier
            // Lihat: https://clintmcmahon.com/Blog/aspnet-core-sub-claim-mapping
            new(JwtRegisteredClaimNames.Sub, user.RefId.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new(JwtRegisteredClaimNames.Name, user.Name ?? string.Empty),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //guid
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        };

        // Add roles ke claims
        //var roles = await _userManager.GetRolesAsync(user);
        var roles = user.UserRoles.Select(e => e.Role).Select(e => e.Name).ToList();
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        // Add user custom claims
        //var userClaims = await _userManager.GetClaimsAsync(user);
        //var claims = await _userManager.GetClaimsAsync(user);
        var userClaims = user.UserClaims.Select(a => new Claim(a.ClaimType, a.ClaimValue)).ToList();
        claims.AddRange(userClaims);

        return claims;
    }

    /// <summary>
    /// Generate Refresh Token
    /// Token random untuk memperbarui access token yang expired
    /// </summary>
    public string GenerateRefreshToken()
    {
        return Base64UrlEncoder.Encode(RandomNumberGenerator.GetBytes(64));
    }
    public Task<string> GenerateEmailConfirmationTokenAsync()
    {
        return Task.FromResult(Base64UrlEncoder.Encode(RandomNumberGenerator.GetBytes(64)));
    }

    public Task<string> GeneratePasswordResetTokenAsync()
    {
        return Task.FromResult(Base64UrlEncoder.Encode(RandomNumberGenerator.GetBytes(64)));
    }
}
