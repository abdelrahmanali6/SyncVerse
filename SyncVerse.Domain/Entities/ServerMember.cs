namespace SyncVerse.Domain.Entities
{
    public class ServerMember
    {
        public Guid Id { get; set; }
        public Guid ServerId { get; set; }
        public string UserId { get; set; }
        public Server Server { get; set; }
        public string? Nickname { get; set; }
        public bool IsOwner { get; set; }
    }
}