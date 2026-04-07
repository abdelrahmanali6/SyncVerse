using Microsoft.EntityFrameworkCore;
using SyncVerse.Application.Interfaces;
using SyncVerse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncVerse.Infrastructure.Repositories
{
    public class ServerBanRepository : Repository<ServerBan>, IServerBanRepository
    {
        public ServerBanRepository(AppDbContext context) : base(context) { }

        public async Task<ServerBan?> GetActiveBanAsync(Guid serverId, string userId)
        {
            return await _context.Set<ServerBan>()
                .FirstOrDefaultAsync(b => b.ServerId == serverId && b.UserId == userId && b.IsActive);
        }

        public async Task<IEnumerable<ServerBan>> GetBansForServerAsync(Guid serverId)
        {
            return await _context.Set<ServerBan>()
                .Where(b => b.ServerId == serverId)
                .OrderByDescending(b => b.BannedAt)
                .ToListAsync();
        }
    }
}