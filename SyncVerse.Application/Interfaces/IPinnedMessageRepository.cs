using SyncVerse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyncVerse.Application.Interfaces
{
    public interface IPinnedMessageRepository : IRepository<PinnedMessage>
    {
        Task<IEnumerable<PinnedMessage>> GetPinnedMessagesForChannelAsync(Guid channelId);
        Task<PinnedMessage?> GetPinAsync(Guid messageId, Guid channelId);
    }
}
