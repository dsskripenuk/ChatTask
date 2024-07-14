using Chat_Test_Task.Models;

namespace Chat_Test_Task.IServices
{
    public interface IMessageService
    {
        Task<Message> SendMessage(int chatId, int userId, string content);
        Task<IEnumerable<Message>> GetMessages(int chatId);
    }
}
