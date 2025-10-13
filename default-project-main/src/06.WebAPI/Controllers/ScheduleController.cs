using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.WebAPI.Configuration;
using MyApp.WebAPI.Models;
using MyApp.WebAPI.Models.DTOs;
using MyApp.WebAPI.Services;

namespace MyApp.WebAPI.Controllers
{
 
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        private readonly ILogger<ScheduleController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public ScheduleController(IScheduleService scheduleService, ILogger<ScheduleController> logger)
        {
            _scheduleService = scheduleService;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ScheduleDto>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetAllSchedules()
        {
            var result = await _scheduleService.GetAllScheduleAsync();
            return Ok(ApiResponse<IEnumerable<ScheduleDto>>.SuccessResult(result));
        }

      
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<PaymentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ScheduleDto>> GetSchedule(int id)
        {
            var result = await _scheduleService.GetScheduleByIdAsync(id);
            return Ok(ApiResponse<ScheduleDto>.SuccessResult(result));
        }

      
        [HttpPost]
        [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
        [ProducesResponseType(typeof(ApiResponse<PaymentDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ScheduleDto>> CreateSchedule(CreateScheduleDto createScheduleDto)
        {
            var result = await _scheduleService.CreateScheduleAsync(createScheduleDto);
            return CreatedAtAction(nameof(GetSchedule), new { id = result.Id }, ApiResponse<ScheduleDto>.SuccessResult(result));
        }

      
        [HttpPut("{id}")]
        [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
        [ProducesResponseType(typeof(ApiResponse<PaymentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ScheduleDto>> UpdateSchedule(int id, UpdateScheduleDto updateScheduleDto)
        {
            var result = await _scheduleService.UpdateScheduleAsync(id, updateScheduleDto);
            return Ok(ApiResponse<ScheduleDto>.SuccessResult(result));

        }


        [HttpDelete("{id}")]
        [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            var result = await _scheduleService.DeleteScheduleAsync(id);
            if (!result)
            {
                return NotFound(ApiResponse<object>.ErrorResult($"Schedule with ID {id} not found"));
            }
            return Ok(ApiResponse<object>.SuccessResult());
        }
    }
}