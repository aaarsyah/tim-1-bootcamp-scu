using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    /// <summary>
    /// Product service interface
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Get products with pagination and filtering
        /// </summary>
        Task<PagedResponse<IEnumerable<ProductDto>>> GetProductsAsync(ProductQueryParameters parameters);
        
        /// <summary>
        /// Get product by ID
        /// </summary>
        Task<ProductDto?> GetProductByIdAsync(int id);
        
        /// <summary>
        /// Create new product
        /// </summary>
        Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto);
        
        /// <summary>
        /// Update product
        /// </summary>
        Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto updateProductDto);
        
        /// <summary>
        /// Delete product
        /// </summary>
        Task<bool> DeleteProductAsync(int id);
    }
}