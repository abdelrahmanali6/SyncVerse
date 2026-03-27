using Microsoft.EntityFrameworkCore;
using SyncVerse.Application.Interfaces;
using SyncVerse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncVerse.Infrastructure.Repositories
{
    public class DirectMessageRepository : Repository<DirectMessage>, IDirectMessageRepository
    {
        public DirectMessageRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<DirectMessage>> GetDirectMessagesAsync(string userId1, string userId2, int page, int pageSize)
        {
            return await _context.DirectMessages
                .Where(dm => (dm.SenderId == userId1 && dm.RecipientId == userId2) || (dm.SenderId == userId2 && dm.RecipientId == userId1))
                .OrderByDescending(dm => dm.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
