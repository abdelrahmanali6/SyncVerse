using SyncVerse.Application.DTOs;
using SyncVerse.Application.Interfaces;
using SyncVerse.Application.Services;
using SyncVerse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SyncVerse.Infrastructure.Services
{
    public class AppFileService : IAppFileService
    {
        private readonly IAppFileRepository _fileRepository;
        private readonly string _uploadRoot;

        public AppFileService(IAppFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
            _uploadRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(_uploadRoot)) Directory.CreateDirectory(_uploadRoot);
        }

        public async Task<AppFileDto> UploadFileAsync(Stream fileStream, string originalFileName, string contentType, long size, string uploadedById)
        {
            if (fileStream == null || size == 0) throw new ArgumentException("Invalid file");

            var savedName = $"{Guid.NewGuid()}_{Path.GetFileName(originalFileName)}";
            var path = Path.Combine(_uploadRoot, savedName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await fileStream.CopyToAsync(stream);
            }

            var appFile = new AppFile
            {
                Id = Guid.NewGuid(),
                FileName = Path.GetFileName(originalFileName),
                FilePath = $"/uploads/{savedName}",
                ContentType = contentType,
                Size = size,
                UploadedById = uploadedById,
                UploadedAt = DateTime.UtcNow
            };

            await _fileRepository.AddAsync(appFile);

            return new AppFileDto
            {
                Id = appFile.Id,
                FileName = appFile.FileName,
                FilePath = appFile.FilePath,
                ContentType = appFile.ContentType,
                Size = appFile.Size,
                UploadedById = appFile.UploadedById,
                UploadedAt = appFile.UploadedAt
            };
        }

        public async Task<IEnumerable<AppFileDto>> GetFilesByUserAsync(string userId)
        {
            var files = await _fileRepository.GetFilesByUserAsync(userId);
            return files.Select(f => new AppFileDto
            {
                Id = f.Id,
                FileName = f.FileName,
                FilePath = f.FilePath,
                ContentType = f.ContentType,
                Size = f.Size,
                UploadedAt = f.UploadedAt,
                UploadedById = f.UploadedById
            });
        }
    }
}