using Microsoft.AspNetCore.WebUtilities;
using MyApp.Shared.DTOs;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using static MudBlazor.CategoryTypes;
using static System.Net.WebRequestMethods;

namespace MyApp.BlazorUI.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IHttpClientFactory _factory;

        public CheckoutService(IHttpClientFactory factory)
        {
            _factory = factory;
        }
        public async Task<List<CartItemResponseDto>> GetOwnCartItem(AuthenticationHeaderValue authorization)
        {
            var _httpClient = _factory.CreateClient("WebAPI");
            _httpClient.DefaultRequestHeaders.Authorization = authorization;
            try
            {
                var response = await _httpClient.GetAsync("api/CartItem/cart");
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<List<CartItemResponseDto>>>();

                    if (apiResponse?.StatusCode == "SUCCESS" && apiResponse.Data != null)
                    {
                        return apiResponse.Data;
                    }
                    return new();
                }
                return new();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetOwnCartItem: Error: {ex.Message}");
                return new();
            }
        }
        public async Task<bool> AddCourseToCartAsync(AuthenticationHeaderValue authorization, int scheduleId)
        {
            var _httpClient = _factory.CreateClient("WebAPI");
            _httpClient.DefaultRequestHeaders.Authorization = authorization;
            var query = new Dictionary<string, string?>
            {
                ["scheduleid"] = scheduleId.ToString()
            };
            try
            {
                var response = await _httpClient.PutAsync(QueryHelpers.AddQueryString("/api/CartItem/add", query), null);
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();

                    if (apiResponse?.StatusCode == "SUCCESS")
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddCourseToCartAsync: Error: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> RemoveCourseFromCart(AuthenticationHeaderValue authorization, int cartId)
        {
            var _httpClient = _factory.CreateClient("WebAPI");
            _httpClient.DefaultRequestHeaders.Authorization = authorization;
            var query = new Dictionary<string, string?>
            {
                ["cartid"] = cartId.ToString()
            };
            try
            {
                var response = await _httpClient.DeleteAsync(QueryHelpers.AddQueryString("/api/CartItem/remove", query));
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();

                    if (apiResponse?.StatusCode == "SUCCESS")
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddCourseToCartAsync: Error: {ex.Message}");
                return false;
            }
            throw new NotImplementedException();
        }
        public async Task<CheckoutResponseDto?> CheckoutItemsAsync(AuthenticationHeaderValue authorization, CheckoutRequestDto request)
        {
            var _httpClient = _factory.CreateClient("WebAPI");
            _httpClient.DefaultRequestHeaders.Authorization = authorization;
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/CartItem/checkout", request);
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<CheckoutResponseDto>>();

                    if (apiResponse?.StatusCode == "SUCCESS" && apiResponse.Data != null)
                    {
                        return apiResponse.Data;
                    }
                    return null;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CheckoutItemsAsync: Error: {ex.Message}");
                return null;
            }
        }
        public async Task<List<PaymentDto>> GetAllPaymentsAsync()
        {
            //GetCourse
            var _httpClient = _factory.CreateClient("WebAPI");
            try
            {
                var response = await _httpClient.GetAsync("api/payment");
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<List<PaymentDto>>>();

                    if (apiResponse?.StatusCode == "SUCCESS" && apiResponse.Data != null)
                    {
                        return apiResponse.Data;
                    }
                    return new();
                }
                return new();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetAllPayments: Error: {ex.Message}");
                return new();
            }
        }
    }
}
