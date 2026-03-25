namespace SyncVerse.Domain
{
    public class MessageReaction
    {
        public Guid Id { get; set; }
        public Guid MessageId { get; set; }
        public Message Message { get; set; }
        public string UserId { get; set; }
        public string Emoji { get; set; }
    }
}