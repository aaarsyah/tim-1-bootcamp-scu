using Microsoft.AspNetCore.Mvc;
using MyApp.WebAPI.Models.DTOs;
using MyApp.WebAPI.Services;
using MyApp.WebAPI.Models;
using System.Net;


namespace MyApp.WebAPI.Controllers
{
    /// <summary>
    /// Categories management controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoriesController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>List of categories</returns>
        /// <response code="200">Returns the list of categories</response>
        [HttpGet]
        [ProducesResponseType(typeof(ActionResult<ApiResponse<IEnumerable<CategoryDto>>>), StatusCodes.Status200OK)] // Swagger documentation
        public async Task<ActionResult<ApiResponse<IEnumerable<CategoryDto>>>> GetCategories()
        {
            var result = await _categoryService.GetAllCategoriesAsync();
            return Ok(ApiResponse<IEnumerable<CategoryDto>>.SuccessResult(result));
        }

        /// <summary>
        /// Get category by ID
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns>Category details</returns>
        /// <response code="200">Returns the category</response>
        /// <response code="404">Category not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ActionResult<ApiResponse<CategoryDto>>), StatusCodes.Status200OK)] // Swagger documentation
        public async Task<ActionResult<ApiResponse<CategoryDto>>> GetCategory(int id)
        {
            var result = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(ApiResponse<CategoryDto>.SuccessResult(result));
        }

        /// <summary>
        /// Create a new category
        /// </summary>
        /// <param name="createCategoryDto">Category data</param>
        /// <returns>Created category</returns>
        /// <response code="201">Category created successfully</response>
        /// <response code="400">Invalid input data</response>
        [HttpPost]
        [ProducesResponseType(typeof(ActionResult<ApiResponse<CategoryDto>>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<CategoryDto>>> 
        CreateCategory(CreateCategoryDto createCategoryDto,
        HttpStatusCode statusCode = HttpStatusCode.Created)
        {
            var result = await _categoryService.CreateCategoryAsync(createCategoryDto);
            return Ok(ApiResponse<CategoryDto>.SuccessResult(result));
        }

        /// <summary>
        /// Update category
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <param name="updateCategoryDto">Updated category data</param>
        /// <returns>Updated category</returns>
        /// <response code="200">Category updated successfully</response>
        /// <response code="404">Category not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ActionResult<ApiResponse<CategoryDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<CategoryDto>>> UpdateCategory(int id, UpdateCategoryDto updateCategoryDto)
        {
            var result = await _categoryService.UpdateCategoryAsync(id, updateCategoryDto);
            return Ok(ApiResponse<CategoryDto>.SuccessResult(result));
        }

        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns>No content</returns>
        /// <response code="204">Category deleted successfully</response>
        /// <response code="404">Category not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
         public async Task<ActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (!result)
            {
            return NotFound($"Category with ID {id} not found");
            }
            return NoContent();
        }
    }
}