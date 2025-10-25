using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyApp.Shared.DTOs;
using MyApp.WebAPI.Configuration;
using MyApp.WebAPI.Data;
using MyApp.WebAPI.Exceptions;
using MyApp.WebAPI.Models.Entities;

namespace MyApp.WebAPI.Services
{
    /// <summary>
    /// User Management Service
    /// Menangani operasi manajemen user: roles, claims, user list
    /// Hanya accessible oleh Admin/Manager
    /// </summary>
    public class UserManagementService : IUserManagementService
    {
        private readonly AppleMusicDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UserManagementService> _logger;

        public UserManagementService(
            AppleMusicDbContext context,
            IMapper mapper,
            ILogger<UserManagementService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDto?> GetUserProfileAsync(int userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(a => a.Id == userId);
            if (user == null)
            {
                throw new NotFoundException($"UserId {userId} not found");
            }
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            //var users = await _context.Users
            //    .Where(u => u.EmailConfirmed) // TODO: Perlukah user yang tidak aktif ditampilkan semua?
            //    .OrderBy(u => u.CreatedAt)
            //    .Skip((page - 1) * pageSize)
            //    .Take(pageSize)
            //    .ToListAsync();

            var users = await _context.Users
                .Include(c => c.UserRoles)
                    .ThenInclude(s => s.Role)
                .Include(c => c.UserClaims)
                .ToListAsync();

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            var roles = await _context.Roles
                .ToListAsync();

            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public async Task<bool> AddRoleToUserAsync(int userId, string roleName)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(a => a.Id == userId);
            if (user == null)
            {
                throw new NotFoundException($"UserId {userId} not found");
            }
            //cek apakah ada role dengan nama itu
            var role = await _context.Roles
                .FirstOrDefaultAsync(a => a.Name == roleName);
            if (role == null)
            {
                _logger.LogWarning("Failed to assign role {RoleName} to user {UserId}", roleName, userId);
                throw new NotFoundException($"Role {roleName} not found");
            }
            //cek apakah role *tidak* ada di user
            var userRole = await _context.UserRoles
                .FirstOrDefaultAsync(a => a.UserId == user.Id && a.RoleId == role.Id);
            if (userRole != null)
            {
                _logger.LogWarning("Failed to assign role {RoleName} to user {UserId}", roleName, userId);
                throw new ValidationException($"User already has role {roleName}");
            }

            _context.UserRoles.Add(new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id,
            });
            await _context.SaveChangesAsync();

            _logger.LogInformation("Role {RoleName} added to user {UserId}", roleName, userId);
            return true;
        }

        public async Task<bool> RemoveRoleFromUserAsync(int userId, string roleName)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(a => a.Id == userId);
            if (user == null)
            {
                throw new NotFoundException($"UserId {userId} not found");
            }
            //cek apakah ada role dengan nama itu
            var role = await _context.Roles
                .FirstOrDefaultAsync(a => a.Name == roleName);
            if (role == null)
            {
                _logger.LogWarning("Failed to remove role {RoleName} from user {UserId}", roleName, userId);
                throw new ValidationException($"Invalid role {roleName}");
            }
            //cek apakah role ada di user
            var userRole = await _context.UserRoles
                .FirstOrDefaultAsync(a => a.UserId == user.Id && a.RoleId == role.Id);
            if (userRole == null)
            {
                _logger.LogWarning("Failed to remove role {RoleName} from user {UserId}", roleName, userId);
                throw new NotFoundException($"Role {roleName} not found");
            }
            
            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Role {RoleName} removed from user {UserId}", roleName, userId);
            return true;
        }

        public async Task<bool> SetClaimForUserAsync(int userId, string claimType, string claimValue)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(a => a.Id == userId);
            if (user == null)
            {
                throw new NotFoundException($"UserId {userId} not found");
            }
            //cek apakah claim *tidak* ada di user
            var userClaim = await _context.UserClaims
                .FirstOrDefaultAsync(a => a.UserId == user.Id && a.ClaimType == claimType);
            if (userClaim != null)
            {
                userClaim.ClaimValue = claimValue;
            }
            else
            {
                _context.UserClaims.Add(new UserClaim
                {
                    UserId = user.Id,
                    ClaimType = claimType,
                    ClaimValue = claimValue
                });
            }
            await _context.SaveChangesAsync();

            _logger.LogInformation("Claim {ClaimType} set to {ClaimValue} for user {UserId}", claimType, claimValue, userId);
            return true;

            
        }

        public async Task<bool> RemoveClaimFromUserAsync(int userId, string claimType)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(a => a.Id == userId);
            if (user == null)
            {
                throw new NotFoundException($"UserId {userId} not found");
            }
            //cek apakah role ada di user
            var userClaim = await _context.UserClaims
                .FirstOrDefaultAsync(a => a.UserId == user.Id && a.ClaimType == claimType);
            if (userClaim == null)
            {
                _logger.LogWarning("Failed to remove claim {ClaimType} from user {UserId}", claimType, userId);
                throw new NotFoundException($"Claim {claimType} not found");
            }
            _context.UserClaims.Remove(userClaim);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Claim {ClaimType} removed from user {UserId}", claimType, userId);
            return true;
        }
        public async Task<bool> ActivateUserAsync(int userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(a => a.Id == userId);
            if (user == null)
            {
                _logger.LogWarning("Failed to activate user {UserId}", userId);
                throw new NotFoundException($"UserId {userId} not found");
            }

            user.IsActive = true;

            await _context.SaveChangesAsync();

            _logger.LogInformation("User {UserId} activated", userId);
            return true;
        }
        public async Task<bool> DeactivateUserAsync(int userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(a => a.Id == userId);
            if (user == null)
            {
                _logger.LogWarning("Failed to deactivate user {UserId}", userId);
                throw new NotFoundException($"UserId {userId} not found");
            }

            user.IsActive = false;
            user.RefreshToken = null; //also invalidate refresh token
            user.RefreshTokenExpiry = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _logger.LogInformation("User {UserId} deactivated", userId);
            return true;
        }
    }
}
