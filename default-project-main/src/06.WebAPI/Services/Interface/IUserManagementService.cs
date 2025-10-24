using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using MyApp.WebAPI.Models.Entities;
using MyApp.Shared.DTOs;

namespace MyApp.WebAPI.Services
{
    public interface IUserManagementService
    {
        Task<UserDto?> GetUserProfileAsync(int userId);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<bool> AssignRoleToUserAsync(int userId, string roleName);
        Task<bool> RemoveRoleFromUserAsync(int userId, string roleName);
        Task<bool> AddClaimToUserAsync(int userId, string claimType, string claimValue);
        Task<bool> RemoveClaimFromUserAsync(int userId, string claimType, string claimValue);
        Task<bool> DeactivateUserAsync(int userId);
    }
}
