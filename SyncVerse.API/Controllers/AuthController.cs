using Microsoft.AspNetCore.Mvc;
using SyncVerse.Application.DTOs;
using SyncVerse.Application.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SyncVerse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            var result = await _userService.RegisterAsync(dto);
            if (result.StartsWith("Invalid") || result.Contains(";"))
                return BadRequest(result);
            return Ok(new { token = result });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var result = await _userService.LoginAsync(dto);
            if (result.StartsWith("Invalid"))
                return Unauthorized(result);
            return Ok(new { token = result });
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();
            var profile = await _userService.GetProfileAsync(userId);
            if (profile == null) return NotFound();
            return Ok(profile);
        }

        [Authorize]
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileDto dto)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();
            var result = await _userService.UpdateProfileAsync(userId, dto);
            if (!result) return BadRequest();
            return NoContent();
        }
    }
}