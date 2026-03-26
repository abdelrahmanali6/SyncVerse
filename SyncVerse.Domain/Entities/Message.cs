using System;
namespace SyncVerse.Domain.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? EditedAt { get; set; }
        public string UserId { get; set; }
        public Guid ChannelId { get; set; }
        public Channel Channel { get; set; }
        public Guid? ReplyToMessageId { get; set; }
        public Message? ReplyToMessage { get; set; }

        // For compatibility with service/DTO naming
        public string SenderId { get => UserId; set => UserId = value; }
        public DateTime SentAt { get => CreatedAt; set => CreatedAt = value; }
    }
}