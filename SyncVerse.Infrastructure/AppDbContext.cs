using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SyncVerse.Domain;

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
        public DbSet<File> Files { get; set; }
        public DbSet<DirectMessage> DirectMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Server>()
                .HasMany(s => s.Members)
                .WithOne(m => m.Server)
                .HasForeignKey(m => m.ServerId);

            builder.Entity<Server>()
                .HasMany(s => s.Channels)
                .WithOne(c => c.Server)
                .HasForeignKey(c => c.ServerId);

            builder.Entity<ServerMember>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId);

            builder.Entity<Channel>()
                .HasMany(c => c.Messages)
                .WithOne(m => m.Channel)
                .HasForeignKey(m => m.ChannelId);

            builder.Entity<Message>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId);

            builder.Entity<Message>()
                .HasOne(m => m.ReplyToMessage)
                .WithMany()
                .HasForeignKey(m => m.ReplyToMessageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MessageReaction>()
                .HasOne(r => r.Message)
                .WithMany()
                .HasForeignKey(r => r.MessageId);

            builder.Entity<MessageReaction>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId);

            builder.Entity<Role>()
                .HasMany(r => r.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId);

            builder.Entity<Role>()
                .HasMany(r => r.Permissions)
                .WithOne(p => p.Role)
                .HasForeignKey(p => p.RoleId);

            builder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany()
                .HasForeignKey(ur => ur.UserId);

            builder.Entity<Invite>()
                .HasOne(i => i.Server)
                .WithMany()
                .HasForeignKey(i => i.ServerId);

            builder.Entity<Invite>()
                .HasOne(i => i.InvitedUser)
                .WithMany()
                .HasForeignKey(i => i.InvitedUserId)
                .IsRequired(false);

            builder.Entity<Friendship>()
                .HasOne(f => f.Requester)
                .WithMany()
                .HasForeignKey(f => f.RequesterId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Friendship>()
                .HasOne(f => f.Addressee)
                .WithMany()
                .HasForeignKey(f => f.AddresseeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId);

            builder.Entity<File>()
                .HasOne(f => f.UploadedBy)
                .WithMany()
                .HasForeignKey(f => f.UploadedById);

            builder.Entity<DirectMessage>()
                .HasOne(dm => dm.Sender)
                .WithMany()
                .HasForeignKey(dm => dm.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<DirectMessage>()
                .HasOne(dm => dm.Recipient)
                .WithMany()
                .HasForeignKey(dm => dm.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<DirectMessage>()
                .HasOne(dm => dm.ReplyToMessage)
                .WithMany()
                .HasForeignKey(dm => dm.ReplyToMessageId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}