using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.WebAPI.Configuration;
using MyApp.WebAPI.Exceptions;
using MyApp.Shared.DTOs;
using System.Net;
using MyApp.WebAPI.Services;
using System.Security.Claims;

namespace MyApp.WebAPI.Controllers
{
    /// <summary>
    /// Invoice management controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly ILogger<InvoiceController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public InvoiceController(IInvoiceService invoiceService, ILogger<InvoiceController> logger)
        {
            _invoiceService = invoiceService;
            _logger = logger;
        }

        /// <summary>
        /// Get all Invoice (ADMIN)
        /// </summary>
        /// <returns>List of Invoice</returns>
        /// <response code="200">Returns the list of invoice</response>
        [HttpGet("admin")]
        [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<InvoiceDto>>), StatusCodes.Status200OK)] // Swagger documentation
        public async Task<ActionResult<ApiResponse<IEnumerable<InvoiceDto>>>> GetAllInvoices()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new TokenInvalidException();
            }

            var result = await _invoiceService.GetAllInvoicesAsync();
            return Ok(ApiResponse<IEnumerable<InvoiceDto>>.SuccessResult(result));
        }

        /// <summary>
        /// Get all Invoice (USER)
        /// </summary>
        /// <returns>List of Invoice</returns>
        /// <response code="200">Returns the list of invoice</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<InvoiceDto>>), StatusCodes.Status200OK)] // Swagger documentation
        public async Task<ActionResult<ApiResponse<IEnumerable<InvoiceDto>>>> GetSelfInvoices()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new TokenInvalidException();
            }

            var result = await _invoiceService.GetAllInvoicesByUserIdAsync(userId);
            return Ok(ApiResponse<IEnumerable<InvoiceDto>>.SuccessResult(result)); 
        }

        /// <summary>
        /// Get invoice by ID (ADMIN)
        /// </summary>
        /// <param name="id">invoice ID</param>
        /// <returns>invoice details</returns>
        /// <response code="200">Returns the invoice</response>
        /// <response code="404">invoice not found</response>
        [HttpGet("admin/{id}")]
        [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
        [ProducesResponseType(typeof(ApiResponse<InvoiceDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<InvoiceDto>>> GetInvoiceAdmin(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new TokenInvalidException();
            }

            var result = await _invoiceService.GetInvoiceByIdAsync(id);
            return Ok(ApiResponse<InvoiceDto>.SuccessResult(result));
        }

        /// <summary>
        /// Get invoice by ID
        /// </summary>
        /// <param name="id">invoice ID</param>
        /// <returns>invoice details</returns>
        /// <response code="200">Returns the invoice</response>
        /// <response code="404">invoice not found</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<InvoiceDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<InvoiceDto>>> GetInvoice(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new TokenInvalidException();
            }
            
            var result = await _invoiceService.GetInvoiceByIdPersonalAsync(userId, id);
            return Ok(ApiResponse<InvoiceDto>.SuccessResult(result));
        }
    }
}