using MyApp.Shared.DTOs;
using System.Net.Http.Headers;

namespace MyApp.BlazorUI.Services;

public interface IUserService
{
    Task<List<UserDto>> GetAllUsersAsync(AuthenticationHeaderValue authorization);
    Task<List<RoleDto>> GetAllRolesAsync(AuthenticationHeaderValue authorization);
    Task<bool> AddRoleToUserAsync(AuthenticationHeaderValue authorization, Guid userRefId, RoleRequestDto request);
    Task<bool> RemoveRoleFromUserAsync(AuthenticationHeaderValue authorization, Guid userRefId, RoleRequestDto request);
    Task<bool> SetClaimForUserAsync(AuthenticationHeaderValue authorization, Guid userRefId, ClaimDto request);
    Task<bool> RemoveClaimFromUserAsync(AuthenticationHeaderValue authorization, Guid userRefId, ClaimDto request);
    Task<bool> ActivateUserAsync(AuthenticationHeaderValue authorization, Guid userRefId);
    Task<bool> DeactivateUserAsync(AuthenticationHeaderValue authorization, Guid userRefId);
    Task<UserDto?> GetSelfUserAsync(AuthenticationHeaderValue authorization);
}
