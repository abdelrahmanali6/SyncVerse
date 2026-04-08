namespace SyncVerse.Application.DTOs
{
    public class UserProfileDto
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }
        public string? Bio { get; set; }
        public string? Badge { get; set; }
        public bool IsVerified { get; set; }
        public bool IsOnline { get; set; }
    }

    public class UpdateUserProfileDto
    {
        public string? Bio { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
