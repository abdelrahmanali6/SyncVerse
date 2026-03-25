namespace SyncVerse.Domain
{
    public class File
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public string UploadedById { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}