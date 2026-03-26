using SyncVerse.Domain.Entities;
using SyncVerse.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyncVerse.Infrastructure.Repositories
{
    public interface IInviteRepository : IRepository<Invite>
    {
        Task<Invite?> GetByCodeAsync(string code);
        Task<IEnumerable<Invite>> GetInvitesForServerAsync(Guid serverId);
    }
}
