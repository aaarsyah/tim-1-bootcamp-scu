using Microsoft.AspNetCore.Mvc;
using MyApp.WebAPI.DTOs;
using MyApp.WebAPI.Models;
using MyApp.WebAPI.Services;

namespace MyApp.WebAPI.Controllers
{
 
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MyClassController : ControllerBase
    {
        private readonly IMyClassService _myclassService;
        private readonly ILogger<MyClassController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public MyClassController(IMyClassService myclassService, ILogger<MyClassController> logger)
        {
            _myclassService = myclassService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MyClassDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MyClassDto>>> GetMyClass()
        {
            try
            {
                var myclass = await _myclassService.GetAllMyClassAsync();
                return Ok(myclass);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving myclass");
                return StatusCode(500, "Internal server error");
            }
        }

      
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MyClassDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MyClassDto>> GetMyClass(int id)
        {
            try
            {
                var myclass = await _myclassService.GetMyClassByIdAsync(id);
                if (myclass == null)
                {
                    return NotFound($"MyClass with ID {id} not found");
                }
                return Ok(myclass);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving myclass {MyClassId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

      
        [HttpPost]
        [ProducesResponseType(typeof(MyClassDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MyClassDto>> CreateMyClass(CreateMyClassDto createMyClassDto)
        {
            var myclass = await _myclassService.CreateMyClassAsync(createMyClassDto);
            var response = ApiResponse<MyClassDto>.SuccessResult(myclass, "MyClass created successfully");
            return CreatedAtAction(nameof(GetMyClass), new { id = myclass.Id }, response);
        }

      
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(MyClassDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MyClassDto>> UpdateMyClass(int id, UpdateMyClassDto updateMyClassDto)
        {
            try
            {
                var myclass = await _myclassService.UpdateMyClassAsync(id, updateMyClassDto);
                if (myclass == null)
                {
                    return NotFound($"MyClass with ID {id} not found");
                }
                return Ok(myclass);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating myclas {MyClassMyClassId}", id);
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMyClass(int id)
        {
            try
            {
                var result = await _myclassService.DeleteMyClassAsync(id);
                if (!result)
                {
                    return NotFound($"MyClass with ID {id} not found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting myclass {MyClassId}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}