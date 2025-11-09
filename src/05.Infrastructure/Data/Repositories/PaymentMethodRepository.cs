using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Models;

namespace MyApp.Infrastructure.Data.Repositories;

public class PaymentMethodRepository : IPaymentMethodRepository
{
    private readonly AppleMusicDbContext _context;
    public PaymentMethodRepository(AppleMusicDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<PaymentMethod>> GetAllAsync()
    {
        return await _context.Set<PaymentMethod>()
            .ToListAsync();
    }
    public async Task<PaymentMethod?> GetByRefIdAsync(Guid refId)
    {
        return await _context.Set<PaymentMethod>()
            .FirstOrDefaultAsync(e => e.RefId == refId);
    }
    public async Task<PaymentMethod> CreateAsync(PaymentMethod entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        _context.Set<PaymentMethod>().Add(entity);
        return await Task.FromResult(entity);
    }
    public async Task<PaymentMethod> UpdateAsync(PaymentMethod entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _context.Set<PaymentMethod>().Update(entity); // akan mencari entity dengan id yang sama dan meng-update entity tersebut
        return await Task.FromResult(entity);
    }
    public async Task<bool> DeleteAsync(Guid refId)
    {
        var entity = await GetByRefIdAsync(refId);
        if (entity != null)
        {
            _context.Set<PaymentMethod>().Remove(entity);
            return true;
        }
        return false;
    }
}
