using MyApp.Shared.DTOs;
using System.Net.Http.Headers;

namespace MyApp.BlazorUI.Services;

public class UserService : IUserService
{
    private readonly IHttpClientFactory _factory;

    public UserService(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    // Mendapatkan semua user
    public async Task<List<UserDto>> GetAllUsersAsync(AuthenticationHeaderValue authorization)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        try
        {
            var response = await _httpClient.GetAsync("api/UserManagement/users");
            if (!response.IsSuccessStatusCode)
            {
                return new();
            }
            
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<UserDto>>>();

            if (apiResponse?.Data == null)
            {
                return new();
            }
            return apiResponse.Data.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"GetAllUsersAsync: Error: {ex.Message}");
            return new();
        }
    }
    public async Task<List<RoleDto>> GetAllRolesAsync(AuthenticationHeaderValue authorization)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        try
        {
            var response = await _httpClient.GetAsync("api/UserManagement/roles");
            if (!response.IsSuccessStatusCode)
            {
                return new();
            }

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<RoleDto>>>();

            if (apiResponse?.Data == null)
            {
                return new();
            }
            return apiResponse.Data.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"GetAllRolesAsync: Error: {ex.Message}");
            return new();
        }
    }
    public async Task<bool> AddRoleToUserAsync(AuthenticationHeaderValue authorization, Guid userRefId, RoleRequestDto request)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/UserManagement/users/{userRefId}/roles/add", request);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
            return apiResponse != null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"AddRoleToUserAsync: Error: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> RemoveRoleFromUserAsync(AuthenticationHeaderValue authorization, Guid userRefId, RoleRequestDto request)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/UserManagement/users/{userRefId}/roles/remove", request);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
            return apiResponse != null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"RemoveRoleFromUserAsync: Error: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> SetClaimForUserAsync(AuthenticationHeaderValue authorization, Guid userRefId, ClaimDto request)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/UserManagement/users/{userRefId}/claims/add", request);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
            return apiResponse != null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"SetClaimForUserAsync: Error: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> RemoveClaimFromUserAsync(AuthenticationHeaderValue authorization, Guid userRefId, ClaimDto request)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/UserManagement/users/{userRefId}/claims/remove", request);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
            return apiResponse != null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"RemoveClaimFromUserAsync: Error: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> ActivateUserAsync(AuthenticationHeaderValue authorization, Guid userRefId)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/UserManagement/users/{userRefId}/activate", new { });
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
            return apiResponse != null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ActivateUserAsync: Error: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> DeactivateUserAsync(AuthenticationHeaderValue authorization, Guid userRefId)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/UserManagement/users/{userRefId}/deactivate", new { });
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
            return apiResponse != null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DeactivateUserAsync: Error: {ex.Message}");
            return false;
        }
    }
    public async Task<UserDto?> GetSelfUserAsync(AuthenticationHeaderValue authorization)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        try
        {
            var response = await _httpClient.GetAsync("api/UserManagement/users/me");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<UserDto>>();

            if (apiResponse?.Data == null)
            {
                return null;
            }
            return apiResponse.Data;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"GetSelfUserAsync: Error: {ex.Message}");
            return null;
        }
    }
    
}
