using CodeStack.API.Dtos.Requests.Event;
using CodeStack.API.Mappers;
using CodeStack.Core.Interfaces.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Event = CodeStack.Domain.Entities.Event;

namespace CodeStack.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
/// <summary>
/// Authenticated CRUD for the current user's calendar events.
/// </summary>
public class EventController(IEventService _eventService) : ControllerBase
{
    private Guid CurrentUserId =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<Event> events = await _eventService.GetByUserAsync(CurrentUserId);
        return Ok(EventMapper.ToDtoList(events));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        Event? ev = await _eventService.GetByIdAsync(id);
        if (ev is null) return NotFound();
        return Ok(EventMapper.ToDto(ev));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEventRequestDto dto)
    {
        Event? ev = await _eventService.CreateAsync(CurrentUserId, dto.Title, dto.Description, dto.StartsAt);
        if (ev is null) return StatusCode(500);
        return CreatedAtAction(nameof(GetById), new { id = ev.Id }, EventMapper.ToDto(ev));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEventRequestDto dto)
    {
        bool updated = await _eventService.UpdateAsync(id, dto.Title, dto.Description, dto.StartsAt);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        bool deleted = await _eventService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
