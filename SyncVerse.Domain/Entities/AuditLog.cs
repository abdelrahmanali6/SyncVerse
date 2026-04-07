using System;

namespace SyncVerse.Domain.Entities
{
    public class AuditLog
    {
        public Guid Id { get; set; }
        public string Action { get; set; }
        public string PerformedById { get; set; }
        public string? TargetUserId { get; set; }
        public Guid? ServerId { get; set; }
        public Guid? ChannelId { get; set; }
        public Guid? MessageId { get; set; }
        public string? Details { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}