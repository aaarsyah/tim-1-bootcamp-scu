// Import AutoMapper untuk object-to-object mapping
using AutoMapper;
// Import Entity Framework Core untuk database operations
using Microsoft.EntityFrameworkCore;
// Import DbContext untuk database operations
using MyApp.WebAPI.Data;
using MyApp.WebAPI.Exceptions;
// Import DTOs untuk data transfer objects
// Import Models untuk entities dan response wrappers
using MyApp.WebAPI.Models;
using MyApp.WebAPI.Models.DTOs;
using MyApp.WebAPI.Models.Entities;

namespace MyApp.WebAPI.Services
{
    /// <summary>
    /// Implementasi business logic untuk Product operations
    /// Class ini berisi semua logic untuk CRUD operations, filtering, dan pagination
    /// </summary>
    public class CourseService : ICourseService
    {
        // Dependencies yang di-inject melalui constructor
        private readonly AppleMusicDbContext _context; // Database context untuk data access
        private readonly IMapper _mapper; // AutoMapper untuk convert Entity <-> DTO
        private readonly ILogger<CourseService> _logger; // Logger untuk logging operations

        /// <summary>
        /// Constructor untuk dependency injection
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="mapper">AutoMapper instance</param>
        /// <param name="logger">Logger instance</param>
        public CourseService(AppleMusicDbContext context, IMapper mapper, ILogger<CourseService> logger)
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
        public async Task<PagedResponse<IEnumerable<CourseDto>>> GetCourseAsync(CourseQueryParameters parameters)
        {
            // Buat base query dengan Include untuk eager loading Category data
            // AsQueryable() memungkinkan kita untuk chain multiple LINQ operations
            var query = _context.Courses
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
            var course = await query
                .Skip((parameters.PageNumber - 1) * parameters.PageSize) // Skip records untuk previous pages
                .Take(parameters.PageSize)                                // Take hanya sebanyak PageSize
                .ToListAsync();                                          // Execute query dan convert ke List

            // ========== CONVERT TO DTOs ==========
            
            // AutoMapper convert dari Product entities ke ProductDto objects
            // Mapping configuration ada di MappingProfile.cs
            var courseDtos = _mapper.Map<IEnumerable<CourseDto>>(course);

            // ========== CREATE PAGINATED RESPONSE ==========
            
            // Buat PagedResponse dengan pagination metadata
            return PagedResponse<IEnumerable<CourseDto>>.Create(
                courseDtos,                    // Data yang akan dikembalikan
                parameters.PageNumber,          // Current page number
                parameters.PageSize,            // Page size
                totalRecords);                  // Total records untuk calculate total pages
        }

        /// <summary>
        /// Get product by ID
        /// </summary>
        public async Task<CourseDto?> GetCourseByIdAsync(int id)
        {
            var course = await _context.Courses
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            return course != null ? _mapper.Map<CourseDto>(course) : null;
        }

        /// <summary>
        /// Create new product
        /// </summary>
        public async Task<CourseDto> CreateCourseAsync(CreateCourseDto createCourseDto)
        {
            // Validate category exists
            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == createCourseDto.CategoryId);
            if (!categoryExists)
            {
                throw new ValidationException($"Category with ID {createCourseDto.CategoryId} does not exist");
            }

            var course = _mapper.Map<Course>(createCourseDto);
            
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            
            // Reload with category for DTO mapping
            await _context.Entry(course).Reference(p => p.Category).LoadAsync();
            
            _logger.LogInformation("Course created: {CourseName} with ID: {CourseId}", 
                course.Name, course.Id);

            return _mapper.Map<CourseDto>(course);
        }

        /// <summary>
        /// Update product
        /// </summary>
        public async Task<CourseDto?> UpdateCourseAsync(int id, UpdateCourseDto updateCourseDto)
        {
            var course = await _context.Courses.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (course == null) throw new NotFoundException($"Invalid Id {id}");

            // Validate category exists if changed
            if (updateCourseDto.CategoryId != course.CategoryId)
            {
                var categoryExists = await _context.Categories.AnyAsync(c => c.Id == updateCourseDto.CategoryId);
                if (!categoryExists)
                {
                    throw new ValidationException($"Category with ID {updateCourseDto.CategoryId} does not exist");
                }
            }

            _mapper.Map(updateCourseDto, course);
            course.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            
            // Reload category if changed
            if (updateCourseDto.CategoryId != course.CategoryId)
            {
                await _context.Entry(course).Reference(p => p.Category).LoadAsync();
            }
            
            _logger.LogInformation("Course updated: {CourseId}", id);

            return _mapper.Map<CourseDto>(course);
        }

        /// <summary>
        /// Delete product
        /// </summary>
        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) throw new NotFoundException($"Invalid Id {id}");

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Course deleted: {CourseId}", id);
            
            return true;
        }
    }
}