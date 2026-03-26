
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SyncVerse.API.Hubs;
using SyncVerse.Application.DTOs;
using SyncVerse.Application.Services;

namespace SyncVerse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IHubContext<ChatHub> _hubContext;
        public MessageController(IMessageService messageService, IHubContext<ChatHub> hubContext)
        {
            _messageService = messageService;
            _hubContext = hubContext;
        }

        [HttpGet("channel/{channelId}")]
        public async Task<IActionResult> GetByChannelId(Guid channelId)
        {
            var messages = await _messageService.GetByChannelIdAsync(channelId);
            return Ok(messages);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(Guid id)
        {
            var message = await _messageService.GetByIdAsync(id);
            if (message == null) return NotFound();
            return Ok(message);
        }


        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CreateMessageDto dto)
        {
            var message = await _messageService.CreateAsync(dto);
            await _hubContext.Clients.Group(message.ChannelId.ToString()).SendAsync("ReceiveMessage", message);
            return CreatedAtAction(nameof(GetById), new { id = message.Id }, message);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _messageService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
