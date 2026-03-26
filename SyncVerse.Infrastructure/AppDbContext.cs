using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SyncVerse.Domain.Entities;

namespace SyncVerse.Infrastructure
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Server> Servers { get; set; }
        public DbSet<ServerMember> ServerMembers { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageReaction> MessageReactions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Invite> Invites { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<AppFile> Files { get; set; }
        public DbSet<DirectMessage> DirectMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Relationships for navigation properties that exist only
        }
    }
}