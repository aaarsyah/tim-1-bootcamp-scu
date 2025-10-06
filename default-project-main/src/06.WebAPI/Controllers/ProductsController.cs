// Import Microsoft.AspNetCore.Mvc untuk controller base classes dan attributes
using Microsoft.AspNetCore.Mvc;
// Import DTOs untuk request/response objects
using WebApplication1.DTOs;
// Import Models untuk response wrapper classes
using WebApplication1.Models;
// Import Services untuk business logic
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    /// <summary>
    /// API Controller untuk mengelola Products
    /// Handles CRUD operations dan advanced querying untuk products
    /// </summary>
    [ApiController] // Attribute yang mengaktifkan automatic model validation dan API-specific behaviors
    [Route("api/[controller]")] // Route template: api/products ([controller] = "Products" tanpa "Controller" suffix)
    [Produces("application/json")] // Default response type adalah JSON
    public class ProductsController : ControllerBase // Inherit dari ControllerBase untuk API controllers
    {
        // Dependency injection fields - readonly untuk immutability
        private readonly IProductService _productService; // Interface untuk business logic
        private readonly ILogger<ProductsController> _logger; // Logger khusus untuk class ini

        /// <summary>
        /// Constructor untuk dependency injection
        /// ASP.NET Core DI container akan otomatis inject dependencies ini
        /// </summary>
        /// <param name="productService">Service untuk product business logic</param>
        /// <param name="logger">Logger untuk logging activities</param>
        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            // Assign injected dependencies ke private fields
            _productService = productService;
            _logger = logger;
        }

        /// <summary>
        /// GET endpoint untuk mengambil products dengan pagination dan filtering
        /// URL: GET api/products dengan query parameters seperti pageNumber, pageSize, search, dll
        /// </summary>
        /// <param name="parameters">Query parameters untuk filtering, sorting, dan pagination</param>
        /// <returns>Paginated list berisi ProductDto objects</returns>
        [HttpGet] // HTTP GET method
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<ProductDto>>), StatusCodes.Status200OK)] // Swagger documentation
        public async Task<ActionResult<PagedResponse<IEnumerable<ProductDto>>>> GetProducts([FromQuery] ProductQueryParameters parameters)
        {
            // [FromQuery] attribute: bind query string parameters ke object properties
            // Contoh: ?pageNumber=1&pageSize=10 akan di-bind ke parameters.PageNumber dan parameters.PageSize
            
            // Panggil service method untuk get products dengan filtering
            var result = await _productService.GetProductsAsync(parameters);
            
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
        [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status200OK)] // Success response type
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)] // Error response type
        public async Task<ActionResult<ApiResponse<ProductDto>>> GetProduct(int id)
        {
            // Panggil service untuk mencari product berdasarkan ID
            // Service akan return null jika product tidak ditemukan
            var product = await _productService.GetProductByIdAsync(id);
            
            // Cek apakah product ditemukan
            if (product == null)
            {
                // Return 404 Not Found dengan error message yang konsisten
                // ApiResponse.ErrorResult membuat response wrapper yang standar
                return NotFound(ApiResponse<object>.ErrorResult($"Product with ID {id} not found"));
            }

            // Return 200 OK dengan product data yang di-wrap dalam ApiResponse
            // ApiResponse.SuccessResult membuat response wrapper untuk success case
            return Ok(ApiResponse<ProductDto>.SuccessResult(product));
        }

        /// <summary>
        /// POST endpoint untuk membuat product baru
        /// URL: POST api/products dengan JSON body
        /// </summary>
        /// <param name="createProductDto">Data product yang akan dibuat</param>
        /// <returns>Created product dengan status 201</returns>
        /// <response code="201">Product created successfully</response>
        /// <response code="400">Invalid input data</response>
        [HttpPost] // HTTP POST method untuk create operation
        [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status201Created)] // Success response
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)] // Validation error response
        public async Task<ActionResult<ApiResponse<ProductDto>>> CreateProduct(CreateProductDto createProductDto)
        {
            // [FromBody] implicit - ASP.NET Core otomatis bind JSON body ke object
            // FluentValidation akan otomatis validate createProductDto sebelum masuk ke method ini
            
            // Panggil service untuk create product baru
            // Service akan handle business logic dan validation
            var product = await _productService.CreateProductAsync(createProductDto);
            
            // Buat success response dengan custom message
            var response = ApiResponse<ProductDto>.SuccessResult(product, "Product created successfully");
            
            // Return 201 Created dengan Location header yang menunjuk ke GET endpoint
            // CreatedAtAction akan generate URL: api/products/{id} di Location header
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, response);
        }

        /// <summary>
        /// PUT endpoint untuk update product yang sudah ada
        /// URL: PUT api/products/1 dengan JSON body
        /// </summary>
        /// <param name="id">Product ID yang akan di-update</param>
        /// <param name="updateProductDto">Data baru untuk product</param>
        /// <returns>Updated product atau 404 jika tidak ditemukan</returns>
        /// <response code="200">Product updated successfully</response>
        /// <response code="404">Product not found</response>
        [HttpPut("{id}")] // HTTP PUT method dengan ID parameter
        [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<ProductDto>>> UpdateProduct(int id, UpdateProductDto updateProductDto)
        {
            // Panggil service untuk update product
            // Service akan return null jika product dengan ID tersebut tidak ditemukan
            var product = await _productService.UpdateProductAsync(id, updateProductDto);
            
            if (product == null)
            {
                // Return 404 jika product tidak ditemukan
                return NotFound(ApiResponse<object>.ErrorResult($"Product with ID {id} not found"));
            }

            // Return 200 OK dengan updated product data
            return Ok(ApiResponse<ProductDto>.SuccessResult(product, "Product updated successfully"));
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
        public async Task<IActionResult> DeleteProduct(int id)
        {
            // Panggil service untuk delete product
            // Service akan return false jika product tidak ditemukan
            var result = await _productService.DeleteProductAsync(id);
            
            if (!result)
            {
                // Return 404 jika product tidak ditemukan
                return NotFound(ApiResponse<object>.ErrorResult($"Product with ID {id} not found"));
            }

            // Return 204 No Content untuk successful deletion
            // No Content berarti operasi berhasil tapi tidak ada data untuk dikembalikan
            return NoContent();
        }
    } // End of ProductsController class
} // End of namespace