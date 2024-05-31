namespace FileStorage.Core.Entities
{
    public class User
    {
        public string Email { get; set; }

        public User(string email)
        {
            Email = email;
        }
        public ICollection<UserFileUpload> FileUploads { get; set; } = new List<UserFileUpload>();
    }
}
