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
    /// Invoice Detail management controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class InvoiceDetailController : ControllerBase
    {
        private readonly IInvoiceDetailService _invoiceDetailService;
        private readonly ILogger<InvoiceDetailController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public InvoiceDetailController(IInvoiceDetailService invoiceDetailService, ILogger<InvoiceDetailController> logger)
        {
            _invoiceDetailService = invoiceDetailService;
            _logger = logger;
        }

        /// <summary>
        /// Get all InvoiceDetail (ADMIN)
        /// </summary>
        /// <returns>List of InvoiceDetail</returns>
        /// <response code="200">Returns the list of invoice</response>
        [HttpGet]
        [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<InvoiceDetailDto>>), StatusCodes.Status200OK)] // Swagger documentation
        public async Task<ActionResult<ApiResponse<IEnumerable<InvoiceDetailDto>>>> GetAllInvoicesDetail()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new AuthenticationException("Token is invalid");
            }

            var result = await _invoiceDetailService.GetAllInvoicesDetailAsync();
            return Ok(ApiResponse<IEnumerable<InvoiceDetailDto>>.SuccessResult(result));
        }

        /// <summary>
        /// Get all InvoiceDetail (USER)
        /// </summary>
        /// <returns>List of InvoiceDetail</returns>
        /// <response code="200">Returns the list of invoice</response>
        [HttpGet("user/{invoiceId:int}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<InvoiceDetailDto>>), StatusCodes.Status200OK)] // Swagger documentation
        public async Task<ActionResult<ApiResponse<IEnumerable<InvoiceDetailDto>>>> GetAllInvoicesDetailUser(int invoiceId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new AuthenticationException("Token is invalid");
            }
            try
            {
                var result = await _invoiceDetailService.GetInvoiceDetailsByUserAsync(invoiceId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving invoice");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get invoice Detail by ID
        /// </summary>
        /// <param name="id">invoice ID</param>
        /// <returns>invoice details</returns>
        /// <response code="200">Returns the invoice</response>
        /// <response code="404">invoice not found</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<InvoiceDetailDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<InvoiceDetailDto>>> GetInvoiceDetail(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new AuthenticationException("Token is invalid");
            }

            var result = await _invoiceDetailService.GetInvoicesDetailByIdAsync(id);
            return Ok(ApiResponse<InvoiceDetailDto>.SuccessResult(result));
        }

        /// <summary>
        /// Create a new Invoice
        /// </summary>
        /// <param name="createInvoiceDto">Invoice data</param>
        /// <returns>Created Invoice</returns>
        /// <response code="201">Invoice created successfully</response>
        /// <response code="400">Invalid input data</response>
        [HttpPost]
        [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
        [ProducesResponseType(typeof(ApiResponse<InvoiceDetailDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<InvoiceDetailDto>>> CreateInvoicesDetail(CreateInvoiceDetailDto createInvoiceDetailDto, HttpStatusCode statusCode = HttpStatusCode.Created)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new AuthenticationException("Token is invalid");
            }
            
            var result = await _invoiceDetailService.CreateInvoicesDetailAsync(createInvoiceDetailDto);
            //return Created(ApiResponse<CategoryDto>.SuccessResult(result));
            return CreatedAtAction(nameof(GetInvoiceDetail), new { id = result.Id }, ApiResponse<InvoiceDetailDto>.SuccessResult(result));

        }

    }
}