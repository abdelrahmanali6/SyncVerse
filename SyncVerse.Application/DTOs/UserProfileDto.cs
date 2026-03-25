namespace SyncVerse.Application.DTOs
{
    public class UserProfileDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? AvatarUrl { get; set; }
        public bool IsOnline { get; set; }
    }
}
