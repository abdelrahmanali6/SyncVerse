namespace SyncVerse.Domain.Entities
{
    public class Friendship
    {
        public Guid Id { get; set; }
        public string RequesterId { get; set; }
        public string AddresseeId { get; set; }
        public bool IsAccepted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}