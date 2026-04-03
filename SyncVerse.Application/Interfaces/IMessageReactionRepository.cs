using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SyncVerse.Domain.Entities;

namespace SyncVerse.Application.Interfaces
{
    public interface IMessageReactionRepository : IRepository<MessageReaction>
    {
        Task<IEnumerable<MessageReaction>> GetReactionsForMessageAsync(Guid messageId);
        Task<MessageReaction?> GetReactionAsync(Guid messageId, string userId, string emoji);
        Task<bool> RemoveReactionAsync(Guid messageId, string userId, string emoji);
    }
}