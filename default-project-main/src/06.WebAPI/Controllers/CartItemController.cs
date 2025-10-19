using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.WebAPI.Exceptions;
using MyApp.WebAPI.Models;
using MyApp.Shared.DTOs;
using System.Net;
using MyApp.WebAPI.Services;
using System.Net;
using System.Security.Claims;

namespace MyApp.WebAPI.Controllers
{
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
        private readonly ICartItemService _cartItemService;
        private readonly ILogger<CartItemController> _logger;

        public CartItemController(
            ICartItemService transactionService,
            ILogger<CartItemController> logger)
        {
            _cartItemService = transactionService;
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
        public async Task<ApiResponse<CheckoutResponseDto>> CheckoutItems(
            [FromBody] CheckoutRequestDto request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new AuthenticationException("Token is invalid");
            }
            _logger.LogInformation(
                "Checkout request received for {UserId} checking out {ItemCartIds.Count} items",
                request.ItemCartIds.Count,
                userId);

            // Call service - no try-catch needed!
            // Exceptions handled by GlobalExceptionHandlingMiddleware
            var result = await _cartItemService.CheckoutItemsAsync(userId, request);

            // Buat success response dengan custom message
            return ApiResponse<CheckoutResponseDto>.SuccessResult(result);
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
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new AuthenticationException("Token is invalid");
            }
            // [FromQuery] attribute: bind query string parameters ke object properties
            // Contoh: ?pageNumber=1&pageSize=10 akan di-bind ke parameters.PageNumber dan parameters.PageSize

            // Panggil service method untuk get products dengan filtering
            var result = await _cartItemService.GetCartItemsByUserIdAsync(userId);

            // Return 200 OK
            return Ok(ApiResponse<IEnumerable<CartItemResponseDto>>.SuccessResult(result));
        }

         /// <summary>
        /// Add course to cart by scheduleId
        /// </summary>
        [HttpPut("add")] 
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status200OK)] // Swagger documentation
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)] // Swagger documentation
        public async Task<ActionResult<ApiResponse<object>>> AddCourseToCart([FromQuery] int scheduleid)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new AuthenticationException("Token is invalid");
            }
            // [FromQuery] attribute: bind query string parameters ke object properties
            // Contoh: ?pageNumber=1&pageSize=10 akan di-bind ke parameters.PageNumber dan parameters.PageSize

            // Panggil service method untuk get products dengan filtering
            await _cartItemService.AddCourseToCartAsync(userId, scheduleid);

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
        public async Task<ActionResult<ApiResponse<object>>> RemoveCourseFromCart([FromQuery] int cartid)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new AuthenticationException("Token is invalid");
            }
            // [FromQuery] attribute: bind query string parameters ke object properties
            // Contoh: ?pageNumber=1&pageSize=10 akan di-bind ke parameters.PageNumber dan parameters.PageSize

            // Panggil service method untuk get products dengan filtering
            await _cartItemService.RemoveCourseFromCartAsync(userId, cartid);

            // Return 200 OK
            return Ok(ApiResponse<object>.SuccessResult());
        }

        // /// <summary>
        // /// Remove course from cart by scheduleId
        // /// </summary>
        // [HttpDelete("schedule/{scheduleId}")]
        // [Authorize]
        // [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status200OK)]
        // [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
        // public async Task<ActionResult<ApiResponse<object>>> RemoveCourseFromCartByScheduleId(int scheduleId)
        // {
        //     var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        //     if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
        //         throw new AuthenticationException("Token is invalid");

        //     _logger.LogInformation("User {UserId} is removing item from cart with ScheduleId={ScheduleId}", userId, scheduleId);

        //     await _cartItemService.RemoveCourseFromCartByScheduleIdAsync(userId, scheduleId);

        //     return Ok(ApiResponse<object>.SuccessResult());
        // }
    }
}