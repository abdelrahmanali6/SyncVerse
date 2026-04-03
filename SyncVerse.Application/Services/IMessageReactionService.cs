using SyncVerse.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyncVerse.Application.Services
{
    public interface IMessageReactionService
    {
        Task<IEnumerable<MessageReactionDto>> GetReactionsForMessageAsync(Guid messageId);
        Task<MessageReactionDto> AddReactionAsync(Guid messageId, string userId, CreateMessageReactionDto dto);
        Task<bool> RemoveReactionAsync(Guid messageId, string userId, string emoji);
    }
}