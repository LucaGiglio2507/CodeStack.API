using CodeStack.Core.Interfaces.Repositories;
using CodeStack.Domain.Entities;
using CodeStack.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace CodeStack.Infrastructure.Repositories;

/// <summary>
/// EF Core persistence for Message; group history ordered by date, bidirectional direct-conversation filter (A↔B), and MarkAsRead timestamp update.
/// </summary>
public class MessageRepository(CodeStackDBContext _context) : IMessageRepository
{
    public async Task<Message?> GetByIdAsync(Guid id)
    {
        return await _context.Messages
            .Include(m => m.Sender)
            .Include(m => m.Receiver)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<IEnumerable<Message>> GetByGroupAsync(Guid groupId)
    {
        return await _context.Messages
            .Include(m => m.Sender)
            .Where(m => m.Group_Id == groupId)
            .OrderBy(m => m.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Message>> GetDirectConversationAsync(Guid userAId, Guid userBId)
    {
        return await _context.Messages
            .Include(m => m.Sender)
            .Where(m =>
                m.Group_Id == null &&
                ((m.Sender_Id == userAId && m.Receiver_Id == userBId) ||
                 (m.Sender_Id == userBId && m.Receiver_Id == userAId)))
            .OrderBy(m => m.CreatedAt)
            .ToListAsync();
    }

    public async Task<Message?> CreateAsync(Message message)
    {
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
        return message;
    }

    public async Task<bool> MarkAsReadAsync(Guid messageId, DateTime readAt)
    {
        Message? message = await _context.Messages.FindAsync(messageId);
        if (message is null) return false;
        message.ReadAt = readAt;
        return await _context.SaveChangesAsync() > 0;
    }
}
