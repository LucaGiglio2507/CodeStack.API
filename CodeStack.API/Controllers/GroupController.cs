using CodeStack.API.Dtos.Requests.Group;
using CodeStack.API.Mappers;
using CodeStack.Core.Interfaces.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Group = CodeStack.Domain.Entities.Group;

namespace CodeStack.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
/// <summary>
/// Authenticated endpoints to list the current user's groups, create a group with initial participants, and add a participant.
/// </summary>
public class GroupController(IGroupService _groupService) : ControllerBase
{
    private Guid CurrentUserId =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<IActionResult> GetMyGroups()
    {
        IEnumerable<Group> groups = await _groupService.GetByUserAsync(CurrentUserId);
        return Ok(GroupMapper.ToDtoList(groups));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGroupRequestDto dto)
    {
        List<Guid> participantIds = dto.ParticipantIds.Contains(CurrentUserId)
            ? dto.ParticipantIds
            : [CurrentUserId, .. dto.ParticipantIds];

        Group? group = await _groupService.CreateAsync(dto.Name, participantIds);
        if (group is null) return StatusCode(500);
        return CreatedAtAction(nameof(GetMyGroups), GroupMapper.ToDto(group));
    }

    [HttpPost("{id:guid}/participants")]
    public async Task<IActionResult> AddParticipant(Guid id, [FromBody] AddParticipantRequestDto dto)
    {
        bool added = await _groupService.AddParticipantAsync(id, dto.UserId);
        if (!added) return Conflict("User is already a participant of this group.");
        return NoContent();
    }
}
