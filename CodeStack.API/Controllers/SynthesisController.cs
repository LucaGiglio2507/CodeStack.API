using CodeStack.API.Dtos.Requests.Synthesis;
using CodeStack.API.Mappers;
using CodeStack.Core.Interfaces.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Synthesis = CodeStack.Domain.Entities.Synthesis;

namespace CodeStack.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
/// <summary>
/// Authenticated CRUD for notes and snippets, with an optional isSnippet filter on the list endpoint and a PATCH archive-toggle endpoint.
/// </summary>
public class SynthesisController(ISynthesisService _synthesisService) : ControllerBase
{
    private Guid CurrentUserId =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool? isSnippet)
    {
        IEnumerable<Synthesis> syntheses = await _synthesisService.GetByUserAsync(CurrentUserId, isSnippet);
        return Ok(SynthesisMapper.ToDtoList(syntheses));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        Synthesis? synthesis = await _synthesisService.GetByIdAsync(id);
        if (synthesis is null) return NotFound();
        return Ok(SynthesisMapper.ToDto(synthesis));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSynthesisRequestDto dto)
    {
        Synthesis? synthesis = await _synthesisService.CreateAsync(
            CurrentUserId, dto.Title, dto.Description, dto.Content, dto.IsSnippet, dto.FolderId);
        if (synthesis is null) return StatusCode(500);
        return CreatedAtAction(nameof(GetById), new { id = synthesis.Id }, SynthesisMapper.ToDto(synthesis));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSynthesisRequestDto dto)
    {
        bool updated = await _synthesisService.UpdateAsync(id, dto.Title, dto.Description, dto.Content, dto.FolderId);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpPatch("{id:guid}/archive")]
    public async Task<IActionResult> Archive(Guid id)
    {
        bool updated = await _synthesisService.ArchiveAsync(id);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        bool deleted = await _synthesisService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
