namespace Chat_Test_Task.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public ICollection<ChatUser> ChatUsers { get; set; } = new List<ChatUser>();
        public ICollection<Chat> CreatedChats { get; set; } = new List<Chat>();
    }
}
