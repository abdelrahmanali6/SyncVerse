using Microsoft.EntityFrameworkCore;
using SyncVerse.Application.Interfaces;
using SyncVerse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncVerse.Infrastructure.Repositories
{
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<UserRole>> GetUserRolesAsync(string userId)
        {
            return await _context.UserRoles
                .Include(ur => ur.Role)
                .ThenInclude(r => r.Permissions)
                .Where(ur => ur.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserRole>> GetUserRolesForServerAsync(string userId, Guid serverId)
        {
            return await _context.UserRoles
                .Include(ur => ur.Role)
                .ThenInclude(r => r.Permissions)
                .Where(ur => ur.UserId == userId && ur.Role.ServerId == serverId)
                .ToListAsync();
        }

        public async Task<bool> HasPermissionAsync(string userId, Guid serverId, string permissionName)
        {
            return await _context.UserRoles
                .Include(ur => ur.Role)
                .ThenInclude(r => r.Permissions)
                .Where(ur => ur.UserId == userId && ur.Role.ServerId == serverId)
                .SelectMany(ur => ur.Role.Permissions)
                .AnyAsync(p => p.Name == permissionName);
        }
    }
}