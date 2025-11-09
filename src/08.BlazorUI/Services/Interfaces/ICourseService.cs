using MyApp.Shared.DTOs;

namespace MyApp.BlazorUI.Services;

public interface ICourseService
{
    Task<IEnumerable<CourseDto>?> GetAllCourseAsync();
    Task<PaginatedResponse<IEnumerable<CourseDto>>?> GetAllCoursePaginatedAsync(int page, int pageSize);
    Task<CourseDto?> GetCourseByIdAsync(Guid courseRefId);
    Task<List<CategoryDto>> GetAllCategoriesAsync();
    Task<CategoryDto?> GetCategoryByIdAsync(Guid categoryRefId);
    Task<List<CourseDto>> GetCourseByCategoryAsync(Guid categoryRefId);

}