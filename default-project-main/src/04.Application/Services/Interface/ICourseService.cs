using MyApp.Shared.DTOs;

namespace MyApp.WebAPI.Services;

/// <summary>
/// Product service interface
/// </summary>
public interface ICourseService
{
    /// <summary>
    /// Get products with pagination and filtering
    /// </summary>
    Task<PaginatedResponse<IEnumerable<CourseDto>>> GetAllCoursesPaginatedAsync(CourseQueryParameters parameters);
    
    /// <summary>
    /// Get product by ID
    /// </summary>
    Task<CourseDto> GetCourseByIdAsync(int id);
    
    /// <summary>
    /// Create new product
    /// </summary>
    Task<CourseDto> CreateCourseAsync(CreateCourseRequestDto createCourseDto);
    
    /// <summary>
    /// Update product
    /// </summary>
    Task<CourseDto> UpdateCourseAsync(int id, UpdateCourseRequestDto updateCourseDto);
    
    /// <summary>
    /// Delete product
    /// </summary>
    Task<bool> DeleteCourseAsync(int id);
}