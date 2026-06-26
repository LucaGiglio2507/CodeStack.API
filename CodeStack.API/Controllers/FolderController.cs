using CodeStack.API.Dtos.Requests.Folder;
using CodeStack.API.Mappers;
using CodeStack.Core.Interfaces.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Folder = CodeStack.Domain.Entities.Folder;

namespace CodeStack.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
/// <summary>
/// Authenticated CRUD for the current user's folders, with an extra endpoint to list direct sub-folders.
/// </summary>
public class FolderController(IFolderService _folderService, ISynthesisService _synthesisService) : ControllerBase
{
    private Guid CurrentUserId =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<Folder> folders = await _folderService.GetRootFoldersByUserAsync(CurrentUserId);
        return Ok(FolderMapper.ToDtoList(folders));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        Folder? folder = await _folderService.GetByIdAsync(id);
        if (folder is null) return NotFound();
        return Ok(FolderMapper.ToDto(folder));
    }

    [HttpGet("{id:guid}/subfolders")]
    public async Task<IActionResult> GetSubFolders(Guid id)
    {
        IEnumerable<Folder> subFolders = await _folderService.GetSubFoldersAsync(id);
        return Ok(FolderMapper.ToDtoList(subFolders));
    }

    [HttpGet("{id:guid}/contents")]
    public async Task<IActionResult> GetContents(Guid id)
    {
        var syntheses = await _synthesisService.GetByFolderAsync(id);
        return Ok(SynthesisMapper.ToDtoList(syntheses));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFolderRequestDto dto)
    {
        Folder? folder = await _folderService.CreateAsync(
            CurrentUserId, dto.Title, dto.Description, dto.Icon, dto.Color, dto.Parent_Folder_Id);
        if (folder is null) return StatusCode(500);
        return CreatedAtAction(nameof(GetById), new { id = folder.Id }, FolderMapper.ToDto(folder));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFolderRequestDto dto)
    {
        bool updated = await _folderService.UpdateAsync(id, dto.Title, dto.Description, dto.Icon, dto.Color, dto.Archived);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        bool deleted = await _folderService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
