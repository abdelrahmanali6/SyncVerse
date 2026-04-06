using SyncVerse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyncVerse.Application.Interfaces
{
    public interface IFriendshipRepository : IRepository<Friendship>
    {
        Task<IEnumerable<Friendship>> GetFriendRequestsAsync(string userId);
        Task<IEnumerable<Friendship>> GetFriendsAsync(string userId);
    }
}