using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Models;
using MyApp.Infrastructure.Data.Repositories.Interfaces;

namespace MyApp.Infrastructure.Data.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly AppleMusicDbContext _context;
    public ScheduleRepository(AppleMusicDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Schedule>> GetAllAsync()
    {
        return await _context.Set<Schedule>()
            .Include(e => e.Course) // include Course supaya CourseName bisa diakses
            .ToListAsync();
    }
    public async Task<Schedule?> GetByRefIdAsync(Guid refId)
    {
        return await _context.Set<Schedule>()
            .Include(e => e.Course) // include Course supaya CourseName bisa diakses
            .FirstOrDefaultAsync(e => e.RefId == refId);
    }
    public async Task<Schedule> CreateAsync(Schedule entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        _context.Set<Schedule>().Add(entity);
        return await Task.FromResult(entity);
    }
    public async Task<bool> DeleteAsync(Guid refId)
    {
        var entity = await GetByRefIdAsync(refId);
        if (entity != null)
        {
            _context.Set<Schedule>().Remove(entity);
            return true;
        }
        return false;
    }
}
