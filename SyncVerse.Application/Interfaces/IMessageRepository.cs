using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SyncVerse.Domain.Entities;

namespace SyncVerse.Application.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<IEnumerable<Message>> GetMessagesForChannelAsync(Guid channelId, int page, int pageSize);
    }
}
