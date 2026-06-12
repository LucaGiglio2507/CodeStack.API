using CodeStack.API.Dtos.Requests.Tag;
using CodeStack.API.Mappers;
using CodeStack.Core.Interfaces.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tag = CodeStack.Domain.Entities.Tag;

namespace CodeStack.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
/// <summary>
/// Authenticated CRUD for the current user's tags.
/// </summary>
public class TagController(ITagService _tagService) : ControllerBase
{
    private Guid CurrentUserId =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<Tag> tags = await _tagService.GetByUserAsync(CurrentUserId);
        return Ok(TagMapper.ToDtoList(tags));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        Tag? tag = await _tagService.GetByIdAsync(id);
        if (tag is null) return NotFound();
        return Ok(TagMapper.ToDto(tag));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTagRequestDto dto)
    {
        Tag? tag = await _tagService.CreateAsync(CurrentUserId, dto.Name, dto.Color);
        if (tag is null) return StatusCode(500);
        return CreatedAtAction(nameof(GetById), new { id = tag.Id }, TagMapper.ToDto(tag));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTagRequestDto dto)
    {
        bool updated = await _tagService.UpdateAsync(id, dto.Name, dto.Color);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        bool deleted = await _tagService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
