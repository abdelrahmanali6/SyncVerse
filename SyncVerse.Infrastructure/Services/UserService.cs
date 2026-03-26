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
                UserName = user.UserName,
                Email = user.Email,
                AvatarUrl = null,
                IsOnline = false
            };
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName ?? "")
            };
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpiryMinutes"])),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
