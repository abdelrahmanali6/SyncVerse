using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncVerse.Application.DTOs;
using SyncVerse.Application.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SyncVerse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DirectMessageController : ControllerBase
    {
        private readonly IDirectMessageService _dmService;
        public DirectMessageController(IDirectMessageService dmService)
        {
            _dmService = dmService;
        }

        [HttpGet("with/{userId}")]
        public async Task<IActionResult> GetConversation(string userId, int page = 1, int pageSize = 50)
        {
            var myId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (myId == null) return Unauthorized();
            var messages = await _dmService.GetConversationAsync(myId, userId, page, pageSize);
            return Ok(messages);
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] CreateDirectMessageDto dto)
        {
            var myId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (myId == null) return Unauthorized();
            var message = await _dmService.SendAsync(myId, dto);
            return Ok(message);
        }
    }
}
