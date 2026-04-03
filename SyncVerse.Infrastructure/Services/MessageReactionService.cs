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
    public class MessageReactionService : IMessageReactionService
    {
        private readonly IMessageReactionRepository _reactionRepository;

        public MessageReactionService(IMessageReactionRepository reactionRepository)
        {
            _reactionRepository = reactionRepository;
        }

        public async Task<IEnumerable<MessageReactionDto>> GetReactionsForMessageAsync(Guid messageId)
        {
            var reactions = await _reactionRepository.GetReactionsForMessageAsync(messageId);
            return reactions.Select(r => new MessageReactionDto
            {
                Id = r.Id,
                MessageId = r.MessageId,
                UserId = r.UserId,
                Emoji = r.Emoji
            });
        }

        public async Task<MessageReactionDto> AddReactionAsync(Guid messageId, string userId, CreateMessageReactionDto dto)
        {
            // Check if reaction already exists
            var existing = await _reactionRepository.GetReactionAsync(messageId, userId, dto.Emoji);
            if (existing != null)
            {
                return new MessageReactionDto
                {
                    Id = existing.Id,
                    MessageId = existing.MessageId,
                    UserId = existing.UserId,
                    Emoji = existing.Emoji
                };
            }

            var reaction = new MessageReaction
            {
                Id = Guid.NewGuid(),
                MessageId = messageId,
                UserId = userId,
                Emoji = dto.Emoji
            };

            await _reactionRepository.AddAsync(reaction);
            return new MessageReactionDto
            {
                Id = reaction.Id,
                MessageId = reaction.MessageId,
                UserId = reaction.UserId,
                Emoji = reaction.Emoji
            };
        }

        public async Task<bool> RemoveReactionAsync(Guid messageId, string userId, string emoji)
        {
            return await _reactionRepository.RemoveReactionAsync(messageId, userId, emoji);
        }
    }
}