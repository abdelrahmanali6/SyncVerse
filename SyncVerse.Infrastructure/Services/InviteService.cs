using SyncVerse.Application.DTOs;
using SyncVerse.Application.Services;
using SyncVerse.Domain.Entities;
using SyncVerse.Infrastructure.Repositories;
using SyncVerse.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncVerse.Infrastructure.Services
{
    public class InviteService : IInviteService
    {
        private readonly IInviteRepository _inviteRepository;
        private readonly IServerRepository _serverRepository;
        public InviteService(IInviteRepository inviteRepository, IServerRepository serverRepository)
        {
            _inviteRepository = inviteRepository;
            _serverRepository = serverRepository;
        }

        public async Task<InviteDto?> GetByCodeAsync(string code)
        {
            var invite = await _inviteRepository.GetByCodeAsync(code);
            if (invite == null) return null;
            return ToDto(invite);
        }

        public async Task<InviteDto> CreateAsync(CreateInviteDto dto)
        {
            // Optionally: validate server exists, permissions, etc.
            var invite = new Invite
            {
                Id = Guid.NewGuid(),
                ServerId = dto.ServerId,
                Code = Guid.NewGuid().ToString("N").Substring(0, 8),
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = dto.ExpiresAt,
                InvitedUserId = dto.InvitedUserId
            };
            await _inviteRepository.AddAsync(invite);
            return ToDto(invite);
        }

        public async Task<bool> AcceptAsync(AcceptInviteDto dto)
        {
            var invite = await _inviteRepository.GetByCodeAsync(dto.Code);
            if (invite == null) return false;
            if (invite.ExpiresAt.HasValue && invite.ExpiresAt.Value < DateTime.UtcNow) return false;
            // Optionally: add user to server, check if already a member, etc.
            return true;
        }

        public async Task<IEnumerable<InviteDto>> GetInvitesForServerAsync(Guid serverId)
        {
            var invites = await _inviteRepository.GetInvitesForServerAsync(serverId);
            return invites.Select(ToDto);
        }

        private static InviteDto ToDto(Invite invite)
        {
            return new InviteDto
            {
                Id = invite.Id,
                ServerId = invite.ServerId,
                Code = invite.Code,
                CreatedAt = invite.CreatedAt,
                ExpiresAt = invite.ExpiresAt,
                InvitedUserId = invite.InvitedUserId
            };
        }
    }
}
