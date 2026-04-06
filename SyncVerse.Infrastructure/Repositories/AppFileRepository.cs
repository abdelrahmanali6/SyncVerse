using Microsoft.EntityFrameworkCore;
using SyncVerse.Application.Interfaces;
using SyncVerse.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncVerse.Infrastructure.Repositories
{
    public class AppFileRepository : Repository<AppFile>, IAppFileRepository
    {
        public AppFileRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<AppFile>> GetFilesByUserAsync(string userId)
        {
            return await _context.Files
                .Where(f => f.UploadedById == userId)
                .ToListAsync();
        }
    }
}