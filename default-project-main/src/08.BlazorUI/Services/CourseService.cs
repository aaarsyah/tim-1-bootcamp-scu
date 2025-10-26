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
        public async Task<IEnumerable<CourseDto>?> GetAllCourseAsync()
        {
            //GetAllCourses
            var _httpClient = _factory.CreateClient("WebAPI");
            //
            var parameter = new CourseQueryParameters();
            var query = new Dictionary<string, string?>
            {
                //["PageNumber"] = parameter.PageNumber.ToString(),
                //["PageSize"] = parameter.PageSize.ToString(),
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
        public async Task<PaginatedResponse<IEnumerable<CourseDto>>?> GetAllCoursePaginatedAsync(int page, int pageSize)
        {
            //GetAllCourses
            var _httpClient = _factory.CreateClient("WebAPI");
            //
            var parameter = new CourseQueryParameters();
            var query = new Dictionary<string, string?>
            {
                ["PageNumber"] = page.ToString(),
                ["PageSize"] = pageSize.ToString(),
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
