using Microsoft.EntityFrameworkCore;
using SyncVerse.Application.Interfaces;
using SyncVerse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncVerse.Infrastructure.Repositories
{
    public class MessageReactionRepository : Repository<MessageReaction>, IMessageReactionRepository
    {
        public MessageReactionRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<MessageReaction>> GetReactionsForMessageAsync(Guid messageId)
        {
            return await _context.MessageReactions
                .Where(r => r.MessageId == messageId)
                .ToListAsync();
        }

        public async Task<MessageReaction?> GetReactionAsync(Guid messageId, string userId, string emoji)
        {
            return await _context.MessageReactions
                .FirstOrDefaultAsync(r => r.MessageId == messageId && r.UserId == userId && r.Emoji == emoji);
        }

        public async Task<bool> RemoveReactionAsync(Guid messageId, string userId, string emoji)
        {
            var reaction = await GetReactionAsync(messageId, userId, emoji);
            if (reaction == null) return false;

            _context.MessageReactions.Remove(reaction);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}