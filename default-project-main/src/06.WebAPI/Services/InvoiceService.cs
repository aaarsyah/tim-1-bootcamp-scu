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
                .Include(m => m.InvoiceDetails)
                .ToListAsync();
            return _mapper.Map<IEnumerable<InvoiceDto>>(invoices);
        }

        //public async Task<IEnumerable<MyClassDto>> GetMyClassesByUserIdAsync(int userId)
        //{
        //    var myClasses = await _context.MyClasses
        //        .Where(m => m.UserId == userId)
        //        .Include(m => m.Schedule)
        //        .ThenInclude(s => s.Course)
        //        .ThenInclude(c => c.Category)
        //        .ToListAsync();
        //    return _mapper.Map<IEnumerable<MyClassDto>>(myClasses);
        //}

        /// <summary>
        /// Get all Invoice
        /// </summary>
        public async Task<IEnumerable<InvoiceDto>> GetAllInvoicesAsync()
        {
            // Misalnya kamu ingin ambil semua invoice
            var invoices = await _context.Invoices
                .Include(m => m.InvoiceDetails)
                .ToListAsync();

            return _mapper.Map<IEnumerable<InvoiceDto>>(invoices);
        }


        /// <summary>
        /// Get Invoice by Id
        /// </summary>

        public async Task<InvoiceDto> GetInvoicesByIdAsync(int id)
        {
            var invoice = await _context.Invoices
                .Include(m => m.InvoiceDetails)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (invoice == null)
                throw new NotFoundException($"Invoice with Id {id} not found");

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