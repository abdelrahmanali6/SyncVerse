using SyncVerse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyncVerse.Application.Interfaces
{
    public interface IAppFileRepository : IRepository<AppFile>
    {
        Task<IEnumerable<AppFile>> GetFilesByUserAsync(string userId);
    }
}