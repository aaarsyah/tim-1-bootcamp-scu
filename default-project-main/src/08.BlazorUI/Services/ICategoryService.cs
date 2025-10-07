using MyApp.BlazorUI.Models;

namespace MyApp.BlazorUI.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryItem>> GetCategoryAsync();
        Task<CategoryItem> CreateCategoryAsync(CategoryItem category);
        Task<CategoryItem> UpdateCategoryAsync(CategoryItem category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}