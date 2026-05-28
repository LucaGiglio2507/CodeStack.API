using CodeStack.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CodeStack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService _userService) : ControllerBase
    {
        /// <summary>
        /// Updates the user's password after verifying the current one.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <param name="dto">The data transfer object containing the current and new passwords.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the update.</returns>
        /// <response code="200">Password updated successfully.</response>
        /// <response code="401">The current password is incorrect.</response>
        /// <response code="404">User not found.</response>
        [HttpPatch("passwordChange/{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PasswordChange(Guid id, [FromBody] PasswordRequestUpdateDto dto)
        {
            try
            {
                var entity = await _userService.GetByIdAsync(id);
                if (entity is null)
                {
                    return NotFound();
                }
                await _userService.UpdatePassword(id, dto.CurrentPassword, dto.NewPassword);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AccessViolationException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred : {ex.Message}");
            }
        }
    }
}
