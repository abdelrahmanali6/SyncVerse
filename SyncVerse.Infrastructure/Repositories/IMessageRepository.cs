using SyncVerse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using SyncVerse.Application.Interfaces;

namespace SyncVerse.Infrastructure.Repositories
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<IEnumerable<Message>> GetMessagesForChannelAsync(Guid channelId, int page, int pageSize);
    }
}
