using CodeStack.Core.Interfaces.Repositories;
using CodeStack.Core.Interfaces.Services.Data;
using CodeStack.Domain.Entities;

namespace CodeStack.Core.Services.Data;

/// <summary>
/// CRUD for notes and code snippets with optional type filtering (isSnippet) and an archive toggle.
/// </summary>
public class SynthesisService(ISynthesisRepository _synthesisRepository) : ISynthesisService
{
    public async Task<Synthesis?> GetByIdAsync(Guid id)
    {
        return await _synthesisRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Synthesis>> GetByUserAsync(Guid userId, bool? isSnippet)
    {
        if (isSnippet.HasValue)
            return await _synthesisRepository.GetByUserAndTypeAsync(userId, isSnippet.Value);

        return await _synthesisRepository.GetByUserAsync(userId);
    }

    public async Task<Synthesis?> CreateAsync(Guid userId, string title, string? description, string? content, bool isSnippet)
    {
        Synthesis synthesis = new Synthesis
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            Content = content,
            IsSnippet = isSnippet,
            Archived = false,
            Created_At = DateTime.UtcNow,
            User_Id = userId
        };
        return await _synthesisRepository.CreateAsync(synthesis);
    }

    public async Task<bool> UpdateAsync(Guid id, string? title, string? description, string? content)
    {
        Synthesis? synthesis = await _synthesisRepository.GetByIdAsync(id);
        if (synthesis is null) return false;

        if (title is not null) synthesis.Title = title;
        if (description is not null) synthesis.Description = description;
        if (content is not null) synthesis.Content = content;

        return await _synthesisRepository.UpdateAsync(synthesis);
    }

    public async Task<bool> ArchiveAsync(Guid id)
    {
        Synthesis? synthesis = await _synthesisRepository.GetByIdAsync(id);
        if (synthesis is null) return false;

        synthesis.Archived = !synthesis.Archived;
        return await _synthesisRepository.UpdateAsync(synthesis);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _synthesisRepository.DeleteAsync(id);
    }
}
