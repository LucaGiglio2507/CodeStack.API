using CodeStack.API.Mappers;
using CodeStack.Core.Interfaces.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Message = CodeStack.Domain.Entities.Message;

namespace CodeStack.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
/// <summary>
/// Authenticated read-only endpoints for group message history and direct-message history between two users.
/// </summary>
public class MessageController(IMessageService _messageService) : ControllerBase
{
    private Guid CurrentUserId =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet("group/{groupId:guid}")]
    public async Task<IActionResult> GetGroupHistory(Guid groupId)
    {
        IEnumerable<Message> messages = await _messageService.GetGroupHistoryAsync(groupId);
        return Ok(MessageMapper.ToDtoList(messages));
    }

    [HttpGet("direct/{userId:guid}")]
    public async Task<IActionResult> GetDirectHistory(Guid userId)
    {
        IEnumerable<Message> messages = await _messageService.GetDirectHistoryAsync(CurrentUserId, userId);
        return Ok(MessageMapper.ToDtoList(messages));
    }
}
