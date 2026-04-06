using Microsoft.EntityFrameworkCore;
using SyncVerse.Application.Interfaces;
using SyncVerse.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncVerse.Infrastructure.Repositories
{
    public class FriendshipRepository : Repository<Friendship>, IFriendshipRepository
    {
        public FriendshipRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Friendship>> GetFriendRequestsAsync(string userId)
        {
            return await _context.Friendships
                .Where(f => f.AddresseeId == userId && !f.IsAccepted)
                .ToListAsync();
        }

        public async Task<IEnumerable<Friendship>> GetFriendsAsync(string userId)
        {
            return await _context.Friendships
                .Where(f => f.IsAccepted && (f.RequesterId == userId || f.AddresseeId == userId))
                .ToListAsync();
        }
    }
}