using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncVerse.Application.Interfaces;
using SyncVerse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SyncVerse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ServerController : ControllerBase
    {
        private readonly IServerRepository _serverRepository;

        public ServerController(IServerRepository serverRepository)
        {
            _serverRepository = serverRepository;
        }

        [HttpGet]

        public async Task<IEnumerable<Server>> GetMyServers()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _serverRepository.GetServersForUserAsync(userId);
        }

        [HttpPost]

        public async Task<IActionResult> Create(Server server)
        {
            await _serverRepository.AddAsync(server);
            return Ok(server);
        }

    }
}