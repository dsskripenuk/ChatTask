using Chat_Test_Task.IServices;
using Microsoft.AspNetCore.SignalR;

namespace Chat_Test_Task.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;

        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task SendMessage(int chatId, string userId, string message)
        {
            var chatMessage = await _messageService.SendMessage(chatId, int.Parse(userId), message);
            await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", userId, message);
        }

        public async Task JoinChat(int chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        }

        public async Task LeaveChat(int chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
        }
    }
}
