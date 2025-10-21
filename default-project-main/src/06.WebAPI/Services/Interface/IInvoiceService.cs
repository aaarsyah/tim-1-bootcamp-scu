using Microsoft.AspNetCore.Mvc;
using MyApp.WebAPI.Models;
using MyApp.Shared.DTOs;
using MyApp.WebAPI.Models.Entities;

namespace MyApp.WebAPI.Services
{
    /// <summary>
    /// Invoice service interface
    /// </summary>
    public interface IInvoiceService
    {
        Task<IEnumerable<InvoiceDto>> GetAllInvoicesUserAsync(int userId);

        /// <summary>
        /// Get all Invoice
        /// </summary>
        Task<IEnumerable<InvoiceDto>> GetAllInvoicesAsync();

        /// <summary>
        /// Get Invoice by ID
        /// </summary>
        Task<InvoiceDto> GetInvoicesByIdAsync(int id);

        /// <summary>
        /// Create new Invoice
        /// </summary>
        Task<InvoiceDto> CreateInvoicesAsync(CreateInvoiceDto createInvoiceDto);

        /// <summary>
        /// Check if Invoice exists
        /// </summary>
        Task<bool> InvoicesExistsAsync(int id);
        
    }
}