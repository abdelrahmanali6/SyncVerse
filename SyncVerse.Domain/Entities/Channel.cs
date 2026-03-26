using System.Collections.Generic;
namespace SyncVerse.Domain.Entities
{
    public class Channel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ServerId { get; set; }
        public Server Server { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}