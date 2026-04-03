using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SyncVerse.API.Hubs;
using SyncVerse.Application.DTOs;
using SyncVerse.Application.Interfaces;
using SyncVerse.Application.Services;
using System.Security.Claims;

namespace SyncVerse.API.Controllers
{
    [ApiController]
    [Route("api/message/{messageId}/reactions")]
    [Authorize]
    public class MessageReactionController : ControllerBase
    {
        private readonly IMessageReactionService _reactionService;
        private readonly IMessageRepository _messageRepository;
        private readonly IHubContext<ChatHub> _hubContext;

        public MessageReactionController(IMessageReactionService reactionService, IMessageRepository messageRepository, IHubContext<ChatHub> hubContext)
        {
            _reactionService = reactionService;
            _messageRepository = messageRepository;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetReactions(Guid messageId)
        {
            var reactions = await _reactionService.GetReactionsForMessageAsync(messageId);
            return Ok(reactions);
        }

        [HttpPost]
        public async Task<IActionResult> AddReaction(Guid messageId, [FromBody] CreateMessageReactionDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var reaction = await _reactionService.AddReactionAsync(messageId, userId, dto);
            var channelId = await _messageRepository.GetChannelIdForMessageAsync(messageId);
            if (channelId.HasValue)
            {
                await _hubContext.Clients.Group(channelId.Value.ToString()).SendAsync("ReceiveReaction", reaction);
            }
            return CreatedAtAction(nameof(GetReactions), new { messageId }, reaction);
        }

        [HttpDelete("{emoji}")]
        public async Task<IActionResult> RemoveReaction(Guid messageId, string emoji)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var result = await _reactionService.RemoveReactionAsync(messageId, userId, emoji);
            if (!result) return NotFound();
            var channelId = await _messageRepository.GetChannelIdForMessageAsync(messageId);
            if (channelId.HasValue)
            {
                await _hubContext.Clients.Group(channelId.Value.ToString()).SendAsync("RemoveReaction", new { MessageId = messageId, UserId = userId, Emoji = emoji });
            }
            return NoContent();
        }
    }
}