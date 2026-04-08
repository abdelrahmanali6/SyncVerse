using SyncVerse.Application.DTOs;
using System.Threading.Tasks;

namespace SyncVerse.Application.Services
{
    public interface IMessageSearchService
    {
        Task<MessageSearchResultDto> SearchMessagesAsync(MessageSearchRequestDto request);
    }
}
