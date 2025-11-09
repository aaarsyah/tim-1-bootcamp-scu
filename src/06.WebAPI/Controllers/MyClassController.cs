using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Feature.MyClasses.Commands;
using MyApp.Application.Feature.MyClasses.Queries;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Configuration;
using MyApp.Shared.DTOs;
using System.Security.Claims;

namespace MyApp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class MyClassController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MyClassController> _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    public MyClassController(IMediator mediator, ILogger<MyClassController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(IEnumerable<MyClassDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MyClassDto>>> GetSelfMyClass()
    {
        var userClaimIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userClaimIdentifier == null || !Guid.TryParse(userClaimIdentifier.Value, out Guid userRefId))
        {
            throw new TokenInvalidException();
        }
        var query = new GetAllMyClassesByUserRefIdQuery { UserRefId = userRefId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

  
    [HttpGet("{refId:Guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<MyClassDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<MyClassDto>>> GetMyClass(Guid refId)
    {
        var query = new GetMyClassByRefIdQuery { RefId = refId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<MyClassDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MyClassDto>> CreateMyClass(CreateMyClassRequestDto createMyClassDto)
    {
        var command = new CreateMyClassCommand { createMyClassDto = createMyClassDto };
        var result = await _mediator.Send(command);
        //return Created(ApiResponse<CategoryDto>.SuccessResult(result));
        return CreatedAtAction(nameof(GetMyClass), new { result.Data?.RefId }, result);
    }

    [HttpDelete("{refId:Guid}")]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<object>>> DeleteMyClass(Guid refId)
    {
        var command = new DeleteMyClassCommand { RefId = refId };
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}