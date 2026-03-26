namespace SyncVerse.Application.DTOs
{
    public class InviteDto
    {
        public Guid Id { get; set; }
        public Guid ServerId { get; set; }
        public string Code { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public string? InvitedUserId { get; set; }
    }

    public class CreateInviteDto
    {
        public Guid ServerId { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public string? InvitedUserId { get; set; }
    }

    public class AcceptInviteDto
    {
        public string Code { get; set; }
        public string UserId { get; set; }
    }
}
