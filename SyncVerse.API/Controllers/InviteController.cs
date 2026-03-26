using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncVerse.Application.DTOs;
using SyncVerse.Application.Services;
using System;
using System.Threading.Tasks;

namespace SyncVerse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InviteController : ControllerBase
    {
        private readonly IInviteService _inviteService;
        public InviteController(IInviteService inviteService)
        {
            _inviteService = inviteService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInviteDto dto)
        {
            var invite = await _inviteService.CreateAsync(dto);
            return Ok(invite);
        }

        [HttpPost("accept")]
        [AllowAnonymous]
        public async Task<IActionResult> Accept([FromBody] AcceptInviteDto dto)
        {
            var result = await _inviteService.AcceptAsync(dto);
            if (!result) return BadRequest("Invalid or expired invite");
            return Ok();
        }

        [HttpGet("server/{serverId}")]
        public async Task<IActionResult> GetForServer(Guid serverId)
        {
            var invites = await _inviteService.GetInvitesForServerAsync(serverId);
            return Ok(invites);
        }

        [HttpGet("{code}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByCode(string code)
        {
            var invite = await _inviteService.GetByCodeAsync(code);
            if (invite == null) return NotFound();
            return Ok(invite);
        }
    }
}
