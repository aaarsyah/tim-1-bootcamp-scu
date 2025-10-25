using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using MyApp.WebAPI.Data;
using MyApp.WebAPI.Exceptions;
using MyApp.Shared.DTOs;
using MyApp.WebAPI.Models.Entities;
using MyApp.WebAPI.Models;

namespace MyApp.WebAPI.Services
{
    /// <summary>
    /// Invoice service implementation
    /// </summary>
    public class InvoiceService : IInvoiceService
    {
        private readonly AppleMusicDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<InvoiceService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public InvoiceService(AppleMusicDbContext context, IMapper mapper, ILogger<InvoiceService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<InvoiceDto>> GetAllInvoicesUserAsync(int userId)
        {
            var invoices = await _context.Invoices
                .Where(m => m.UserId == userId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<InvoiceDto>>(invoices);
        }

        /// <summary>
        /// Get all Invoice
        /// </summary>
        public async Task<IEnumerable<InvoiceDto>> GetAllInvoicesAsync()
        {
            // Misalnya kamu ingin ambil semua invoice
            var invoices = await _context.Invoices.ToListAsync(); // hapus .Include(c => c.Courses) jika Courses gak ada
            return _mapper.Map<IEnumerable<InvoiceDto>>(invoices);
        }


        /// <summary>
        /// Get Invoice by Id
        /// </summary>

        public async Task<InvoiceDto> GetInvoicesByIdAsync(int id)
        {
            var invoice = await _context.Invoices.FirstOrDefaultAsync(c => c.Id == id);
            if (invoice == null)
                throw new NotFoundException($"Invoice with Id {id} not found");

            return _mapper.Map<InvoiceDto>(invoice);
        }

        /// <summary>
        /// Get create Invoice
        /// </summary>

        public async Task<InvoiceDto> CreateInvoicesAsync(CreateInvoiceDto createInvoiceDto)
        {
            var invoice = _mapper.Map<Invoice>(createInvoiceDto);

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Invoice created: {RefCode} with ID: {Id}", invoice.RefCode, invoice.Id);

            return _mapper.Map<InvoiceDto>(invoice);
        }


        /// <summary>
        /// Check if Invoice exists
        /// </summary>
        public async Task<bool> InvoicesExistsAsync(int id)
        {
            return await _context.Invoices.AnyAsync(c => c.Id == id);
        }
    }
}