using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MyApp.WebAPI.Configuration;
using MyApp.WebAPI.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace MyApp.WebAPI.Services
{
    /// <summary>
    /// Token Service Interface
    /// Mendefinisikan operasi untuk JWT token management
    /// </summary>
    public interface ITokenService
    {
        Task<string> GenerateAccessTokenAsync(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
        Task<bool> IsTokenValidAsync(string token);
        Task<string> GenerateEmailConfirmationTokenAsync();
        Task<string> GeneratePasswordResetTokenAsync();
    }

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
        private readonly UserManager<User> _userManager;
        private readonly ILogger<TokenService> _logger;

        public TokenService(JwtSettings jwtSettings, UserManager<User> userManager, ILogger<TokenService> logger)
        {
            _jwtSettings = jwtSettings;
            _userManager = userManager;
            _logger = logger;
        }

        /// <summary>
        /// Generate JWT Access Token
        /// Token berisi user information dan claims sebagai payload
        /// </summary>
        public async Task<string> GenerateAccessTokenAsync(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            // Build claims dari user
            var claims = await BuildClaimsAsync(user);

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
        /// Generate Refresh Token
        /// Token random untuk memperbarui access token yang expired
        /// </summary>
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Base64UrlEncoder.Encode(randomNumber);
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
        private async Task<List<Claim>> BuildClaimsAsync(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email ?? string.Empty),
                new(ClaimTypes.Name, user.UserName ?? string.Empty),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //guid
                new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            // Add roles ke claims
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            // Add user custom claims
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            return claims;
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
}
