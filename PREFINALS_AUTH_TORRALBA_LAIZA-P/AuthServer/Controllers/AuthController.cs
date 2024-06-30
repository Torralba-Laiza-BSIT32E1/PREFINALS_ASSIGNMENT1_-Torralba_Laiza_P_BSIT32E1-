using AuthServer.Core;
using AuthServer.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AuthController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (await _userService.GetUserAsync(user.Username) != null)
                return Conflict("User already exists");

            if (await _userService.CreateUserAsync(user))
                return Ok("User created successfully");

            return BadRequest("Failed to create user");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await _userService.GetUserAsync(loginRequest.Username);
            if (user == null)
                return NotFound("User not found");


            if (loginRequest.Password != "your_password_validation_logic_here")
                return Unauthorized("Invalid credentials");

            var token = await _authService.GenerateJwtTokenAsync(user);
            return Ok(new { Token = token });
        }
    }
}