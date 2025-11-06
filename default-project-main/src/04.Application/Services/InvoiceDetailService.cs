using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;
using MyApp.Base.Exceptions;
using MyApp.Shared.DTOs;
using MyApp.Domain.Models;
using Microsoft.Extensions.Logging;

namespace MyApp.WebAPI.Services;

/// <summary>
/// Invoice service implementation
/// </summary>
public class InvoiceDetailService : IInvoiceDetailService
{
    private readonly AppleMusicDbContext _context;
    private readonly IMapper _mapper;
    //private readonly ILogger<InvoiceDetailService> _logger;
    /// <summary>
    /// Constructor
    /// </summary>
    public InvoiceDetailService(AppleMusicDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        //_logger = logger;
    }
    /// <summary>
    /// Get InvoiceDetail-InvoiceDetail berdasarkan invoice id (hanya jika user id sesuai dengan invoice tersebut)
    /// </summary>
    public async Task<IEnumerable<InvoiceDetailDto>> GetInvoiceDetailsByInvoiceIdAsync(int invoiceId)
    {
        var invoice = await _context.Invoices
            .FirstOrDefaultAsync(d => d.Id == invoiceId);
        if (invoice == null)
        {
            throw new NotFoundException("Invoice Id", invoiceId);
        }
        var invoicesDetail = await _context.InvoiceDetails
            .Where(d => d.InvoiceId == invoiceId)
            .ToListAsync(); 
        return _mapper.Map<IEnumerable<InvoiceDetailDto>>(invoicesDetail);
    }
    /// <summary>
    /// Get InvoiceDetail-InvoiceDetail berdasarkan invoice id (hanya jika user id sesuai dengan invoice tersebut)
    /// </summary>
    public async Task<IEnumerable<InvoiceDetailDto>> GetInvoiceDetailsByInvoiceIdPersonalAsync(int userId, int invoiceId)
    {
        var invoice = await _context.Invoices
            .FirstOrDefaultAsync(d => d.Id == invoiceId);
        if (invoice?.UserId != userId)
        {
            throw new NotFoundException("Invoice Id", invoiceId); //throw exception yang serupa untuk merahasiakan data
        }
        if (invoice == null)
        {
            throw new NotFoundException("Invoice Id", invoiceId);
        }
        var invoiceDetails = await _context.InvoiceDetails
            .Where(d => d.InvoiceId == invoiceId)
            .ToListAsync();
        return _mapper.Map<IEnumerable<InvoiceDetailDto>>(invoiceDetails);
    }
}