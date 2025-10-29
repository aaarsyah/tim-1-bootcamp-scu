using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Infrastructure.Configuration;
using MyApp.Base.Exceptions;
using MyApp.Shared.DTOs;
using MyApp.WebAPI.Services;
using System.Security.Claims;

namespace MyApp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    private readonly ILogger<PaymentController> _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
    {
        _paymentService = paymentService;
        _logger = logger;
    }
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<PaymentDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<ApiResponse<IEnumerable<PaymentDto>>>> GetAllPaymentMethods()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
        {
            throw new TokenInvalidException();
        }
        var result = await _paymentService.GetAllPaymentAsync();
        return Ok(ApiResponse<IEnumerable<PaymentDto>>.SuccessResult(result));
    }

    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<PaymentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<ApiResponse<PaymentDto>>> GetPayment(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
        {
            throw new TokenInvalidException();
        }
        var result = await _paymentService.GetPaymentByIdAsync(id);
        return Ok(ApiResponse<PaymentDto>.SuccessResult(result));
    }


    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<PaymentDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<PaymentDto>>> CreatePayment(CreatePaymentDto createPaymentDto)
    {
        var result = await _paymentService.CreatePaymentAsync(createPaymentDto);
        return CreatedAtAction(nameof(GetPayment), new { id = result.Id }, ApiResponse<PaymentDto>.SuccessResult(result));
    }


    [HttpPut("{id}")]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<PaymentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<PaymentDto>>> UpdatePayment(int id, UpdatePaymentDto updatePaymentDto)
    {
        var result = await _paymentService.UpdatePaymentAsync(id, updatePaymentDto);
        return Ok(ApiResponse<PaymentDto>.SuccessResult(result));
    }


    [HttpDelete("{id}")]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<object>>> DeletePayment(int id)
    {
        var result = await _paymentService.DeletePaymentAsync(id);
        return Ok(ApiResponse<object>.SuccessResult());
    }
}