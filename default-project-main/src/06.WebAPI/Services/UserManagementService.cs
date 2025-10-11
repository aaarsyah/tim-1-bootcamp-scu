using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using MyApp.WebAPI.Models.Entities;
using MyApp.WebAPI.Models.DTOs;

namespace MyApp.WebAPI.Services
{
    /// <summary>
    /// User Management Service
    /// Menangani operasi manajemen user: roles, claims, user list
    /// Hanya accessible oleh Admin/Manager
    /// </summary>
    public class UserManagementService : IUserManagementService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<UserManagementService> _logger;

        public UserManagementService(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            ILogger<UserManagementService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<UserDto?> GetUserProfileAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);
            var claims = await _userManager.GetClaimsAsync(user);

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email ?? string.Empty,
                Roles = roles.ToList(),
                Claims = claims.Select(c => new ClaimDto { Type = c.Type, Value = c.Value }).ToList()
            };
        }

        public async Task<List<UserDto>> GetAllUsersAsync(int page = 1, int pageSize = 10)
        {
            var users = await _userManager.Users
                .Where(u => u.IsActive)
                .OrderBy(u => u.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var userProfiles = new List<UserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var claims = await _userManager.GetClaimsAsync(user);

                userProfiles.Add(new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email ?? string.Empty,
                    Roles = roles.ToList(),
                    Claims = claims.Select(c => new ClaimDto { Type = c.Type, Value = c.Value }).ToList()
                });
            }

            return userProfiles;
        }

        public async Task<bool> AssignRoleToUserAsync(int userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return false;

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists) return false;

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                _logger.LogInformation("Role {RoleName} assigned to user {UserId}", roleName, userId);
                return true;
            }

            _logger.LogWarning("Failed to assign role {RoleName} to user {UserId}", roleName, userId);
            return false;
        }

        public async Task<bool> RemoveRoleFromUserAsync(int userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return false;

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                _logger.LogInformation("Role {RoleName} removed from user {UserId}", roleName, userId);
                return true;
            }

            _logger.LogWarning("Failed to remove role {RoleName} from user {UserId}", roleName, userId);
            return false;
        }

        public async Task<bool> AddClaimToUserAsync(int userId, string claimType, string claimValue)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return false;

            var claim = new Claim(claimType, claimValue);
            var result = await _userManager.AddClaimAsync(user, claim);
            
            if (result.Succeeded)
            {
                _logger.LogInformation("Claim {ClaimType}:{ClaimValue} added to user {UserId}", claimType, claimValue, userId);
                return true;
            }

            _logger.LogWarning("Failed to add claim {ClaimType}:{ClaimValue} to user {UserId}", claimType, claimValue, userId);
            return false;
        }

        public async Task<bool> RemoveClaimFromUserAsync(int userId, string claimType, string claimValue)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return false;

            var claim = new Claim(claimType, claimValue);
            var result = await _userManager.RemoveClaimAsync(user, claim);
            
            if (result.Succeeded)
            {
                _logger.LogInformation("Claim {ClaimType}:{ClaimValue} removed from user {UserId}", claimType, claimValue, userId);
                return true;
            }

            _logger.LogWarning("Failed to remove claim {ClaimType}:{ClaimValue} from user {UserId}", claimType, claimValue, userId);
            return false;
        }

        public async Task<bool> DeactivateUserAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return false;

            user.IsActive = false;
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                _logger.LogInformation("User {UserId} deactivated", userId);
                return true;
            }

            _logger.LogWarning("Failed to deactivate user {UserId}", userId);
            return false;
        }
    }
}
