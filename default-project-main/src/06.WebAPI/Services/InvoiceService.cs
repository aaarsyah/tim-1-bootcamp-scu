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
        /// <summary>
        /// Get Invoice-Invoice berdasarkan user id
        /// </summary>
        public async Task<IEnumerable<InvoiceDto>> GetAllInvoicesByUserIdAsync(int userId)
        {
            var invoices = await _context.Invoices
                .Where(m => m.UserId == userId)
                .Include(m => m.InvoiceDetails)
                .ToListAsync();
            return _mapper.Map<IEnumerable<InvoiceDto>>(invoices);
        }
        /// <summary>
        /// Get semua Invoice
        /// </summary>
        public async Task<IEnumerable<InvoiceDto>> GetAllInvoicesAsync()
        {
            // Misalnya kamu ingin ambil semua invoice
            var invoices = await _context.Invoices
                .Include(m => m.User)
                .Include(m => m.InvoiceDetails)
                .ToListAsync();

            return _mapper.Map<IEnumerable<InvoiceDto>>(invoices);
        }
        /// <summary>
        /// Get Invoice berdasarkan invoice id
        /// </summary>
        public async Task<InvoiceDto> GetInvoiceByIdAsync(int invoiceId)
        {
            var invoice = await _context.Invoices
                .Include(m => m.InvoiceDetails)
                .FirstOrDefaultAsync(c => c.Id == invoiceId);
            if (invoice == null)
            {
                throw new NotFoundException("Invoice Id", invoiceId);
            }
            return _mapper.Map<InvoiceDto>(invoice);
        }
        /// <summary>
        /// Get Invoice berdasarkan invoice id (hanya jika user id sesuai dengan invoice tersebut)
        /// </summary>
        public async Task<InvoiceDto> GetInvoiceByIdPersonalAsync(int userId, int invoiceId)
        {
            var invoice = await _context.Invoices
                .Include(m => m.InvoiceDetails)
                .FirstOrDefaultAsync(c => c.Id == invoiceId);
            if (invoice?.UserId != userId)
            {
                throw new NotFoundException("Invoice Id", invoiceId); //throw exception yang serupa untuk merahasiakan data
            }
            if (invoice == null)
            {
                throw new NotFoundException("Invoice Id", invoiceId);
            }
            return _mapper.Map<InvoiceDto>(invoice);
        }
    }
}