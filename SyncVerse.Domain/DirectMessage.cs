namespace SyncVerse.Domain
{
    public class DirectMessage
    {
        public Guid Id { get; set; }
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }
        public string RecipientId { get; set; }
        public ApplicationUser Recipient { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? EditedAt { get; set; }
        public Guid? ReplyToMessageId { get; set; }
        public DirectMessage? ReplyToMessage { get; set; }
    }
}