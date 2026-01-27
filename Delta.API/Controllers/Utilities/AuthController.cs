using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Delta.Application.DTOs.Utilities;
using Delta.Application.Interfaces.Utilities;

namespace Delta.API.Controllers.Utilities
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        // 🔓 Login does NOT require JWT
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.AuthenticateAsync(loginDto);

            if (result == null)
                return Unauthorized(new { message = "Invalid credentials" });

            return Ok(result);
        }
    }
}
