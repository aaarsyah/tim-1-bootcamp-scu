using MyApp.WebAPI.DTOs;

namespace MyApp.WebAPI.Services
{
    /// <summary>
    /// Category service interface
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Get all categories
        /// </summary>
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        
        /// <summary>
        /// Get category by ID
        /// </summary>
        Task<CategoryDto?> GetCategoryByIdAsync(int id);
        
        /// <summary>
        /// Create new category
        /// </summary>
        Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        
        /// <summary>
        /// Update category
        /// </summary>
        Task<CategoryDto?> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto);
        
        /// <summary>
        /// Delete category
        /// </summary>
        Task<bool> DeleteCategoryAsync(int id);
        
        /// <summary>
        /// Check if category exists
        /// </summary>
        Task<bool> CategoryExistsAsync(int id);
    }
}