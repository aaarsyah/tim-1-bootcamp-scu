using MyApp.Shared.DTOs;

namespace MyApp.WebAPI.Services;

/// <summary>
/// Invoice service interface
/// </summary>
public interface IInvoiceService
{
    Task<IEnumerable<InvoiceDto>> GetAllInvoicesByUserIdAsync(int userId);

    /// <summary>
    /// Get all Invoice
    /// </summary>
    Task<IEnumerable<InvoiceDto>> GetAllInvoicesAsync();

    /// <summary>
    /// Get Invoice by ID
    /// </summary>
    Task<InvoiceDto> GetInvoiceByIdAsync(int invoiceId);
    /// <summary>
    /// Get Invoice by ID
    /// </summary>
    Task<InvoiceDto> GetInvoiceByIdPersonalAsync(int userId, int invoiceId);
}