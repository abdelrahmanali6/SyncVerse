using System;
namespace SyncVerse.Domain.Entities
{
    public class Invite
    {
        public Guid Id { get; set; }
        public Guid ServerId { get; set; }
        public Server Server { get; set; }
        public string Code { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public string? InvitedUserId { get; set; }
    }
}