using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Feature.CartItems.Commands;
using MyApp.Application.Feature.CartItems.Queries;
using MyApp.Base.Exceptions;
using MyApp.Shared.DTOs;
using System.Security.Claims;

namespace MyApp.WebAPI.Controllers;

/// <summary>
/// Checkout Controller
/// Purpose: Handle HTTP requests for course checkouts
/// 
/// Endpoints:
/// - POST /api/transaction/transfer - Transfer money
/// - GET /api/transaction/{id} - Get transaction details
/// - GET /api/transaction/account/{accountNumber} - Get account transactions
/// - POST /api/transaction/{id}/reverse - Reverse transaction
/// 
/// Note: No try-catch needed! 
/// GlobalExceptionHandlingMiddleware handles all errors automatically
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CartItemController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CartItemController> _logger;

    public CartItemController(
        IMediator mediator,
        ILogger<CartItemController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Checkout items
    /// </summary>
    [HttpPost("checkout")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<CheckoutResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<CheckoutResponseDto>>> CheckoutItems(
        [FromBody] CheckoutRequestDto request)
    {
        var userClaimIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userClaimIdentifier == null || !Guid.TryParse(userClaimIdentifier.Value, out Guid userRefId))
        {
            throw new TokenInvalidException();
        }
        _logger.LogInformation(
            "Checkout request received for {UserRefId} checking out {ItemCartIdsCount} items",
            request.ItemCartRefIds.Count,
            userRefId);

        // Call service - no try-catch needed!
        // Exceptions handled by GlobalExceptionHandlingMiddleware
        var command = new CheckoutCartItemsCommand { UserRefId = userRefId, CheckoutDto = request };
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    /// <summary>
    /// GET endpoint untuk mengambil products dengan pagination dan filtering
    /// URL: GET api/products dengan query parameters seperti pageNumber, pageSize, search, dll
    /// </summary>
    /// <param name="parameters">Query parameters untuk filtering, sorting, dan pagination</param>
    /// <returns>Paginated list berisi ProductDto objects</returns>
    [HttpGet("cart")] // HTTP GET method
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<CartItemResponseDto>>), StatusCodes.Status200OK)] // Swagger documentation
    [Produces("application/json")]
    public async Task<ActionResult<ApiResponse<IEnumerable<CartItemResponseDto>>>> GetOwnCartItems()
    {
        var userClaimIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userClaimIdentifier == null || !Guid.TryParse(userClaimIdentifier.Value, out Guid userRefId))
        {
            throw new TokenInvalidException();
        }
        // [FromQuery] attribute: bind query string parameters ke object properties
        // Contoh: ?pageNumber=1&pageSize=10 akan di-bind ke parameters.PageNumber dan parameters.PageSize

        var query = new GetAllCartItemsByUserRefIdQuery { UserRefId = userRefId };
        var result = await _mediator.Send(query);

        // Return 200 OK
        return Ok(result);
    }

     /// <summary>
    /// Add course to cart by scheduleId
    /// </summary>
    [HttpPut("add")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status200OK)] // Swagger documentation
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)] // Swagger documentation
    public async Task<ActionResult<ApiResponse<object>>> AddCourseToCart([FromQuery] Guid scheduleRefId)
    {
        var userClaimIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userClaimIdentifier == null || !Guid.TryParse(userClaimIdentifier.Value, out Guid userRefId))
        {
            throw new TokenInvalidException();
        }
        // [FromQuery] attribute: bind query string parameters ke object properties
        // Contoh: ?pageNumber=1&pageSize=10 akan di-bind ke parameters.PageNumber dan parameters.PageSize

        // Panggil service method untuk get products dengan filtering
        var command = new AddCourseToCartCommand { UserRefId = userRefId, ScheduleRefId = scheduleRefId };
        var result = await _mediator.Send(command);

        // Return 200 OK
        return Ok(ApiResponse<object>.SuccessResult());
    }
    
    /// <summary>
    /// Remove by cartId
    /// </summary>
    [HttpDelete("remove")] 
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status200OK)] // Swagger documentation
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)] // Swagger documentation
    public async Task<ActionResult<ApiResponse<object>>> RemoveCourseFromCart([FromQuery] Guid cartItemRefId)
    {
        var userClaimIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userClaimIdentifier == null || !Guid.TryParse(userClaimIdentifier.Value, out Guid userRefId))
        {
            throw new TokenInvalidException();
        }
        // [FromQuery] attribute: bind query string parameters ke object properties
        // Contoh: ?pageNumber=1&pageSize=10 akan di-bind ke parameters.PageNumber dan parameters.PageSize

        // Panggil service method untuk get products dengan filtering
        var command = new RemoveCourseFromCartCommand { UserRefId = userRefId, CartItemRefId = cartItemRefId };
        var result = await _mediator.Send(command);

        // Return 200 OK
        return Ok(ApiResponse<object>.SuccessResult());
    }
}