using System.Collections.Generic;
namespace SyncVerse.Domain.Entities
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ServerId { get; set; }
        public Server Server { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Permission> Permissions { get; set; }
    }
}