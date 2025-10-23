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
    }
}