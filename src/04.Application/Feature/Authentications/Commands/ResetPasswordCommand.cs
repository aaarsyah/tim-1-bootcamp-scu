using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Configuration;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;
using MyApp.Application.Services;

namespace MyApp.Application.Feature.Authentications.Commands;

public class ResetPasswordCommand : IRequest<ApiResponse<object>>
{
    public ResetPasswordRequestDto ResetPasswordDto { get; set; }
}
public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ApiResponse<object>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<ResetPasswordCommandHandler> _logger;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;
    private readonly IPasswordService _passwordService;
    private readonly JwtSettings _jwtSettings;
    public ResetPasswordCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<ResetPasswordCommandHandler> logger,
        ITokenService tokenService,
        IEmailService emailService,
        IPasswordService passwordService,
        JwtSettings jwtSettings)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _tokenService = tokenService;
        _emailService = emailService;
        _passwordService = passwordService;
        _jwtSettings = jwtSettings;
    }
    public async Task<ApiResponse<object>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Reset password attempt for email: {Email}", request.ResetPasswordDto.Email);

        var user = await _unitOfWork.UserManager.GetUserByEmailAsync(request.ResetPasswordDto.Email);
        if (user == null)
        {
            _logger.LogWarning("Reset password attempt failed: user not found for {Email}", request.ResetPasswordDto.Email);
            throw new ValidationException("Invalid or expired reset token.");
        }

        // Validasi token buatan sendiri
        if (user.PasswordResetToken != request.ResetPasswordDto.PasswordResetToken ||
            user.PasswordResetTokenExpiry < DateTime.UtcNow)
        {
            _logger.LogWarning("Invalid or expired token for {Email}", user.Email);
            // Demi keamanan: tetap return error yang sama untuk mencegah bocornya email pengguna
            throw new ValidationException("Invalid or expired reset token.");
        }

        // Ganti password baru (reset password)
        user.PasswordHash = _passwordService.HashPassword(request.ResetPasswordDto.NewPassword);
        user.UpdatedAt = DateTime.UtcNow;

        // Reset password reset token
        user.PasswordResetToken = null;
        user.PasswordResetTokenExpiry = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync();

        // Kirim email notifikasi berhasil
        await _emailService.SendPasswordChangedNotificationAsync(user.Email, user.Name ?? "User");

        _logger.LogInformation("Password reset successful for {Email}", user.Email);

        return ApiResponse<object>.SuccessResult();
    }
}