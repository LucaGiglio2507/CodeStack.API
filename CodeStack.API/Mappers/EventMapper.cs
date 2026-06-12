using CodeStack.API.Dtos.Responses.Event;
using CodeStack.Domain.Entities;

namespace CodeStack.API.Mappers;

public static class EventMapper
{
    public static EventResponseDto ToDto(Event ev) => new()
    {
        Id = ev.Id,
        Title = ev.Title,
        Description = ev.Description,
        Starts_At = ev.Starts_At
    };

    public static IEnumerable<EventResponseDto> ToDtoList(IEnumerable<Event> events)
        => events.Select(ToDto);
}
