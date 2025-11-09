using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.UserManagement.Commands;

public class RemoveRoleFromUserCommand : IRequest<ApiResponse<object>>
{
    public Guid RefId { get; set; }
    public required RoleRequestDto RoleDto { get; set; }
}
public class RemoveRoleFromUserCommandHandler : IRequestHandler<RemoveRoleFromUserCommand, ApiResponse<object>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RemoveRoleFromUserCommandHandler> _logger;
    public RemoveRoleFromUserCommandHandler(IUnitOfWork unitOfWork, ILogger<RemoveRoleFromUserCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public async Task<ApiResponse<object>> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserManager.GetUserByRefIdAsync(request.RefId);
        if (user == null)
        {
            throw new NotFoundException("User RefId", request.RefId);
        }
        var role = await _unitOfWork.UserManager.GetRoleByNameAsync(request.RoleDto.RoleName);
        if (role == null)
        {
            _logger.LogWarning("Failed to assign role {RoleName} to user {UserRefId}", request.RoleDto.RoleName, request.RefId);
            throw new ValidationException($"Invalid role {request.RoleDto.RoleName}");
        }

        var deleted = await _unitOfWork.UserManager.RemoveRoleFromUserAsync(user, role);
        await _unitOfWork.SaveChangesAsync();
        if (!deleted)
        {
            throw new ValidationException($"User doesn't have role {request.RoleDto.RoleName}");
        }

        _logger.LogInformation("Role {RoleName} removed from user {UserId}", role.Name, user.RefId);

        return ApiResponse<object>.SuccessResult();
    }
}
