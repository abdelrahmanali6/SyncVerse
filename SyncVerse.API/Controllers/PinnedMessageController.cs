using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncVerse.Application.DTOs;
using SyncVerse.Application.Services;
using System.Security.Claims;

namespace SyncVerse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PinnedMessageController : ControllerBase
    {
        private readonly IPinnedMessageService _pinnedService;

        public PinnedMessageController(IPinnedMessageService pinnedService)
        {
            _pinnedService = pinnedService;
        }

        [HttpPost("{channelId}/pin/{messageId}")]
        public async Task<IActionResult> PinMessage(Guid channelId, Guid messageId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var pinned = await _pinnedService.PinMessageAsync(messageId, channelId, userId);
            return Ok(pinned);
        }

        [HttpDelete("{channelId}/unpin/{messageId}")]
        public async Task<IActionResult> UnpinMessage(Guid channelId, Guid messageId)
        {
            var result = await _pinnedService.UnpinMessageAsync(messageId, channelId);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("{channelId}/pinned")]
        public async Task<IActionResult> GetPinnedMessages(Guid channelId)
        {
            var pinned = await _pinnedService.GetPinnedMessagesAsync(channelId);
            return Ok(pinned);
        }
    }
}
