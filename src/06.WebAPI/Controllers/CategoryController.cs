using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Feature.Categories.Commands;
using MyApp.Application.Feature.Categories.Queries;
using MyApp.Application.Configuration;
using MyApp.Shared.DTOs;

namespace MyApp.WebAPI.Controllers;

/// <summary>
/// Categories management controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Constructor
    /// </summary>
    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
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
        var query = new GetAllCategoriesQuery { };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Get category by ID
    /// </summary>
    /// <param name="refId">Category ID</param>
    /// <returns>Category details</returns>
    /// <response code="200">Returns the category</response>
    /// <response code="404">Category not found</response>
    [HttpGet("{refId:Guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<CategoryDto>>> GetCategory(Guid refId)
    {
        var query = new GetCategoryByRefIdQuery { RefId = refId };
        var result = await _mediator.Send(query);
        return Ok(result);
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
    public async Task<ActionResult<ApiResponse<CategoryDto>>> CreateCategory(CreateCategoryRequestDto createCategoryDto)
    {
        var command = new CreateCategoryCommand { CreateCategoryDto = createCategoryDto };
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCategory), new { result.Data?.RefId }, result);

    }

    /// <summary>
    /// Update category
    /// </summary>
    /// <param name="refId">Category ID</param>
    /// <param name="updateCategoryDto">Updated category data</param>
    /// <returns>Updated category</returns>
    /// <response code="200">Category updated successfully</response>
    /// <response code="404">Category not found</response>
    [HttpPut("{refId:Guid}")]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<CategoryDto>>> UpdateCategory(Guid refId, UpdateCategoryRequestDto updateCategoryDto)
    {
        var command = new UpdateCategoryCommand { RefId = refId, UpdateCategoryDto = updateCategoryDto };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Delete category
    /// </summary>
    /// <param name="refId">Category ID</param>
    /// <returns>No content</returns>
    /// <response code="204">Category deleted successfully</response>
    /// <response code="404">Category not found</response>
    [HttpDelete("{refId:Guid}")]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<object>>> DeleteCategory(Guid refId)
    {
        var command = new DeleteCategoryCommand { RefId = refId };
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}