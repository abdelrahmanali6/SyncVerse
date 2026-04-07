using SyncVerse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyncVerse.Application.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<IEnumerable<Role>> GetRolesForServerAsync(Guid serverId);
        Task<Role?> GetRoleWithPermissionsAsync(Guid roleId);
    }
}