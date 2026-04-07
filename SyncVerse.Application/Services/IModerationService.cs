using SyncVerse.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyncVerse.Application.Services
{
    public interface IModerationService
    {
        Task<bool> KickUserAsync(Guid serverId, string targetUserId, string performedById, string? reason = null);
        Task<bool> BanUserAsync(Guid serverId, string targetUserId, string performedById, string? reason = null);
        Task<IEnumerable<AuditLogDto>> GetAuditLogsAsync(Guid serverId);
        Task<IEnumerable<ServerBanDto>> GetBansAsync(Guid serverId);
        Task<bool> CanModerateAsync(string userId, Guid serverId);
    }
}