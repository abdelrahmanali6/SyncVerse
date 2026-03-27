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
    public class DirectMessageService : IDirectMessageService
    {
        private readonly IDirectMessageRepository _dmRepository;
        public DirectMessageService(IDirectMessageRepository dmRepository)
        {
            _dmRepository = dmRepository;
        }

        public async Task<IEnumerable<DirectMessageDto>> GetConversationAsync(string userId1, string userId2, int page = 1, int pageSize = 50)
        {
            var messages = await _dmRepository.GetDirectMessagesAsync(userId1, userId2, page, pageSize);
            return messages.Select(ToDto);
        }

        public async Task<DirectMessageDto> SendAsync(string senderId, CreateDirectMessageDto dto)
        {
            var dm = new DirectMessage
            {
                Id = Guid.NewGuid(),
                SenderId = senderId,
                RecipientId = dto.RecipientId,
                Content = dto.Content,
                CreatedAt = DateTime.UtcNow,
                EditedAt = null,
                ReplyToMessageId = dto.ReplyToMessageId
            };
            await _dmRepository.AddAsync(dm);
            return ToDto(dm);
        }

        private static DirectMessageDto ToDto(DirectMessage dm)
        {
            return new DirectMessageDto
            {
                Id = dm.Id,
                SenderId = dm.SenderId,
                RecipientId = dm.RecipientId,
                Content = dm.Content,
                CreatedAt = dm.CreatedAt,
                EditedAt = dm.EditedAt,
                ReplyToMessageId = dm.ReplyToMessageId
            };
        }
    }
}
