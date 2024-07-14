using Chat_Test_Task.IServices;
using Chat_Test_Task.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chat_Test_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController: ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatsController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost]
        public async Task<ActionResult<Chat>> CreateChat([FromBody] Chat chat)
        {
            var createdChat = await _chatService.CreateChat(chat);
            return CreatedAtAction(nameof(GetChat), new { id = createdChat.Id }, createdChat);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Chat>> GetChat(int id)
        {
            var chat = await _chatService.GetChat(id);
            if (chat == null)
            {
                return NotFound();
            }
            return chat;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChat(int id, [FromQuery] int userId)
        {
            try
            {
                await _chatService.DeleteChat(id, userId);
                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Chat>>> SearchChats([FromQuery] string query)
        {
            var chats = await _chatService.SearchChats(query);
            return Ok(chats);
        }
    }
}
