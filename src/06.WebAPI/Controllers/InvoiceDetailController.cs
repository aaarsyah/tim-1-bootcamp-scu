using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Feature.Invoices.Queries;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Configuration;
using MyApp.Shared.DTOs;
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
    private readonly IMediator _mediator;
    private readonly ILogger<InvoiceDetailController> _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    public InvoiceDetailController(IMediator mediator, ILogger<InvoiceDetailController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get all InvoiceDetail (ADMIN)
    /// </summary>
    /// <returns>List of InvoiceDetail</returns>
    /// <response code="200">Returns the list of invoice</response>
    [HttpGet("admin/{refId:Guid}")]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<InvoiceDetailDto>>), StatusCodes.Status200OK)] // Swagger documentation
    public async Task<ActionResult<ApiResponse<IEnumerable<InvoiceDetailDto>>>> GetInvoiceDetailsByInvoiceIdAdmin(Guid refId)
    {
        var query = new GetInvoiceDetailsByInvoiceIdProtectedQuery { RefId = refId, IsPrivileged = true, UserRefId = Guid.Empty };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Get all InvoiceDetail (USER)
    /// </summary>
    /// <returns>List of InvoiceDetail</returns>
    /// <response code="200">Returns the list of invoice</response>
    [HttpGet("{refId:Guid}")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<InvoiceDetailDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<ApiResponse<IEnumerable<InvoiceDetailDto>>>> GetInvoiceDetailsByInvoiceId(Guid refId)
    {
        var userClaimIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userClaimIdentifier == null || !Guid.TryParse(userClaimIdentifier.Value, out Guid userRefId))
        {
            throw new TokenInvalidException();
        }
        var query = new GetInvoiceDetailsByInvoiceIdProtectedQuery { RefId = refId, IsPrivileged = false, UserRefId = userRefId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}