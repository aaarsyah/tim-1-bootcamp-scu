using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyApp.Base.Exceptions;
using MyApp.Application.Configuration;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;
using MyApp.Application.Services;

namespace MyApp.Application.Feature.Authentications.Commands;

public class LogoutCommand : IRequest<ApiResponse<object>>
{
    public Guid UserRefId { get; set; }
}
public class LogoutCommandHandler : IRequestHandler<LogoutCommand, ApiResponse<object>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<LogoutCommandHandler> _logger;
    public LogoutCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<LogoutCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public async Task<ApiResponse<object>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserManager.GetUserByRefIdAsync(request.UserRefId);
        if (user == null)
        {
            throw new ValidationException("User is invalid");
        }
        // Invalidate refresh token
        user.RefreshToken = null;
        user.RefreshTokenExpiry = DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("User logged out: {UserRefId}", user.RefId);
        return ApiResponse<object>.SuccessResult();
    }
}