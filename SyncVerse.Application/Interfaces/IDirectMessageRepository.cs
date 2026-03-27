using SyncVerse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyncVerse.Application.Interfaces
{
    public interface IDirectMessageRepository : IRepository<DirectMessage>
    {
        Task<IEnumerable<DirectMessage>> GetDirectMessagesAsync(string userId1, string userId2, int page, int pageSize);
    }
}
