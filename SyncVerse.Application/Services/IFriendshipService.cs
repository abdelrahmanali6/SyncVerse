using SyncVerse.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyncVerse.Application.Services
{
    public interface IFriendshipService
    {
        Task<FriendshipDto> SendFriendRequestAsync(string requesterId, string addresseeId);
        Task<bool> AcceptFriendRequestAsync(Guid requestId, string userId);
        Task<bool> DeclineFriendRequestAsync(Guid requestId, string userId);
        Task<IEnumerable<FriendshipDto>> GetFriendRequestsAsync(string userId);
        Task<IEnumerable<FriendshipDto>> GetFriendsAsync(string userId);
    }
}