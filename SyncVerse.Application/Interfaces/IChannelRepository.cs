using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SyncVerse.Domain.Entities;

namespace SyncVerse.Application.Interfaces
{
    public interface IChannelRepository : IRepository<Channel>
    {
        Task<IEnumerable<Channel>> GetChannelsForServerAsync(Guid serverId);
    }
}
