using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Configuration;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;
using MyApp.WebAPI.Services;

namespace MyApp.Application.Feature.Authentications.Commands;

public class ForgotPasswordCommand : IRequest<ApiResponse<object>>
{
    public ForgotPasswordRequestDto ForgotPasswordDto { get; set; }
}
public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, ApiResponse<object>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<ForgotPasswordCommandHandler> _logger;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;
    private readonly IPasswordService _passwordService;
    private readonly JwtSettings _jwtSettings;
    public ForgotPasswordCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<ForgotPasswordCommandHandler> logger,
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
    public async Task<ApiResponse<object>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Forgot password requested for email: {Email}", request.ForgotPasswordDto.Email);

        var user = await _unitOfWork.UserManager.GetUserByEmailAsync(request.ForgotPasswordDto.Email);

        //Menghindari Email Enumeration Attack (menghindari hacker mengetahui email mana yang valid)
        if (user == null)
        {
            _logger.LogWarning("Forgot password requested for non-existing email: {Email}", request.ForgotPasswordDto.Email);
            // Demi keamanan: tetap return sukses tanpa memberitahu apakah email valid
            return ApiResponse<object>.SuccessResult();
        }

        // Generate token reset password
        var token = await _tokenService.GeneratePasswordResetTokenAsync();
        user.PasswordResetToken = token;
        user.PasswordResetTokenExpiry = DateTime.UtcNow.AddHours(1); ; // Token valid for 1 hour

        await _unitOfWork.SaveChangesAsync();

        // Buat link reset (menuju frontend URL BlazorUI)
        var resetLink = $"https://localhost:7069/reset-password?email={Uri.EscapeDataString(user.Email)}&token={Uri.EscapeDataString(token)}";

        // Jika email terdaftar dalam database, Kirim email reset password
        await _emailService.SendPasswordResetAsync(user.Email, user.Name ?? string.Empty, resetLink);

        _logger.LogInformation("Password reset email sent to {Email}", user.Email);

        return ApiResponse<object>.SuccessResult();
    }
}