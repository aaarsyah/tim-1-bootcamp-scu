using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyApp.Base.Exceptions;
using MyApp.Domain.Models;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;
using MyApp.Application.Services;

namespace MyApp.Application.Feature.Authentications.Commands;

public class RegisterCommand : IRequest<ApiResponse<object>>
{
    public RegisterRequestDto RegisterDto { get; set; }
}
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ApiResponse<object>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RegisterCommandHandler> _logger;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;
    private readonly IPasswordService _passwordService;
    public RegisterCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<RegisterCommandHandler> logger,
        ITokenService tokenService,
        IEmailService emailService,
        IPasswordService passwordService)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _tokenService = tokenService;
        _emailService = emailService;
        _passwordService = passwordService;
    }
    public async Task<ApiResponse<object>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Cek apakah email sudah terdaftar
        var existingUser = await _unitOfWork.UserManager.GetUserByEmailAsync(request.RegisterDto.Email);
        if (existingUser != null)
        {
            throw new ValidationException("User with this email already exists");
        }

        _logger.LogInformation("User registration attempt for email: {Email}", request.RegisterDto.Email);
        var strategy = _unitOfWork.CreateExecutionStrategy();
        return await strategy.ExecuteAsync(async () =>
        {
            // ===== STEP 1 =====
            using var transaction = _unitOfWork.BeginTransaction();
            try
            {
                // Generate email confirmation token
                var confirmationToken = await _tokenService.GenerateEmailConfirmationTokenAsync();

                // Create new user
                var user = new User
                {
                    IsActive = true,
                    Name = request.RegisterDto.Name,
                    Email = request.RegisterDto.Email,
                    PasswordHash = _passwordService.HashPassword(request.RegisterDto.NewPassword),
                    EmailConfirmed = false,
                    EmailConfirmationToken = confirmationToken,
                    EmailConfirmationTokenExpiry = DateTime.UtcNow.AddHours(24)
                };

                await _unitOfWork.UserManager.CreateUserAsync(user);
                await _unitOfWork.SaveChangesAsync(); //Id di auto-generate setelah di save

                // Assign default role "User"

                var userRole = await _unitOfWork.UserManager.GetDefaultRoleAsync();
                if (userRole != null)
                {
                    await _unitOfWork.UserManager.AddRoleToUserAsync(user, userRole);
                }

                //// Add default claims
                //var userClaim = new UserClaim
                //{
                //    UserId = user.Id,
                //    ClaimType = "can_view_profile",
                //    ClaimValue = "true",
                //    CreatedAt = DateTime.UtcNow,
                //    UpdatedAt = DateTime.UtcNow
                //}; // TODO: Belum tahu claim mau diapakan

                // Save user role dan claims
                
                await _unitOfWork.SaveChangesAsync();

                // Kirim email konfirmasi
                var confirmationLink = $"https://localhost:7069/confirm-email?email={Uri.EscapeDataString(user.Email)}&token={Uri.EscapeDataString(confirmationToken)}";
                await _emailService.SendEmailConfirmationAsync(user.Email, user.Name, confirmationLink);

                _logger.LogInformation("User registration successful for email: {Email}", user.Email);

                // ===== STEP 9 =====
                // All operations succeeded, make permanent
                _unitOfWork.CommitTransaction(transaction);

                return ApiResponse<object>.SuccessResult();
            }
            catch (Exception)
            {
                // ===== ROLLBACK ON ERROR =====
                // Any exception -> undo all changes
                _unitOfWork.RollbackTransaction(transaction);
                _logger.LogError("User registration failed for email: {Email}. Transaction rolled back.", request.RegisterDto.Email);
                throw; // Re-throw to be handled by middleware
            }
        });
    }
}