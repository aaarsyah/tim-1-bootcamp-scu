using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Shared.DTOs;
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
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<UserDto>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserDto>>>> GetAllUsers()
        {
            var user = await _userManagementService.GetAllUsersAsync();
            
            return Ok(ApiResponse<IEnumerable<UserDto>>.SuccessResult(user));
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
            return Ok(ApiResponse<UserDto>.SuccessResult(user));
        }

        [HttpGet("roles")]
        [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<RoleDto>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IEnumerable<RoleDto>>>> GetAllRoles()
        {
            var user = await _userManagementService.GetAllRolesAsync();
            return Ok(ApiResponse<IEnumerable<RoleDto>>.SuccessResult(user));
        }

        /// <summary>
        /// Assign Role to User
        /// POST /api/usermanagement/users/{userId}/roles
        /// Requires: Admin role
        /// </summary>
        [HttpPut("users/{userId}/roles/add")]
        [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<object>>> AssignRole(int userId, [FromBody] RoleRequestDto request)
        {
            var result = await _userManagementService.AddRoleToUserAsync(userId, request.RoleName);
            
            return Ok(ApiResponse<object>.SuccessResult());
        }

        /// <summary>
        /// Remove Role from User
        /// DELETE /api/usermanagement/users/{userId}/roles/{roleName}
        /// Requires: Admin role
        /// </summary>
        [HttpPut("users/{userId}/roles/remove")]
        [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<object>>> RemoveRole(int userId, [FromBody] RoleRequestDto request)
        {
            var result = await _userManagementService.RemoveRoleFromUserAsync(userId, request.RoleName);

            return Ok(ApiResponse<object>.SuccessResult());
        }

        /// <summary>
        /// Add Claim to User
        /// POST /api/usermanagement/users/{userId}/claims
        /// Requires: Admin role
        /// </summary>
        [HttpPut("users/{userId}/claims/add")]
        [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<object>>> SetClaim(int userId, [FromBody] ClaimDto claim)
        {
            var result = await _userManagementService.SetClaimForUserAsync(userId, claim.Type, claim.Value);

            return Ok(ApiResponse<object>.SuccessResult());
        }

        /// <summary>
        /// Remove Claim from User
        /// DELETE /api/usermanagement/users/{userId}/claims
        /// Requires: Admin role
        /// </summary>
        [HttpPut("users/{userId}/claims/remove")]
        [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<object>>> RemoveClaim(int userId, [FromBody] ClaimDto claim)
        {
            var result = await _userManagementService.RemoveClaimFromUserAsync(userId, claim.Type);

            return Ok(ApiResponse<object>.SuccessResult());
        }

        /// <summary>
        /// Activate User
        /// DELETE /api/usermanagement/users/{userId}
        /// Requires: Admin role
        /// </summary>
        [HttpPut("users/{userId}/activate")]
        [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<object>>> ActivateUser(int userId)
        {
            var result = await _userManagementService.ActivateUserAsync(userId);

            return Ok(ApiResponse<object>.SuccessResult());
        }
        /// <summary>
        /// Deactivate User
        /// DELETE /api/usermanagement/users/{userId}
        /// Requires: Admin role
        /// </summary>
        [HttpPut("users/{userId}/deactivate")]
        [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<object>>> DeactivateUser(int userId)
        {
            var result = await _userManagementService.DeactivateUserAsync(userId);

            return Ok(ApiResponse<object>.SuccessResult());
        }
    }
}
