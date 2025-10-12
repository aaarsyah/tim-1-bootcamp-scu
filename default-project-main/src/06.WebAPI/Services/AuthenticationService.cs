using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using MyApp.WebAPI.Configuration;
using MyApp.WebAPI.Models.DTOs;
using MyApp.WebAPI.Models.Entities;
using MyApp.WebAPI.Exceptions;

namespace MyApp.WebAPI.Services
{
    /// <summary>
    /// Authentication Service Implementation
    /// Menangani semua operasi authentication: register, login, logout, refresh token
    /// 
    /// Key Features:
    /// - User registration dengan validation
    /// - Login dengan JWT token generation
    /// - Refresh token untuk perpanjang session
    /// - Password change
    /// - Account lockout protection
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ITokenService tokenService,
            JwtSettings jwtSettings,
            ILogger<AuthenticationService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _jwtSettings = jwtSettings;
            _logger = logger;
        }

        /// <summary>
        /// Register User Baru
        /// Membuat user account baru dengan role default "User"
        /// </summary>
        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            _logger.LogInformation("User registration attempt for email: {Email}", request.Email);

            // Cek apakah email sudah terdaftar
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                //return new AuthResponseDto
                //{
                //    Success = false,
                //    Message = "User with this email already exists"
                //};
                throw new ValidationException("User with this email already exists");
            }

            // Create new user
            var user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                Name = request.Name,
                EmailConfirmed = true // For demo purposes, set to true
            };

            // Create user dengan password
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogWarning("User registration failed for {Email}: {Errors}", request.Email, errors);
                
                //return new AuthResponseDto
                //{
                //    Success = false,
                //    Message = $"Registration failed: {errors}"
                //};
                throw new ValidationException($"Registration failed: {errors}");
            }

            // Assign default role "User"
            await _userManager.AddToRoleAsync(user, "User");

            // Add default claims
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("can_view_profile", "true"));

            _logger.LogInformation("User registration successful for email: {Email}", request.Email);

            // Generate JWT tokens
            var accessToken = await _tokenService.GenerateAccessTokenAsync(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            // Save refresh token ke database
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
            await _userManager.UpdateAsync(user);

            return new AuthResponseDto
            {
                Message = "Registration successful",
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiry = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                User = await MapToUserDto(user)
            };
        }

        /// <summary>
        /// Login User
        /// Validasi credentials dan generate JWT tokens
        /// </summary>
        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            _logger.LogInformation("Login attempt for email: {Email}", request.Email);

            // Cari user berdasarkan email
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !user.IsActive)
            {
                _logger.LogWarning("Login failed: User not found or inactive for email: {Email}", request.Email);
                //return new AuthResponseDto
                //{
                //    Success = false,
                //    Message = "Invalid email or password"
                //};
                throw new ValidationException("Invalid email or password");
            }

            // Validasi password dengan lockout protection
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);
            if (!result.Succeeded)
            {
                _logger.LogWarning("Login failed for email: {Email}. Reason: {Reason}", request.Email, result.ToString());
                
                if (result.IsLockedOut)
                {
                    //return new AuthResponseDto
                    //{
                    //    Success = false,
                    //    Message = "Account is locked out. Please try again later."
                    //};
                    throw new ValidationException("Account is locked out. Please try again later.");
                }

                //return new AuthResponseDto
                //{
                //    Success = false,
                //    Message = "Invalid email or password"
                //};
                throw new ValidationException("Invalid email or password");
            }

            // TODO: Use the rememberMe field
            
            // Generate JWT tokens
            var accessToken = await _tokenService.GenerateAccessTokenAsync(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            // Save refresh token
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
            await _userManager.UpdateAsync(user);

            _logger.LogInformation("Login successful for email: {Email}", request.Email);

            return new AuthResponseDto
            {
                Message = "Login successful",
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiry = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                User = await MapToUserDto(user)
            };
        }

        /// <summary>
        /// Refresh Token
        /// Perpanjang session dengan refresh token tanpa perlu login ulang
        /// </summary>
        public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request)
        {
            // Extract claims dari expired access token
            var principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
            if (principal == null)
            {
                //return new AuthResponseDto
                //{
                //    Success = false,
                //    Message = "Invalid access token"
                //};
                throw new AuthenticationException("Invalid access token");
            }

            var userIdClaim = principal.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                //return new AuthResponseDto
                //{
                //    Success = false,
                //    Message = "Invalid token claims"
                //};
                throw new AuthenticationException("Invalid token claims");
            }

            // Validasi refresh token
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                //return new AuthResponseDto
                //{
                //    Success = false,
                //    Message = "Invalid refresh token"
                //};
                throw new AuthenticationException("Token is invalid");
            }
            if (await _userManager.IsEmailConfirmedAsync(user))
            {
                throw new AuthenticationException("User has not confirmed email");
            }

            if (user.RefreshToken != request.RefreshToken)
            {
                throw new AuthenticationException("Refresh token is invalid");
            }
            if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new AuthenticationException("Refresh token has expired");
            }
            // Generate tokens baru
            var accessToken = await _tokenService.GenerateAccessTokenAsync(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            // Update refresh token
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
            await _userManager.UpdateAsync(user);

            return new AuthResponseDto
            {
                Message = "Token refreshed successfully",
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiry = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                User = await MapToUserDto(user)
            };
        }

        /// <summary>
        /// Logout User
        /// Invalidate refresh token
        /// </summary>
        public async Task<bool> LogoutAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new AuthenticationException("Token is invalid");
            }
            // Invalidate refresh token
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            _logger.LogInformation("User logged out: {UserId}", userId);
            return true;
        }

        /// <summary>
        /// Change Password
        /// Ubah password user yang sudah login
        /// </summary>
        public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordRequestDto request)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new AuthenticationException("Token is invalid");
            }
            
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                _logger.LogWarning("Password change failed for user: {UserId}", userId);
                throw new ValidationException("Password change failed");
            }

            _logger.LogInformation("Password changed successfully for user: {UserId}", userId);
            return true;
            
        }

        /// <summary>
        /// Map User entity to UserDto
        /// </summary>
        private async Task<UserDto> MapToUserDto(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = await _userManager.GetClaimsAsync(user);

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email ?? string.Empty,
                IsActive = user.IsActive,
                IsAdmin = user.IsAdmin,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Roles = roles.ToList(),
                Claims = claims.Select(c => new ClaimDto { Type = c.Type, Value = c.Value }).ToList()
            };
        }
    }
}
