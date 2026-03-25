using System.Threading.Tasks;
using SyncVerse.Application.DTOs;

namespace SyncVerse.Application.Services
{
    public interface IUserService
    {
        Task<string> RegisterAsync(UserRegisterDto dto);
        Task<string> LoginAsync(UserLoginDto dto);
        Task<UserProfileDto?> GetProfileAsync(string userId);
    }
}
