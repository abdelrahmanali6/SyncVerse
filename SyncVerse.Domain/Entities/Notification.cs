namespace SyncVerse.Domain.Entities
{
    public class Notification
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}