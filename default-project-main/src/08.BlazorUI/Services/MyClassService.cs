using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MyApp.Shared.DTOs;
using System.Net.Http.Headers;

namespace MyApp.BlazorUI.Services
{
    public interface IMyClassService
    {
        Task<List<MyClassDto>> GetOwnMyClasses(AuthenticationHeaderValue authorization);
    }
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
                var response = await _httpClient.GetFromJsonAsync<List<MyClassDto>>("api/MyClass");

                return response?.ToList() ?? new ();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching MyClass data: {ex.Message}");
                return new();
            }
        }
    }
}