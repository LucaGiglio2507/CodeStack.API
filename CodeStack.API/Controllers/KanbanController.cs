using CodeStack.API.Dtos.Requests.Kanban;
using CodeStack.API.Dtos.Requests.KanbanColumn;
using CodeStack.API.Mappers;
using CodeStack.Core.Interfaces.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Kanban = CodeStack.Domain.Entities.Kanban;
using KanbanColumn = CodeStack.Domain.Entities.KanbanColumn;

namespace CodeStack.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
/// <summary>
/// Authenticated CRUD for Kanban boards, member management, and column management (including drag-and-drop reorder).
/// </summary>
public class KanbanController(IKanbanService _kanbanService, IKanbanColumnService _columnService) : ControllerBase
{
    private Guid CurrentUserId =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    // boards

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<Kanban> kanbans = await _kanbanService.GetByUserAsync(CurrentUserId);
        return Ok(KanbanMapper.ToDtoList(kanbans));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        Kanban? kanban = await _kanbanService.GetByIdAsync(id);
        if (kanban is null) return NotFound();
        return Ok(KanbanMapper.ToDto(kanban));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateKanbanRequestDto dto)
    {
        Kanban? kanban = await _kanbanService.CreateAsync(CurrentUserId, dto.Name, dto.Description, dto.IconUrl);
        if (kanban is null) return StatusCode(500);
        return CreatedAtAction(nameof(GetById), new { id = kanban.Id }, KanbanMapper.ToDto(kanban));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateKanbanRequestDto dto)
    {
        bool updated = await _kanbanService.UpdateAsync(id, dto.Name, dto.Description, dto.IconUrl);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        bool deleted = await _kanbanService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }

    // members

    [HttpPost("{id:guid}/members")]
    public async Task<IActionResult> AddMember(Guid id, [FromBody] AddKanbanMemberRequestDto dto)
    {
        bool added = await _kanbanService.AddMemberAsync(id, dto.UserId, dto.Role);
        if (!added) return Conflict("User is already a member of this board.");
        return NoContent();
    }

    [HttpDelete("{id:guid}/members/{userId:guid}")]
    public async Task<IActionResult> RemoveMember(Guid id, Guid userId)
    {
        bool removed = await _kanbanService.RemoveMemberAsync(id, userId);
        if (!removed) return NotFound();
        return NoContent();
    }

    // columns

    [HttpGet("{id:guid}/columns")]
    public async Task<IActionResult> GetColumns(Guid id)
    {
        IEnumerable<KanbanColumn> columns = await _columnService.GetByKanbanAsync(id);
        return Ok(KanbanColumnMapper.ToDtoList(columns));
    }

    [HttpPost("{id:guid}/columns")]
    public async Task<IActionResult> CreateColumn(Guid id, [FromBody] CreateKanbanColumnRequestDto dto)
    {
        KanbanColumn? column = await _columnService.CreateAsync(id, dto.Name, dto.Color);
        if (column is null) return StatusCode(500);
        return CreatedAtAction(nameof(GetColumns), new { id }, KanbanColumnMapper.ToDto(column));
    }

    [HttpPut("{id:guid}/columns/{columnId:guid}")]
    public async Task<IActionResult> UpdateColumn(Guid id, Guid columnId, [FromBody] UpdateKanbanColumnRequestDto dto)
    {
        bool updated = await _columnService.UpdateAsync(columnId, dto.Name, dto.Color);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:guid}/columns/{columnId:guid}")]
    public async Task<IActionResult> DeleteColumn(Guid id, Guid columnId)
    {
        try
        {
            bool deleted = await _columnService.DeleteAsync(columnId);
            if (!deleted) return NotFound();
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPatch("{id:guid}/columns/reorder")]
    public async Task<IActionResult> ReorderColumns(Guid id, [FromBody] ReorderColumnsRequestDto dto)
    {
        await _columnService.ReorderAsync(id, dto.OrderedColumnIds);
        return NoContent();
    }
}
