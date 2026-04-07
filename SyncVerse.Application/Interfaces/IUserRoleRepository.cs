using SyncVerse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyncVerse.Application.Interfaces
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        Task<IEnumerable<UserRole>> GetUserRolesAsync(string userId);
        Task<IEnumerable<UserRole>> GetUserRolesForServerAsync(string userId, Guid serverId);
        Task<bool> HasPermissionAsync(string userId, Guid serverId, string permissionName);
    }
}