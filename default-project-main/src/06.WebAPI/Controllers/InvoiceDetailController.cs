using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Infrastructure.Configuration;
using MyApp.Base.Exceptions;
using MyApp.Shared.DTOs;
using MyApp.WebAPI.Services;
using System.Security.Claims;

namespace MyApp.WebAPI.Controllers;

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
    [HttpGet("admin/{invoiceId:int}")]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<InvoiceDetailDto>>), StatusCodes.Status200OK)] // Swagger documentation
    public async Task<ActionResult<ApiResponse<IEnumerable<InvoiceDetailDto>>>> GetInvoiceDetailsByInvoiceIdAdmin(int invoiceId)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
        {
            throw new TokenInvalidException();
        }
        var result = await _invoiceDetailService.GetInvoiceDetailsByInvoiceIdAsync(invoiceId);
        return Ok(ApiResponse<IEnumerable<InvoiceDetailDto>>.SuccessResult(result));
    }

    /// <summary>
    /// Get all InvoiceDetail (USER)
    /// </summary>
    /// <returns>List of InvoiceDetail</returns>
    /// <response code="200">Returns the list of invoice</response>
    [HttpGet("{invoiceId:int}")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<InvoiceDetailDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<ApiResponse<IEnumerable<InvoiceDetailDto>>>> GetInvoiceDetailsByInvoiceId(int invoiceId)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
        {
            throw new TokenInvalidException();
        }
        var result = await _invoiceDetailService.GetInvoiceDetailsByInvoiceIdPersonalAsync(userId, invoiceId);
        return Ok(ApiResponse<IEnumerable<InvoiceDetailDto>>.SuccessResult(result));
    }
}