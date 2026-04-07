using Microsoft.EntityFrameworkCore;
using SyncVerse.Application.Interfaces;
using SyncVerse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncVerse.Infrastructure.Repositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Message>> GetMessagesForChannelAsync(Guid channelId, int page, int pageSize)
        {
            return await _context.Messages
                .Where(m => m.ChannelId == channelId)
                .OrderByDescending(m => m.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(m => m.MessageReactions)
                .ToListAsync();
        }

        public async Task<Message?> GetByIdWithReactionsAsync(Guid id)
        {
            return await _context.Messages
                .Include(m => m.MessageReactions)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Message?> GetByIdWithChannelAsync(Guid id)
        {
            return await _context.Messages
                .Include(m => m.Channel)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Guid?> GetChannelIdForMessageAsync(Guid messageId)
        {
            return await _context.Messages
                .Where(m => m.Id == messageId)
                .Select(m => m.ChannelId)
                .FirstOrDefaultAsync();
        }
    }
}
