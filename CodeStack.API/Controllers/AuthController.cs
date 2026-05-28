using CodeStack.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeStack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService _authService,
        IJwtService _jwtService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto _loginRequest)
        {
            try
            {
                User user = await _authService.LoginAsync(_loginRequest.Email, _loginRequest.HashedPassword);
                string token = _jwtService.GenerateToken(user);
                return Ok(new LoginResponseDto
                {
                    Token = token,
                    IsActive = user.IsActive
                });
            }

            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { ex.Message });
            }
        }
        /// <summary>
        /// Registers a new user in the system with a default password.
        /// This endpoint is restricted to users with the 'Administrator' role.
        /// </summary>
        /// <param name="_registerRequest">The user registration data including email, name, and role.</param>
        /// <returns>The newly created user details.</returns>
        /// <response code="200">Returns the created user object.</response>
        /// <response code="400">Returned if the email already exists, the role is invalid, or a validation error occurs.</response>
        /// <response code="401">Returned if the requester is not authenticated or does not have Administrator privileges.</response>
        [HttpPost("register")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Register(RegisterRequestDto _registerRequest)
        {
            try
            {
                User? user = await _authService.RegisterAsync(_registerRequest.Email, _registerRequest.FirstName,
                    _registerRequest.LastName, _registerRequest.Role);
                return Ok(user);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
