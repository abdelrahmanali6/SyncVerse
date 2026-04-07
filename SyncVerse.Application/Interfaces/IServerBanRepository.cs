using SyncVerse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyncVerse.Application.Interfaces
{
    public interface IServerBanRepository : IRepository<ServerBan>
    {
        Task<ServerBan?> GetActiveBanAsync(Guid serverId, string userId);
        Task<IEnumerable<ServerBan>> GetBansForServerAsync(Guid serverId);
    }
}