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
    public class AdminController(IAdminService _adminService) : ControllerBase
    {

        [HttpDelete("users/{id:guid}")]
        public async Task<IActionResult> DisableUser(Guid id)
        {
            var adminIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(adminIdClaim, out Guid adminId)) return Unauthorized();
            //var adminId = Guid.Parse("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d");

            var user = await _adminService.DisableUserAsync(id, adminId);

            if (user == null)
            {
                return BadRequest("Action impossible.");
            }

            var response = user.ToDisableResponseDto();

            return Ok(response);
        }
    }
}
