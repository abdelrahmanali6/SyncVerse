using SyncVerse.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyncVerse.Application.Services
{
    public interface IInviteService
    {
        Task<InviteDto?> GetByCodeAsync(string code);
        Task<InviteDto> CreateAsync(CreateInviteDto dto);
        Task<bool> AcceptAsync(AcceptInviteDto dto);
        Task<IEnumerable<InviteDto>> GetInvitesForServerAsync(Guid serverId);
    }
}
