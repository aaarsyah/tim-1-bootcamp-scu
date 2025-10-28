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
        Task<IEnumerable<InvoiceDetailDto>> GetInvoiceDetailsByInvoiceIdPersonalAsync(int userId, int invoiceId);

        /// <summary>
        /// Get all Invoice
        /// </summary>
        Task<IEnumerable<InvoiceDetailDto>> GetInvoiceDetailsByInvoiceIdAsync(int invoiceId);
    }
}