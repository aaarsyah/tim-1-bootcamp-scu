using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyApp.Base.Exceptions;
using MyApp.Application.Configuration;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;
using MyApp.Application.Services;

namespace MyApp.Application.Feature.Authentications.Commands;

public class ChangePasswordCommand : IRequest<ApiResponse<object>>
{
    public Guid UserRefId { get; set; }
    public ChangePasswordRequestDto ChangePasswordDto { get; set; }
}
public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ApiResponse<object>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ChangePasswordCommandHandler> _logger;
    private readonly IPasswordService _passwordService;
    public ChangePasswordCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<ChangePasswordCommandHandler> logger,
        IPasswordService passwordService)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _passwordService = passwordService;
    }
    public async Task<ApiResponse<object>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserManager.GetUserByRefIdAsync(request.UserRefId);
        if (user == null)
        {
            throw new ValidationException("User is invalid");
        }

        // Verify password
        if (!_passwordService.VerifyPassword(request.ChangePasswordDto.Password, user.PasswordHash))
        {
            _logger.LogWarning("Password change failed for user: {UserRefId}", user.RefId);
            throw new ValidationException("Invalid password.");
        }

        //var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        user.PasswordHash = _passwordService.HashPassword(request.ChangePasswordDto.NewPassword);
        user.UpdatedAt = DateTime.UtcNow;

        _logger.LogInformation("Password changed successfully for user: {UserId}", user.RefId);
        return ApiResponse<object>.SuccessResult();
    }
}