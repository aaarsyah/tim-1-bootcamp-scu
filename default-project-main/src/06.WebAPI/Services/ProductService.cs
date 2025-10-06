// Import AutoMapper untuk object-to-object mapping
using AutoMapper;
// Import DbContext untuk database operations
using WebApplication1.Data;
// Import DTOs untuk data transfer objects
using WebApplication1.DTOs;
// Import Models untuk entities dan response wrappers
using WebApplication1.Models;
// Import Entity Framework Core untuk database operations
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Services
{
    /// <summary>
    /// Implementasi business logic untuk Product operations
    /// Class ini berisi semua logic untuk CRUD operations, filtering, dan pagination
    /// </summary>
    public class ProductService : IProductService
    {
        // Dependencies yang di-inject melalui constructor
        private readonly ProductDbContext _context; // Database context untuk data access
        private readonly IMapper _mapper; // AutoMapper untuk convert Entity <-> DTO
        private readonly ILogger<ProductService> _logger; // Logger untuk logging operations

        /// <summary>
        /// Constructor untuk dependency injection
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="mapper">AutoMapper instance</param>
        /// <param name="logger">Logger instance</param>
        public ProductService(ProductDbContext context, IMapper mapper, ILogger<ProductService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method untuk mengambil products dengan pagination, filtering, dan sorting
        /// Supports: search, category filter, price range, pagination, dan sorting
        /// </summary>
        /// <param name="parameters">Parameter untuk filtering dan pagination</param>
        /// <returns>PagedResponse berisi products dan pagination info</returns>
        public async Task<PagedResponse<IEnumerable<ProductDto>>> GetProductsAsync(ProductQueryParameters parameters)
        {
            // Buat base query dengan Include untuk eager loading Category data
            // AsQueryable() memungkinkan kita untuk chain multiple LINQ operations
            var query = _context.Products
                .Include(p => p.Category) // Eager load Category untuk avoid N+1 problem
                .AsQueryable();

            // ========== APPLY FILTERS ==========
            
            // Filter berdasarkan search term (nama atau deskripsi product)
            if (!string.IsNullOrEmpty(parameters.Search))
            {
                // Contains() akan di-translate ke SQL LIKE '%search%'
                query = query.Where(p => p.Name.Contains(parameters.Search) || 
                                       p.Description.Contains(parameters.Search));
            }

            // Filter berdasarkan category ID
            if (parameters.CategoryId.HasValue)
            {
                // HasValue check untuk nullable int - pastikan user provide category filter
                query = query.Where(p => p.CategoryId == parameters.CategoryId.Value);
            }

            // Filter berdasarkan minimum price
            if (parameters.MinPrice.HasValue)
            {
                // Greater than or equal untuk minimum price threshold
                query = query.Where(p => p.Price >= parameters.MinPrice.Value);
            }

            // Filter berdasarkan maximum price
            if (parameters.MaxPrice.HasValue)
            {
                // Less than or equal untuk maximum price threshold
                query = query.Where(p => p.Price <= parameters.MaxPrice.Value);
            }

            // ========== APPLY SORTING ==========
            
            // Switch expression untuk determine sorting logic berdasarkan SortBy parameter
            // Default sorting adalah by Name jika SortBy tidak dikenali
            query = parameters.SortBy.ToLower() switch
            {
                // Sort by price
                "price" => parameters.SortDirection.ToLower() == "desc" 
                    ? query.OrderByDescending(p => p.Price)  // Descending: mahal ke murah
                    : query.OrderBy(p => p.Price),           // Ascending: murah ke mahal
                
                // Sort by created date
                "createdat" => parameters.SortDirection.ToLower() == "desc"
                    ? query.OrderByDescending(p => p.CreatedAt) // Descending: terbaru dulu
                    : query.OrderBy(p => p.CreatedAt),          // Ascending: terlama dulu
                
                // Default sort by name
                _ => parameters.SortDirection.ToLower() == "desc"
                    ? query.OrderByDescending(p => p.Name)   // Descending: Z to A
                    : query.OrderBy(p => p.Name)             // Ascending: A to Z
            };

            // ========== EXECUTE PAGINATION ==========
            
            // Count total records untuk pagination info (sebelum Skip/Take)
            var totalRecords = await query.CountAsync();
            
            // Apply pagination dengan Skip dan Take
            var products = await query
                .Skip((parameters.PageNumber - 1) * parameters.PageSize) // Skip records untuk previous pages
                .Take(parameters.PageSize)                                // Take hanya sebanyak PageSize
                .ToListAsync();                                          // Execute query dan convert ke List

            // ========== CONVERT TO DTOs ==========
            
            // AutoMapper convert dari Product entities ke ProductDto objects
            // Mapping configuration ada di MappingProfile.cs
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            // ========== CREATE PAGINATED RESPONSE ==========
            
            // Buat PagedResponse dengan pagination metadata
            return PagedResponse<IEnumerable<ProductDto>>.Create(
                productDtos,                    // Data yang akan dikembalikan
                parameters.PageNumber,          // Current page number
                parameters.PageSize,            // Page size
                totalRecords);                  // Total records untuk calculate total pages
        }

        /// <summary>
        /// Get product by ID
        /// </summary>
        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            return product != null ? _mapper.Map<ProductDto>(product) : null;
        }

        /// <summary>
        /// Create new product
        /// </summary>
        public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
        {
            // Validate category exists
            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == createProductDto.CategoryId);
            if (!categoryExists)
            {
                throw new ArgumentException($"Category with ID {createProductDto.CategoryId} does not exist");
            }

            var product = _mapper.Map<Product>(createProductDto);
            
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            
            // Reload with category for DTO mapping
            await _context.Entry(product).Reference(p => p.Category).LoadAsync();
            
            _logger.LogInformation("Product created: {ProductName} with ID: {ProductId}", 
                product.Name, product.Id);

            return _mapper.Map<ProductDto>(product);
        }

        /// <summary>
        /// Update product
        /// </summary>
        public async Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto updateProductDto)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return null;

            // Validate category exists if changed
            if (updateProductDto.CategoryId != product.CategoryId)
            {
                var categoryExists = await _context.Categories.AnyAsync(c => c.Id == updateProductDto.CategoryId);
                if (!categoryExists)
                {
                    throw new ArgumentException($"Category with ID {updateProductDto.CategoryId} does not exist");
                }
            }

            _mapper.Map(updateProductDto, product);
            product.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            
            // Reload category if changed
            if (updateProductDto.CategoryId != product.CategoryId)
            {
                await _context.Entry(product).Reference(p => p.Category).LoadAsync();
            }
            
            _logger.LogInformation("Product updated: {ProductId}", id);

            return _mapper.Map<ProductDto>(product);
        }

        /// <summary>
        /// Delete product
        /// </summary>
        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Product deleted: {ProductId}", id);
            
            return true;
        }
    }
}