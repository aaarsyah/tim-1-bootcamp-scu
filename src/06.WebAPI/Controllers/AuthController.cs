using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Feature.Authentications.Commands;
using MyApp.Base.Exceptions;
using MyApp.Shared.DTOs;
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
    private readonly IMediator _mediator;

    public AuthController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Register User Baru
    /// POST /api/auth/register
    /// </summary>
    [HttpPost("register")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<object>>> Register([FromBody] RegisterRequestDto request)
    {
        var command = new RegisterCommand { RegisterDto = request,  };
        var result = await _mediator.Send(command);

        return Ok(result);
        // Semua error akan throw exception dan akan di catch di middleware
    }

    [HttpPost("confirm-email")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<object>>> ConfirmEmail([FromBody] ConfirmEmailRequestDto request)
    {
        var command = new ConfirmEmailCommand { ConfirmEmailDto = request, };
        var result = await _mediator.Send(command);

        return Ok(result);
        // Semua error akan throw exception dan akan di catch di middleware
    }

    /// <summary>
    /// Login
    /// POST /api/auth/login
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login([FromBody] LoginRequestDto request)
    {
        var command = new LoginCommand { LoginDto = request, };
        var result = await _mediator.Send(command);
        
        return Ok(result);
        // Semua error akan throw exception dan akan di catch di middleware
    }

    /// <summary>
    /// Refresh Token
    /// POST /api/auth/refresh-token
    /// </summary>
    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<AuthResponseDto>>> RefreshToken([FromBody] RefreshTokenRequestDto request)
    {
        var command = new RefreshTokenCommand { RefreshTokenDto = request, };
        var result = await _mediator.Send(command);
        
        return Ok(result);
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
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<object>>> Logout()
    {
        var userClaimIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userClaimIdentifier == null || !Guid.TryParse(userClaimIdentifier.Value, out Guid userRefId))
        {
            throw new TokenInvalidException();
        }
        var command = new LogoutCommand { UserRefId = userRefId };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Change Password
    /// POST /api/auth/change-password
    /// Requires: JWT Token
    /// </summary>
    [HttpPost("change-password")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<object>>> ChangePassword([FromBody] ChangePasswordRequestDto request)
    {
        var userClaimIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userClaimIdentifier == null || !Guid.TryParse(userClaimIdentifier.Value, out Guid userRefId))
        {
            throw new TokenInvalidException();
        }
        var command = new ChangePasswordCommand { UserRefId = userRefId };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Request password reset new
    /// </summary>
    [HttpPost("forgot-password")]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<object>>> ForgotPassword([FromBody] ForgotPasswordRequestDto request)
    {
        var command = new ForgotPasswordCommand { ForgotPasswordDto = request };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Reset password with token
    /// </summary>
    [HttpPost("reset-password")]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<object>>> ResetPassword([FromBody] ResetPasswordRequestDto request)
    {
        var command = new ResetPasswordCommand { ResetPasswordDto = request };
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
