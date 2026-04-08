using System;

namespace SyncVerse.Application.DTOs
{
    public class PinnedMessageDto
    {
        public Guid Id { get; set; }
        public Guid MessageId { get; set; }
        public Guid ChannelId { get; set; }
        public string PinnedById { get; set; } = string.Empty;
        public DateTime PinnedAt { get; set; }
        public MessageDto? Message { get; set; }
    }
}