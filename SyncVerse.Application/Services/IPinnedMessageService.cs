using SyncVerse.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyncVerse.Application.Services
{
    public interface IPinnedMessageService
    {
        Task<PinnedMessageDto> PinMessageAsync(Guid messageId, Guid channelId, string userId);
        Task<bool> UnpinMessageAsync(Guid messageId, Guid channelId);
        Task<IEnumerable<PinnedMessageDto>> GetPinnedMessagesAsync(Guid channelId);
    }
}
