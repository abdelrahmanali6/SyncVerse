using System.Collections.Generic;
namespace SyncVerse.Domain.Entities
{
    public class Server
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? AvatarUrl { get; set; }
        public ICollection<ServerMember> Members { get; set; }
        public ICollection<Channel> Channels { get; set; }
    }
}