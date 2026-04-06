using SyncVerse.Application.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SyncVerse.Application.Services
{
    public interface IAppFileService
    {
        Task<AppFileDto> UploadFileAsync(Stream fileStream, string originalFileName, string contentType, long size, string uploadedById);
        Task<IEnumerable<AppFileDto>> GetFilesByUserAsync(string userId);
    }
}