using Chat_Test_Task.Controllers;
using Chat_Test_Task.IServices;
using Chat_Test_Task.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
namespace Chat_Test_Task.Tests;

public class ChatsControllerTests
{
    private readonly Mock<IChatService> _mockChatService;
    private readonly ChatsController _controller;

    public ChatsControllerTests()
    {
        _mockChatService = new Mock<IChatService>();
        _controller = new ChatsController(_mockChatService.Object);
    }

    [Fact]
    public async Task CreateChat_ReturnsCreatedAtAction_WithCreatedChat()
    {
        // Arrange
        var chat = new Chat { Id = 1, Name = "Test Chat" };
        _mockChatService.Setup(service => service.CreateChat(chat)).ReturnsAsync(chat);

        // Act
        var result = await _controller.CreateChat(chat);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<Chat>(createdAtActionResult.Value);
        Assert.Equal(chat.Id, returnValue.Id);
    }

    [Fact]
    public async Task GetChat_ReturnsChat_WhenChatExists()
    {
        // Arrange
        var chat = new Chat { Id = 1, Name = "Test Chat" };
        _mockChatService.Setup(service => service.GetChat(chat.Id)).ReturnsAsync(chat);

        // Act
        var result = await _controller.GetChat(chat.Id);

        // Assert
        var okResult = Assert.IsType<ActionResult<Chat>>(result);
        var returnValue = Assert.IsType<Chat>(okResult.Value);
        Assert.Equal(chat.Id, returnValue.Id);
    }

    [Fact]
    public async Task GetChat_ReturnsNotFound_WhenChatDoesNotExist()
    {
        // Arrange
        _mockChatService.Setup(service => service.GetChat(It.IsAny<int>())).ReturnsAsync((Chat)null);

        // Act
        var result = await _controller.GetChat(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task DeleteChat_ReturnsNoContent_WhenChatIsDeleted()
    {
        // Arrange
        var chatId = 1;
        var userId = 1;
        _mockChatService.Setup(service => service.DeleteChat(chatId, userId)).Returns((Task<bool>)Task.CompletedTask);

        // Act
        var result = await _controller.DeleteChat(chatId, userId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteChat_ReturnsForbid_WhenUserIsUnauthorized()
    {
        // Arrange
        var chatId = 1;
        var userId = 2;
        _mockChatService.Setup(service => service.DeleteChat(chatId, userId)).Throws<UnauthorizedAccessException>();

        // Act
        var result = await _controller.DeleteChat(chatId, userId);

        // Assert
        Assert.IsType<ForbidResult>(result);
    }

    [Fact]
    public async Task SearchChats_ReturnsOk_WithListOfChats()
    {
        // Arrange
        var query = "Test";
        var mockChats = new List<Chat> { new Chat { Id = 1, Name = "Test Chat" } };
        _mockChatService.Setup(service => service.SearchChats(query)).ReturnsAsync(mockChats);

        // Act
        var result = await _controller.SearchChats(query);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<Chat>>(okResult.Value);
        Assert.Single(returnValue);
    }
}