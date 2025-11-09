using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyApp.Base.Exceptions;
using MyApp.Application.Configuration;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;
using MyApp.Application.Services;

namespace MyApp.Application.Feature.Authentications.Commands;

public class LoginCommand : IRequest<ApiResponse<AuthResponseDto>>
{
    public required LoginRequestDto LoginDto { get; set; }
}
public class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResponse<AuthResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<LoginCommandHandler> _logger;
    private readonly ITokenService _tokenService;
    private readonly IPasswordService _passwordService;
    private readonly JwtSettings _jwtSettings;

    private const int MAX_FAILED_ATTEMPTS = 5;
    private const int LOCKOUT_TIME_MINUTES = 15;
    public LoginCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<LoginCommandHandler> logger,
        ITokenService tokenService,
        IPasswordService passwordService,
        JwtSettings jwtSettings)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _tokenService = tokenService;
        _passwordService = passwordService;
        _jwtSettings = jwtSettings;
    }
    public async Task<ApiResponse<AuthResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Login attempt for email: {Email}", request.LoginDto.Email);

        // Cari user berdasarkan email
        var user = await _unitOfWork.UserManager.GetUserByEmailAsync(request.LoginDto.Email);
        if (user == null)
        {
            _logger.LogWarning("Login failed: User not found or inactive for email: {Email}", request.LoginDto.Email);
            throw new ValidationException("Invalid email or password.");
        }
        if (!user.EmailConfirmed)
        {
            throw new ValidationException("Invalid email or password.");
        }
        // Check account lockout
        if (!user.IsActive)
        {
            throw new AccountInactiveException("Account is inactive. Contact the administrator for help.");
        }
        // Check account lockout
        if (user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTime.UtcNow)
        {
            var remainingTime = (user.LockoutEnd.Value - DateTime.UtcNow).TotalMinutes;
            throw new AccountLockedException($"Account is locked. Please try again in {Math.Ceiling(remainingTime)} minutes.");
        }

        // Verify password
        if (!_passwordService.VerifyPassword(request.LoginDto.Password, user.PasswordHash))
        {
            // Increment failed attempts
            user.FailedLoginAttempts++;
            _logger.LogWarning("Login failed for email: {Email}. Reason: Invalid password", user.Email);

            if (user.FailedLoginAttempts >= MAX_FAILED_ATTEMPTS)
            {
                user.LockoutEnd = DateTime.UtcNow.AddMinutes(LOCKOUT_TIME_MINUTES);
                _logger.LogWarning("Account locked for user: {Email}", user.Email);
            }

            await _unitOfWork.SaveChangesAsync();

            throw new ValidationException("Invalid email or password");
        }

        // Reset failed attempts on successful login
        user.FailedLoginAttempts = 0;
        // Tidak perlu reset LockoutEnd, karena lockout sudah expired ketika user sudah bisa login
        user.LastLoginAt = DateTime.UtcNow;

        // Generate JWT tokens
        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        // Save refresh token
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = request.LoginDto.RememberMe
            ? DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenRememberMeExpirationDays)
            : DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Login successful for email: {Email}", user.Email);

        var authResponseDto = new AuthResponseDto
        {
            Message = "Login successful",
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            AccessTokenExpiry = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes)
        };
        return ApiResponse<AuthResponseDto>.SuccessResult(authResponseDto);
    }
}