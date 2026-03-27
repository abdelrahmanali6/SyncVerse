using SyncVerse.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyncVerse.Application.Services
{
    public interface IDirectMessageService
    {
        Task<IEnumerable<DirectMessageDto>> GetConversationAsync(string userId1, string userId2, int page = 1, int pageSize = 50);
        Task<DirectMessageDto> SendAsync(string senderId, CreateDirectMessageDto dto);
    }
}
