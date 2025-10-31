using MyApp.Shared.DTOs;

namespace MyApp.BlazorUI.Services;

public interface ICourseService
{
    Task<IEnumerable<CourseDto>?> GetAllCourseAsync();
    Task<PaginatedResponse<IEnumerable<CourseDto>>?> GetAllCoursePaginatedAsync(int page, int pageSize);
    Task<CourseDto?> GetCourseByIdAsync(int CourseId);
    Task<List<CategoryDto>> GetAllCategoriesAsync();
    Task<CategoryDto?> GetCategoryByIdAsync(int CategoryId);
    Task<List<CourseDto>> GetCourseByCategoryAsync(int CategoryId);

}