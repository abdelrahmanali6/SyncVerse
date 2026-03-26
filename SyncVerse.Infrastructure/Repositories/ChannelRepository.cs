using Microsoft.EntityFrameworkCore;
using SyncVerse.Application.Interfaces;
using SyncVerse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncVerse.Infrastructure.Repositories
{
    public class ChannelRepository : Repository<Channel>, IChannelRepository
    {
        public ChannelRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Channel>> GetChannelsForServerAsync(Guid serverId)
        {
            return await _context.Channels
                .Where(c => c.ServerId == serverId)
                .ToListAsync();
        }
    }
}
