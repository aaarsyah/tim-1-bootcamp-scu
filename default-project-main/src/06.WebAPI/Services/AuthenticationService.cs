using Humanizer;
using Microsoft.EntityFrameworkCore;
using MyApp.Shared.DTOs;
using MyApp.WebAPI.Configuration;
using MyApp.WebAPI.Data;
using MyApp.WebAPI.Exceptions;
using MyApp.WebAPI.Models.Entities;

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
        private readonly AppleMusicDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IEmailService _emailService;
        private readonly IPasswordService _passwordService;

        private const int MAX_FAILED_ATTEMPTS = 5;
        private const int LOCKOUT_TIME_MINUTES = 15;

        public AuthenticationService(
            AppleMusicDbContext context,
            ITokenService tokenService,
            JwtSettings jwtSettings,
            ILogger<AuthenticationService> logger,
            IEmailService emailService,
            IPasswordService passwordService
        )
            
        {
            _context = context;
            _tokenService = tokenService;
            _jwtSettings = jwtSettings;
            _logger = logger;
            _emailService = emailService; //confirm-email
            _passwordService = passwordService;
        }

        /// <summary>
        /// Register User Baru
        /// Membuat user account baru dengan role default "User"
        /// </summary>
        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            // Cek apakah email sudah terdaftar
            //var existingUser = await _userManager.FindByEmailAsync(request.Email);
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(a => a.Email == request.Email);
            if (existingUser != null)
            {
                throw new ValidationException("User with this email already exists");
            }

            _logger.LogInformation("User registration attempt for email: {Email}", request.Email);

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Generate email confirmation token
                var confirmationToken = await _tokenService.GenerateEmailConfirmationTokenAsync();

                // Create new user
                var user = new User
                {
                    Name = request.Name,
                    Email = request.Email,
                    PasswordHash = _passwordService.HashPassword(request.Password),
                    EmailConfirmed = false,
                    EmailConfirmationToken = confirmationToken,
                    EmailConfirmationTokenExpiry = DateTime.UtcNow.AddHours(24),
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System"
                };

                //await _unitOfWork.Users.AddAsync(user);
                //await _unitOfWork.CompleteAsync();
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync(); //Id di auto-generate setelah di save

                // Assign default role "User"

                var userRole = new UserRole
                {
                    UserId = user.Id,
                    RoleId = 1, // User role
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System"
                };

                // Add default claims
                var userClaim = new UserClaim
                {
                    UserId = user.Id,
                    ClaimType = "can_view_profile",
                    ClaimValue = "true",
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System"
                };

                // Save user role dan claims
                await _context.UserRoles.AddAsync(userRole); // TODO: Await all to make it faster
                await _context.UserClaims.AddAsync(userClaim);
                await _context.SaveChangesAsync();

                // Kirim email konfirmasi
                var confirmationLink = $"https://localhost:5070/confirm-email?email={Uri.EscapeDataString(user.Email)}&token={Uri.EscapeDataString(confirmationToken)}";
                await _emailService.SendEmailConfirmationAsync(user.Email, user.Name, confirmationLink);

                _logger.LogInformation("User registration successful for email: {Email}", request.Email);

                // Generate JWT tokens
                var accessToken = await _tokenService.GenerateAccessTokenAsync(user);
                var refreshToken = await _tokenService.GenerateRefreshTokenAsync();

                // Save refresh token
                // TODO: Shouldn't refresh token be given after email is confirmed?
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);

                // ===== STEP 9 =====
                // All operations succeeded, make permanent
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new AuthResponseDto
                {
                    Message = "Registration successful",
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    AccessTokenExpiry = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                    User = await MapToUserDto(user)
                };
            }
            catch (Exception ex)
            {
                // ===== ROLLBACK ON ERROR =====
                // Any exception -> undo all changes
                await transaction.RollbackAsync();
                _logger.LogError("Register failed, transaction rolled back");
                throw; // Re-throw to be handled by middleware
            }
        }

        /// <summary>
        /// Login User
        /// Validasi credentials dan generate JWT tokens
        /// </summary>
        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            _logger.LogInformation("Login attempt for email: {Email}", request.Email);

            // Cari user berdasarkan email
            //var user = await _userManager.FindByEmailAsync(request.Email);
            var user = await _context.Users
                .FirstOrDefaultAsync(a => a.Email == request.Email);
            if (user == null || !user.EmailConfirmed)
            {
                _logger.LogWarning("Login failed: User not found or inactive for email: {Email}", request.Email);
                throw new ValidationException("Invalid email or password");
            }
            // Check account lockout
            if (user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTime.UtcNow)
            {
                var remainingTime = (user.LockoutEnd.Value - DateTime.UtcNow).TotalMinutes;
                throw new ValidationException($"Account is locked. Please try again in {Math.Ceiling(remainingTime)} minutes");
            }

            // Verify password
            if (!_passwordService.VerifyPassword(request.Password, user.PasswordHash))
            {
                // Increment failed attempts
                user.FailedLoginAttempts++;
                //_logger.LogWarning("Login failed for email: {Email}. Reason: {Reason}", request.Email, result.ToString());

                if (user.FailedLoginAttempts >= MAX_FAILED_ATTEMPTS)
                {
                    user.LockoutEnd = DateTime.UtcNow.AddMinutes(LOCKOUT_TIME_MINUTES);
                    _logger.LogWarning($"Account locked for user: {user.Email}");
                }

                await _context.SaveChangesAsync();

                throw new ValidationException("Invalid email or password");
            }

            // Reset failed attempts on successful login
            user.FailedLoginAttempts = 0;
            //user.LockoutEnd = null; // Tidak perlu reset LockoutEnd, karena lockout sudah expired ketika user sudah bisa login
            user.LastLoginAt = DateTime.UtcNow;

            // TODO: Use the rememberMe field?
            // Generate JWT tokens
            var accessToken = await _tokenService.GenerateAccessTokenAsync(user);
            var refreshToken = await _tokenService.GenerateRefreshTokenAsync();

            // Save refresh token
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = request.RememberMe
                ? DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenRememberMeExpirationDays)
                : DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
            await _context.SaveChangesAsync();

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
            //var user = await _userManager.FindByIdAsync(userId.ToString());
            var user = await _context.Users
                .FirstOrDefaultAsync(a => a.Id == userId);
            if (user == null)
            {
                //return new AuthResponseDto
                //{
                //    Success = false,
                //    Message = "Invalid refresh token"
                //};
                throw new AuthenticationException("Token is invalid");
            }
            if (!user.EmailConfirmed)
            {
                throw new AuthenticationException("User has not confirmed email");
            }

            if (user.RefreshToken != request.RefreshToken)
            {
                throw new AuthenticationException("Refresh token is invalid");
            }
            if (user.RefreshTokenExpiry <= DateTime.UtcNow)
            {
                throw new AuthenticationException("Refresh token has expired");
            }
            // Generate tokens baru
            var accessToken = await _tokenService.GenerateAccessTokenAsync(user);
            var refreshToken = await _tokenService.GenerateRefreshTokenAsync();

            // Update refresh token
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
            await _context.SaveChangesAsync();

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
            var user = await _context.Users
                .FirstOrDefaultAsync(a => a.Id == userId);
            if (user == null)
            {
                throw new AuthenticationException("Token is invalid");
            }
            // Invalidate refresh token
            user.RefreshToken = null;
            user.RefreshTokenExpiry = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            _logger.LogInformation("User logged out: {UserId}", userId);
            return true;
        }

        /// <summary> 
        /// Change Password for authenticated user = change password ketika sudah login di profile misalnya
        /// Ubah password user yang sudah login
        /// </summary>
        public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordRequestDto request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(a => a.Id == userId);
            if (user == null)
            {
                throw new AuthenticationException("Token is invalid");
            }

            if (request.NewPassword != request.ConfirmNewPassword)
            {
                _logger.LogWarning("Password confirmation mismatch for {Email}", user.Email);
                throw new ValidationException("Password and confirmation password do not match.");
            }

            // Verify password
            if (!_passwordService.VerifyPassword(request.NewPassword, user.PasswordHash))
            {
                _logger.LogWarning("Password change failed for user: {UserId}", userId);
                throw new ValidationException("Password change failed");
            }

            //var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            user.PasswordHash = _passwordService.HashPassword(request.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;
            user.UpdatedBy = user.Name;

            _logger.LogInformation("Password changed successfully for user: {UserId}", userId);
            return true;
        }

        //Forgot Password = Request password reset 
        //Langsung Menggunakan HTTPClient - Service nya di Blazor UI
        public async Task<bool> ForgotPasswordAsync(ForgotPasswordRequestDto request)
        {
            _logger.LogInformation("Forgot password requested for email: {Email}", request.Email);

            var user = await _context.Users
                .FirstOrDefaultAsync(a => a.Email == request.Email);

            //Menghindari Email Enumeration Attack (menghindari hacker mengetahui email mana yang valid)
            if (user == null)
            {
                _logger.LogWarning("Forgot password requested for non-existing email: {Email}", request.Email);
                // Demi keamanan: tetap return sukses tanpa memberitahu apakah email valid
                return true;
            }

            // Generate token reset password
            var token = await _tokenService.GeneratePasswordResetTokenAsync();
            user.PasswordResetToken = token;
            user.PasswordResetTokenExpiry = DateTime.UtcNow.AddHours(1); ; // Token valid for 1 hour

            await _context.SaveChangesAsync();

            // Buat link reset (menuju frontend URL BlazorUI)
            var resetLink = $"http://localhost:5070/reset-password?email={Uri.EscapeDataString(user.Email)}&token={Uri.EscapeDataString(token)}";


            // Jika email terdaftar dalam database, Kirim email reset password
            await _emailService.SendPasswordResetAsync(user.Email, user.Name ?? string.Empty, resetLink); 

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

            var user = await _context.Users
                .FirstOrDefaultAsync(a => a.Email == request.Email);
            if (user == null)
            {
                _logger.LogWarning("Reset password attempt failed: user not found for {Email}", request.Email);
                throw new AuthenticationException("Invalid user or token.");
            }

            // Validasi token buatan sendiri
            if (user.PasswordResetToken != request.AccessToken ||
                user.PasswordResetTokenExpiry < DateTime.UtcNow)
            {
                _logger.LogWarning("Invalid or expired token for {Email}", request.Email);
                throw new ValidationException("Invalid or expired reset token.");
            }

            // Verify password
            if (!_passwordService.VerifyPassword(request.NewPassword, user.PasswordHash))
            {
                _logger.LogWarning("Password reset failed for user: {UserId}", user.Id);
                throw new ValidationException("Password reset failed");
            }

            //var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            user.PasswordHash = _passwordService.HashPassword(request.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;
            user.UpdatedBy = user.Name;

            // Reset password reset token
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpiry = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            // Kirim email notifikasi berhasil
            await _emailService.SendPasswordChangedNotificationAsync(user.Email, user.Name ?? "User");

            _logger.LogInformation("Password reset successful for {Email}", request.Email);

            return true;
        }


        /// <summary>
        /// Map User entity to UserDto
        /// </summary>
        private async Task<UserDto> MapToUserDto(User user)
        {
            var usera = await _context.Users
                .FirstOrDefaultAsync(a => a.Email == user.Email);
            //var roles = await _userManager.GetRolesAsync(user);
            var roles = await _context.UserRoles
                .Where(a => a.UserId == user.Id)
                .Include(a => a.Role)
                .Select(a => a.Role.Name)
                .ToListAsync();
            //var claims = await _userManager.GetClaimsAsync(user);
            var claims = await _context.UserClaims
                .Where(a => a.UserId == user.Id)
                .Select(a => new ClaimDto { Type = a.ClaimType, Value = a.ClaimValue })
                .ToListAsync();

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email ?? string.Empty,
                EmailConfirmed = user.EmailConfirmed,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt ?? DateTime.MinValue,
                Roles = roles,
                Claims = claims
            };
        }
    }
}
