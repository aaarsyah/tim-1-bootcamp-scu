using Microsoft.AspNetCore.Mvc;
using MyApp.WebAPI.Models.DTOs;
using MyApp.WebAPI.Models;

using MyApp.WebAPI.Services;

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
        /// Transfer money between accounts
        /// 
        /// POST /api/transaction/transfer
        /// 
        /// Request Body:
        /// {
        ///   "fromAccountNumber": "ACC001",
        ///   "toAccountNumber": "ACC002",
        ///   "amount": 100.50,
        ///   "description": "Payment for invoice"
        /// }
        /// 
        /// Success Response (200):
        /// {
        ///   "success": true,
        ///   "data": {
        ///     "transactionId": "TXN202510051230451234",
        ///     "fromAccountNumber": "ACC001",
        ///     "toAccountNumber": "ACC002",
        ///     "amount": 100.50,
        ///     "status": "Completed",
        ///     "createdAt": "2025-10-05T12:30:45Z"
        ///   },
        ///   "message": "Transfer completed successfully"
        /// }
        /// 
        /// Error Response (400 - Insufficient Balance):
        /// {
        ///   "errorCode": "INSUFFICIENT_BALANCE",
        ///   "message": "Insufficient balance in account ACC001",
        ///   "details": {
        ///     "currentBalance": 50.00,
        ///     "requestedAmount": 100.50
        ///   },
        ///   "traceId": "abc12345"
        /// }
        /// </summary>
        [HttpPost("checkout")]
        [ProducesResponseType(typeof(ApiResponse<CheckoutResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
        public async Task<ApiResponse<CheckoutResponseDto>> TransferMoney(
            [FromBody] CheckoutRequestDto request)
        {
            _logger.LogInformation(
                "Checkout request received for {UserId} checking out {ItemCartIds.Count} items",
                request.ItemCartIds.Count,
                request.UserId);

            // Call service - no try-catch needed!
            // Exceptions handled by GlobalExceptionHandlingMiddleware
            var result = await _cartItemService.CheckoutItemsAsync(request);

            // Buat success response dengan custom message
            return ApiResponse<CheckoutResponseDto>.SuccessResult(result);
        }
        /// <summary>
        /// GET endpoint untuk mengambil products dengan pagination dan filtering
        /// URL: GET api/products dengan query parameters seperti pageNumber, pageSize, search, dll
        /// </summary>
        /// <param name="parameters">Query parameters untuk filtering, sorting, dan pagination</param>
        /// <returns>Paginated list berisi ProductDto objects</returns>
        [HttpGet] // HTTP GET method
        [ProducesResponseType(typeof(IEnumerable<CartItemResponseDto>), StatusCodes.Status200OK)] // Swagger documentation
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<CartItemResponseDto>>> GetAllCartItem()
        {
            // [FromQuery] attribute: bind query string parameters ke object properties
            // Contoh: ?pageNumber=1&pageSize=10 akan di-bind ke parameters.PageNumber dan parameters.PageSize

            // Panggil service method untuk get products dengan filtering
            var result = await _cartItemService.GetAllCartItemAsync();

            // Return 200 OK
            return Ok(result);
        }
        [HttpGet("test")] // HTTP GET method
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<CartItemResponseDto>>), StatusCodes.Status200OK)] // Swagger documentation
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)] // Swagger documentation
        public async Task<CartItemResponseDto> AddCartItemAsync([FromQuery] int userId, [FromQuery] int scheduleid)
        {
            throw new NotImplementedException();
        }
    }
}
