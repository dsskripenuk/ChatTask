using Chat_Test_Task.Models;

namespace Chat_Test_Task.IServices
{
    public interface IChatService
    {
        Task<Chat> CreateChat(Chat chat);
        Task<Chat> GetChat(int id);
        Task<bool> DeleteChat(int id, int userId);
        Task AddUserToChat(int chatId, int userId);
        Task RemoveUserFromChat(int chatId, int userId);
        Task<IEnumerable<Chat>> SearchChats(string query);
    }
}
