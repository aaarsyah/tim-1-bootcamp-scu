// Import Microsoft.AspNetCore.Mvc untuk controller base classes dan attributes
using Microsoft.AspNetCore.Mvc;
// Import DTOs untuk request/response objects
using MyApp.WebAPI.Models.DTOs;
// Import Services untuk business logic
using MyApp.WebAPI.Services;
using MyApp.WebAPI.Models;


namespace MyApp.WebAPI.Controllers
{
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
        [HttpGet] // HTTP GET method
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<CourseDto>>), StatusCodes.Status200OK)] // Swagger documentation
        public async Task<ActionResult<PagedResponse<IEnumerable<CourseDto>>>> GetCourse([FromQuery] CourseQueryParameters parameters)
        {
            // [FromQuery] attribute: bind query string parameters ke object properties
            // Contoh: ?pageNumber=1&pageSize=10 akan di-bind ke parameters.PageNumber dan parameters.PageSize
            
            // Panggil service method untuk get products dengan filtering
            var result = await _courseService.GetCourseAsync(parameters);
            
            // Return 200 OK dengan paginated response
            return Ok(result);
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
        [ProducesResponseType(typeof(ApiResponse<CourseDto>), StatusCodes.Status200OK)] // Success response type
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)] // Error response type
        public async Task<ActionResult<ApiResponse<CourseDto>>> GetCourse(int id)
        {
            // Panggil service untuk mencari product berdasarkan ID
            // Service akan return null jika product tidak ditemukan
            var course = await _courseService.GetCourseByIdAsync(id);
            
            // Cek apakah product ditemukan
            if (course == null)
            {
                // Return 404 Not Found dengan error message yang konsisten
                // ApiResponse.ErrorResult membuat response wrapper yang standar
                return NotFound(ApiResponse<object>.ErrorResult($"Course with ID {id} not found"));
            }

            // Return 200 OK dengan product data yang di-wrap dalam ApiResponse
            // ApiResponse.SuccessResult membuat response wrapper untuk success case
            return Ok(ApiResponse<CourseDto>.SuccessResult(course));
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
        [ProducesResponseType(typeof(ApiResponse<CourseDto>), StatusCodes.Status201Created)] // Success response
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)] // Validation error response
        public async Task<ActionResult<ApiResponse<CourseDto>>> CreateCourse(CreateCourseDto createCourseDto)
        {
            // [FromBody] implicit - ASP.NET Core otomatis bind JSON body ke object
            // FluentValidation akan otomatis validate createProductDto sebelum masuk ke method ini
            
            // Panggil service untuk create product baru
            // Service akan handle business logic dan validation
            var course = await _courseService.CreateCourseAsync(createCourseDto);
            
            // Buat success response dengan custom message
            var response = ApiResponse<CourseDto>.SuccessResult(course);
            
            // Return 201 Created dengan Location header yang menunjuk ke GET endpoint
            // CreatedAtAction akan generate URL: api/Course/{id} di Location header
            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, response);
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
        [ProducesResponseType(typeof(ApiResponse<CourseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<CourseDto>>> UpdateCourse(int id, UpdateCourseDto updateCourseDto)
        {
            // Panggil service untuk update product
            // Service akan return null jika product dengan ID tersebut tidak ditemukan
            var course = await _courseService.UpdateCourseAsync(id, updateCourseDto);
            
            if (course == null)
            {
                // Return 404 jika course tidak ditemukan
                return NotFound(ApiResponse<object>.ErrorResult($"Course with ID {id} not found"));
            }

            // Return 200 OK dengan updated product data
            return Ok(ApiResponse<CourseDto>.SuccessResult(course));
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
        [ProducesResponseType(StatusCodes.Status204NoContent)] // Success response tanpa body
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            // Panggil service untuk delete product
            // Service akan return false jika product tidak ditemukan
            var result = await _courseService.DeleteCourseAsync(id);
            
            if (!result)
            {
                // Return 404 jika product tidak ditemukan
                return NotFound(ApiResponse<object>.ErrorResult($"Course with ID {id} not found"));
            }

            // Return 204 No Content untuk successful deletion
            // No Content berarti operasi berhasil tapi tidak ada data untuk dikembalikan
            return NoContent();
        }
    } // End of ProductsController class
} // End of namespace