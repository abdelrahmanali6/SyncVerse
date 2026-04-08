using Microsoft.EntityFrameworkCore;
using SyncVerse.Application.Interfaces;
using SyncVerse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncVerse.Infrastructure.Repositories
{
    public class PinnedMessageRepository : Repository<PinnedMessage>, IPinnedMessageRepository
    {
        public PinnedMessageRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<PinnedMessage>> GetPinnedMessagesForChannelAsync(Guid channelId)
        {
            return await _context.Set<PinnedMessage>()
                .Include(pm => pm.Message)
                .Where(pm => pm.ChannelId == channelId)
                .OrderByDescending(pm => pm.PinnedAt)
                .ToListAsync();
        }

        public async Task<PinnedMessage?> GetPinAsync(Guid messageId, Guid channelId)
        {
            return await _context.Set<PinnedMessage>()
                .FirstOrDefaultAsync(pm => pm.MessageId == messageId && pm.ChannelId == channelId);
        }
    }
}
