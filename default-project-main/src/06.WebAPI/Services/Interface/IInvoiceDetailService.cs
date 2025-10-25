using Microsoft.AspNetCore.Mvc;
using MyApp.WebAPI.Models;
using MyApp.Shared.DTOs;
using MyApp.WebAPI.Models.Entities;

namespace MyApp.WebAPI.Services
{
    /// <summary>
    /// Invoice service interface
    /// </summary>
    public interface IInvoiceDetailService
    {
        Task<List<InvoiceDetailDto>> GetInvoiceDetailsByInvoiceIdPersonalAsync(int userId, int invoiceId);

        /// <summary>
        /// Get all Invoice
        /// </summary>
        Task<IEnumerable<InvoiceDetailDto>> GetAllInvoicesDetailAsync();

        /// <summary>
        /// Get Invoice by ID
        /// </summary>
        Task<InvoiceDetailDto> GetInvoicesDetailByIdAsync(int id);

        /// <summary>
        /// Create new Invoice
        /// </summary>
        Task<InvoiceDetailDto> CreateInvoicesDetailAsync(CreateInvoiceDetailDto createInvoiceDetailDto);

        /// <summary>
        /// Check if Invoice exists
        /// </summary>
        Task<bool> InvoicesDetailExistsAsync(int id);

    }
}