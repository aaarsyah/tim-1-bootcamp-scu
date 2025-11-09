using MyApp.Shared.DTOs;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace MyApp.BlazorUI.Services;

public class MyClassService : IMyClassService
{
    private readonly IHttpClientFactory _factory;

    public MyClassService(
        IHttpClientFactory factory)
    {
        _factory = factory;
    }
    public async Task<List<MyClassDto>> GetOwnMyClasses(AuthenticationHeaderValue authorization)
    {
        var _httpClient = _factory.CreateClient("WebAPI");
        _httpClient.DefaultRequestHeaders.Authorization = authorization;
        try
        {
            var response = await _httpClient.GetAsync("api/MyClass");
            var rawJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response JSON: {rawJson}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"GetCourse: Error HTTP {response.StatusCode}");
                return new();
            }

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<List<MyClassDto>>>();

            if (apiResponse?.Data != null)
            {
                return apiResponse.Data
                    .OrderByDescending(x => x.Date)
                .ToList() ?? new();
            }

            return new();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching MyClass data: {ex.Message}");
            return new();
        }
    }
}