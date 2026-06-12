using CodeStack.Domain.Entities;

namespace CodeStack.Core.Interfaces.Repositories;

public interface IMessageRepository
{
    Task<Message?> GetByIdAsync(Guid id);
    Task<IEnumerable<Message>> GetByGroupAsync(Guid groupId);
    Task<IEnumerable<Message>> GetDirectConversationAsync(Guid userAId, Guid userBId);
    Task<Message?> CreateAsync(Message message);
    Task<bool> MarkAsReadAsync(Guid messageId, DateTime readAt);
}
