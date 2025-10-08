using MyApp.WebAPI.Models;
using MyApp.WebAPI.Models.DTOs;

namespace MyApp.WebAPI.Services
{
    /// <summary>
    /// Product service interface
    /// </summary>
    public interface ICourseService
    {
        /// <summary>
        /// Get products with pagination and filtering
        /// </summary>
        Task<PagedResponse<IEnumerable<CourseDto>>> GetCourseAsync(CourseQueryParameters parameters);
        
        /// <summary>
        /// Get product by ID
        /// </summary>
        Task<CourseDto?> GetCourseByIdAsync(int id);
        
        /// <summary>
        /// Create new product
        /// </summary>
        Task<CourseDto> CreateCourseAsync(CreateCourseDto createCourseDto);
        
        /// <summary>
        /// Update product
        /// </summary>
        Task<CourseDto?> UpdateCourseAsync(int id, UpdateCourseDto updateCourseDto);
        
        /// <summary>
        /// Delete product
        /// </summary>
        Task<bool> DeleteCourseAsync(int id);
    }
}