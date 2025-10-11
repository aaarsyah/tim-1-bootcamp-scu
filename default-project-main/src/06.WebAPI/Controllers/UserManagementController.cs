using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.WebAPI.Models.DTOs;
using MyApp.WebAPI.Services;
using MyApp.WebAPI.Models;
using MyApp.WebAPI.Configuration;

namespace MyApp.WebAPI.Controllers
{
    /// <summary>
    /// User Management Controller
    /// Endpoint untuk Admin/Manager mengelola users:
    /// - List users
    /// - Get user detail
    /// - Assign/Remove roles
    /// - Add/Remove claims
    /// - Deactivate users
    /// 
    /// Semua endpoints butuh authentication dan authorization
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Semua endpoints butuh authentication
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;
        private readonly ILogger<UserManagementController> _logger;

        public UserManagementController(
            IUserManagementService userManagementService,
            ILogger<UserManagementController> logger)
        {
            _userManagementService = userManagementService;
            _logger = logger;
        }

        /// <summary>
        /// Get All Users (Paginated)
        /// GET /api/usermanagement/users?page=1&pageSize=10
        /// Requires: Admin role
        /// </summary>
        [HttpGet("users")]
        [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
        [ProducesResponseType(typeof(ApiResponse<List<UserDto>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<List<UserDto>>>> GetAllUsers(
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10)
        {
            var user = await _userManagementService.GetAllUsersAsync(page, pageSize);
            
            return Ok(ApiResponse<List<UserDto>>.SuccessResult(user));
        }

        /// <summary>
        /// Get User by ID
        /// GET /api/usermanagement/users/{userId}
        /// Requires: Admin role or higher
        /// </summary>
        [HttpGet("users/{userId}")]
        [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
        [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<UserDto>>> GetUser(int userId)
        {
            var user = await _userManagementService.GetUserProfileAsync(userId);
            if (user == null)
            {
                return NotFound(ApiResponse<object>.ErrorResult("User not found"));
            }

            return Ok(ApiResponse<UserDto>.SuccessResult(user));
        }

        /// <summary>
        /// Assign Role to User
        /// POST /api/usermanagement/users/{userId}/roles
        /// Requires: Admin role
        /// </summary>
        [HttpPost("users/{userId}/roles")]
        [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<bool>>> AssignRole(
            int userId, 
            [FromBody] AssignRoleRequestDto request)
        {
            var result = await _userManagementService.AssignRoleToUserAsync(userId, request.RoleName);
            
            return Ok(ApiResponse<bool>.SuccessResult(result));
        }

        /// <summary>
        /// Remove Role from User
        /// DELETE /api/usermanagement/users/{userId}/roles/{roleName}
        /// Requires: Admin role
        /// </summary>
        [HttpDelete("users/{userId}/roles/{roleName}")]
        [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<bool>>> RemoveRole(int userId, string roleName)
        {
            var result = await _userManagementService.RemoveRoleFromUserAsync(userId, roleName);
            
            return Ok(ApiResponse<bool>.SuccessResult(result));
        }

        /// <summary>
        /// Add Claim to User
        /// POST /api/usermanagement/users/{userId}/claims
        /// Requires: Admin role
        /// </summary>
        [HttpPost("users/{userId}/claims")]
        [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<bool>>> AddClaim(
            int userId, 
            [FromBody] AddClaimRequestDto request)
        {
            var result = await _userManagementService.AddClaimToUserAsync(userId, request.ClaimType, request.ClaimValue);
            
            return Ok(ApiResponse<bool>.SuccessResult(result));
        }

        /// <summary>
        /// Remove Claim from User
        /// DELETE /api/usermanagement/users/{userId}/claims
        /// Requires: Admin role
        /// </summary>
        [HttpDelete("users/{userId}/claims")]
        [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<bool>>> RemoveClaim(
            int userId, 
            [FromBody] ClaimDto claim)
        {
            var result = await _userManagementService.RemoveClaimFromUserAsync(userId, claim.Type, claim.Value);
            
            return Ok(ApiResponse<bool>.SuccessResult(result));
        }

        /// <summary>
        /// Deactivate User
        /// DELETE /api/usermanagement/users/{userId}
        /// Requires: Admin role
        /// </summary>
        [HttpDelete("users/{userId}")]
        [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<bool>>> DeactivateUser(int userId)
        {
            var result = await _userManagementService.DeactivateUserAsync(userId);
            
            return Ok(ApiResponse<bool>.SuccessResult(result));
        }
    }
}
