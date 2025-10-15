using MyApp.WebAPI.Models.Entities;
using MyApp.WebAPI.Models.Dtos;

namespace MyApp.WebAPI.Services.Interfaces
{
    public interface IInvoiceDetailsService
    {
        Task<IEnumerable<InvoiceDetailsResponseDto>> GetAllAsync();
        Task<InvoiceDetailsResponseDto?> GetByIdAsync(int id);
        Task<InvoiceDetails?> CreateAsync(InvoiceDetails invoiceDetails);
        Task<bool> UpdateAsync(int id, InvoiceDetails invoiceDetails);
        Task<bool> DeleteAsync(int id);
    }
}
