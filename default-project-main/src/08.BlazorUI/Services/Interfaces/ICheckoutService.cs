using MyApp.Shared.DTOs;
using System.Net.Http.Headers;

namespace MyApp.BlazorUI.Services;

public interface ICheckoutService
{
    Task<List<CartItemResponseDto>> GetSelfCartItem(AuthenticationHeaderValue authorization);
    Task<bool> AddCourseToCartAsync(AuthenticationHeaderValue authorization, Guid scheduleRefId);
    Task<bool> RemoveCourseFromCart(AuthenticationHeaderValue authorization, Guid cartItemRefId);
    Task<CheckoutResponseDto?> CheckoutItemsAsync(AuthenticationHeaderValue authorization, CheckoutRequestDto request);
    Task<List<PaymentMethodDto>> GetAllPaymentsAsync(AuthenticationHeaderValue authorization);
    Task<List<InvoiceDto>> GetAllInvoiceAdminAsync(AuthenticationHeaderValue authorization);
    Task<List<InvoiceDto>> GetSelfInvoicesAsync(AuthenticationHeaderValue authorization);
    Task<InvoiceDto?> GetInvoiceByIdAdminAsync(AuthenticationHeaderValue authorization, Guid invoiceRefId);
    Task<InvoiceDto?> GetInvoiceByIdAsync(AuthenticationHeaderValue authorization, Guid invoiceRefId);
    Task<List<InvoiceDetailDto>> GetInvoiceDetailsByInvoiceIdAdminAsync(AuthenticationHeaderValue authorization, Guid invoiceRefId);
    Task<List<InvoiceDetailDto>> GetInvoiceDetailsByInvoiceIdAsync(AuthenticationHeaderValue authorization, Guid invoiceRefId);
}