using System;

namespace SyncVerse.Application.DTOs
{
    public class MessageSearchRequestDto
    {
        public string? Query { get; set; }
        public Guid? ChannelId { get; set; }
        public string? UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
    
    public class MessageSearchResultDto
    {
        public IEnumerable<MessageDto> Messages { get; set; } = new List<MessageDto>();
        public int Total { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}