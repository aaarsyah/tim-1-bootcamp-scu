using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Models;

namespace MyApp.Infrastructure.Data.Repositories;

public class MyClassRepository : IMyClassRepository
{
    private readonly AppleMusicDbContext _context;
    public MyClassRepository(AppleMusicDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<MyClass>> GetAllAsync()
    {
        return await _context.Set<MyClass>()
            .Include(e => e.Schedule)
                .ThenInclude(e2 => e2.Course)
                    .ThenInclude(e3 => e3.Category)
            .ToListAsync();
    }
    public async Task<IEnumerable<MyClass>?> GetAllByUserRefIdAsync(Guid userRefId)
    {
        var user = await _context.Set<User>()
            .FirstOrDefaultAsync(c => c.RefId == userRefId);
        if (user == null)
        {
            return null;
        }
        return await _context.Set<MyClass>()
            .Where(e => e.UserId == user.Id)
            .Include(e => e.Schedule)
                .ThenInclude(e2 => e2.Course)
                    .ThenInclude(e3 => e3.Category)
            .ToListAsync();
    }
    public async Task<MyClass?> GetByRefIdAsync(Guid refId)
    {
        return await _context.Set<MyClass>()
            .Include(e => e.Schedule)
                .ThenInclude(e2 => e2.Course)
                    .ThenInclude(e3 => e3.Category)
            .FirstOrDefaultAsync(e => e.RefId == refId);
    }
    public async Task<MyClass> CreateAsync(MyClass entity)
    {
        _context.Set<MyClass>().Add(entity);
        return await Task.FromResult(entity);
    }
    public async Task<bool> DeleteAsync(Guid refId)
    {
        var entity = await GetByRefIdAsync(refId);
        if (entity != null)
        {
            _context.Set<MyClass>().Remove(entity);
            return true;
        }
        return false;
    }
}
