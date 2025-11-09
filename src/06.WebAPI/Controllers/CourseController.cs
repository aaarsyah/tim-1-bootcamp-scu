using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Feature.Courses.Commands;
using MyApp.Application.Feature.Courses.Queries;
using MyApp.Application.Configuration;
// Import DTOs untuk request/response objects
using MyApp.Shared.DTOs;
// Import Services untuk business logic


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
    private readonly IMediator _mediator;
    private readonly ILogger<CourseController> _logger;

    /// <summary>
    /// Constructor untuk dependency injection
    /// ASP.NET Core DI container akan otomatis inject dependencies ini
    /// </summary>
    /// <param name="courseService">Service untuk product business logic</param>
    /// <param name="logger">Logger untuk logging activities</param>
    public CourseController(IMediator mediator, ILogger<CourseController> logger)
    {
        _mediator = mediator;
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
        var query = new GetAllCoursesPaginatedQuery { Parameters = parameters };
        var result = await _mediator.Send(query);
        
        // Return 200 OK dengan paginated response
        return Ok(result);
    }

    /// <summary>
    /// GET endpoint untuk mengambil single product berdasarkan ID
    /// URL: GET api/products/1
    /// </summary>
    /// <param name="refId">Product ID dari URL path</param>
    /// <returns>Product details atau 404 jika tidak ditemukan</returns>
    /// <response code="200">Returns the product</response>
    /// <response code="404">Product not found</response>
    [HttpGet("{refId:Guid}")] // Route template dengan parameter: api/products/{id}
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<CourseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<CourseDto>>> GetCourse(Guid refId)
    {
        var query = new GetCourseByRefIdQuery { RefId = refId };
        var result = await _mediator.Send(query);
        return Ok(result);
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
    public async Task<ActionResult<ApiResponse<CourseDto>>> CreateCourse(CreateCourseRequestDto createCourseDto)
    {
        var command = new CreateCourseCommand { createCourseDto = createCourseDto };
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCourse), new { result.Data?.RefId }, result);
    }

    /// <summary>
    /// PUT endpoint untuk update product yang sudah ada
    /// URL: PUT api/products/1 dengan JSON body
    /// </summary>
    /// <param name="refId">Product ID yang akan di-update</param>
    /// <param name="updateCourseDto">Data baru untuk product</param>
    /// <returns>Updated product atau 404 jika tidak ditemukan</returns>
    /// <response code="200">Product updated successfully</response>
    /// <response code="404">Product not found</response>
    [HttpPut("{refId:Guid}")] // HTTP PUT method dengan ID parameter
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<CourseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<CourseDto>>> UpdateCourse(Guid refId, UpdateCourseRequestDto updateCourseDto)
    {
        //var validator = new UpdateCourseDtoValidator();
        //await validator.ValidateAndThrowAsync(updateCourseDto);
        var command = new UpdateCourseCommand { RefId = refId, UpdateCourseDto = updateCourseDto };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// DELETE endpoint untuk menghapus product
    /// URL: DELETE api/products/1
    /// </summary>
    /// <param name="refId">Product ID yang akan dihapus</param>
    /// <returns>204 No Content jika berhasil, 404 jika tidak ditemukan</returns>
    /// <response code="204">Product deleted successfully</response>
    /// <response code="404">Product not found</response>
    [HttpDelete("{refId:Guid}")] // HTTP DELETE method
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<object>>> DeleteCourse(Guid refId)
    {
        var command = new DeleteCourseCommand { RefId = refId };
        var result = await _mediator.Send(command);
        return Ok(result);
    }
} // End of ProductsController class
// End of namespace