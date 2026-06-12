using CodeStack.API.Dtos.Requests.KanbanTask;
using CodeStack.API.Mappers;
using CodeStack.Core.Interfaces.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DomainTask = CodeStack.Domain.Entities.Task;

namespace CodeStack.API.Controllers;

[Route("api/kanban/{kanbanId:guid}/tasks")]
[ApiController]
[Authorize]
/// <summary>
/// Authenticated CRUD for tasks within a Kanban board, plus a PATCH move endpoint to transfer a task between columns.
/// </summary>
public class KanbanTaskController(IKanbanTaskService _taskService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(Guid kanbanId)
    {
        IEnumerable<DomainTask> tasks = await _taskService.GetByKanbanAsync(kanbanId);
        return Ok(KanbanTaskMapper.ToDtoList(tasks));
    }

    [HttpGet("{taskId:guid}")]
    public async Task<IActionResult> GetById(Guid kanbanId, Guid taskId)
    {
        DomainTask? task = await _taskService.GetByIdAsync(taskId);
        if (task is null) return NotFound();
        return Ok(KanbanTaskMapper.ToDto(task));
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid kanbanId, [FromBody] CreateKanbanTaskRequestDto dto)
    {
        DomainTask? task = await _taskService.CreateAsync(
            dto.ColumnId, dto.Title, dto.Description,
            dto.Priority, dto.AssigneeId, dto.TagIds);
        if (task is null) return StatusCode(500);
        return CreatedAtAction(nameof(GetById), new { kanbanId, taskId = task.Id }, KanbanTaskMapper.ToDto(task));
    }

    [HttpPut("{taskId:guid}")]
    public async Task<IActionResult> Update(Guid kanbanId, Guid taskId, [FromBody] UpdateKanbanTaskRequestDto dto)
    {
        bool updated = await _taskService.UpdateAsync(taskId, dto.Title, dto.Description, dto.Priority, dto.AssigneeId);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpPatch("{taskId:guid}/move")]
    public async Task<IActionResult> Move(Guid kanbanId, Guid taskId, [FromBody] MoveKanbanTaskRequestDto dto)
    {
        try
        {
            bool moved = await _taskService.MoveAsync(taskId, dto.NewColumnId);
            if (!moved) return NotFound();
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{taskId:guid}")]
    public async Task<IActionResult> Delete(Guid kanbanId, Guid taskId)
    {
        bool deleted = await _taskService.DeleteAsync(taskId);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
