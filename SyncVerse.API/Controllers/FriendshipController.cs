using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncVerse.Application.Services;
using System.Security.Claims;

namespace SyncVerse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FriendshipController : ControllerBase
    {
        private readonly IFriendshipService _friendshipService;

        public FriendshipController(IFriendshipService friendshipService)
        {
            _friendshipService = friendshipService;
        }

        [HttpPost("request/{addresseeId}")]
        public async Task<IActionResult> SendRequest(string addresseeId)
        {
            var requesterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(requesterId)) return Unauthorized();

            var request = await _friendshipService.SendFriendRequestAsync(requesterId, addresseeId);
            return Ok(request);
        }

        [HttpPost("accept/{requestId}")]
        public async Task<IActionResult> AcceptRequest(Guid requestId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var accepted = await _friendshipService.AcceptFriendRequestAsync(requestId, userId);
            if (!accepted) return BadRequest();
            return NoContent();
        }

        [HttpPost("decline/{requestId}")]
        public async Task<IActionResult> DeclineRequest(Guid requestId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var declined = await _friendshipService.DeclineFriendRequestAsync(requestId, userId);
            if (!declined) return BadRequest();
            return NoContent();
        }

        [HttpGet("requests")]
        public async Task<IActionResult> GetRequests()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var requests = await _friendshipService.GetFriendRequestsAsync(userId);
            return Ok(requests);
        }

        [HttpGet("friends")]
        public async Task<IActionResult> GetFriends()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var friends = await _friendshipService.GetFriendsAsync(userId);
            return Ok(friends);
        }
    }
}