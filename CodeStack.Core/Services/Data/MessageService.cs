using CodeStack.Core.Interfaces.Repositories;
using CodeStack.Core.Interfaces.Services.Data;
using CodeStack.Domain.Entities;

namespace CodeStack.Core.Services.Data;

/// <summary>
/// Creates group and direct messages, retrieves conversation history, and marks messages as read.
/// </summary>
public class MessageService(IMessageRepository _messageRepository) : IMessageService
{
    public async Task<Message?> SendToGroupAsync(Guid senderId, Guid groupId, string content)
    {
        Message message = new Message
        {
            Id = Guid.NewGuid(),
            Content = content,
            CreatedAt = DateTime.UtcNow,
            Sender_Id = senderId,
            Group_Id = groupId
        };
        return await _messageRepository.CreateAsync(message);
    }

    public async Task<Message?> SendDirectAsync(Guid senderId, Guid receiverId, string content)
    {
        Message message = new Message
        {
            Id = Guid.NewGuid(),
            Content = content,
            CreatedAt = DateTime.UtcNow,
            Sender_Id = senderId,
            Receiver_Id = receiverId
        };
        return await _messageRepository.CreateAsync(message);
    }

    public async Task<IEnumerable<Message>> GetGroupHistoryAsync(Guid groupId)
    {
        return await _messageRepository.GetByGroupAsync(groupId);
    }

    public async Task<IEnumerable<Message>> GetDirectHistoryAsync(Guid userAId, Guid userBId)
    {
        return await _messageRepository.GetDirectConversationAsync(userAId, userBId);
    }

    public async Task<bool> MarkAsReadAsync(Guid messageId)
    {
        return await _messageRepository.MarkAsReadAsync(messageId, DateTime.UtcNow);
    }
}
