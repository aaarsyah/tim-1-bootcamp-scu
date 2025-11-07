using MyApp.Shared.DTOs;

namespace MyApp.WebAPI.Services;
public interface IPaymentService
{
    Task<IEnumerable<PaymentMethodDto>> GetAllPaymentAsync();
 
    Task<PaymentMethodDto> GetPaymentByIdAsync(int id);

    Task<PaymentMethodDto> CreatePaymentAsync(CreatePaymentMethodRequestDto createPaymentDto);
    
    Task<PaymentMethodDto> UpdatePaymentAsync(int id, UpdatePaymentRequestDto updatePaymentDto);
    
    Task<bool> DeletePaymentAsync(int id);
 
    Task<bool> PaymentExistsAsync(int id);
}