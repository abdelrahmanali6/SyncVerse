using System;

namespace SyncVerse.Application.DTOs
{
    public class AuditLogDto
    {
        public Guid Id { get; set; }
        public string Action { get; set; } = string.Empty;
        public string PerformedById { get; set; } = string.Empty;
        public string? TargetUserId { get; set; }
        public Guid? ServerId { get; set; }
        public Guid? ChannelId { get; set; }
        public Guid? MessageId { get; set; }
        public string? Details { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}