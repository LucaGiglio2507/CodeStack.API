using CodeStack.Domain.Entities;

namespace CodeStack.Core.Interfaces.Services.Data;

public interface IMessageService
{
    Task<Message?> SendToGroupAsync(Guid senderId, Guid groupId, string content);
    Task<Message?> SendDirectAsync(Guid senderId, Guid receiverId, string content);
    Task<IEnumerable<Message>> GetGroupHistoryAsync(Guid groupId);
    Task<IEnumerable<Message>> GetDirectHistoryAsync(Guid userAId, Guid userBId);
    Task<bool> MarkAsReadAsync(Guid messageId);
}
