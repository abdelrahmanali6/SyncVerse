using System;

namespace SyncVerse.Domain.Entities
{
    public class PinnedMessage
    {
        public Guid Id { get; set; }
        public Guid MessageId { get; set; }
        public Message Message { get; set; }
        public Guid ChannelId { get; set; }
        public Channel Channel { get; set; }
        public string PinnedById { get; set; }
        public DateTime PinnedAt { get; set; }
    }
}