using Microsoft.EntityFrameworkCore;
using SyncVerse.Application.Interfaces;
using SyncVerse.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncVerse.Infrastructure.Repositories
{
    public class ServerRepository : Repository<Server>, IServerRepository
    {
        public ServerRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Server>> GetServersForUserAsync(string userId)
        {
            return await _context.Servers
                .Include(s => s.Members)
                .Where(s => s.Members.Any(m => m.UserId == userId))
                .ToListAsync();
        }
    }
}
