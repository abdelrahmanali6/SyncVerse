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
    public class PinnedMessageService : IPinnedMessageService
    {
        private readonly IPinnedMessageRepository _pinnedRepository;

        public PinnedMessageService(IPinnedMessageRepository pinnedRepository)
        {
            _pinnedRepository = pinnedRepository;
        }

        public async Task<PinnedMessageDto> PinMessageAsync(Guid messageId, Guid channelId, string userId)
        {
            var existing = await _pinnedRepository.GetPinAsync(messageId, channelId);
            if (existing != null) return MapToDto(existing);

            var pinnedMessage = new PinnedMessage
            {
                Id = Guid.NewGuid(),
                MessageId = messageId,
                ChannelId = channelId,
                PinnedById = userId,
                PinnedAt = DateTime.UtcNow
            };

            await _pinnedRepository.AddAsync(pinnedMessage);
            return MapToDto(pinnedMessage);
        }

        public async Task<bool> UnpinMessageAsync(Guid messageId, Guid channelId)
        {
            var pin = await _pinnedRepository.GetPinAsync(messageId, channelId);
            if (pin == null) return false;

            await _pinnedRepository.DeleteAsync(pin);
            return true;
        }

        public async Task<IEnumerable<PinnedMessageDto>> GetPinnedMessagesAsync(Guid channelId)
        {
            var pinned = await _pinnedRepository.GetPinnedMessagesForChannelAsync(channelId);
            return pinned.Select(MapToDto);
        }

        private PinnedMessageDto MapToDto(PinnedMessage pin)
        {
            return new PinnedMessageDto
            {
                Id = pin.Id,
                MessageId = pin.MessageId,
                ChannelId = pin.ChannelId,
                PinnedById = pin.PinnedById,
                PinnedAt = pin.PinnedAt
            };
        }
    }
}
