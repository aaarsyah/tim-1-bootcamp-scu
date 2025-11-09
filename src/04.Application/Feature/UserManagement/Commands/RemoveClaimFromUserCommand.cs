using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.UserManagement.Commands;

public class RemoveClaimFromUserCommand : IRequest<ApiResponse<object>>
{
    public Guid RefId { get; set; }
    public required string ClaimType { get; set; }
}
public class RemoveClaimFromUserCommandHandler : IRequestHandler<RemoveClaimFromUserCommand, ApiResponse<object>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RemoveClaimFromUserCommandHandler> _logger;
    public RemoveClaimFromUserCommandHandler(IUnitOfWork unitOfWork, ILogger<RemoveClaimFromUserCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public async Task<ApiResponse<object>> Handle(RemoveClaimFromUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserManager.GetUserByRefIdAsync(request.RefId);
        if (user == null)
        {
            throw new NotFoundException("User RefId", request.RefId);
        }
        var deleted = await _unitOfWork.UserManager.RemoveClaimFromUserAsync(user, request.ClaimType);
        await _unitOfWork.SaveChangesAsync();
        if (!deleted)
        {
            throw new ValidationException($"User doesn't have claim {request.ClaimType}");
        }

        _logger.LogInformation("Claim {ClaimType} removed from user {UserRefId}", request.ClaimType, user.RefId);

        return ApiResponse<object>.SuccessResult();
    }
}
