using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Models;

namespace MyApp.Infrastructure.Data.Repositories;

public class UserManagerRepository : IUserManagerRepository
{
    private readonly AppleMusicDbContext _context;
    public UserManagerRepository(AppleMusicDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _context.Set<User>()
            .Include(e => e.UserRoles)
                .ThenInclude(e2 => e2.Role)
            .Include(e => e.UserClaims)
            .ToListAsync();
    }
    
    public async Task<User?> GetUserByRefIdAsync(Guid userRefId)
    {
        return await _context.Set<User>()
            .Include(e => e.UserRoles)
                .ThenInclude(e2 => e2.Role)
            .Include(e => e.UserClaims)
            .FirstOrDefaultAsync(a => a.RefId == userRefId);
    }
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Set<User>()
            .Include(e => e.UserRoles)
                .ThenInclude(e2 => e2.Role)
            .Include(e => e.UserClaims)
            .FirstOrDefaultAsync(a => a.Email == NormalizeEmail(email));
    }
    private string NormalizeEmail(string email)
    {
        // TODO: Implement proper email normalization (some email provider such as gmail.com ignore + and .)
        // Refer to https://github.com/johno/normalize-email/blob/master/index.js for implementation
        return email.ToLower();
    }
    public async Task<User> CreateUserAsync(User userEntity)
    {
        userEntity.Email = NormalizeEmail(userEntity.Email);
        userEntity.CreatedAt = DateTime.UtcNow;
        userEntity.UpdatedAt = DateTime.UtcNow;
        _context.Set<User>().Add(userEntity);
        return await Task.FromResult(userEntity);
    }
    public async Task<IEnumerable<Role>> GetAllRolesAsync()
    {
        return await _context.Set<Role>()
            .ToListAsync();
    }
    public async Task<Role?> GetDefaultRoleAsync()
    {
        return await _context.Set<Role>()
            .FirstOrDefaultAsync(e => e.Id == 1 && e.Name == "User");
    }
    public async Task<Role?> GetRoleByNameAsync(string name)
    {
        return await _context.Set<Role>()
            .FirstOrDefaultAsync(e => e.Name == name);
    }
    public async Task<bool> AddRoleToUserAsync(User userEntity, Role roleEntity)
    {
        var entity = await _context.Set<UserRole>()
            .FirstOrDefaultAsync(a => a.UserId == userEntity.Id && a.RoleId == roleEntity.Id);
        if (entity != null)
        {
            return false; // role sudah ada
        }
        entity = new UserRole
        {
            UserId = userEntity.Id,
            RoleId = roleEntity.Id,
            CreatedAt = DateTime.UtcNow
        };
        _context.Set<UserRole>().Add(entity);
        return true;
    }
    public async Task<bool> RemoveRoleFromUserAsync(User userEntity, Role roleEntity)
    {
        var entity = await _context.Set<UserRole>()
            .FirstOrDefaultAsync(a => a.UserId == userEntity.Id && a.RoleId == roleEntity.Id);
        if (entity != null)
        {
            _context.Set<UserRole>().Remove(entity);
            return true;
        }
        return false;
    }
    public async Task<UserClaim> SetClaimForUserAsync(User userEntity, string claimType, string claimValue)
    {
        var entity = await _context.Set<UserClaim>()
            .FirstOrDefaultAsync(a => a.UserId == userEntity.Id && a.ClaimType == claimType);
        if (entity != null)
        {
            entity.ClaimValue = claimValue; // ubah claim value untuk claim type yang sudah ada
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Set<UserClaim>().Update(entity);
            return await Task.FromResult(entity);
        }
        entity = new UserClaim
        {
            UserId = userEntity.Id,
            ClaimType = claimType,
            ClaimValue = claimValue,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _context.Set<UserClaim>().Add(entity);
        return await Task.FromResult(entity);
    }
    public async Task<bool> RemoveClaimFromUserAsync(User userEntity, string claimType)
    {
        var entity = await _context.Set<UserClaim>()
            .FirstOrDefaultAsync(a => a.UserId == userEntity.Id && a.ClaimType == claimType);
        if (entity != null)
        {
            _context.Set<UserClaim>().Remove(entity);
            return true;
        }
        return false;
    }
    public async Task<bool> SetActiveForUserAsync(Guid userRefId, bool isActive)
    {
        var entity = await GetUserByRefIdAsync(userRefId);
        if (entity != null)
        {
            entity.IsActive = isActive;
            _context.Set<User>().Update(entity);
            return true;
        }
        return false;
    }
}
