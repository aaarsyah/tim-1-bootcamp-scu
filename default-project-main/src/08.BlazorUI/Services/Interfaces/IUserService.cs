using MyApp.Shared.DTOs;
using System.Net.Http.Headers;

namespace MyApp.BlazorUI.Services;

public interface IUserService
{
    Task<List<UserDto>> GetAllUsersAsync(AuthenticationHeaderValue authorization);
    Task<List<RoleDto>> GetAllRolesAsync(AuthenticationHeaderValue authorization);
    Task<bool> AddRoleToUserAsync(AuthenticationHeaderValue authorization, int userId, RoleRequestDto request);
    Task<bool> RemoveRoleFromUserAsync(AuthenticationHeaderValue authorization, int userId, RoleRequestDto request);
    Task<bool> SetClaimForUserAsync(AuthenticationHeaderValue authorization, int userId, ClaimDto request);
    Task<bool> RemoveClaimFromUserAsync(AuthenticationHeaderValue authorization, int userId, ClaimDto request);
    Task<bool> ActivateUserAsync(AuthenticationHeaderValue authorization, int userId);
    Task<bool> DeactivateUserAsync(AuthenticationHeaderValue authorization, int userId);
    Task<UserDto?> GetSelfUserAsync(AuthenticationHeaderValue authorization);
}
