using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Models;
using MyApp.Infrastructure.Data.Repositories.Interfaces;

namespace MyApp.Infrastructure.Data.Repositories;

public class CartItemRepository : ICartItemRepository
{
    private readonly AppleMusicDbContext _context;
    public CartItemRepository(AppleMusicDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<CartItem>> GetAllAsync()
    {
        return await _context.Set<CartItem>()
            .Include(e => e.Schedule)
                .ThenInclude(e2 => e2.Course) // chaining ke Course untuk mendapatkan courseName
            .ToListAsync();
    }
    public async Task<IEnumerable<CartItem>?> GetAllByUserRefIdAsync(Guid userRefId)
    {
        var user = await _context.Set<User>()
            .FirstOrDefaultAsync(e => e.RefId == userRefId);
        if (user == null)
        {
            return null;
        }
        return await _context.Set<CartItem>()
            .Where(e => e.UserId == user.Id)
            .Include(e => e.Schedule)
                .ThenInclude(e2 => e2.Course)
            .ToListAsync();
    }
    public async Task<IEnumerable<CartItem>> GetAllByRefIdsAsync(List<Guid> refIds)
    {
        return await _context.Set<CartItem>()
            .Where(e => refIds.Contains(e.RefId))
            .Include(e => e.Schedule)
                .ThenInclude(e2 => e2.Course) // chaining ke Course untuk mendapatkan courseName
            .ToListAsync();
    }
    public async Task<CartItem?> GetByRefIdAsync(Guid refId)
    {
        return await _context.Set<CartItem>()
            .Include(e => e.Schedule)
                .ThenInclude(e2 => e2.Course) // chaining ke Course untuk mendapatkan courseName
            .FirstOrDefaultAsync(e => e.RefId == refId);
    }
    public async Task<CartItem> CreateAsync(CartItem entity)
    {
        _context.Set<CartItem>().Add(entity);
        return await Task.FromResult(entity);
    }
    public async Task<bool> DeleteAsync(Guid refId)
    {
        var entity = await _context.Set<CartItem>()
            .FirstOrDefaultAsync(e => e.RefId == refId);
        if (entity != null)
        {
            _context.Set<CartItem>().Remove(entity);
            return true;
        }
        return false;
    }
    public async Task DeleteEntitiesAsync(List<CartItem> entities)
    {
        await Task.Run(() => { _context.CartItems.RemoveRange(entities); });
    }
}
