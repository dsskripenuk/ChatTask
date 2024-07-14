namespace Chat_Test_Task.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }
        public ICollection<ChatUser> ChatUsers { get; set; } = new List<ChatUser>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
