using Microsoft.AspNetCore.Mvc;
using MyApp.WebAPI.DTOs;
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
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutService _transactionService;
        private readonly ILogger<CheckoutController> _logger;

        public CheckoutController(
            ICheckoutService transactionService,
            ILogger<CheckoutController> logger)
        {
            _transactionService = transactionService;
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
        [ProducesResponseType(typeof(ApiResponse<CheckoutResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<>), 400)]
        [ProducesResponseType(typeof(ApiResponse<>), 404)]
        public async Task<ActionResult<ApiResponse<CheckoutResponseDto>>> TransferMoney(
            [FromBody] CheckoutRequestDto request)
        {
            _logger.LogInformation(
                "Checkout request received for {UserId} checking out {ItemCartIds.Count} items",
                request.ItemCartIds.Count,
                request.UserId);

            // Call service - no try-catch needed!
            // Exceptions handled by GlobalExceptionHandlingMiddleware
            var result = await _transactionService.CheckoutItemsAsync(request);

            return Ok(new ApiResponse<CheckoutResponseDto>
            {
                Success = true,
                Data = result,
                Message = "Checkout completed successfully"
            });
        }
    }
}
