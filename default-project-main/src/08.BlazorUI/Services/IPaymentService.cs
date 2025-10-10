using MyApp.BlazorUI.Models;

namespace MyApp.BlazorUI.Services
{
    public interface IPaymentService
    {
        Task<List<PaymentItem>> GetPaymentAsync();
        Task<PaymentItem> CreatePaymentAsync(PaymentItem payment);
        Task<PaymentItem> UpdatePaymentAsync(PaymentItem payment);
        Task<bool> DeletePaymentAsync(int id);
    }
}