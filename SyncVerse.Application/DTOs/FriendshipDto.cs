namespace SyncVerse.Application.DTOs
{
    public class FriendshipDto
    {
        public Guid Id { get; set; }
        public string RequesterId { get; set; } = string.Empty;
        public string AddresseeId { get; set; } = string.Empty;
        public bool IsAccepted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}