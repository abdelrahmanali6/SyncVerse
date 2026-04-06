using SyncVerse.Application.DTOs;
using SyncVerse.Application.Interfaces;
using SyncVerse.Application.Services;
using SyncVerse.Domain.Entities;
using SyncVerse.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncVerse.Infrastructure.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<MessageDto?> GetByIdAsync(Guid id)
        {
            var message = await _messageRepository.GetByIdWithReactionsAsync(id);
            if (message == null) return null;
            return new MessageDto
            {
                Id = message.Id,
                Content = message.Content,
                ChannelId = message.ChannelId,
                SenderId = message.SenderId,
                SentAt = message.SentAt,
                Reactions = message.MessageReactions.Select(r => new MessageReactionDto
                {
                    Id = r.Id,
                    MessageId = r.MessageId,
                    UserId = r.UserId,
                    Emoji = r.Emoji
                })
            };
        }

        public async Task<IEnumerable<MessageDto>> GetByChannelIdAsync(Guid channelId)
        {
            var messages = await _messageRepository.GetMessagesForChannelAsync(channelId, 1, 50);
            return messages.Select(m => new MessageDto
            {
                Id = m.Id,
                Content = m.Content,
                ChannelId = m.ChannelId,
                SenderId = m.SenderId,
                SentAt = m.SentAt,
                Reactions = m.MessageReactions.Select(r => new MessageReactionDto
                {
                    Id = r.Id,
                    MessageId = r.MessageId,
                    UserId = r.UserId,
                    Emoji = r.Emoji
                })
            });
        }

        public async Task<MessageDto> CreateAsync(CreateMessageDto dto)
        {
            var message = new Message
            {
                Id = Guid.NewGuid(),
                Content = dto.Content,
                ChannelId = dto.ChannelId,
                SenderId = dto.SenderId,
                SentAt = DateTime.UtcNow
            };
            await _messageRepository.AddAsync(message);
            return new MessageDto
            {
                Id = message.Id,
                Content = message.Content,
                ChannelId = message.ChannelId,
                SenderId = message.SenderId,
                SentAt = message.SentAt
            };
        }

        public async Task<bool> UpdateAsync(Guid id, string content)
        {
            var message = await _messageRepository.GetByIdAsync(id);
            if (message == null) return false;
            message.Content = content;
            message.EditedAt = DateTime.UtcNow;
            await _messageRepository.UpdateAsync(message);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var message = await _messageRepository.GetByIdAsync(id);
            if (message == null) return false;
            await _messageRepository.DeleteAsync(message);
            return true;
        }
    }
}
