using SyncVerse.Application.DTOs;
using SyncVerse.Application.Interfaces;
using SyncVerse.Application.Services;
using SyncVerse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncVerse.Infrastructure.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IFriendshipRepository _friendshipRepository;

        public FriendshipService(IFriendshipRepository friendshipRepository)
        {
            _friendshipRepository = friendshipRepository;
        }

        public async Task<FriendshipDto> SendFriendRequestAsync(string requesterId, string addresseeId)
        {
            if (requesterId == addresseeId) throw new ArgumentException("Cannot friend yourself");

            var existing = (await _friendshipRepository.GetFriendsAsync(requesterId)).FirstOrDefault(f =>
                (f.RequesterId == requesterId && f.AddresseeId == addresseeId)
                || (f.RequesterId == addresseeId && f.AddresseeId == requesterId));

            if (existing != null) throw new InvalidOperationException("Friend request already exists or you are already friends");

            var friendship = new Friendship
            {
                Id = Guid.NewGuid(),
                RequesterId = requesterId,
                AddresseeId = addresseeId,
                IsAccepted = false,
                CreatedAt = DateTime.UtcNow
            };

            await _friendshipRepository.AddAsync(friendship);

            return new FriendshipDto
            {
                Id = friendship.Id,
                RequesterId = friendship.RequesterId,
                AddresseeId = friendship.AddresseeId,
                IsAccepted = friendship.IsAccepted,
                CreatedAt = friendship.CreatedAt
            };
        }

        public async Task<bool> AcceptFriendRequestAsync(Guid requestId, string userId)
        {
            var friendship = await _friendshipRepository.GetByIdAsync(requestId);
            if (friendship == null || friendship.AddresseeId != userId || friendship.IsAccepted) return false;

            friendship.IsAccepted = true;
            await _friendshipRepository.UpdateAsync(friendship);
            return true;
        }

        public async Task<bool> DeclineFriendRequestAsync(Guid requestId, string userId)
        {
            var friendship = await _friendshipRepository.GetByIdAsync(requestId);
            if (friendship == null || friendship.AddresseeId != userId) return false;

            await _friendshipRepository.DeleteAsync(friendship);
            return true;
        }

        public async Task<IEnumerable<FriendshipDto>> GetFriendRequestsAsync(string userId)
        {
            var requests = await _friendshipRepository.GetFriendRequestsAsync(userId);
            return requests.Select(f => new FriendshipDto
            {
                Id = f.Id,
                RequesterId = f.RequesterId,
                AddresseeId = f.AddresseeId,
                IsAccepted = f.IsAccepted,
                CreatedAt = f.CreatedAt
            });
        }

        public async Task<IEnumerable<FriendshipDto>> GetFriendsAsync(string userId)
        {
            var friends = await _friendshipRepository.GetFriendsAsync(userId);
            return friends.Select(f => new FriendshipDto
            {
                Id = f.Id,
                RequesterId = f.RequesterId,
                AddresseeId = f.AddresseeId,
                IsAccepted = f.IsAccepted,
                CreatedAt = f.CreatedAt
            });
        }
    }
}