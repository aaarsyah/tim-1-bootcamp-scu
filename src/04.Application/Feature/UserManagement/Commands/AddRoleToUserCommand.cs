using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.UserManagement.Commands;

public class AddRoleToUserCommand : IRequest<ApiResponse<object>>
{
    public Guid RefId { get; set; }
    public RoleRequestDto RoleDto { get; set; } = null!;
}
public class AddRoleToUserCommandHandler : IRequestHandler<AddRoleToUserCommand, ApiResponse<object>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddRoleToUserCommandHandler> _logger;
    public AddRoleToUserCommandHandler(IUnitOfWork unitOfWork, ILogger<AddRoleToUserCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public async Task<ApiResponse<object>> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
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
        
        var added = await _unitOfWork.UserManager.AddRoleToUserAsync(user, role);
        await _unitOfWork.SaveChangesAsync();
        if (!added)
        {
            throw new ValidationException($"User already has role {request.RoleDto.RoleName}");
        }

        _logger.LogInformation("Role {RoleName} added to user {UserId}", role.Name, user.RefId);

        return ApiResponse<object>.SuccessResult();
    }
}
