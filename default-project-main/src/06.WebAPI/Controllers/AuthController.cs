using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MyApp.WebAPI.Models.DTOs;
using MyApp.WebAPI.Services;
using MyApp.WebAPI.Models;

namespace MyApp.WebAPI.Controllers
{
    /// <summary>
    /// Authentication Controller
    /// Menangani semua endpoint untuk authentication:
    /// - Register: Membuat user baru
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

        public AuthController(
            IAuthenticationService authenticationService,
            IUserManagementService userManagementService,
            ILogger<AuthController> logger)
        {
            _authenticationService = authenticationService;
            _userManagementService = userManagementService;
            _logger = logger;
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
            
            if (result.Success)
            {
                return Ok(ApiResponse<AuthResponseDto>.SuccessResult(result));
            }

            return BadRequest(ApiResponse<AuthResponseDto>.ErrorResult("Registration failed"));
        }

        /// <summary>
        /// Login
        /// POST /api/auth/login
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login([FromBody] LoginRequestDto request)
        {
            var result = await _authenticationService.LoginAsync(request);
            
            if (result.Success)
            {
                return Ok(ApiResponse<AuthResponseDto>.SuccessResult(result));
            }

            return Unauthorized(ApiResponse<AuthResponseDto>.ErrorResult("Invalid credentials"));
        }

        /// <summary>
        /// Refresh Token
        /// POST /api/auth/refresh-token
        /// </summary>
        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> RefreshToken([FromBody] RefreshTokenRequestDto request)
        {
            var result = await _authenticationService.RefreshTokenAsync(request);
            
            if (result.Success)
            {
                return Ok(ApiResponse<AuthResponseDto>.SuccessResult(result));
            }

            return Unauthorized(ApiResponse<AuthResponseDto>.ErrorResult("Unauthorized access"));
        }

        /// <summary>
        /// Logout
        /// POST /api/auth/logout
        /// Requires: JWT Token (user must be logged in)
        /// </summary>
        [HttpPost("logout")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<bool>>> Logout()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return BadRequest(ApiResponse<bool>.ErrorResult("Invalid user ID"));
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
                return BadRequest(ApiResponse<bool>.ErrorResult("Invalid user ID"));
            }

            var result = await _authenticationService.ChangePasswordAsync(userId, request);
            
            return Ok(ApiResponse<bool>.SuccessResult(result));
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

            var profile = await _userManagementService.GetUserProfileAsync(userId);
            if (profile == null)
            {
                return NotFound(ApiResponse<UserDto>.ErrorResult("User Profile not found"));
            }

            return Ok(ApiResponse<UserDto>.SuccessResult(profile));
        }
    }
}
