namespace SyncVerse.Application.DTOs
{
    public class AppFileDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long Size { get; set; }
        public string UploadedById { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; }
    }
}