using CodeStack.Core.Interfaces.Repositories;
using CodeStack.Core.Interfaces.Services.Data;
using CodeStack.Domain.Entities;

namespace CodeStack.Core.Services.Data;

/// <summary>
/// CRUD for calendar events scoped to a user.
/// </summary>
public class EventService(IEventRepository _eventRepository) : IEventService
{
    public async Task<Event?> GetByIdAsync(Guid id)
    {
        return await _eventRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Event>> GetByUserAsync(Guid userId)
    {
        return await _eventRepository.GetByUserAsync(userId);
    }

    public async Task<Event?> CreateAsync(Guid userId, string title, string? description, DateTime startsAt)
    {
        Event ev = new Event
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            Starts_At = startsAt,
            User_Id = userId
        };
        return await _eventRepository.CreateAsync(ev);
    }

    public async Task<bool> UpdateAsync(Guid id, string? title, string? description, DateTime? startsAt)
    {
        Event? ev = await _eventRepository.GetByIdAsync(id);
        if (ev is null) return false;

        if (title is not null) ev.Title = title;
        if (description is not null) ev.Description = description;
        if (startsAt is not null) ev.Starts_At = startsAt.Value;

        return await _eventRepository.UpdateAsync(ev);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _eventRepository.DeleteAsync(id);
    }
}
