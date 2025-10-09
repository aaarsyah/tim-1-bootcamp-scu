using Microsoft.AspNetCore.Mvc;
using MyApp.WebAPI.Models.DTOs;
using MyApp.WebAPI.Services;
using MyApp.WebAPI.Models;


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
        [ProducesResponseType(typeof(IEnumerable<ScheduleDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetSchedule()
        {
            try
            {
                var schedule = await _scheduleService.GetAllScheduleAsync();
                return Ok(schedule);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving schedule");
                return StatusCode(500, "Internal server error");
            }
        }

      
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ScheduleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ScheduleDto>> GetSchedule(int id)
        {
            try
            {
                var schedule = await _scheduleService.GetScheduleByIdAsync(id);
                if (schedule == null)
                {
                    return NotFound($"Schedule with ID {id} not found");
                }
                return Ok(schedule);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving schedule ScheduleId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

      
        [HttpPost]
        [ProducesResponseType(typeof(ScheduleDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ScheduleDto>> CreateSchedule(CreateScheduleDto createScheduleDto)
        {
            var schedule = await _scheduleService.CreateScheduleAsync(createScheduleDto);
            var response = ApiResponse<ScheduleDto>.SuccessResult(schedule);
            return CreatedAtAction(nameof(GetSchedule), new { id = schedule.Id }, response);
        }

      
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ScheduleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ScheduleDto>> UpdateSchedule(int id, UpdateScheduleDto updateScheduleDto)
        {
            var schedule = await _scheduleService.UpdateScheduleAsync(id, updateScheduleDto);
            
            if (schedule == null)
            {
                // Return 404 jika course tidak ditemukan
                return NotFound(ApiResponse<object>.ErrorResult($"Schedule with ID {id} not found"));
            }

            return Ok(ApiResponse<ScheduleDto>.SuccessResult(schedule));

        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            try
            {
                var result = await _scheduleService.DeleteScheduleAsync(id);
                if (!result)
                {
                    return NotFound($"Schedule with ID {id} not found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting schedule {ScheduleId}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}