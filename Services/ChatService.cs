using Chat_Test_Task.Data;
using Chat_Test_Task.IServices;
using Chat_Test_Task.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat_Test_Task.Services
{
    public class ChatService : IChatService
    {
        private readonly ChatAppContext _context;

        public ChatService(ChatAppContext context)
        {
            _context = context;
        }

        public async Task<Chat> CreateChat(Chat chat)
        {
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();
            return chat;
        }

        public async Task<Chat> GetChat(int id)
        {
            return await _context.Chats.Include(c => c.Messages)
                                       .Include(c => c.ChatUsers)
                                       .ThenInclude(cu => cu.User)
                                       .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> DeleteChat(int id, int userId)
        {
            var chat = await _context.Chats.FindAsync(id);
            if (chat == null || chat.CreatedByUserId != userId)
            {
                return false;
            }

            _context.Chats.Remove(chat);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Chat>> SearchChats(string query)
        {
            return await _context.Chats
                                 .Where(c => c.Name.Contains(query))
                                 .ToListAsync();
        }

        public async Task AddUserToChat(int chatId, int userId)
        {
            var chat = await _context.Chats.Include(c => c.ChatUsers).FirstOrDefaultAsync(c => c.Id == chatId);
            var user = await _context.Users.FindAsync(userId);

            if (chat != null && user != null)
            {
                if (!chat.ChatUsers.Any(cu => cu.UserId == userId))
                {
                    chat.ChatUsers.Add(new ChatUser { ChatId = chatId, UserId = userId });
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task RemoveUserFromChat(int chatId, int userId)
        {
            var chat = await _context.Chats.Include(c => c.ChatUsers).FirstOrDefaultAsync(c => c.Id == chatId);
            var user = await _context.Users.FindAsync(userId);

            if (chat != null && user != null)
            {
                var chatUser = chat.ChatUsers.FirstOrDefault(cu => cu.UserId == userId);
                if (chatUser != null)
                {
                    chat.ChatUsers.Remove(chatUser);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
