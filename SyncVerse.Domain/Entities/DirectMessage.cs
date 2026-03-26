namespace SyncVerse.Domain.Entities
{
    public class DirectMessage
    {
        public Guid Id { get; set; }
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? EditedAt { get; set; }
        public Guid? ReplyToMessageId { get; set; }
        public DirectMessage? ReplyToMessage { get; set; }
    }
}