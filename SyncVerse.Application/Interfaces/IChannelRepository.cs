using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SyncVerse.Domain;

namespace SyncVerse.Application.Interfaces
{
    public interface IChannelRepository : IRepository<Channel>
    {
        Task<IEnumerable<Channel>> GetChannelsForServerAsync(Guid serverId);
    }
}
