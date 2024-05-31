namespace FileStorage.Core.Entities
{
    public class UserFileUpload
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public long FileId { get; set; }

        public virtual File File { get; set; }
        public virtual User User { get; set; }
    }
}