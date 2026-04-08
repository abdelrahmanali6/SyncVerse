using Microsoft.AspNetCore.Identity;
namespace SyncVerse.Infrastructure
{
    public class ApplicationUser : IdentityUser
    {
        public string? Bio { get; set; }
        public string? Badge { get; set; }
        public bool IsVerified { get; set; }
        public string? AvatarUrl { get; set; }
    }
}