using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApp.WebAPI.Data;
using MyApp.WebAPI.Exceptions;
using MyApp.WebAPI.Models.DTOs;

namespace MyApp.WebAPI.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly AppleMusicDbContext _context;
        private readonly IMapper _mapper;

        public InvoiceService(AppleMusicDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InvoiceResponseDto>> GetAllInvoicesAsync()
        {
            var invoices = await _context.Invoices
               .Include(i => i.User)
               .Include(i => i.PaymentMethod)
               .Include(i => i.InvoiceDetails)
                    .ThenInclude(d => d.Schedule) // kalau ada relasi lanjutan
                    .ToListAsync();


            return _mapper.Map<IEnumerable<InvoiceResponseDto>>(invoices);
        }

        public async Task<InvoiceResponseDto> GetInvoiceByIdAsync(int id)
        {
            var invoice = await _context.Invoices
                .Include(i => i.User)
                .Include(i => i.PaymentMethod)
                .Include(i => i.InvoiceDetails)
                    .ThenInclude(d => d.Schedule)
                        .ThenInclude(s => s.Course)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
                throw new NotFoundException($"Invoice with ID {id} not found");

            return _mapper.Map<InvoiceResponseDto>(invoice);
        }

        public async Task<IEnumerable<InvoiceResponseDto>> GetInvoicesByUserIdAsync(int userId)
        {
            var invoices = await _context.Invoices
                .Where(i => i.UserId == userId)
                .Include(i => i.User)
                .Include(i => i.PaymentMethod)
                .Include(i => i.InvoiceDetails)
                    .ThenInclude(d => d.Schedule)
                        .ThenInclude(s => s.Course)
                .ToListAsync();

            return _mapper.Map<IEnumerable<InvoiceResponseDto>>(invoices);
        }
    }
}
