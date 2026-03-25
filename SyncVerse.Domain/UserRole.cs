namespace SyncVerse.Domain
{
    public class UserRole
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}