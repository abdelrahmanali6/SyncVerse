namespace SyncVerse.Application.DTOs
{
    public class MessageReactionDto
    {
        public Guid Id { get; set; }
        public Guid MessageId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Emoji { get; set; } = string.Empty;
    }

    public class CreateMessageReactionDto
    {
        public string Emoji { get; set; } = string.Empty;
    }
}