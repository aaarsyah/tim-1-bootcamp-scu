using Microsoft.AspNetCore.Identity;
using MyApp.WebAPI.Models.Entities;
using MyApp.WebAPI.Models.DTOs;
using MyApp.WebAPI.Configuration;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
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
        private readonly IEmailService _emailService;

        public AuthenticationService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ITokenService tokenService,
            JwtSettings jwtSettings,
            ILogger<AuthenticationService> logger,
            IEmailService emailService
        )
            
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _jwtSettings = jwtSettings;
            _logger = logger;
            _emailService = emailService; //confirm-email
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
                throw new ValidationException("User with this email already exists");
            }

            // Generate email confirmation token
            var confirmationToken = await _tokenService.GenerateEmailConfirmationTokenAsync();

            // Create new user
            var user = new User
            {
                UserName = request.Name,
                Email = request.Email,
                EmailConfirmed = false, //true if user already confirmed email
                EmailConfirmationToken = confirmationToken,
                EmailConfirmationTokenExpiry = DateTime.UtcNow.AddHours(24),
                CreatedAt = DateTime.UtcNow
            };

            // Create user dengan password
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogWarning("User registration failed for {Email}: {Errors}", request.Email, errors);
                
                throw new ValidationException($"Registration failed: {errors}");
            }

            // Assign default role "User"
            await _userManager.AddToRoleAsync(user, "User");

            // Add default claims
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("can_view_profile", "true"));

            // Kirim email konfirmasi
            var confirmationLink = $"https://localhost:5099/confirm-email?email={user.Email}&token={confirmationToken}";
            await _emailService.SendEmailConfirmationAsync(user.Email, user.UserName, confirmationLink);

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
            if (user == null || !user.EmailConfirmed)
            {
                _logger.LogWarning("Login failed: User not found or inactive for email: {Email}", request.Email);
                  throw new ValidationException("Invalid email or password");
            }

            // Validasi password dengan lockout protection
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);
            if (!result.Succeeded)
            {
                _logger.LogWarning("Login failed for email: {Email}. Reason: {Reason}", request.Email, result.ToString());
                
                if (result.IsLockedOut)
                {
                    throw new ValidationException("Account is locked out. Please try again later.");
                }

                throw new ValidationException("Invalid email or password");
            }

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
                throw new ValidationException("Invalid access token");
            }

            var userIdClaim = principal.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new ValidationException("Invalid token claims");
            }

            // Validasi refresh token
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null || !user.EmailConfirmed || user.RefreshToken != request.RefreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new ValidationException("Invalid refresh token");
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
        /// Change Password for authenticated user (POST)
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

        //Forgot Password = Request password reset
        //Langsung Menggunakan HTTPClient - Service nya di Blazor UI
        public async Task<bool> ForgotPasswordAsync(ForgotPasswordRequestDto request)
        {
            _logger.LogInformation("Forgot password requested for email: {Email}", request.Email);

            var user = await _userManager.FindByEmailAsync(request.Email);

            //Menghindari Email Enumeration Attack (menghindari hacker mengetahui email mana yang valid)
            if (user == null)
            {
                _logger.LogWarning("Forgot password requested for non-existing email: {Email}", request.Email);
                // Demi keamanan: tetap return sukses tanpa memberitahu apakah email valid
                return true;
            }

            // Generate token reset password
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Buat link reset (menuju frontend URL BlazorUI)
            var resetLink = $"https://localhost:5099/reset-password?email={Uri.EscapeDataString(user.Email)}&token={Uri.EscapeDataString(token)}";


            // Jika email terdaftar dalam database, Kirim email reset password
            await _emailService.SendPasswordResetAsync(user.Email, user.Name, resetLink); 

            _logger.LogInformation("Password reset email sent to {Email}", user.Email);

            return true;
        }

        /// <summary>
        /// Reset password menggunakan token dari email
        /// </summary>
        public async Task<bool> ResetPasswordAsync(ResetPasswordRequestDto request)
        {
            _logger.LogInformation("Reset password attempt for email: {Email}", request.Email);

            if (request.NewPassword != request.ConfirmPassword)
            {
                _logger.LogWarning("Password confirmation mismatch for {Email}", request.Email);
                throw new ValidationException("Password and confirmation password do not match.");
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                _logger.LogWarning("Reset password attempt failed: user not found for {Email}", request.Email);
                throw new AuthenticationException("Invalid token or user.");
            }

            // Jalankan proses reset password
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogWarning("Password reset failed for {Email}: {Errors}", request.Email, errors);
                throw new ValidationException($"Password reset failed: {errors}");
            }

            // Kirim email notifikasi berhasil
            await _emailService.SendPasswordChangedNotificationAsync(user.Email, user.UserName ?? "User");

            _logger.LogInformation("Password reset successful for {Email}", request.Email);

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
                EmailConfirmed = user.EmailConfirmed,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Roles = roles.ToList(),
                Claims = claims.Select(c => new ClaimDto { Type = c.Type, Value = c.Value }).ToList()
            };
        }
    }
}
