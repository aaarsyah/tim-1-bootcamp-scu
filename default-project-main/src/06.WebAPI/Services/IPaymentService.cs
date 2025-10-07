using WebApplication1.DTOs;

namespace WebApplication1.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetAllPaymentAsync();
     
        Task<PaymentDto?> GetPaymentByIdAsync(int id);
   
        Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto createPaymentDto);
        
        Task<PaymentDto?> UpdatePaymentAsync(int id, UpdatePaymentDto updatePaymentDto);
        

        Task<bool> DeletePaymentAsync(int id);
     
        Task<bool> PaymentExistsAsync(int id);
    }
}