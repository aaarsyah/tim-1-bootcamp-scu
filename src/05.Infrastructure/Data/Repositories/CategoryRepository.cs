using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Models;
using MyApp.Infrastructure.Data.Repositories.Interfaces;

namespace MyApp.Infrastructure.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppleMusicDbContext _context;
    public CategoryRepository(AppleMusicDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Set<Category>()
            .Include(e => e.Courses)
            .ToListAsync();
    }
    public async Task<Category?> GetByRefIdAsync(Guid refId)
    {
        return await _context.Set<Category>()
            .Include(e => e.Courses)
            .FirstOrDefaultAsync(e => e.RefId == refId);
    }
    public async Task<Category> CreateAsync(Category entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        _context.Set<Category>().Add(entity);
        return await Task.FromResult(entity);
    }
    public async Task<Category> UpdateAsync(Category entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _context.Set<Category>().Update(entity); // akan mencari entity dengan id yang sama dan meng-update entity tersebut
        return await Task.FromResult(entity);
    }
    public async Task<bool> DeleteAsync(Guid refId)
    {
        var entity = await GetByRefIdAsync(refId);
        if (entity != null)
        {
            _context.Set<Category>().Remove(entity);
            return true;
        }
        return false;
    }
}
