using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Models;
using MyApp.Infrastructure.Data.Repositories.Interfaces;

namespace MyApp.Infrastructure.Data.Repositories;

public class InvoiceManagerRepository : IInvoiceManagerRepository
{
    private readonly AppleMusicDbContext _context;
    public InvoiceManagerRepository(AppleMusicDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Invoice>> GetAllAsync()
    {
        return await _context.Set<Invoice>()
            .Include(e => e.User)
            .Include(e => e.InvoiceDetails)
            .ToListAsync();
    }
    public async Task<IEnumerable<Invoice>?> GetAllByUserRefIdAsync(Guid userRefId)
    {
        var user = await _context.Set<User>()
            .FirstOrDefaultAsync(e => e.RefId == userRefId);
        if (user == null)
        {
            return null;
        }
        return await _context.Set<Invoice>()
            .Where(e => e.UserId == user.Id)
            .Include(e => e.User)
            .Include(e => e.InvoiceDetails)
            .ToListAsync();
    }
    public async Task<Invoice?> GetByRefIdAsync(Guid invoiceRefId)
    {
        return await _context.Set<Invoice>()
            .Include(e => e.User)
            .Include(e => e.InvoiceDetails)
            .FirstOrDefaultAsync(e => e.RefId == invoiceRefId);
    }
    public async Task<Invoice> CreateAsync(Invoice entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        _context.Set<Invoice>().Add(entity);
        return await Task.FromResult(entity);
    }
    public async Task<IEnumerable<InvoiceDetail>?> GetDetailsByRefIdAsync(Guid invoiceRefId)
    {
        var invoice = await _context.Set<Invoice>()
            .FirstOrDefaultAsync(e => e.RefId == invoiceRefId);
        if (invoice == null)
        {
            return null;
        }
        return await _context.Set<InvoiceDetail>()
            .Where(e => e.InvoiceId == invoice.Id)
            .ToListAsync();
    }
    public async Task<InvoiceDetail> CreateDetailAsync(InvoiceDetail entity)
    {
        _context.Set<InvoiceDetail>().Add(entity);
        return await Task.FromResult(entity);
    }
}
