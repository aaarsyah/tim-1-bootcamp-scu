using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Feature.PaymentMethods.Commands;
using MyApp.Application.Feature.PaymentMethods.Queries;
using MyApp.Application.Configuration;
using MyApp.Shared.DTOs;

namespace MyApp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PaymentController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PaymentController> _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    public PaymentController(IMediator mediator, ILogger<PaymentController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<PaymentMethodDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<ApiResponse<IEnumerable<PaymentMethodDto>>>> GetAllPaymentMethods()
    {
        var query = new GetAllPaymentMethodsQuery { };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{refId:Guid}")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<PaymentMethodDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<ApiResponse<PaymentMethodDto>>> GetPayment(Guid refId)
    {
        var query = new GetPaymentMethodByRefIdQuery { RefId = refId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<PaymentMethodDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<PaymentMethodDto>>> CreatePayment(CreatePaymentMethodRequestDto createPaymentDto)
    {
        var command = new CreatePaymentMethodCommand { createPaymentMethodDto = createPaymentDto };
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetPayment), new { result.Data?.RefId }, result);
    }


    [HttpPut("{refId:Guid}")]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<PaymentMethodDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<PaymentMethodDto>>> UpdatePayment(Guid refId, UpdatePaymentMethodRequestDto updatePaymentDto)
    {
        var command = new UpdatePaymentMethodCommand { RefId = refId, updatePaymentMethodDto = updatePaymentDto };
        var result = await _mediator.Send(command);
        return Ok(result);
    }


    [HttpDelete("{refId:Guid}")]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<object>>> DeletePayment(Guid refId)
    {
        var command = new DeletePaymentMethodCommand { RefId = refId };
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}