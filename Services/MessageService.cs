using Chat_Test_Task.Data;
using Chat_Test_Task.IServices;
using Chat_Test_Task.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat_Test_Task.Services
{
    public class MessageService : IMessageService
    {
        private readonly ChatAppContext _context;

        public MessageService(ChatAppContext context)
        {
            _context = context;
        }

        public async Task<Message> SendMessage(int chatId, int userId, string content)
        {
            var message = new Message
            {
                ChatId = chatId,
                UserId = userId,
                Content = content,
                Timestamp = DateTime.Now
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return message;
        }

        public async Task<IEnumerable<Message>> GetMessages(int chatId)
        {
            return await _context.Messages.Where(m => m.ChatId == chatId).ToListAsync();
        }
    }
}
