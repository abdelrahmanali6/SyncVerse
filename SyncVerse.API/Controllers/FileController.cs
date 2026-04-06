using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyncVerse.Application.DTOs;
using SyncVerse.Application.Services;
using System.Security.Claims;

namespace SyncVerse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FileController : ControllerBase
    {
        private readonly IAppFileService _fileService;

        public FileController(IAppFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            if (file == null || file.Length == 0) return BadRequest("No file uploaded");

            var result = await _fileService.UploadFileAsync(file.OpenReadStream(), file.FileName, file.ContentType, file.Length, userId);
            return CreatedAtAction(nameof(GetByUser), new { userId }, result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(string userId)
        {
            var files = await _fileService.GetFilesByUserAsync(userId);
            return Ok(files);
        }
    }
}