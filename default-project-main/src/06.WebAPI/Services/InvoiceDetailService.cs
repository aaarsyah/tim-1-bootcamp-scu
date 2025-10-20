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
    public class InvoiceDetailService : IInvoiceDetailService
    {
        private readonly AppleMusicDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<InvoiceDetailService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public InvoiceDetailService(AppleMusicDbContext context, IMapper mapper, ILogger<InvoiceDetailService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all Invoice
        /// </summary>
        public async Task<IEnumerable<InvoiceDetailDto>> GetAllInvoicesDetailAsync()
        {
            // Misalnya kamu ingin ambil semua invoice
            var invoicesDetail = await _context.InvoiceDetails.ToListAsync(); // hapus .Include(c => c.Courses) jika Courses gak ada
            return _mapper.Map<IEnumerable<InvoiceDetailDto>>(invoicesDetail);
        }

        public async Task<InvoiceDetailDto> GetInvoicesDetailByIdAsync(int id)
        {
            var invoiceDetail = await _context.InvoiceDetails.FirstOrDefaultAsync(c => c.Id == id);
            if (invoiceDetail == null)
                throw new NotFoundException($"InvoiceDetail with Id {id} not found");

            return _mapper.Map<InvoiceDetailDto>(invoiceDetail);
        }

        public async Task<InvoiceDetailDto> CreateInvoicesDetailAsync(CreateInvoiceDetailDto createInvoiceDetailDto)
        {
            var invoiceDetail = _mapper.Map<InvoiceDetail>(createInvoiceDetailDto);

            _context.InvoiceDetails.Add(invoiceDetail);
            await _context.SaveChangesAsync();

            _logger.LogInformation("InvoiceDetail created: {RefCode} with ID: {Id}", invoiceDetail.RefCode, invoiceDetail.Id);

            return _mapper.Map<InvoiceDetailDto>(invoiceDetail);
        }


        /// <summary>
        /// Check if Invoice exists
        /// </summary>
        public async Task<bool> InvoicesDetailExistsAsync(int id)
        {
            return await _context.InvoiceDetails.AnyAsync(c => c.Id == id);
        }
    }
}