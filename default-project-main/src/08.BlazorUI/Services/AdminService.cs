using Microsoft.AspNetCore.WebUtilities;
using MyApp.Shared.DTOs;
using System.Net.Http.Headers;

namespace MyApp.BlazorUI.Services;

public class AdminService : IAdminService
{

    private readonly IHttpClientFactory _factory;

    public AdminService(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    public async Task<List<CourseDto>> GetAllCourseAsync()
    {
        //GetAllCourses
        var _httpClient = _factory.CreateClient("WebAPI");
        //
        var parameter = new CourseQueryParameters();
        var query = new Dictionary<string, string?>
        {
            ["Search"] = parameter.Search,
            ["CategoryId"] = parameter.CategoryId.ToString(),
            ["MinPrice"] = parameter.MinPrice.ToString(),
            ["MaxPrice"] = parameter.MaxPrice.ToString(),
            ["SortBy"] = parameter.SortBy,
            ["SortDirection"] = parameter.SortDirection,
            // ...
        };
        try
        {
            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("api/Course/v2", query));
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<PaginatedResponse<IEnumerable<CourseDto>>>>();

                if (apiResponse?.Data?.Data != null)
                {
                    return apiResponse.Data.Data.ToList();
                }
                return new();
            }
            return new();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"GetAllCourseAsync: Error: {ex.Message}");
            return new();
        }
    }


    public async Task<CourseDto?> CreateCourseAsync(AuthenticationHeaderValue authorization, CreateCourseRequestDto request)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/Course", request);
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<CourseDto>>();

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
            Console.WriteLine($"CreateCourseAsync: Error: {ex.Message}");
            return null;
        }
    }

    public async Task<CourseDto?> UpdateCourseAsync(AuthenticationHeaderValue authorization, Guid refId, UpdateCourseRequestDto request)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        //
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Course/{refId}", request);
            Console.WriteLine($"Response: {response.Content.ReadAsStringAsync().Result}");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<CourseDto>>();

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
            Console.WriteLine($"UpdateCourseAsync: Error: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> DeleteCourseAsync(AuthenticationHeaderValue authorization, Guid refId)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        //
        try
        {
            var response = await _httpClient.DeleteAsync($"api/Course/{refId}");
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
            return apiResponse != null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DeleteCourseAsync: Error: {ex.Message}");
            return false;
        }
    }

    public async Task<List<CategoryDto>> GetAllCategoryAsync()
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        try
        {
            var response = await _httpClient.GetAsync("api/Category");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<List<CategoryDto>>>();

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
            Console.WriteLine($"GetAllPaymentMethodsAsync: Error: {ex.Message}");
            return new();
        }
    }


    public async Task<CategoryDto?> CreateCategoryAsync(AuthenticationHeaderValue authorization, CreateCategoryRequestDto request)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/Category", request);
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<CategoryDto>>();

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
            Console.WriteLine($"CreateCourseAsync: Error: {ex.Message}");
            return null;
        }
    }

    public async Task<CategoryDto?> UpdateCategoryAsync(AuthenticationHeaderValue authorization, Guid refId, UpdateCategoryRequestDto request)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        //
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Category/{refId}", request);
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<CategoryDto>>();

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
            Console.WriteLine($"UpdateCourseAsync: Error: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> DeleteCategoryAsync(AuthenticationHeaderValue authorization, Guid refId)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        //
        try
        {
            var response = await _httpClient.DeleteAsync($"api/Category/{refId}");
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
            return apiResponse != null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DeleteCourseAsync: Error: {ex.Message}");
            return false;
        }
    }

    public async Task<List<PaymentMethodDto>> GetAllPaymentMethodsAsync()
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        try
        {
            var response = await _httpClient.GetAsync("api/Payment");
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
            Console.WriteLine($"GetAllPaymentMethodsAsync: Error: {ex.Message}");
            return new();
        }
    }

    public async Task<PaymentMethodDto?> CreatePaymentMethodAsync(AuthenticationHeaderValue authorization, CreatePaymentMethodRequestDto request)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/Payment", request);
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<PaymentMethodDto>>();

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
            Console.WriteLine($"CreatePaymentMethodAsync: Error: {ex.Message}");
            return null;
        }
    }

    public async Task<PaymentMethodDto?> UpdatePaymentMethodAsync(AuthenticationHeaderValue authorization, Guid refId, UpdatePaymentMethodRequestDto request)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        //
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Payment/{refId}", request);
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<PaymentMethodDto>>();

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
            Console.WriteLine($"UpdatePaymentMethodAsync: Error: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> DeletePaymentMethodAsync(AuthenticationHeaderValue authorization, Guid refId)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        //
        try
        {
            var response = await _httpClient.DeleteAsync($"api/Payment/{refId}");
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
            return apiResponse != null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DeletePaymentMethodAsync: Error: {ex.Message}");
            return false;
        }
    }
}
