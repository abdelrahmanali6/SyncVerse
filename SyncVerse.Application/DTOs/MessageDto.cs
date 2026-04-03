namespace SyncVerse.Application.DTOs
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public Guid ChannelId { get; set; }
        public string SenderId { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
        public IEnumerable<MessageReactionDto> Reactions { get; set; } = new List<MessageReactionDto>();
    }

    public class CreateMessageDto
    {
        public string Content { get; set; } = string.Empty;
        public Guid ChannelId { get; set; }
        public string SenderId { get; set; } = string.Empty;
    }
}
