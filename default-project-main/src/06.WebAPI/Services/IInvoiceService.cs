using MyApp.WebAPI.Models.DTOs;

namespace MyApp.WebAPI.Services
{
    public interface IInvoiceService
    {
        Task<IEnumerable<InvoiceResponseDto>> GetAllInvoicesAsync();
        Task<InvoiceResponseDto> GetInvoiceByIdAsync(int id);
        Task<IEnumerable<InvoiceResponseDto>> GetInvoicesByUserIdAsync(int userId);
    }
}
