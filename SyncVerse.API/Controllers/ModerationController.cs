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
    public class ModerationController : ControllerBase
    {
        private readonly IModerationService _moderationService;

        public ModerationController(IModerationService moderationService)
        {
            _moderationService = moderationService;
        }

        [HttpPost("kick/{serverId}/{targetUserId}")]
        public async Task<IActionResult> KickUser(Guid serverId, string targetUserId, [FromBody] ModerationRequestDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var result = await _moderationService.KickUserAsync(serverId, targetUserId, userId, request?.Reason);
            if (!result) return Forbid();
            return NoContent();
        }

        [HttpPost("ban/{serverId}/{targetUserId}")]
        public async Task<IActionResult> BanUser(Guid serverId, string targetUserId, [FromBody] ModerationRequestDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var result = await _moderationService.BanUserAsync(serverId, targetUserId, userId, request?.Reason);
            if (!result) return Forbid();
            return NoContent();
        }

        [HttpGet("logs/{serverId}")]
        public async Task<IActionResult> GetLogs(Guid serverId)
        {
            var logs = await _moderationService.GetAuditLogsAsync(serverId);
            return Ok(logs);
        }

        [HttpGet("bans/{serverId}")]
        public async Task<IActionResult> GetBans(Guid serverId)
        {
            var bans = await _moderationService.GetBansAsync(serverId);
            return Ok(bans);
        }
    }
}