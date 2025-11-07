using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Shared.DTOs;
using MyApp.Base.Exceptions;
using MyApp.WebAPI.Services;
using MyApp.Application.Validators;
using System.Security.Claims;

namespace MyApp.WebAPI.Controllers;

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
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<bool>>> Register([FromBody] RegisterRequestDto request)
    {
        var result = await _authenticationService.RegisterAsync(request);

        return Ok(ApiResponse<object>.SuccessResult());
        // Semua error akan throw exception dan akan di catch di middleware
    }

    [HttpPost("confirm-email")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<object>>> ConfirmEmail([FromBody] ConfirmEmailRequestDto request)
    {
        var result = await _authenticationService.ConfirmEmailAsync(request);

        return Ok(ApiResponse<object>.SuccessResult());
        // Semua error akan throw exception dan akan di catch di middleware
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
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<object>>> Logout()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
        {
            throw new TokenInvalidException();
        }
        var result = await _authenticationService.LogoutAsync(userId);
        return Ok(ApiResponse<object>.SuccessResult());
    }

    /// <summary>
    /// Change Password
    /// POST /api/auth/change-password
    /// Requires: JWT Token
    /// </summary>
    [HttpPost("change-password")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<object>>> ChangePassword([FromBody] ChangePasswordRequestDto request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
        {
            throw new TokenInvalidException();
        }
        var result = await _authenticationService.ChangePasswordAsync(userId, request);
        return Ok(ApiResponse<object>.SuccessResult());
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
    /// GET /api/auth/me
    /// Requires: JWT Token
    /// </summary>
    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<UserDto>>> GetProfile()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
        {
            throw new TokenInvalidException();
        }
        var profile = await _userManagementService.GetUserProfileAsync(userId);
        return Ok(ApiResponse<UserDto>.SuccessResult(profile));
    }
}
