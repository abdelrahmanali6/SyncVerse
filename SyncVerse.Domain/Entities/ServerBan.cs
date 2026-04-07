using System;

namespace SyncVerse.Domain.Entities
{
    public class ServerBan
    {
        public Guid Id { get; set; }
        public Guid ServerId { get; set; }
        public string UserId { get; set; }
        public string BannedById { get; set; }
        public string? Reason { get; set; }
        public DateTime BannedAt { get; set; }
        public bool IsActive { get; set; }
        public Server Server { get; set; }
    }
}