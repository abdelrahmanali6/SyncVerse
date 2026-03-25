using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SyncVerse.Domain;

namespace SyncVerse.Application.Interfaces
{
    public interface IServerRepository : IRepository<Server>
    {
        Task<IEnumerable<Server>> GetServersForUserAsync(string userId);
    }
}
