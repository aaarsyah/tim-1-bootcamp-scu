using MyApp.Shared.DTOs;
using System.Net.Http.Headers;

namespace MyApp.BlazorUI.Services
{
    public interface ICheckoutService
    {
        Task<List<CartItemResponseDto>> GetOwnCartItem(AuthenticationHeaderValue authorization);
        Task<bool> AddCourseToCartAsync(AuthenticationHeaderValue authorization, int scheduleId);
        Task<bool> RemoveCourseFromCart(AuthenticationHeaderValue authorization, int cartId);
        Task<CheckoutResponseDto?> CheckoutItemsAsync(AuthenticationHeaderValue authorization, CheckoutRequestDto request);
        Task<List<PaymentDto>> GetAllPaymentsAsync();
        Task<List<InvoiceDto>> GetOwnInvoicesAsync(AuthenticationHeaderValue authorization);
        Task<InvoiceDto?> GetInvoiceByIdAsync(AuthenticationHeaderValue authorization, int invoiceId);
        Task<List<InvoiceDetailDto>> GetInvoiceDetailsByInvoiceIdAsync(AuthenticationHeaderValue authorization, int invoiceId);
    }
}