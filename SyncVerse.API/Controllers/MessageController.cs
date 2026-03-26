using Microsoft.AspNetCore.Mvc;
using SyncVerse.Application.DTOs;
using SyncVerse.Application.Services;

namespace SyncVerse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
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
