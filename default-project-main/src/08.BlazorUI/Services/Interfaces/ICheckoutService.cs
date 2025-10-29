using MyApp.Shared.DTOs;
using System.Net.Http.Headers;

namespace MyApp.BlazorUI.Services;

public interface ICheckoutService
{
    Task<List<CartItemResponseDto>> GetSelfCartItem(AuthenticationHeaderValue authorization);
    Task<bool> AddCourseToCartAsync(AuthenticationHeaderValue authorization, int scheduleId);
    Task<bool> RemoveCourseFromCart(AuthenticationHeaderValue authorization, int cartId);
    Task<CheckoutResponseDto?> CheckoutItemsAsync(AuthenticationHeaderValue authorization, CheckoutRequestDto request);
    Task<List<PaymentDto>> GetAllPaymentsAsync(AuthenticationHeaderValue authorization);
    Task<List<InvoiceDto>> GetAllInvoiceAdminAsync(AuthenticationHeaderValue authorization);
    Task<List<InvoiceDto>> GetSelfInvoicesAsync(AuthenticationHeaderValue authorization);
    Task<InvoiceDto?> GetInvoiceByIdAdminAsync(AuthenticationHeaderValue authorization, int invoiceId);
    Task<InvoiceDto?> GetInvoiceByIdAsync(AuthenticationHeaderValue authorization, int invoiceId);
    Task<List<InvoiceDetailDto>> GetInvoiceDetailsByInvoiceIdAdminAsync(AuthenticationHeaderValue authorization, int invoiceId);
    Task<List<InvoiceDetailDto>> GetInvoiceDetailsByInvoiceIdAsync(AuthenticationHeaderValue authorization, int invoiceId);
}