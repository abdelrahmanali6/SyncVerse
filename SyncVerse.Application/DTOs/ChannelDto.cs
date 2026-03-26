namespace SyncVerse.Application.DTOs
{
    public class ChannelDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ServerId { get; set; }
    }

    public class CreateChannelDto
    {
        public string Name { get; set; } = string.Empty;
        public Guid ServerId { get; set; }
    }

    public class UpdateChannelDto
    {
        public string Name { get; set; } = string.Empty;
    }
}
