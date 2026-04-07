using Microsoft.EntityFrameworkCore;
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
    public class ModerationService : IModerationService
    {
        private readonly IServerBanRepository _banRepository;
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly AppDbContext _context;

        public ModerationService(
            IServerBanRepository banRepository,
            IAuditLogRepository auditLogRepository,
            IUserRoleRepository userRoleRepository,
            AppDbContext context)
        {
            _banRepository = banRepository;
            _auditLogRepository = auditLogRepository;
            _userRoleRepository = userRoleRepository;
            _context = context;
        }

        public async Task<bool> BanUserAsync(Guid serverId, string targetUserId, string performedById, string? reason = null)
        {
            if (!await CanModerateAsync(performedById, serverId)) return false;
            if (string.Equals(targetUserId, performedById, StringComparison.OrdinalIgnoreCase)) return false;

            var ban = await _banRepository.GetActiveBanAsync(serverId, targetUserId);
            if (ban != null) return false;

            var serverBan = new ServerBan
            {
                Id = Guid.NewGuid(),
                ServerId = serverId,
                UserId = targetUserId,
                BannedById = performedById,
                Reason = reason,
                BannedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _banRepository.AddAsync(serverBan);
            await LogActionAsync("BanUser", performedById, targetUserId, serverId, null, null, reason);
            return true;
        }

        public async Task<bool> KickUserAsync(Guid serverId, string targetUserId, string performedById, string? reason = null)
        {
            if (!await CanModerateAsync(performedById, serverId)) return false;
            if (string.Equals(targetUserId, performedById, StringComparison.OrdinalIgnoreCase)) return false;

            var membership = await _context.ServerMembers
                .FirstOrDefaultAsync(m => m.ServerId == serverId && m.UserId == targetUserId);

            if (membership == null) return false;

            _context.ServerMembers.Remove(membership);
            await _context.SaveChangesAsync();
            await LogActionAsync("KickUser", performedById, targetUserId, serverId, null, null, reason);
            return true;
        }

        public async Task<IEnumerable<AuditLogDto>> GetAuditLogsAsync(Guid serverId)
        {
            var logs = await _auditLogRepository.GetLogsForServerAsync(serverId);
            return logs.Select(log => new AuditLogDto
            {
                Id = log.Id,
                Action = log.Action,
                PerformedById = log.PerformedById,
                TargetUserId = log.TargetUserId,
                ServerId = log.ServerId,
                ChannelId = log.ChannelId,
                MessageId = log.MessageId,
                Details = log.Details,
                CreatedAt = log.CreatedAt
            });
        }

        public async Task<IEnumerable<ServerBanDto>> GetBansAsync(Guid serverId)
        {
            var bans = await _banRepository.GetBansForServerAsync(serverId);
            return bans.Select(ban => new ServerBanDto
            {
                Id = ban.Id,
                ServerId = ban.ServerId,
                UserId = ban.UserId,
                BannedById = ban.BannedById,
                Reason = ban.Reason,
                BannedAt = ban.BannedAt,
                IsActive = ban.IsActive
            });
        }

        public async Task<bool> CanModerateAsync(string userId, Guid serverId)
        {
            var serverMember = await _context.ServerMembers
                .FirstOrDefaultAsync(m => m.ServerId == serverId && m.UserId == userId);

            if (serverMember != null && serverMember.IsOwner) return true;
            return await _userRoleRepository.HasPermissionAsync(userId, serverId, "ModerateMembers");
        }

        private async Task LogActionAsync(string action, string performedById, string? targetUserId, Guid serverId, Guid? channelId, Guid? messageId, string? details)
        {
            var log = new AuditLog
            {
                Id = Guid.NewGuid(),
                Action = action,
                PerformedById = performedById,
                TargetUserId = targetUserId,
                ServerId = serverId,
                ChannelId = channelId,
                MessageId = messageId,
                Details = details,
                CreatedAt = DateTime.UtcNow
            };
            await _auditLogRepository.AddAsync(log);
        }
    }
}