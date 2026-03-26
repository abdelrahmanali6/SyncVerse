using SyncVerse.Application.DTOs;
using SyncVerse.Application.Services;
using SyncVerse.Domain.Entities;
using SyncVerse.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncVerse.Infrastructure.Services
{
    public class ChannelService : IChannelService
    {
        private readonly IChannelRepository _channelRepository;

        public ChannelService(IChannelRepository channelRepository)
        {
            _channelRepository = channelRepository;
        }

        public async Task<ChannelDto?> GetByIdAsync(Guid id)
        {
            var channel = await _channelRepository.GetByIdAsync(id);
            if (channel == null) return null;
            return new ChannelDto { Id = channel.Id, Name = channel.Name, ServerId = channel.ServerId };
        }

        public async Task<IEnumerable<ChannelDto>> GetByServerIdAsync(Guid serverId)
        {
            var channels = await _channelRepository.GetChannelsForServerAsync(serverId);
            return channels.Select(c => new ChannelDto { Id = c.Id, Name = c.Name, ServerId = c.ServerId });
        }

        public async Task<ChannelDto> CreateAsync(CreateChannelDto dto)
        {
            var channel = new Channel { Id = Guid.NewGuid(), Name = dto.Name, ServerId = dto.ServerId };
            await _channelRepository.AddAsync(channel);
            return new ChannelDto { Id = channel.Id, Name = channel.Name, ServerId = channel.ServerId };
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateChannelDto dto)
        {
            var channel = await _channelRepository.GetByIdAsync(id);
            if (channel == null) return false;
            channel.Name = dto.Name;
            await _channelRepository.UpdateAsync(channel);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var channel = await _channelRepository.GetByIdAsync(id);
            if (channel == null) return false;
            await _channelRepository.DeleteAsync(channel);
            return true;
        }
    }
}
