using Chat_Test_Task.Models;

namespace Chat_Test_Task.IServices
{
    public interface IUserService
    {
        Task<User> CreateUser(User user);
        Task<User> GetUser(int id);
    }
}
