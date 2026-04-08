using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SyncVerse.Application.DTOs;
using SyncVerse.Application.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SyncVerse.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> RegisterAsync(UserRegisterDto dto)
        {
            var user = new ApplicationUser { UserName = dto.UserName, Email = dto.Email };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded) return string.Join(";", result.Errors);
            return await GenerateJwtToken(user);
        }

        public async Task<string> LoginAsync(UserLoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null) return "Invalid credentials";
            if (!await _userManager.CheckPasswordAsync(user, dto.Password)) return "Invalid credentials";
            return await GenerateJwtToken(user);
        }

        public async Task<UserProfileDto?> GetProfileAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;
            return new UserProfileDto
            {
                Id = user.Id,
                UserName = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                AvatarUrl = user.AvatarUrl,
                Bio = user.Bio,
                Badge = user.Badge,
                IsVerified = user.IsVerified,
                IsOnline = false
            };
        }

        public async Task<bool> UpdateProfileAsync(string userId, UpdateUserProfileDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            if (!string.IsNullOrWhiteSpace(dto.Bio))
                user.Bio = dto.Bio;

            if (dto.AvatarUrl != null)
                user.AvatarUrl = dto.AvatarUrl;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secret = jwtSettings["Secret"] ?? throw new InvalidOperationException("JWT Secret is not configured.");
            var issuer = jwtSettings["Issuer"] ?? throw new InvalidOperationException("JWT Issuer is not configured.");
            var audience = jwtSettings["Audience"] ?? throw new InvalidOperationException("JWT Audience is not configured.");
            var expiryMinutesStr = jwtSettings["ExpiryMinutes"] ?? throw new InvalidOperationException("JWT ExpiryMinutes is not configured.");
            if (!int.TryParse(expiryMinutesStr, out var expiryMinutes))
                throw new InvalidOperationException("JWT ExpiryMinutes is not a valid integer.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName ?? string.Empty)
            };
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
