using Microsoft.EntityFrameworkCore;
using SyncVerse.Application.DTOs;
using SyncVerse.Application.Interfaces;
using SyncVerse.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncVerse.Infrastructure.Services
{
    public class MessageSearchService : IMessageSearchService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly AppDbContext _context;

        public MessageSearchService(IMessageRepository messageRepository, AppDbContext context)
        {
            _messageRepository = messageRepository;
            _context = context;
        }

        public async Task<MessageSearchResultDto> SearchMessagesAsync(MessageSearchRequestDto request)
        {
            var query = _context.Messages.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Query))
            {
                query = query.Where(m => m.Content.Contains(request.Query));
            }

            if (request.ChannelId.HasValue)
            {
                query = query.Where(m => m.ChannelId == request.ChannelId);
            }

            if (!string.IsNullOrWhiteSpace(request.UserId))
            {
                query = query.Where(m => m.UserId == request.UserId);
            }

            if (request.StartDate.HasValue)
            {
                query = query.Where(m => m.CreatedAt >= request.StartDate);
            }

            if (request.EndDate.HasValue)
            {
                query = query.Where(m => m.CreatedAt <= request.EndDate);
            }

            var total = await query.CountAsync();
            var messages = await query
                .Include(m => m.MessageReactions)
                .OrderByDescending(m => m.CreatedAt)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var messageDtos = messages.Select(m => new MessageDto
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

            return new MessageSearchResultDto
            {
                Messages = messageDtos,
                Total = total,
                Page = request.Page,
                PageSize = request.PageSize
            };
        }
    }
}
