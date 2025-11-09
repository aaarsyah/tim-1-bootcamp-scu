using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Models;
using MyApp.Infrastructure.Data.Repositories.Interfaces;

namespace MyApp.Infrastructure.Data.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly AppleMusicDbContext _context;
    public CourseRepository(AppleMusicDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _context.Set<Course>()
            .Include(e => e.Category)
            .Include(e => e.Schedules)
            .ToListAsync();
    }
    public async Task<IQueryable<Course>> GetAllAsQueryableAsync()
    {
        return await Task.FromResult(_context.Set<Course>()
            .Include(e => e.Category)
            .Include(e => e.Schedules)
            .AsQueryable());
    }
    public async Task<Course?> GetByRefIdAsync(Guid refId)
    {
        return await _context.Set<Course>()
            .Include(e => e.Category)
            .Include(e => e.Schedules) 
            .FirstOrDefaultAsync(e => e.RefId == refId);
    }
    public async Task<Course> CreateAsync(Course entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        _context.Set<Course>().Add(entity);
        return await Task.FromResult(entity);
    }
    public async Task<Course> UpdateAsync(Course entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _context.Set<Course>().Update(entity); // akan mencari entity dengan id yang sama dan meng-update entity tersebut
        return await Task.FromResult(entity);
    }
    public async Task<bool> DeleteAsync(Guid refId)
    {
        var entity = await GetByRefIdAsync(refId);
        if (entity != null)
        {
            _context.Set<Course>().Remove(entity);
            return true;
        }
        return false;
    }
}
