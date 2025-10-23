using Microsoft.AspNetCore.WebUtilities;
using MyApp.Shared.DTOs;

namespace MyApp.BlazorUI.Services
{
    public class CourseService : ICourseService
    {
        private readonly IHttpClientFactory _factory;

        public CourseService(IHttpClientFactory factory)
        {
            _factory = factory;
        }
        public async Task<IEnumerable<CourseDto>?> GetAllCourseAsyncv2()
        {
            //GetAllCourses
            var _httpClient = _factory.CreateClient("WebAPI");
            //
            var a = new CourseQueryParameters();
            var query = new Dictionary<string, string?>
            {
                ["Search"] = a.Search,
                ["CategoryId"] = a.CategoryId.ToString(),
                ["MinPrice"] = a.MinPrice.ToString(),
                ["MaxPrice"] = a.MaxPrice.ToString(),
                ["SortBy"] = a.SortBy,
                ["SortDirection"] = a.SortDirection,
                // ...
            };
            try
            {
                var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("api/Course/v2", query));
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<PagedResponse<IEnumerable<CourseDto>>>>();

                    if (apiResponse?.StatusCode == "SUCCESS" && apiResponse.Data != null
                        && apiResponse.Data.Data != null)
                    {
                        return apiResponse.Data.Data;
                    }
                    return null;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetCourseAsyncv2: Error: {ex.Message}");
                return null;
            }
        }
        public async Task<CourseDto?> GetCourseByIdAsync(int CourseId)
        {
            //GetCourse
            var _httpClient = _factory.CreateClient("WebAPI");
            try
            {
                var response = await _httpClient.GetAsync($"api/Course/{CourseId}");
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<CourseDto>>();

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
                Console.WriteLine($"GetCourse: Error: {ex.Message}");
                return null;
            }
        }
        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            //GetCourse
            var _httpClient = _factory.CreateClient("WebAPI");
            try
            {
                var response = await _httpClient.GetAsync("api/Category");
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<List<CategoryDto>>>();

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
                Console.WriteLine($"GetAllCategories: Error: {ex.Message}");
                return new();
            }
        }
        public async Task<CategoryDto?> GetCategoryByIdAsync(int CategoryId)
        {
            //GetCourse
            var _httpClient = _factory.CreateClient("WebAPI");
            try
            {
                var response = await _httpClient.GetAsync($"api/Category/{CategoryId}");
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<CategoryDto>>();

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
                Console.WriteLine($"GetCourse: Error: {ex.Message}");
                return null;
            }
        }
    }
}
