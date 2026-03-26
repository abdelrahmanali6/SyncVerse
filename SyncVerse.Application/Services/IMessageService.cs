using SyncVerse.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyncVerse.Application.Services
{
    public interface IMessageService
    {
        Task<MessageDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<MessageDto>> GetByChannelIdAsync(Guid channelId);
        Task<MessageDto> CreateAsync(CreateMessageDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
