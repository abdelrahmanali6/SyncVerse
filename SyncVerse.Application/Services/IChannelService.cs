using SyncVerse.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyncVerse.Application.Services
{
    public interface IChannelService
    {
        Task<ChannelDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<ChannelDto>> GetByServerIdAsync(Guid serverId);
        Task<ChannelDto> CreateAsync(CreateChannelDto dto);
        Task<bool> UpdateAsync(Guid id, UpdateChannelDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
