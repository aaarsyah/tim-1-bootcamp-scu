using AutoMapper;
using MyApp.WebAPI.Data;
using Microsoft.EntityFrameworkCore;
using MyApp.WebAPI.Models.Entities;
using MyApp.WebAPI.Models.DTOs;

namespace MyApp.WebAPI.Services
{
    /// <summary>
    /// Category service implementation
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly AppleMusicDbContext _context;
        private readonly IMapper _mapper;
        /// Constructor
        public CategoryService(AppleMusicDbContext context, IMapper mapper, ILogger<CategoryService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories
<<<<<<< HEAD
                .Include(c => c.Course)
=======
                .Include(c => c.Courses)
>>>>>>> Feature/D22-Endpoint_AND_Merge_Transaction_AND_UI
                .ToListAsync();

            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories
<<<<<<< HEAD
                .Include(c => c.Course)
=======
                .Include(c => c.Courses)
>>>>>>> Feature/D22-Endpoint_AND_Merge_Transaction_AND_UI
                .FirstOrDefaultAsync(c => c.Id == id);

            return category != null ? _mapper.Map<CategoryDto>(category) : null;
        }

        /// <summary>
        /// Create new category
        /// </summary>
        {
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Category created: {CategoryName} with ID: {CategoryId}", 
                category.Name, category.Id);

            return _mapper.Map<CategoryDto>(category);
        }

        /// <summary>
        /// Update category
        /// </summary>
        public async Task<CategoryDto?> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;

            _mapper.Map(updateCategoryDto, category);
            category.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Category updated: {CategoryId}", id);

            return _mapper.Map<CategoryDto>(category);
        }

        /// <summary>
        /// Delete category
        /// </summary>
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            // Check if category has products
            var hasProducts = await _context.Course.AnyAsync(p => p.CategoryId == id);
            if (hasProducts)
            {
                throw new InvalidOperationException("Cannot delete category that has products");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Category deleted: {CategoryId}", id);
            
            return true;
        }

        /// <summary>
        /// Check if category exists
        /// </summary>
        public async Task<bool> CategoryExistsAsync(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }
    }
}