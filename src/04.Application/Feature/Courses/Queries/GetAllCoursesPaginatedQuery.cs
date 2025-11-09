using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.Courses.Queries;

public class GetAllCoursesPaginatedQuery : IRequest<ApiResponse<PaginatedResponse<IEnumerable<CourseDto>>>>
{
    public CourseQueryParameters Parameters;
}
public class GetAllCoursesPaginatedQueryHandler : IRequestHandler<GetAllCoursesPaginatedQuery, ApiResponse<PaginatedResponse<IEnumerable<CourseDto>>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetAllCoursesPaginatedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResponse<PaginatedResponse<IEnumerable<CourseDto>>>> Handle(GetAllCoursesPaginatedQuery request, CancellationToken cancellationToken)
    {
        // Buat base query dengan Include untuk eager loading Category data
        // AsQueryable() memungkinkan kita untuk chain multiple LINQ operations
        var query = await _unitOfWork.Courses.GetAllAsync();

        // ========== APPLY FILTERS ==========

        // Filter berdasarkan search term (nama atau deskripsi product)
        if (!string.IsNullOrEmpty(request.Parameters.Search))
        {
            // Contains() akan di-translate ke SQL LIKE '%search%'
            query = query.Where(p => p.Name.Contains(request.Parameters.Search) ||
                                   p.Description.Contains(request.Parameters.Search));
        }

        // Filter berdasarkan category ID
        if (request.Parameters.CategoryId.HasValue)
        {
            // HasValue check untuk nullable int - pastikan user provide category filter
            query = query.Where(p => p.CategoryId == request.Parameters.CategoryId.Value);
        }

        // Filter berdasarkan minimum price
        if (request.Parameters.MinPrice.HasValue)
        {
            // Greater than or equal untuk minimum price threshold
            query = query.Where(p => p.Price >= request.Parameters.MinPrice.Value);
        }

        // Filter berdasarkan maximum price
        if (request.Parameters.MaxPrice.HasValue)
        {
            // Less than or equal untuk maximum price threshold
            query = query.Where(p => p.Price <= request.Parameters.MaxPrice.Value);
        }

        // ========== APPLY SORTING ==========

        // Switch expression untuk determine sorting logic berdasarkan SortBy parameter
        // Default sorting adalah by Name jika SortBy tidak dikenali
        query = request.Parameters.SortBy.ToLower() switch
        {
            // Sort by price
            "price" => request.Parameters.SortDirection.ToLower() == "desc"
                ? query.OrderByDescending(p => p.Price)  // Descending: mahal ke murah
                : query.OrderBy(p => p.Price),           // Ascending: murah ke mahal

            // Sort by created date
            "createdat" => request.Parameters.SortDirection.ToLower() == "desc"
                ? query.OrderByDescending(p => p.CreatedAt) // Descending: terbaru dulu
                : query.OrderBy(p => p.CreatedAt),          // Ascending: terlama dulu

            // Default sort by name
            _ => request.Parameters.SortDirection.ToLower() == "desc"
                ? query.OrderByDescending(p => p.Name)   // Descending: Z to A
                : query.OrderBy(p => p.Name)             // Ascending: A to Z
        };

        // ========== EXECUTE PAGINATION ==========

        // Count total records untuk pagination info (sebelum Skip/Take)
        var totalRecords = query.Count();

        // Apply pagination dengan Skip dan Take
        var course = query
            .Skip((request.Parameters.PageNumber - 1) * request.Parameters.PageSize) // Skip records untuk previous pages
            .Take(request.Parameters.PageSize)                                // Take hanya sebanyak PageSize
            .ToList();                                          // Execute query dan convert ke List

        // ========== CONVERT TO DTOs ==========

        // AutoMapper convert dari Product entities ke ProductDto objects
        // Mapping configuration ada di MappingProfile.cs
        var coursesDto = _mapper.Map<IEnumerable<CourseDto>>(course);

        // ========== CREATE PAGINATED RESPONSE ==========

        // Buat PagedResponse dengan pagination metadata
        var paginatedCoursesDto = PaginatedResponse<IEnumerable<CourseDto>>.Create(
            coursesDto,                    // Data yang akan dikembalikan
            request.Parameters.PageNumber,          // Current page number
            request.Parameters.PageSize,            // Page size
            totalRecords);                  // Total records untuk calculate total pages

        return ApiResponse<PaginatedResponse<IEnumerable<CourseDto>>>.SuccessResult(paginatedCoursesDto);
    }
}
