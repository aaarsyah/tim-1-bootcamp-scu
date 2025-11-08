using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Feature.CartItems.Queries;
using MyApp.Application.Feature.Categories.Queries;
using MyApp.Application.Feature.Invoices.Queries;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Configuration;
using MyApp.Shared.DTOs;
using MyApp.WebAPI.Services;
using System.Security.Claims;

namespace MyApp.WebAPI.Controllers;

/// <summary>
/// Invoice management controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class InvoiceController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<InvoiceController> _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    public InvoiceController(IMediator mediator, ILogger<InvoiceController> logger)
    {
        _mediator = mediator;
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
        var query = new GetAllInvoicesQuery { };
        var result = await _mediator.Send(query);
        return Ok(result);
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
        var userClaimIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userClaimIdentifier == null || !Guid.TryParse(userClaimIdentifier.Value, out Guid userRefId))
        {
            throw new TokenInvalidException();
        }
        var query = new GetAllInvoicesByUserRefIdQuery { UserRefId = userRefId };
        var result = await _mediator.Send(query);
        return Ok(result); 
    }

    /// <summary>
    /// Get invoice by ID (ADMIN)
    /// </summary>
    /// <param name="refId">invoice ID</param>
    /// <returns>invoice details</returns>
    /// <response code="200">Returns the invoice</response>
    /// <response code="404">invoice not found</response>
    [HttpGet("admin/{refId:Guid}")]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<InvoiceDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<InvoiceDto>>> GetInvoiceAdmin(Guid refId)
    {
        var query = new GetInvoiceByRefIdAsyncProtectedQuery { RefId = refId, IsPrivileged = true, UserRefId = Guid.Empty };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Get invoice by ID
    /// </summary>
    /// <param name="refId">invoice ID</param>
    /// <returns>invoice details</returns>
    /// <response code="200">Returns the invoice</response>
    /// <response code="404">invoice not found</response>
    [HttpGet("{refId:Guid}")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<InvoiceDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<InvoiceDto>>> GetInvoice(Guid refId)
    {
        var userClaimIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userClaimIdentifier == null || !Guid.TryParse(userClaimIdentifier.Value, out Guid userRefId))
        {
            throw new TokenInvalidException();
        }
        var query = new GetInvoiceByRefIdAsyncProtectedQuery { RefId = refId, IsPrivileged = false, UserRefId = userRefId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}