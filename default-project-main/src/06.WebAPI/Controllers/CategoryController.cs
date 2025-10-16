using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.WebAPI.Configuration;
using MyApp.WebAPI.Models;
using MyApp.Shared.DTOs;
using MyApp.WebAPI.Services;
using System.Net;

namespace MyApp.WebAPI.Controllers
{
    /// <summary>
    /// Categories management controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
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
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<CategoryDto>>), StatusCodes.Status200OK)] // Swagger documentation
        public async Task<ActionResult<ApiResponse<IEnumerable<CategoryDto>>>> GetAllCategories()
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
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
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
        [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
        [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<CategoryDto>>> CreateCategory(CreateCategoryDto createCategoryDto, HttpStatusCode statusCode = HttpStatusCode.Created)
        {
            var result = await _categoryService.CreateCategoryAsync(createCategoryDto);
            //return Created(ApiResponse<CategoryDto>.SuccessResult(result));
            return CreatedAtAction(nameof(GetCategory), new { id = result.Id }, ApiResponse<CategoryDto>.SuccessResult(result));

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
        [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
        [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
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
        [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<object>>> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (!result)
            {
                return NotFound(ApiResponse<object>.ErrorResult($"Category with ID {id} not found"));
            }
            return Ok(ApiResponse<object>.SuccessResult());
        }
    }
}