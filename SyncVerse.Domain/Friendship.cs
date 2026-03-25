namespace SyncVerse.Domain
{
    public class Friendship
    {
        public Guid Id { get; set; }
        public string RequesterId { get; set; }
        public ApplicationUser Requester { get; set; }
        public string AddresseeId { get; set; }
        public ApplicationUser Addressee { get; set; }
        public bool IsAccepted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}