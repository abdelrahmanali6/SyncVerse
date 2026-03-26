namespace SyncVerse.Application.DTOs
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public Guid ChannelId { get; set; }
        public string SenderId { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
    }

    public class CreateMessageDto
    {
        public string Content { get; set; } = string.Empty;
        public Guid ChannelId { get; set; }
        public string SenderId { get; set; } = string.Empty;
    }
}
