using CodeStack.API.Dtos.Responses.Message;
using CodeStack.API.Mappers;
using CodeStack.Core.Interfaces.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using Group = CodeStack.Domain.Entities.Group;
using Message = CodeStack.Domain.Entities.Message;

namespace CodeStack.API.Hubs;

[Authorize]
public class MessageHub(IMessageService _messageService, IGroupService _groupService) : Hub
{
private Guid CurrentUserId =>
Guid.Parse(Context.User!.FindFirstValue(ClaimTypes.NameIdentifier)!);

    public override async Task OnConnectedAsync()
    {
        Guid userId = CurrentUserId;
        IEnumerable<Group> groups = await _groupService.GetByUserAsync(userId);

        foreach (Group group in groups)
            await Groups.AddToGroupAsync(Context.ConnectionId, group.Id.ToString());

        await base.OnConnectedAsync();
    }


    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }



    public async Task SendToGroup(Guid groupId, string content)
    {
        Message? message = await _messageService.SendToGroupAsync(CurrentUserId, groupId, content);
        if (message is null) return;

        await Clients.Group(groupId.ToString())
            .SendAsync("ReceiveMessage", MessageMapper.ToDto(message));
    }

    public async Task SendDirect(Guid receiverId, string content)
    {
        Guid senderId = CurrentUserId;
        Message? message = await _messageService.SendDirectAsync(senderId, receiverId, content);
        if (message is null) return;

        MessageResponseDto dto = MessageMapper.ToDto(message);
        await Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", dto);
        await Clients.Caller.SendAsync("ReceiveMessage", dto);
    }

    public async Task MarkAsRead(Guid messageId)
    {
        bool updated = await _messageService.MarkAsReadAsync(messageId);
        if (!updated) return;

        await Clients.Caller.SendAsync("MessageRead", messageId);
    }
}
