using Microsoft.EntityFrameworkCore;
using SyncVerse.Application.Interfaces;
using SyncVerse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncVerse.Infrastructure.Repositories
{
    public class AuditLogRepository : Repository<AuditLog>, IAuditLogRepository
    {
        public AuditLogRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<AuditLog>> GetLogsForServerAsync(Guid serverId)
        {
            return await _context.Set<AuditLog>()
                .Where(log => log.ServerId == serverId)
                .OrderByDescending(log => log.CreatedAt)
                .ToListAsync();
        }
    }
}