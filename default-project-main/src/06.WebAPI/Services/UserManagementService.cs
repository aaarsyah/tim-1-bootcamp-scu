using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApp.Shared.DTOs;
using MyApp.WebAPI.Data;
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
            if (user == null) return null;
            //var roles = await _userManager.GetRolesAsync(user);
            var roles = await _context.UserRoles
                .Where(a => a.UserId == userId)
                .Include(a => a.Role)
                .Select(a => a.Role.Name)
                .ToListAsync();
            //var claims = await _userManager.GetClaimsAsync(user);
            var claims = await _context.UserClaims
                .Where(a => a.UserId == userId)
                .Select(a => new ClaimDto { Type = a.ClaimType, Value = a.ClaimValue })
                .ToListAsync();

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Roles = roles,
                Claims = claims
            };
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

            //var userProfiles = new List<UserDto>();

            //foreach (var user in users)
            //{
            //    //var roles = await _userManager.GetRolesAsync(user);
            //    var roles = await _context.UserRoles
            //        .Where(a => a.UserId == user.Id)
            //        .Include(a => a.Role)
            //        .Select(a => a.Role.Name)
            //        .ToListAsync();
            //    //var claims = await _userManager.GetClaimsAsync(user);
            //    var claims = await _context.UserClaims
            //        .Where(a => a.UserId == user.Id)
            //        .Select(a => new ClaimDto { Type = a.ClaimType, Value = a.ClaimValue })
            //        .ToListAsync();

            //    userProfiles.Add(new UserDto
            //    {
            //        Id = user.Id,
            //        Name = user.Name ?? string.Empty,
            //        Email = user.Email ?? string.Empty,
            //        Roles = roles,
            //        Claims = claims
            //    });
            //}

            //return userProfiles;
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<bool> AssignRoleToUserAsync(int userId, string roleName)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(a => a.Id == userId);
            if (user == null) return false;

            //var roleExists = await _roleManager.RoleExistsAsync(roleName);
            //cek apakah ada role dengan nama itu
            var role = await _context.Roles
                .FirstOrDefaultAsync(a => a.Name == roleName);
            if (role == null)
            {
                _logger.LogWarning("Failed to assign role {RoleName} to user {UserId}", roleName, userId);
                return false;
            }
            //cek apakah role *tidak* ada di user
            var userRole = await _context.UserRoles
                .FirstOrDefaultAsync(a => a.UserId == user.Id && a.RoleId == role.Id);
            if (userRole != null)
            {
                _logger.LogWarning("Failed to remove role {RoleName} from user {UserId}", roleName, userId);
                return false;
            }

            //var result = await _userManager.AddToRoleAsync(user, roleName);
            _context.UserRoles.Add(new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id,
            });
            await _context.SaveChangesAsync();


            return true;

            
        }

        public async Task<bool> RemoveRoleFromUserAsync(int userId, string roleName)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(a => a.Id == userId);
            if (user == null) return false;

            //cek apakah ada role dengan nama itu
            var role = await _context.Roles
                .FirstOrDefaultAsync(a => a.Name == roleName);
            if (role == null)
            {
                _logger.LogWarning("Failed to assign role {RoleName} to user {UserId}", roleName, userId);
                return false;
            }
            //cek apakah role ada di user
            var userRole = await _context.UserRoles
                .FirstOrDefaultAsync(a => a.UserId == user.Id && a.RoleId == role.Id);
            if (userRole == null)
            {
                _logger.LogWarning("Failed to remove role {RoleName} from user {UserId}", roleName, userId);
                return false;
            }
            //var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Role {RoleName} removed from user {UserId}", roleName, userId);
            return true;
        }

        public async Task<bool> AddClaimToUserAsync(int userId, string claimType, string claimValue)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(a => a.Id == userId);
            if (user == null) return false;

            //cek apakah claim *tidak* ada di user
            var userClaim = await _context.UserClaims
                .FirstOrDefaultAsync(a => a.UserId == user.Id && a.ClaimType == claimType);
            if (userClaim != null)
            {
                _logger.LogWarning("Failed to add claim {ClaimType}:{ClaimValue} to user {UserId}", claimType, claimValue, userId);
                return false;
            }

            //var claim = new Claim(claimType, claimValue);
            //var result = await _userManager.AddClaimAsync(user, claim);

            _context.UserClaims.Add(new UserClaim
            {
                UserId = user.Id,
                ClaimType = claimType,
                ClaimValue = claimValue
            });
            await _context.SaveChangesAsync();

            _logger.LogInformation("Claim {ClaimType}:{ClaimValue} added to user {UserId}", claimType, claimValue, userId);
            return true;

            
        }

        public async Task<bool> RemoveClaimFromUserAsync(int userId, string claimType, string claimValue)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(a => a.Id == userId);
            if (user == null) return false;

            //cek apakah role ada di user
            var userClaim = await _context.UserClaims
                .FirstOrDefaultAsync(a => a.UserId == user.Id && a.ClaimType == claimType);
            if (userClaim == null)
            {
                _logger.LogWarning("Failed to remove claim {ClaimType}:{ClaimValue} from user {UserId}", claimType, claimValue, userId);
                return false;
            }
            //var result = await _userManager.RemoveClaimAsync(user, claim);
            _context.UserClaims.Remove(userClaim);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Claim {ClaimType}:{ClaimValue} removed from user {UserId}", claimType, claimValue, userId);
            return true;
        }

        public async Task<bool> DeactivateUserAsync(int userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(a => a.Id == userId);
            if (user == null)
            {
                _logger.LogWarning("Failed to deactivate user {UserId}", userId);
                return false;
            }

            user.EmailConfirmed = false;
            user.RefreshToken = null;
            user.RefreshTokenExpiry = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _logger.LogInformation("User {UserId} deactivated", userId);
            return true;
        }
    }
}
