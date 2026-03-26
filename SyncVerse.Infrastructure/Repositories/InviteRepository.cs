using Microsoft.EntityFrameworkCore;
using SyncVerse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncVerse.Infrastructure.Repositories
{
    public class InviteRepository : Repository<Invite>, IInviteRepository
    {
        public InviteRepository(AppDbContext context) : base(context) { }

        public async Task<Invite?> GetByCodeAsync(string code)
        {
            return await _dbSet.FirstOrDefaultAsync(i => i.Code == code);
        }

        public async Task<IEnumerable<Invite>> GetInvitesForServerAsync(Guid serverId)
        {
            return await _dbSet.Where(i => i.ServerId == serverId).ToListAsync();
        }
    }
}
