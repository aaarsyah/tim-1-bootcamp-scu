using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MyApp.WebAPI.Models.DTOs;
using MyApp.WebAPI.Services;
using MyApp.WebAPI.Models;
using MyApp.WebAPI.Exceptions;
using MyApp.WebAPI.Validators;
using Microsoft.AspNetCore.Identity;

using MyApp.WebAPI.Models.Entities;

namespace MyApp.WebAPI.Controllers
{
    /// <summary>
    /// Authentication Controller
    /// Menangani semua endpoint untuk authentication:
    /// - Register: Membuat user baru
    /// - Confirm-Email : EmailConfirmed = true
    /// - Login: Authenticate dan dapat JWT token
    /// - Refresh Token: Perpanjang session
    /// - Logout: Invalidate session
    /// - Change Password: Ubah password
    /// - Get Profile: Lihat profil sendiri
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserManagementService _userManagementService;
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<User> _userManager;
        

        public AuthController(
            IAuthenticationService authenticationService,
            IUserManagementService userManagementService,
            ILogger<AuthController> logger,
            UserManager<User> userManager)
        {
            _authenticationService = authenticationService;
            _userManagementService = userManagementService;
            _logger = logger;
            _userManager = userManager;
        }

        /// <summary>
        /// Register User Baru
        /// POST /api/auth/register
        /// </summary>
        [HttpPost("register")]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Register([FromBody] RegisterRequestDto request)
        {
            var result = await _authenticationService.RegisterAsync(request);
            
            return Ok(ApiResponse<AuthResponseDto>.SuccessResult(result));
            // Semua error akan throw exception dan akan di catch di middleware
        }

        //TODO : Still cannot confirm-email
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest("Invalid email");
            }

            // Cek token valid dan belum expired
            if (user.EmailConfirmationToken != token || user.EmailConfirmationTokenExpiry < DateTime.UtcNow)
            {
                return BadRequest("Invalid or expired confirmation token");
            }

            // Tandai email sebagai terverifikasi
            user.EmailConfirmed = true;
            user.EmailConfirmationToken = null; // hapus token agar tidak bisa digunakan lagi
            user.EmailConfirmationTokenExpiry = null;

            await _userManager.UpdateAsync(user);

            return Ok("Email has been successfully confirmed. You can now log in.");
        }

        /// <summary>
        /// Login
        /// POST /api/auth/login
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login([FromBody] LoginRequestDto request)
        {
            var result = await _authenticationService.LoginAsync(request);
            
           return Ok(ApiResponse<AuthResponseDto>.SuccessResult(result));
            // Semua error akan throw exception dan akan di catch di middleware
        }

        /// <summary>
        /// Refresh Token
        /// POST /api/auth/refresh-token
        /// </summary>
        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> RefreshToken([FromBody] RefreshTokenRequestDto request)
        {
            var result = await _authenticationService.RefreshTokenAsync(request);
            
            return Ok(ApiResponse<AuthResponseDto>.SuccessResult(result));
            // Semua error akan throw exception dan akan di catch di middleware
        }

        /// <summary>
        /// Logout
        /// POST /api/auth/logout
        /// Requires: JWT Token (user must be logged in)
        /// </summary>
        [HttpPost("logout")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<bool>>> Logout()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new AuthenticationException("Token is invalid");
            }

            var result = await _authenticationService.LogoutAsync(userId);
            
            return Ok(ApiResponse<bool>.SuccessResult(result));
        }

        /// <summary>
        /// Change Password
        /// POST /api/auth/change-password
        /// Requires: JWT Token
        /// </summary>
        [HttpPost("change-password")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<bool>>> ChangePassword([FromBody] ChangePasswordRequestDto request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new AuthenticationException("Token is invalid");
            }

            var result = await _authenticationService.ChangePasswordAsync(userId, request);

            return Ok(ApiResponse<bool>.SuccessResult(result));
        }

        /// <summary>
        /// Request password reset new
        /// </summary>
        [HttpPost("forgot-password")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto request)
        {
            var validator = new ForgotPasswordRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }
            var result = await _authenticationService.ForgotPasswordAsync(request);
            return Ok(ApiResponse<bool>.SuccessResult(result));
        }

        /// <summary>
        /// Reset password with token
        /// </summary>
        [HttpPost("reset-password")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto request)
        {
            var validator = new ResetPasswordRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                throw new ValidationException(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));

            var result = await _authenticationService.ResetPasswordAsync(request);
            return Ok(ApiResponse<bool>.SuccessResult(result));

            // var command = new ResetPasswordCommand { Request = request };
            // var result = await _mediator.Send(command);
            
        }

        /// <summary>
        /// Get Current User Profile
        /// GET /api/auth/profile
        /// Requires: JWT Token
        /// </summary>
        [HttpGet("profile")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<UserDto>>> GetProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return BadRequest(ApiResponse<UserDto>.ErrorResult("Invalid user ID"));
            }

            // TODO: Is there token validation?
            var profile = await _userManagementService.GetUserProfileAsync(userId);
            if (profile == null)
            {
                throw new NotFoundException("User Profile not found");
            }

            return Ok(ApiResponse<UserDto>.SuccessResult(profile));
        }
    }
}
