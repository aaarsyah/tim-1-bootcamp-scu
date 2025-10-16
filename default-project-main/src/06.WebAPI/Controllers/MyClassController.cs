using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.WebAPI.Configuration;
using MyApp.WebAPI.Exceptions;
using MyApp.WebAPI.Models;
using MyApp.Shared.DTOs;
using MyApp.WebAPI.Services;
using System.Security.Claims;


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
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<MyClassDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MyClassDto>>> GetOwnMyClasses()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new AuthenticationException("Token is invalid");
            }
            try
            {
                var myclass = await _myclassService.GetMyClassesByUserIdAsync(userId);
                return Ok(myclass);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving myclass");
                return StatusCode(500, "Internal server error");
            }
        }

      
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<MyClassDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<MyClassDto>>> GetMyClass(int id)
        {
            var result = await _myclassService.GetMyClassByIdAsync(id);
            return Ok(ApiResponse<MyClassDto>.SuccessResult(result));
        }

      
        [HttpPost]
        [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
        [ProducesResponseType(typeof(ApiResponse<MyClassDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MyClassDto>> CreateMyClass(CreateMyClassDto createMyClassDto)
        {
            var result = await _myclassService.CreateMyClassAsync(createMyClassDto);
            return CreatedAtAction(nameof(GetMyClass), new { id = result.Id }, ApiResponse<MyClassDto>.SuccessResult(result));
        }

      
        [HttpPut("{id}")]
        [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
        [ProducesResponseType(typeof(ApiResponse<MyClassDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MyClassDto>> UpdateMyClass(int id, UpdateMyClassDto updateMyClassDto)
        {
            var result = await _myclassService.UpdateMyClassAsync(id, updateMyClassDto);
            return Ok(ApiResponse<MyClassDto>.SuccessResult(result));
        }


        [HttpDelete("{id}")]
        [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<object>>> DeleteMyClass(int id)
        {
            var result = await _myclassService.DeleteMyClassAsync(id);
            if (!result)
            {
                return NotFound(ApiResponse<object>.ErrorResult($"MyClass with ID {id} not found"));
            }
            return Ok(ApiResponse<object>.SuccessResult());
        }
    }
}