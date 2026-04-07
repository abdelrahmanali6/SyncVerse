using System;

namespace SyncVerse.Application.DTOs
{
    public class ServerBanDto
    {
        public Guid Id { get; set; }
        public Guid ServerId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string BannedById { get; set; } = string.Empty;
        public string? Reason { get; set; }
        public DateTime BannedAt { get; set; }
        public bool IsActive { get; set; }
    }
}