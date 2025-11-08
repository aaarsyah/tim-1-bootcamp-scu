using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Feature.CartItems.Commands;
using MyApp.Application.Feature.Categories.Commands;
using MyApp.Application.Feature.Categories.Queries;
using MyApp.Application.Feature.Schedules.Commands;
using MyApp.Application.Feature.Schedules.Queries;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Configuration;
using MyApp.Shared.DTOs;
using MyApp.WebAPI.Services;
using Org.BouncyCastle.Ocsp;
using System.Security.Claims;

namespace MyApp.WebAPI.Controllers;

/// <summary>
/// User Management Controller
/// Endpoint untuk Admin/Manager mengelola users:
/// - List users
/// - Get user detail
/// - Assign/Remove roles
/// - Add/Remove claims
/// - Deactivate users
/// 
/// Semua endpoints butuh authentication dan authorization
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize] // Semua endpoints butuh authentication
public class UserManagementController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UserManagementController> _logger;

    public UserManagementController(IMediator mediator, ILogger<UserManagementController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get All Users (Paginated)
    /// GET /api/usermanagement/users?page=1&pageSize=10
    /// Requires: Admin role
    /// </summary>
    [HttpGet("users")]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<UserDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IEnumerable<UserDto>>>> GetAllUsers()
    {
        var query = new GetAllUsersQuery { };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Get User by ID
    /// GET /api/usermanagement/users/{userId}
    /// Requires: Admin role or higher
    /// </summary>
    [HttpGet("users/{refId:Guid}")]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<UserDto>>> GetUser(Guid refId)
    {
        var query = new GetUserByRefIdQuery { RefId = refId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("roles")]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<RoleDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IEnumerable<RoleDto>>>> GetAllRoles()
    {
        var query = new GetAllRolesQuery { };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Assign Role to User
    /// POST /api/usermanagement/users/{userId}/roles
    /// Requires: Admin role
    /// </summary>
    [HttpPut("users/{refId:Guid}/roles/add")]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<object>>> AssignRole(Guid refId, [FromBody] RoleRequestDto request)
    {
        var command = new AddRoleToUserCommand { RefId = refId, RoleDto = request };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Remove Role from User
    /// DELETE /api/usermanagement/users/{userId}/roles/{roleName}
    /// Requires: Admin role
    /// </summary>
    [HttpPut("users/{refId:Guid}/roles/remove")]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<object>>> RemoveRole(Guid refId, [FromBody] RoleRequestDto request)
    {
        var command = new RemoveRoleFromUserCommand { RefId = refId, RoleDto = request };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Add Claim to User
    /// POST /api/usermanagement/users/{userId}/claims
    /// Requires: Admin role
    /// </summary>
    [HttpPut("users/{refId:Guid}/claims/add")]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<object>>> SetClaim(Guid refId, [FromBody] ClaimDto claim)
    {
        var command = new SetClaimForUserCommand { RefId = refId, ClaimType = claim.Type, ClaimValue = claim.Value };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Remove Claim from User
    /// DELETE /api/usermanagement/users/{userId}/claims
    /// Requires: Admin role
    /// </summary>
    [HttpPut("users/{refId:Guid}/claims/remove")]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<object>>> RemoveClaim(Guid refId, [FromBody] ClaimDto claim)
    {
        var command = new RemoveClaimFromUserCommand { RefId = refId, ClaimType = claim.Type };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Activate User
    /// DELETE /api/usermanagement/users/{userId}
    /// Requires: Admin role
    /// </summary>
    [HttpPut("users/{refId:Guid}/activate")]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<object>>> ActivateUser(Guid refId)
    {
        var command = new SetActiveForUserCommand { RefId = refId, isActive = true };
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    /// <summary>
    /// Deactivate User
    /// DELETE /api/usermanagement/users/{userId}
    /// Requires: Admin role
    /// </summary>
    [HttpPut("users/{refId:Guid}/deactivate")]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<object>>> DeactivateUser(Guid refId)
    {
        var command = new SetActiveForUserCommand { RefId = refId, isActive = false };
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpGet("users/me")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<UserDto>>> GetSelfUser()
    {
        var userClaimIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userClaimIdentifier == null || !Guid.TryParse(userClaimIdentifier.Value, out Guid userRefId))
        {
            throw new TokenInvalidException();
        }
        var command = new GetUserByRefIdQuery { RefId = userRefId };
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
