// Import Microsoft.AspNetCore.Mvc untuk controller base classes dan attributes
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Validators;
using MyApp.Infrastructure.Configuration;
// Import DTOs untuk request/response objects
using MyApp.Shared.DTOs;
// Import Services untuk business logic
using MyApp.WebAPI.Services;


namespace MyApp.WebAPI.Controllers;

/// <summary>
/// API Controller untuk mengelola Products
/// Handles CRUD operations dan advanced querying untuk products
/// </summary>
[ApiController] // Attribute yang mengaktifkan automatic model validation dan API-specific behaviors
[Route("api/[controller]")] // Route template: api/products ([controller] = "Products" tanpa "Controller" suffix)
[Produces("application/json")] // Default response type adalah JSON
public class CourseController : ControllerBase // Inherit dari ControllerBase untuk API controllers
{
    // Dependency injection fields - readonly untuk immutability
    private readonly ICourseService _courseService; // Interface untuk business logic
    private readonly ILogger<CourseController> _logger; // Logger khusus untuk class ini

    /// <summary>
    /// Constructor untuk dependency injection
    /// ASP.NET Core DI container akan otomatis inject dependencies ini
    /// </summary>
    /// <param name="courseService">Service untuk product business logic</param>
    /// <param name="logger">Logger untuk logging activities</param>
    public CourseController(ICourseService courseService, ILogger<CourseController> logger)
    {
        // Assign injected dependencies ke private fields
        _courseService = courseService;
        _logger = logger;
    }

    /// <summary>
    /// GET endpoint untuk mengambil products dengan pagination dan filtering
    /// URL: GET api/products dengan query parameters seperti pageNumber, pageSize, search, dll
    /// </summary>
    /// <param name="parameters">Query parameters untuk filtering, sorting, dan pagination</param>
    /// <returns>Paginated list berisi ProductDto objects</returns>
    [HttpGet("v2")] // HTTP GET method
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<PaginatedResponse<IEnumerable<CourseDto>>>), StatusCodes.Status200OK)] // Swagger documentation
    public async Task<ActionResult<ApiResponse<PaginatedResponse<IEnumerable<CourseDto>>>>> GetAllCoursesV2([FromQuery] CourseQueryParameters parameters)
    {
        // [FromQuery] attribute: bind query string parameters ke object properties
        // Contoh: ?pageNumber=1&pageSize=10 akan di-bind ke parameters.PageNumber dan parameters.PageSize
        
        // Panggil service method untuk get products dengan filtering
        var result = await _courseService.GetAllCoursesPaginatedAsync(parameters);
        
        // Return 200 OK dengan paginated response
        return Ok(ApiResponse<PaginatedResponse<IEnumerable<CourseDto>>>.SuccessResult(result));
    }

    /// <summary>
    /// GET endpoint untuk mengambil single product berdasarkan ID
    /// URL: GET api/products/1
    /// </summary>
    /// <param name="id">Product ID dari URL path</param>
    /// <returns>Product details atau 404 jika tidak ditemukan</returns>
    /// <response code="200">Returns the product</response>
    /// <response code="404">Product not found</response>
    [HttpGet("{id}")] // Route template dengan parameter: api/products/{id}
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<CourseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<CourseDto>>> GetCourse(int id)
    {
        var result = await _courseService.GetCourseByIdAsync(id);
        return Ok(ApiResponse<CourseDto>.SuccessResult(result));
    }

    /// <summary>
    /// POST endpoint untuk membuat product baru
    /// URL: POST api/products dengan JSON body
    /// </summary>
    /// <param name="createCourseDto">Data product yang akan dibuat</param>
    /// <returns>Created product dengan status 201</returns>
    /// <response code="201">Product created successfully</response>
    /// <response code="400">Invalid input data</response>
    [HttpPost] // HTTP POST method untuk create operation
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<CourseDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<CourseDto>>> CreateCourse(CreateCourseDto createCourseDto)
    {
        var result = await _courseService.CreateCourseAsync(createCourseDto);
        return CreatedAtAction(nameof(GetCourse), new { id = result.Id }, ApiResponse<CourseDto>.SuccessResult(result));
    }

    /// <summary>
    /// PUT endpoint untuk update product yang sudah ada
    /// URL: PUT api/products/1 dengan JSON body
    /// </summary>
    /// <param name="id">Product ID yang akan di-update</param>
    /// <param name="updateCourseDto">Data baru untuk product</param>
    /// <returns>Updated product atau 404 jika tidak ditemukan</returns>
    /// <response code="200">Product updated successfully</response>
    /// <response code="404">Product not found</response>
    [HttpPut("{id}")] // HTTP PUT method dengan ID parameter
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<CourseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<CourseDto>>> UpdateCourse(int id, UpdateCourseDto updateCourseDto)
    {
        //var validator = new UpdateCourseDtoValidator();
        //await validator.ValidateAndThrowAsync(updateCourseDto);
        var result = await _courseService.UpdateCourseAsync(id, updateCourseDto);
        return Ok(ApiResponse<CourseDto>.SuccessResult(result));
    }

    /// <summary>
    /// DELETE endpoint untuk menghapus product
    /// URL: DELETE api/products/1
    /// </summary>
    /// <param name="id">Product ID yang akan dihapus</param>
    /// <returns>204 No Content jika berhasil, 404 jika tidak ditemukan</returns>
    /// <response code="204">Product deleted successfully</response>
    /// <response code="404">Product not found</response>
    [HttpDelete("{id}")] // HTTP DELETE method
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<object>>> DeleteCourse(int id)
    {
        var result = await _courseService.DeleteCourseAsync(id);
        return Ok(ApiResponse<object>.SuccessResult());
    }
} // End of ProductsController class
// End of namespace