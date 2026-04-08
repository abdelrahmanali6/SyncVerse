using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncVerse.Application.DTOs;
using SyncVerse.Application.Services;

namespace SyncVerse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SearchController : ControllerBase
    {
        private readonly IMessageSearchService _searchService;

        public SearchController(IMessageSearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost("messages")]
        public async Task<IActionResult> SearchMessages([FromBody] MessageSearchRequestDto request)
        {
            var results = await _searchService.SearchMessagesAsync(request);
            return Ok(results);
        }
    }
}
