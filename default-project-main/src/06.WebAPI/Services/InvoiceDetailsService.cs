using Microsoft.EntityFrameworkCore;
using MyApp.WebAPI.Data;
using MyApp.WebAPI.Models.Entities;
using MyApp.WebAPI.Models.Dtos;
using MyApp.WebAPI.Services.Interfaces;
using AutoMapper;

namespace MyApp.WebAPI.Services
{
    public class InvoiceDetailsService : IInvoiceDetailsService
    {
        private readonly AppleMusicDbContext _context;
        private readonly IMapper _mapper;

        public InvoiceDetailsService(AppleMusicDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InvoiceDetailsResponseDto>> GetAllAsync()
        {
            var details = await _context.InvoiceDetails
                .Include(d => d.Schedule)
                .Include(d => d.Invoice)
                .ToListAsync();

            return _mapper.Map<List<InvoiceDetailsResponseDto>>(details);
        }

        public async Task<InvoiceDetailsResponseDto?> GetByIdAsync(int id)
        {
            var details = await _context.InvoiceDetails
                .Include(d => d.Schedule)
                .Include(d => d.Invoice)
                .FirstOrDefaultAsync(d => d.Id == id);

            return details == null ? null : _mapper.Map<InvoiceDetailsResponseDto>(details);
        }

        public async Task<InvoiceDetails?> CreateAsync(InvoiceDetails invoiceDetails)
        {
            _context.InvoiceDetails.Add(invoiceDetails);
            await _context.SaveChangesAsync();
            return invoiceDetails;
        }

        public async Task<bool> UpdateAsync(int id, InvoiceDetails invoiceDetails)
        {
            var existing = await _context.InvoiceDetails.FindAsync(id);
            if (existing == null)
                return false;

            _context.Entry(existing).CurrentValues.SetValues(invoiceDetails);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var details = await _context.InvoiceDetails.FindAsync(id);
            if (details == null)
                return false;

            _context.InvoiceDetails.Remove(details);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
