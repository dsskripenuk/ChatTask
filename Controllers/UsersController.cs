using Chat_Test_Task.IServices;
using Chat_Test_Task.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chat_Test_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username))
            {
                return BadRequest("User is null or Username is required.");
            }

            var createdUser = await _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }
    }
}
