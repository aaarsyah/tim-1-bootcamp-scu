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
        /// Get all Invoice
        /// </summary>
        /// <returns>List of Invoice</returns>
        /// <response code="200">Returns the list of invoice</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<InvoiceDto>>), StatusCodes.Status200OK)] // Swagger documentation
        public async Task<ActionResult<ApiResponse<IEnumerable<InvoiceDto>>>> GetAllIvoices()
        {
            var result = await _invoiceService.GetAllInvoicesAsync();
            return Ok(ApiResponse<IEnumerable<InvoiceDto>>.SuccessResult(result));
        }

        /// <summary>
        /// Get invoice by ID
        /// </summary>
        /// <param name="id">invoice ID</param>
        /// <returns>invoice details</returns>
        /// <response code="200">Returns the invoice</response>
        /// <response code="404">invoice not found</response>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<InvoiceDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<InvoiceDto>>> GetInvoice(int id)
        {
            var result = await _invoiceService.GetInvoicesByIdAsync(id);
            return Ok(ApiResponse<InvoiceDto>.SuccessResult(result));
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
        [ProducesResponseType(typeof(ApiResponse<InvoiceDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<InvoiceDto>>> CreateInvoices(CreateInvoiceDto createInvoiceDto, HttpStatusCode statusCode = HttpStatusCode.Created)
        {
            var result = await _invoiceService.CreateInvoicesAsync(createInvoiceDto);
            //return Created(ApiResponse<CategoryDto>.SuccessResult(result));
            return CreatedAtAction(nameof(GetInvoice), new { id = result.Id }, ApiResponse<InvoiceDto>.SuccessResult(result));

        }

    }
}