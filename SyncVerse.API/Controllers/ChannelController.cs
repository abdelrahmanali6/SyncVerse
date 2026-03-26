using Microsoft.AspNetCore.Mvc;
using SyncVerse.Application.DTOs;
using SyncVerse.Application.Services;

namespace SyncVerse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChannelController : ControllerBase
    {
        private readonly IChannelService _channelService;
        public ChannelController(IChannelService channelService)
        {
            _channelService = channelService;
        }

        [HttpGet("server/{serverId}")]
        public async Task<IActionResult> GetByServerId(Guid serverId)
        {
            var channels = await _channelService.GetByServerIdAsync(serverId);
            return Ok(channels);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(Guid id)
        {
            var channel = await _channelService.GetByIdAsync(id);
            if (channel == null) return NotFound();
            return Ok(channel);
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CreateChannelDto dto)
        {
            var channel = await _channelService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = channel.Id }, channel);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateChannelDto dto)
        {
            var result = await _channelService.UpdateAsync(id, dto);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _channelService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
