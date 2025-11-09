using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Feature.Schedules.Commands;
using MyApp.Application.Feature.Schedules.Queries;
using MyApp.Infrastructure.Configuration;
using MyApp.Shared.DTOs;

namespace MyApp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ScheduleController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ScheduleController> _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    public ScheduleController(IMediator mediator, ILogger<ScheduleController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ScheduleDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetAllSchedules()
    {
        var query = new GetAllSchedulesQuery { };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

  
    [HttpGet("{refId:Guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<PaymentMethodDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ScheduleDto>> GetSchedule(Guid refId)
    {
        var query = new GetScheduleByRefIdQuery { RefId = refId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

  
    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<PaymentMethodDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ScheduleDto>> CreateSchedule(CreateScheduleRequestDto createScheduleDto)
    {
        var command = new CreateScheduleCommand { createScheduleDto = createScheduleDto };
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetSchedule), new { result.Data?.RefId }, result);
    }

    [HttpDelete("{refId:Guid}")]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSchedule(Guid refId)
    {
        var command = new DeleteScheduleCommand { RefId = refId };
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}