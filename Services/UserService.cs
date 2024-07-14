using Chat_Test_Task.Data;
using Chat_Test_Task.IServices;
using Chat_Test_Task.Models;

namespace Chat_Test_Task.Services
{
    public class UserService : IUserService
    {
        private readonly ChatAppContext _context;

        public UserService(ChatAppContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
