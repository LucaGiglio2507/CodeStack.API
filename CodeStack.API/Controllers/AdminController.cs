using CodeStack.API.Dtos.Responses;
using CodeStack.Core.Interfaces.Services;
using CodeStack.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CodeStack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    /// <summary>
    /// Restricted to the Administrator role; exposes a single endpoint to disable a user account.
    /// </summary>
    public class AdminController(IAdminService _adminService) : ControllerBase
    {

        [HttpDelete("users/{id:guid}")]
        public async Task<IActionResult> DisableUser(Guid id)
        {
            string? adminIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(adminIdClaim, out Guid adminId)) return Unauthorized();

            User? user = await _adminService.DisableUserAsync(id, adminId);

            if (user == null)
            {
                return BadRequest("Action impossible.");
            }

            UserDisableResponseDto response = user.ToDisableResponseDto();

            return Ok(response);
        }
    }
}
