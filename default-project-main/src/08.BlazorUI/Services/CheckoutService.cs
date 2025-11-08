using Microsoft.AspNetCore.WebUtilities;
using MyApp.Shared.DTOs;
using System.Net.Http.Headers;

namespace MyApp.BlazorUI.Services;

public class CheckoutService : ICheckoutService
{
    private readonly IHttpClientFactory _factory;

    public CheckoutService(IHttpClientFactory factory)
    {
        _factory = factory;
    }
    public async Task<List<CartItemResponseDto>> GetSelfCartItem(AuthenticationHeaderValue authorization)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        try
        {
            var response = await _httpClient.GetAsync("api/CartItem/cart");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<List<CartItemResponseDto>>>();

                if (apiResponse?.Data != null)
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
    public async Task<bool> AddCourseToCartAsync(AuthenticationHeaderValue authorization, Guid scheduleRefId)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        var query = new Dictionary<string, string?>
        {
            ["scheduleRefId"] = scheduleRefId.ToString()
        };
        try
        {
            var response = await _httpClient.PutAsync(QueryHelpers.AddQueryString("/api/CartItem/add", query), null);
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();

                if (apiResponse != null)
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
    public async Task<bool> RemoveCourseFromCart(AuthenticationHeaderValue authorization, Guid cartItemRefId)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        var query = new Dictionary<string, string?>
        {
            ["cartItemRefId"] = cartItemRefId.ToString()
        };
        try
        {
            var response = await _httpClient.DeleteAsync(QueryHelpers.AddQueryString("/api/CartItem/remove", query));
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();

                if (apiResponse != null)
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

                if (apiResponse?.Data != null)
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
    public async Task<List<PaymentMethodDto>> GetAllPaymentsAsync(AuthenticationHeaderValue authorization)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        try
        {
            var response = await _httpClient.GetAsync("api/payment");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<List<PaymentMethodDto>>>();

                if (apiResponse?.Data != null)
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

    public async Task<List<InvoiceDto>> GetAllInvoiceAdminAsync(AuthenticationHeaderValue authorization)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        try
        {
            var response = await _httpClient.GetAsync("api/Invoice/admin");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<InvoiceDto>>>();

                if (apiResponse?.Data != null)
                {
                    return apiResponse.Data.ToList();
                }
                return new();
            }
            return new();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"GetAllInvoice: Error: {ex.Message}");
            return new();
        }
    }
    public async Task<List<InvoiceDto>> GetSelfInvoicesAsync(AuthenticationHeaderValue authorization)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        try
        {
            var response = await _httpClient.GetAsync("api/Invoice");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<InvoiceDto>>>();

                if (apiResponse?.Data != null)
                {
                    return apiResponse.Data
                        .OrderByDescending(i => i.CreatedAt) // urutkan descending by tanggal terbaru
                        .ToList();
                }
                return new();
            }
            return new();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"GetOwnInvoicesAsync: Error: {ex.Message}");
            return new();
        }
    }

    public async Task<InvoiceDto?> GetInvoiceByIdAdminAsync(AuthenticationHeaderValue authorization, Guid invoiceRefId)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        try
        {
            var response = await _httpClient.GetAsync($"api/Invoice/admin/{invoiceRefId}");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<InvoiceDto>>();

                if (apiResponse?.Data != null)
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

    public async Task<InvoiceDto?> GetInvoiceByIdAsync(AuthenticationHeaderValue authorization, Guid invoiceRefId)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        try
        {
            var response = await _httpClient.GetAsync($"api/Invoice/{invoiceRefId}");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<InvoiceDto>>();

                if (apiResponse?.Data != null)
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

    public async Task<List<InvoiceDetailDto>> GetInvoiceDetailsByInvoiceIdAdminAsync(AuthenticationHeaderValue authorization, Guid invoiceRefId)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        try
        {
            var response = await _httpClient.GetAsync($"api/InvoiceDetail/admin/{invoiceRefId}");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<InvoiceDetailDto>>>();

                if (apiResponse?.Data != null)
                {
                    return apiResponse.Data.ToList();
                }
                return new();
            }
            return new();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"GetAllInvoice: Error: {ex.Message}");
            return new();
        }
    }
    public async Task<List<InvoiceDetailDto>> GetInvoiceDetailsByInvoiceIdAsync(AuthenticationHeaderValue authorization, Guid invoiceRefId)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        try
        {
            var response = await _httpClient.GetAsync($"api/InvoiceDetail/{invoiceRefId}");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<InvoiceDetailDto>>>();

                if (apiResponse?.Data != null)
                {
                    return apiResponse.Data.ToList();
                }
                return new();
            }
            return new();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"GetOwnInvoicesAsync: Error: {ex.Message}");
            return new();
        }
    }
}
