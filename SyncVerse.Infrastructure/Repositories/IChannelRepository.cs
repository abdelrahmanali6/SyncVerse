using SyncVerse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using SyncVerse.Application.Interfaces;

namespace SyncVerse.Infrastructure.Repositories
{
    public interface IChannelRepository : IRepository<Channel>
    {
        Task<IEnumerable<Channel>> GetChannelsForServerAsync(Guid serverId);
    }
}
